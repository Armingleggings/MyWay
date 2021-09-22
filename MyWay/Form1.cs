using MyWay.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyWay
{
	public partial class Form1 : Form
	{

	
		// Loads registry functions
		private Regis regStuff = new Regis();
		// Loads preferences from the file
		private Prefs prefs = new Prefs();
		// A timer to make sure the system behaves
		private Timer watcher;
		// Watcher action toggles
		private bool watchNumL = false;

		public Form1()
		{
			InitializeComponent();			
		}

		// If the preference matches the yesno, everything is fine. Otherwise notate the disparity
		private void PrefMatch(string prefName, string yesno, Label which)
		{
			// Matches the preference
			if (prefs.GetPref(prefName) == yesno)
			{
				which.Text = "⬤";
				which.Font = new Font("Arial", 25, FontStyle.Regular);
				which.Padding = new Padding(5,0,5,0);
			}
			else
			{
				which.Text = "⭗";
				which.Font = new Font("MS Gothic", 29, FontStyle.Bold);
				which.Padding = new Padding(0, 0, 0, 0);
			}
		}

		private bool FixedF1()
		{
			// EGADS! It's active!
			if (regStuff.F1HelpActive())
			{
				F1FixerBtnDot.ForeColor = Color.FromArgb(244, 180, 0);
				F1FixerBtnDot.TextAlign = ContentAlignment.TopRight;
				F1FixerBtnArea.BackgroundImage = Properties.Resources.yellow_btn_bk_sm;
				F1FixerButtonBox.Text = "Fix it!";
				PrefMatch("KillF1Help", "no", F1FixerBtnDot);
				return false;
			}
			else
			{
				F1FixerBtnDot.ForeColor = Color.FromArgb(80, 214, 0);
				F1FixerBtnDot.TextAlign = ContentAlignment.TopLeft;
				F1FixerBtnArea.BackgroundImage = Properties.Resources.green_btn_bk_sm;
				F1FixerButtonBox.Text = "Fixed!";
				PrefMatch("KillF1Help", "yes", F1FixerBtnDot);
				return true;
			}
		}

		private void F1FixerBtnDot_Click(object sender, EventArgs e)
		{
			statusWindow.Text = "Clicked F1Fixer";
			bool itsFixed = FixedF1();
			if (itsFixed)
			{
				// Toggle it
				regStuff.RestoreF1();
				// Save the new setting
				prefs.SetPref("KillF1Help", "no");
			}
			else
			{
				regStuff.KillF1();
				prefs.SetPref("KillF1Help", "yes");
			}
			// Run it again to update the view
			FixedF1();
		}

		private bool FixedCMD()
		{
			if (!regStuff.CMDContextOn())
			{
				CMDFixerBtnDot.ForeColor = Color.FromArgb(244, 180, 0);
				CMDFixerBtnDot.TextAlign = ContentAlignment.TopRight;
				CMDFixerBtnArea.BackgroundImage = Properties.Resources.yellow_btn_bk_sm;
				CMDFixerButtonBox.Text = "Fix it!";
				PrefMatch("RestoreAdminCMDContext", "no", CMDFixerBtnDot);
				return false;
			}
			else
			{
				CMDFixerBtnDot.ForeColor = Color.FromArgb(80, 214, 0);
				CMDFixerBtnDot.TextAlign = ContentAlignment.TopLeft;
				CMDFixerBtnArea.BackgroundImage = Properties.Resources.green_btn_bk_sm;
				CMDFixerButtonBox.Text = "Fixed!";
				PrefMatch("RestoreAdminCMDContext", "yes", CMDFixerBtnDot);
				return true;
			}
		}

		private void CMDFixerBtnDot_Click(object sender, EventArgs e)
		{
			bool itsFixed = FixedCMD();
			if (itsFixed)
			{
				// Toggle it
				regStuff.CMDdisable();
				// Save the new setting
				prefs.SetPref("RestoreAdminCMDContext", "no");
			}
			else
			{
				regStuff.CMDenable();
				prefs.SetPref("RestoreAdminCMDContext", "yes");
			}
			// Run it again to update the view
			FixedCMD();
		}

		private bool NumLIsOn()
		{
			if (Control.IsKeyLocked(Keys.NumLock))
			{
				NumLPic.Image = Properties.Resources.num_lock_on;
				return true;
			}
			else
			{
				NumLPic.Image = Properties.Resources.num_lock_off;
				return false;
			}
		}


		private bool FixedNumL()
		{
			NumLIsOn();
			// It's active!
			if (!watchNumL)
			{
				NumLFixerBtnDot.ForeColor = Color.FromArgb(244, 180, 0);
				NumLFixerBtnDot.TextAlign = ContentAlignment.TopRight;
				NumLFixerBtnArea.BackgroundImage = Properties.Resources.yellow_btn_bk_sm;
				NumLFixerButtonBox.Text = "Fix it!";
				PrefMatch("ForceNumLockAlwaysOn", "no", NumLFixerBtnDot);
				return false;
			}
			else
			{
				NumLFixerBtnDot.ForeColor = Color.FromArgb(80, 214, 0);
				NumLFixerBtnDot.TextAlign = ContentAlignment.TopLeft;
				NumLFixerBtnArea.BackgroundImage = Properties.Resources.green_btn_bk_sm;
				NumLFixerButtonBox.Text = "Fixed!";
				PrefMatch("ForceNumLockAlwaysOn", "yes", NumLFixerBtnDot);
				return true;
			}
		}

		private void NumLFixerBtnDot_Click(object sender, EventArgs e)
		{			
			if (watchNumL)
			{
				// Toggle it
				watchNumL = false;
				// Save the new setting
				prefs.SetPref("ForceNumLockAlwaysOn", "no");
			}
			else
			{
				watchNumL = true;
				prefs.SetPref("ForceNumLockAlwaysOn", "yes");
			}
			// Run it again to update the view
			FixedNumL();
		}

		/// <summary>
		///  MAKE THE THINGS - Group by go away, thumbnail view, show extensions, show hidden files, always expands
		/// </summary>


		public void StartWatcher()
		{
			watcher = new Timer();
			watcher.Tick += new EventHandler(WatchIt);
			watcher.Interval = 2000; // in miliseconds
			watcher.Start();
		}


		[DllImport("user32.dll")]
		static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
		private void PressKb(Keys keyCode)
		{
			const int KEYEVENTF_EXTENDEDKEY = 0x1;
			const int KEYEVENTF_KEYUP = 0x2;

			keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
			keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
		}

		private int count = 0;
		private void WatchIt(object sender, EventArgs e)
		{
			count++;
			statusWindow.Text = $"in watcher: {count}";
			// If we're watching Num Lock, force it on
			if (watchNumL && !NumLIsOn())
			{
				// Force it back on
				PressKb(Keys.NumLock);
			}
			// Show Num Lock Status
			NumLIsOn();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StartWatcher();
			FixedF1();
			FixedCMD();
			// Check prefs file for watcher
			if (prefs.GetPref("ForceNumLockAlwaysOn") == "yes")
				watchNumL = true;
			FixedNumL();

		}


	}
}
