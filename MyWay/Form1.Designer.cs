
namespace MyWay
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.fckF1label = new System.Windows.Forms.Label();
			this.fckF1Description = new System.Windows.Forms.TextBox();
			this.fckF1KillBtn = new System.Windows.Forms.Button();
			this.fckF1Status = new System.Windows.Forms.TextBox();
			this.fckF1RestoreBtn = new System.Windows.Forms.Button();
			this.CmdHereLbl = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.numLockHelp = new System.Windows.Forms.TextBox();
			this.numLockLbl = new System.Windows.Forms.Label();
			this.numWatchDisableBtn = new System.Windows.Forms.Button();
			this.numStatus = new System.Windows.Forms.TextBox();
			this.numWatchEnableBtn = new System.Windows.Forms.Button();
			this.statusWindow = new System.Windows.Forms.TextBox();
			this.cmdDisableBtn = new System.Windows.Forms.Button();
			this.cmdStatus = new System.Windows.Forms.TextBox();
			this.cmdEnableBtn = new System.Windows.Forms.Button();
			this.numLockStatusDot = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.numLockStatusDot)).BeginInit();
			this.SuspendLayout();
			// 
			// fckF1label
			// 
			this.fckF1label.AutoSize = true;
			this.fckF1label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.fckF1label.Location = new System.Drawing.Point(13, 85);
			this.fckF1label.Name = "fckF1label";
			this.fckF1label.Size = new System.Drawing.Size(143, 15);
			this.fckF1label.TabIndex = 0;
			this.fckF1label.Text = "F1 Help Function Disable";
			// 
			// fckF1Description
			// 
			this.fckF1Description.BackColor = System.Drawing.Color.Gainsboro;
			this.fckF1Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.fckF1Description.CausesValidation = false;
			this.fckF1Description.Cursor = System.Windows.Forms.Cursors.Default;
			this.fckF1Description.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.fckF1Description.Location = new System.Drawing.Point(13, 142);
			this.fckF1Description.Margin = new System.Windows.Forms.Padding(8);
			this.fckF1Description.Multiline = true;
			this.fckF1Description.Name = "fckF1Description";
			this.fckF1Description.ReadOnly = true;
			this.fckF1Description.Size = new System.Drawing.Size(775, 53);
			this.fckF1Description.TabIndex = 1;
			this.fckF1Description.Text = resources.GetString("fckF1Description.Text");
			// 
			// fckF1KillBtn
			// 
			this.fckF1KillBtn.Location = new System.Drawing.Point(13, 107);
			this.fckF1KillBtn.Name = "fckF1KillBtn";
			this.fckF1KillBtn.Size = new System.Drawing.Size(200, 25);
			this.fckF1KillBtn.TabIndex = 2;
			this.fckF1KillBtn.Text = "Break it\'s knee-caps";
			this.fckF1KillBtn.UseVisualStyleBackColor = true;
			this.fckF1KillBtn.Click += new System.EventHandler(this.fckF1KillBtn_Click);
			// 
			// fckF1Status
			// 
			this.fckF1Status.Cursor = System.Windows.Forms.Cursors.Default;
			this.fckF1Status.Location = new System.Drawing.Point(219, 108);
			this.fckF1Status.Name = "fckF1Status";
			this.fckF1Status.ReadOnly = true;
			this.fckF1Status.Size = new System.Drawing.Size(363, 23);
			this.fckF1Status.TabIndex = 3;
			this.fckF1Status.Text = "Current State of F1 Help:";
			// 
			// fckF1RestoreBtn
			// 
			this.fckF1RestoreBtn.Location = new System.Drawing.Point(588, 107);
			this.fckF1RestoreBtn.Name = "fckF1RestoreBtn";
			this.fckF1RestoreBtn.Size = new System.Drawing.Size(200, 25);
			this.fckF1RestoreBtn.TabIndex = 4;
			this.fckF1RestoreBtn.Text = "Restore F1, but seriously... why?";
			this.fckF1RestoreBtn.UseVisualStyleBackColor = true;
			this.fckF1RestoreBtn.Click += new System.EventHandler(this.fckF1RestoreBtn_Click);
			// 
			// CmdHereLbl
			// 
			this.CmdHereLbl.AutoSize = true;
			this.CmdHereLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.CmdHereLbl.Location = new System.Drawing.Point(13, 223);
			this.CmdHereLbl.Name = "CmdHereLbl";
			this.CmdHereLbl.Size = new System.Drawing.Size(148, 15);
			this.CmdHereLbl.TabIndex = 5;
			this.CmdHereLbl.Text = "CMD Here Context menu";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.Gainsboro;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.textBox1.Location = new System.Drawing.Point(16, 271);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(771, 52);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// numLockHelp
			// 
			this.numLockHelp.BackColor = System.Drawing.Color.Gainsboro;
			this.numLockHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.numLockHelp.Cursor = System.Windows.Forms.Cursors.Default;
			this.numLockHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.numLockHelp.Location = new System.Drawing.Point(16, 361);
			this.numLockHelp.Multiline = true;
			this.numLockHelp.Name = "numLockHelp";
			this.numLockHelp.ReadOnly = true;
			this.numLockHelp.Size = new System.Drawing.Size(771, 31);
			this.numLockHelp.TabIndex = 7;
			this.numLockHelp.Text = "Stupid Numlock should ALWAYS BE ON. Why do they even HAVE that lever!? The green " +
    "dot shows status, the controls below set or disable the watcher";
			// 
			// numLockLbl
			// 
			this.numLockLbl.AutoSize = true;
			this.numLockLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.numLockLbl.Location = new System.Drawing.Point(19, 343);
			this.numLockLbl.Name = "numLockLbl";
			this.numLockLbl.Size = new System.Drawing.Size(108, 15);
			this.numLockLbl.TabIndex = 8;
			this.numLockLbl.Text = "Numlock Watcher";
			// 
			// numWatchDisableBtn
			// 
			this.numWatchDisableBtn.Location = new System.Drawing.Point(588, 397);
			this.numWatchDisableBtn.Name = "numWatchDisableBtn";
			this.numWatchDisableBtn.Size = new System.Drawing.Size(200, 25);
			this.numWatchDisableBtn.TabIndex = 11;
			this.numWatchDisableBtn.Text = "Restore user-toggle dumbness";
			this.numWatchDisableBtn.UseVisualStyleBackColor = true;
			this.numWatchDisableBtn.Click += new System.EventHandler(this.numWatchDisableBtn_Click);
			// 
			// numStatus
			// 
			this.numStatus.Cursor = System.Windows.Forms.Cursors.Default;
			this.numStatus.Location = new System.Drawing.Point(219, 398);
			this.numStatus.Name = "numStatus";
			this.numStatus.ReadOnly = true;
			this.numStatus.Size = new System.Drawing.Size(363, 23);
			this.numStatus.TabIndex = 10;
			this.numStatus.Text = "Num Lock Status";
			// 
			// numWatchEnableBtn
			// 
			this.numWatchEnableBtn.Location = new System.Drawing.Point(13, 397);
			this.numWatchEnableBtn.Name = "numWatchEnableBtn";
			this.numWatchEnableBtn.Size = new System.Drawing.Size(200, 25);
			this.numWatchEnableBtn.TabIndex = 9;
			this.numWatchEnableBtn.Text = "Force on";
			this.numWatchEnableBtn.UseVisualStyleBackColor = true;
			this.numWatchEnableBtn.Click += new System.EventHandler(this.numWatchEnableBtn_Click);
			// 
			// statusWindow
			// 
			this.statusWindow.BackColor = System.Drawing.Color.Gainsboro;
			this.statusWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.statusWindow.CausesValidation = false;
			this.statusWindow.Cursor = System.Windows.Forms.Cursors.Default;
			this.statusWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.statusWindow.Location = new System.Drawing.Point(13, 13);
			this.statusWindow.Margin = new System.Windows.Forms.Padding(8);
			this.statusWindow.Multiline = true;
			this.statusWindow.Name = "statusWindow";
			this.statusWindow.ReadOnly = true;
			this.statusWindow.Size = new System.Drawing.Size(775, 64);
			this.statusWindow.TabIndex = 12;
			this.statusWindow.Text = "Satus window";
			// 
			// cmdDisableBtn
			// 
			this.cmdDisableBtn.Location = new System.Drawing.Point(588, 241);
			this.cmdDisableBtn.Name = "cmdDisableBtn";
			this.cmdDisableBtn.Size = new System.Drawing.Size(200, 25);
			this.cmdDisableBtn.TabIndex = 15;
			this.cmdDisableBtn.Text = "Remove CMD contect option";
			this.cmdDisableBtn.UseVisualStyleBackColor = true;
			this.cmdDisableBtn.Click += new System.EventHandler(this.cmdDisableBtn_Click);
			// 
			// cmdStatus
			// 
			this.cmdStatus.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdStatus.Location = new System.Drawing.Point(219, 242);
			this.cmdStatus.Name = "cmdStatus";
			this.cmdStatus.ReadOnly = true;
			this.cmdStatus.Size = new System.Drawing.Size(363, 23);
			this.cmdStatus.TabIndex = 14;
			this.cmdStatus.Text = "Current State of CMD context menu";
			// 
			// cmdEnableBtn
			// 
			this.cmdEnableBtn.Location = new System.Drawing.Point(13, 241);
			this.cmdEnableBtn.Name = "cmdEnableBtn";
			this.cmdEnableBtn.Size = new System.Drawing.Size(200, 25);
			this.cmdEnableBtn.TabIndex = 13;
			this.cmdEnableBtn.Text = "Restore Admin CMD";
			this.cmdEnableBtn.UseVisualStyleBackColor = true;
			this.cmdEnableBtn.Click += new System.EventHandler(this.cmdEnableBtn_Click);
			// 
			// numLockStatusDot
			// 
			this.numLockStatusDot.Image = ((System.Drawing.Image)(resources.GetObject("numLockStatusDot.Image")));
			this.numLockStatusDot.Location = new System.Drawing.Point(126, 346);
			this.numLockStatusDot.Name = "numLockStatusDot";
			this.numLockStatusDot.Size = new System.Drawing.Size(15, 12);
			this.numLockStatusDot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.numLockStatusDot.TabIndex = 16;
			this.numLockStatusDot.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.numLockStatusDot);
			this.Controls.Add(this.cmdDisableBtn);
			this.Controls.Add(this.cmdStatus);
			this.Controls.Add(this.cmdEnableBtn);
			this.Controls.Add(this.statusWindow);
			this.Controls.Add(this.numWatchDisableBtn);
			this.Controls.Add(this.numStatus);
			this.Controls.Add(this.numWatchEnableBtn);
			this.Controls.Add(this.numLockLbl);
			this.Controls.Add(this.numLockHelp);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.CmdHereLbl);
			this.Controls.Add(this.fckF1RestoreBtn);
			this.Controls.Add(this.fckF1Status);
			this.Controls.Add(this.fckF1KillBtn);
			this.Controls.Add(this.fckF1Description);
			this.Controls.Add(this.fckF1label);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.numLockStatusDot)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label fckF1label;
		private System.Windows.Forms.TextBox fckF1Description;
		private System.Windows.Forms.Button fckF1KillBtn;
		private System.Windows.Forms.TextBox fckF1Status;
		private System.Windows.Forms.Button fckF1RestoreBtn;
		private System.Windows.Forms.Label CmdHereLbl;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox numLockHelp;
		private System.Windows.Forms.Label numLockLbl;
		private System.Windows.Forms.Button numWatchDisableBtn;
		private System.Windows.Forms.TextBox numStatus;
		private System.Windows.Forms.Button numWatchEnableBtn;
		private System.Windows.Forms.TextBox statusWindow;
		private System.Windows.Forms.Button cmdDisableBtn;
		private System.Windows.Forms.TextBox cmdStatus;
		private System.Windows.Forms.Button cmdEnableBtn;
		private System.Windows.Forms.PictureBox numLockStatusDot;
	}
}

