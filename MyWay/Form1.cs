using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;
using MyWay.Helpers;

namespace MyWay
{
	public partial class Form1 : Form
	{
		private readonly string root = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
		private string loggedInUser;
		private SecurityIdentifier loggedInSID;
		private string loggedInSIDStr;
		// Loads preferences
		private Prefs prefs = new Prefs();

		public Form1()
		{
			InitializeComponent();
		}


		[DllImport("Wtsapi32.dll")]
		private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WtsInfoClass wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);
		[DllImport("Wtsapi32.dll")]
		private static extern void WTSFreeMemory(IntPtr pointer);

		private enum WtsInfoClass
		{
			WTSUserName = 5,
			WTSDomainName = 7,
		}

		private static string GetUsername(int sessionId, bool prependDomain = true)
		{
			IntPtr buffer;
			int strLen;
			string username = "SYSTEM";
			if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSUserName, out buffer, out strLen) && strLen > 1)
			{
				username = Marshal.PtrToStringAnsi(buffer);
				WTSFreeMemory(buffer);
				if (prependDomain)
				{
					if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSDomainName, out buffer, out strLen) && strLen > 1)
					{
						username = Marshal.PtrToStringAnsi(buffer) + "\\" + username;
						WTSFreeMemory(buffer);
					}
				}
			}
			return username;
		}

		private void CheckF1()
		{
			using (var hku = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
			{
				using (var F1key = hku.OpenSubKey(loggedInSIDStr + @"\SOFTWARE\Classes\TypeLib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0\win32"))
				{
					// EGADS! It's active!
					if (F1key == null)
					{
						fckF1RestoreBtn.Enabled = false;
						fckF1KillBtn.Enabled = true;
						fckF1Status.Text = "That creepy bugger is waiting and watching.";
						fckF1Status.BackColor = Color.FromArgb(255, 232, 0);
					}
					else
					{
						fckF1RestoreBtn.Enabled = true;
						fckF1KillBtn.Enabled = false;
						fckF1Status.Text = "The F1-Help function had been put in it's place.";
						fckF1Status.BackColor = Color.FromArgb(0, 200, 50);
						prefs.SetPref("KillF1Help", "yes");
					}
				}
			}
		}

		private void fckF1KillBtn_Click(object sender, EventArgs e)
		{
			using (var hku = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
			{
				RegistryKey setter = hku.CreateSubKey(loggedInSIDStr + @"\SOFTWARE\Classes\Typelib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0\win32", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "");
				setter.Close();
				setter = hku.CreateSubKey(loggedInSIDStr + @"\SOFTWARE\Classes\Typelib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0\win64", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "");
				setter.Close();
			}
			prefs.SetPref("KillF1Help", "yes");
			CheckF1();
		}

		private void fckF1RestoreBtn_Click(object sender, EventArgs e)
		{
			Registry.Users.DeleteSubKeyTree(loggedInSIDStr + @"\SOFTWARE\Classes\Typelib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0");
			prefs.SetPref("KillF1Help", "no");
			CheckF1();
		}

		private void CheckCMD()
		{
			using (var hku = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64))
			{
				using (var cmdKey = hku.OpenSubKey(@"\Directory\shell\OpenCmdHereAsAdmin"))
				{
					// EGADS! It's active!
					if (cmdKey == null)
					{
						cmdEnableBtn.Enabled = true;
						cmdDisableBtn.Enabled = false;
						cmdStatus.Text = "CMD context is missing. Thanks Microsoft.";
						cmdStatus.BackColor = Color.FromArgb(255, 232, 0);
					}
					else
					{
						cmdEnableBtn.Enabled = false;
						cmdDisableBtn.Enabled = true;
						cmdStatus.Text = "CMD context restored and ready";
						cmdStatus.BackColor = Color.FromArgb(0, 200, 50);
					}
				}
			}
		}

		private void cmdEnableBtn_Click(object sender, EventArgs e)
		{
			// BASED ON THE REGISTRY HACKS By Shawn Brink
			// https://www.tenforums.com/tutorials/59686-open-command-window-here-administrator-add-windows-10-a.html

			using (var reg = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64))
			{
				RegistryKey setter;
				setter = reg.CreateSubKey(@"Directory\shell\OpenCmdHereAsAdmin", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "Open command window here as administrator");
				//setter.DeleteValue("Extended");
				setter.SetValue("Icon", "imageres.dll,-5324");
				setter.Close();
				
				setter = reg.CreateSubKey(@"Directory\shell\OpenCmdHereAsAdmin\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "cmd /c echo|set/p=\"%L\"|powershell -NoP -W 1 -NonI -NoL \"SaPs 'cmd' -Args '/c \"\"\"cd /d',$([char]34+$Input+[char]34),'^&^& start /b cmd.exe\"\"\"' -Verb RunAs\"");
				setter.Close();

				setter = reg.CreateSubKey(@"Directory\Background\shell\OpenCmdHereAsAdmin", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "Open command window here as administrator");
				//setter.DeleteValue("Extended");
				setter.SetValue("Icon", "imageres.dll,-5324");
				setter.Close();

				setter = reg.CreateSubKey(@"Directory\Background\shell\OpenCmdHereAsAdmin\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "cmd /c echo|set/p=\"%L\"|powershell -NoP -W 1 -NonI -NoL \"SaPs 'cmd' -Args '/c \"\"\"cd /d',$([char]34+$Input+[char]34),'^&^& start /b cmd.exe\"\"\"' -Verb RunAs\"");
				setter.Close();

				setter = reg.CreateSubKey(@"Drive\shell\OpenCmdHereAsAdmin", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "Open command window here as administrator");
				//setter.DeleteValue("Extended");
				setter.SetValue("Icon", "imageres.dll,-5324");
				setter.Close();

				setter = reg.CreateSubKey(@"Drive\shell\OpenCmdHereAsAdmin\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
				setter.SetValue("", "cmd /c echo|set/p=\"%L\"|powershell -NoP -W 1 -NonI -NoL \"SaPs 'cmd' -Args '/c \"\"\"cd /d',$([char]34+$Input+[char]34),'^&^& start /b cmd.exe\"\"\"' -Verb RunAs\"");
				setter.Close();
			}
			prefs.SetPref("RestoreAdminCMDContext", "yes");
			CheckCMD();
		}

		private void cmdDisableBtn_Click(object sender, EventArgs e)
		{
			Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\OpenCmdHereAsAdmin");
			Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\Background\shell\OpenCmdHereAsAdmin");
			Registry.ClassesRoot.DeleteSubKeyTree(@"Drive\shell\OpenCmdHereAsAdmin");

			prefs.SetPref("RestoreAdminCMDContext", "no");
			CheckCMD();
		}


		private void NumLockSpotCheck()
		{
			if (IsKeyLocked(Keys.NumLock))
			{
				numLockStatusDot.Visible = true;
			}
			else
			{
				numLockStatusDot.Visible = false;
			}
		}
		private void CheckNumLock()
		{
			NumLockSpotCheck();
			// It's active!
			if (prefs.GetPref("ForceNumLockAlwaysOn") != "yes")
			{
				numWatchDisableBtn.Enabled = false;
				numWatchEnableBtn.Enabled = true;
				numStatus.Text = "NumLock Watcher is disabled. Are you mad?";
				numStatus.BackColor = Color.FromArgb(255, 232, 0);
			}
			else
			{
				numWatchDisableBtn.Enabled = true;
				numWatchEnableBtn.Enabled = false;
				numStatus.Text = "NumLock Watcher is watching...";
				numStatus.BackColor = Color.FromArgb(0, 200, 50);
			}
		}

		private void numWatchDisableBtn_Click(object sender, EventArgs e)
		{
			prefs.SetPref("ForceNumLockAlwaysOn", "no");

			CheckNumLock();
		}

		private void numWatchEnableBtn_Click(object sender, EventArgs e)
		{
			prefs.SetPref("ForceNumLockAlwaysOn", "yes");

			CheckNumLock();
		}

		/// <summary>
		///  MAKE THE THINGS - Group by go away, thumbnail view, show extensions, show hidden files, always expands
		/// </summary>

		private void Form1_Load(object sender, EventArgs e)
		{

			loggedInUser = GetUsername(Process.GetCurrentProcess().SessionId);
			NTAccount f = new NTAccount(loggedInUser);
			loggedInSID = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
			loggedInSIDStr = loggedInSID.ToString();

			statusWindow.Text = @"
				name = "+loggedInUser+@"
				SID = "+ loggedInSIDStr + @"
			";

			CheckF1();
			CheckCMD();

	

		}


	}
}
