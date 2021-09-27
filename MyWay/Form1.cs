using MyWay.Helpers;
using System;
using System.Collections.Generic;
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
		// Translation array from fix shortname to prefsfile name
		private Dictionary<string, string> prefNames = new Dictionary<string, string> {
			["F1"] = "KillF1UnhelpfulHelp",
			["CMD"] = "RestoreAdminCMDContext",
			["NumL"] = "ForceNumLockAlwaysOn",
			["Expand"] = "ExpandFolders",
			["FileExt"] = "ShowFileExtensionsNotJustStupidIcons",
			["ShowFiles"] = "ShowNonSystemHiddenFiles",
			["UserNav"] = "RemoveUserNavTurd",
		};

		public Form1()
		{
			InitializeComponent();			
		}

		private void SetFixButton(string which, bool isFixed)
		{
			var theForm = this.FindForm();
			Label buttonDot = (Label)theForm.Controls.Find(which+"FixerBtnDot", true)[0];
			FlowLayoutPanel buttonBk = (FlowLayoutPanel)theForm.Controls.Find(which+"FixerBtnArea", true)[0];
			GroupBox buttonBox = (GroupBox)theForm.Controls.Find(which+"FixerBtnBox", true)[0];

			buttonDot.ForeColor = isFixed ? Color.FromArgb(80, 214, 0) : Color.FromArgb(244, 180, 0);
			buttonDot.TextAlign = isFixed ? ContentAlignment.TopLeft : ContentAlignment.TopRight;
			buttonBk.BackgroundImage = isFixed ? Properties.Resources.green_btn_bk_sm : Properties.Resources.yellow_btn_bk_sm;
			buttonBox.Text = isFixed ? "Fixed!" : "Fix it!";

			// If our preference matches what the button currnetly is
			if (prefs.GetPref(prefNames[which]) == (isFixed ? "yes" : "no"))
			{
				buttonDot.Text = "⬤";
				buttonDot.Font = new Font("Arial", 25, FontStyle.Regular);
				buttonDot.Padding = new Padding(5, 0, 5, 0);
			}
			else
			{
				buttonDot.Text = "⭗";
				buttonDot.Font = new Font("MS Gothic", 29, FontStyle.Bold);
				buttonDot.Padding = new Padding(0, 0, 0, 0);
			}
		}

		// Telling exactly how something is fixed or not varies by the change so we use this funciton to determine it
		private bool IsFixed(string which)
		{
			// If we're watching, it's fixed
			if ("NumL" == which) return watchNumL;

			// For anything registry related
			return regStuff.IsFixed(which);
		}

		private void FixIt(string which)
		{
			if ("NumL" == which) watchNumL = true;
			else regStuff.FixIt(which);
		}
		private void BreakIt(string which)
		{
			if ("NumL" == which) watchNumL = false;
			else regStuff.BreakIt(which);
		}

		private void ToggleFix(string which)
		{
			if (IsFixed(which))
			{
				prefs.SetPref(prefNames[which], "no"); // was fixed, now it's not
				BreakIt(which);
				SetFixButton(which, false);
			}
			else
			{
				prefs.SetPref(prefNames[which], "yes"); // was fixed, now it's not
				FixIt(which);
				SetFixButton(which, true);
			}
		}

		private void F1FixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("F1");
		}

		private void CMDFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("CMD");
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

		private void NumLFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("NumL");
		}

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

		private void ExpandFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("Expand");
		}

		private void ExtensionFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("FileExt");
		}

		private void HideFileFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("ShowFiles");
		}

		private void UserNavFixerBtnDot_Click(object sender, EventArgs e)
		{
			ToggleFix("UserNav");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StartWatcher();
			// Check prefs file for watcher
			if (prefs.GetPref("ForceNumLockAlwaysOn") == "yes")
				watchNumL = true;

			// Initial load. Test each fix and show proper status
			foreach (var pref in prefNames)
			{
				// The button needs to be on
				if (IsFixed(pref.Key))
					SetFixButton(pref.Key, true);
				else
					SetFixButton(pref.Key, false);
			}

		}

		/// <summary>
		///  MAKE THE THINGS - Group by go away, thumbnail view,
		/// </summary>


	}
}
