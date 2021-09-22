using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace MyWay.Helpers
{
	class Regis
	{
		private string loggedInUser;
		private SecurityIdentifier loggedInSID;
		private string loggedInSIDStr;

		[DllImport("Wtsapi32.dll")]
		private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WtsInfoClass wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);
		[DllImport("Wtsapi32.dll")]
		private static extern void WTSFreeMemory(IntPtr pointer);

		private enum WtsInfoClass
		{
			WTSUserName = 5,
			WTSDomainName = 7,
		}

		public Regis()
		{
			loggedInUser = GetUsername(Process.GetCurrentProcess().SessionId);
			NTAccount f = new NTAccount(loggedInUser);
			loggedInSID = (SecurityIdentifier)f.Translate(typeof(SecurityIdentifier));
			loggedInSIDStr = loggedInSID.ToString();
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
		public bool F1HelpActive()
		{
			using (var hku = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64))
			{
				using (var F1key = hku.OpenSubKey(loggedInSIDStr + @"\SOFTWARE\Classes\TypeLib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0\win32"))
				{
					return F1key == null;
				}
			}
		}

		public void KillF1()
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
		}

		public void RestoreF1()
		{
			Registry.Users.DeleteSubKeyTree(loggedInSIDStr + @"\SOFTWARE\Classes\Typelib\{8cec5860-07a1-11d9-b15e-000d56bfe6ee}\1.0\0");
		}

		public bool CMDContextOn()
		{
			using (var hku = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64))
			{
				using (var cmdKey = hku.OpenSubKey(@"\Directory\shell\OpenCmdHereAsAdmin"))
				{
					return cmdKey != null;
				}
			}
		}

		public void CMDenable()
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
		}

		public void CMDdisable()
		{
			Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\OpenCmdHereAsAdmin");
			Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\Background\shell\OpenCmdHereAsAdmin");
			Registry.ClassesRoot.DeleteSubKeyTree(@"Drive\shell\OpenCmdHereAsAdmin");
		}
	}
}
