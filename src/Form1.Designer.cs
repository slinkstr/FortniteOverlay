using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FortniteOverlay
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.uploadFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.downloadFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.showOverlayCheckbox = new System.Windows.Forms.CheckBox();
            this.consoleLogTextBox = new System.Windows.Forms.TextBox();
            this.hostNameTextBox = new System.Windows.Forms.TextBox();
            this.hostIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.selfGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.squadmateGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox2 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox3 = new System.Windows.Forms.PictureBox();
            this.minimizeTrayCheckbox = new System.Windows.Forms.CheckBox();
            this.squadmateNameTextBox1 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox2 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox3 = new System.Windows.Forms.Label();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.debugOverlayCheckbox = new System.Windows.Forms.CheckBox();
            this.updateNoticeLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.uploadFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // uploadFrequencyNumericUpDown
            // 
            this.uploadFrequencyNumericUpDown.Location = new System.Drawing.Point(12, 12);
            this.uploadFrequencyNumericUpDown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.uploadFrequencyNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uploadFrequencyNumericUpDown.Name = "uploadFrequencyNumericUpDown";
            this.uploadFrequencyNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.uploadFrequencyNumericUpDown.TabIndex = 1;
            this.uploadFrequencyNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Upload Frequency (sec)";
            // 
            // downloadFrequencyNumericUpDown
            // 
            this.downloadFrequencyNumericUpDown.Location = new System.Drawing.Point(12, 38);
            this.downloadFrequencyNumericUpDown.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.downloadFrequencyNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.downloadFrequencyNumericUpDown.Name = "downloadFrequencyNumericUpDown";
            this.downloadFrequencyNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.downloadFrequencyNumericUpDown.TabIndex = 3;
            this.downloadFrequencyNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Download Frequency (sec)";
            // 
            // showOverlayCheckbox
            // 
            this.showOverlayCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showOverlayCheckbox.AutoSize = true;
            this.showOverlayCheckbox.Checked = true;
            this.showOverlayCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOverlayCheckbox.Location = new System.Drawing.Point(686, 12);
            this.showOverlayCheckbox.Name = "showOverlayCheckbox";
            this.showOverlayCheckbox.Size = new System.Drawing.Size(98, 17);
            this.showOverlayCheckbox.TabIndex = 5;
            this.showOverlayCheckbox.Text = "Enable Overlay";
            this.showOverlayCheckbox.UseVisualStyleBackColor = true;
            // 
            // consoleLogTextBox
            // 
            this.consoleLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleLogTextBox.Location = new System.Drawing.Point(12, 238);
            this.consoleLogTextBox.Multiline = true;
            this.consoleLogTextBox.Name = "consoleLogTextBox";
            this.consoleLogTextBox.ReadOnly = true;
            this.consoleLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.consoleLogTextBox.Size = new System.Drawing.Size(776, 211);
            this.consoleLogTextBox.TabIndex = 6;
            this.consoleLogTextBox.WordWrap = false;
            // 
            // hostNameTextBox
            // 
            this.hostNameTextBox.Location = new System.Drawing.Point(68, 70);
            this.hostNameTextBox.Name = "hostNameTextBox";
            this.hostNameTextBox.ReadOnly = true;
            this.hostNameTextBox.Size = new System.Drawing.Size(300, 20);
            this.hostNameTextBox.TabIndex = 7;
            // 
            // hostIdTextBox
            // 
            this.hostIdTextBox.Location = new System.Drawing.Point(68, 96);
            this.hostIdTextBox.Name = "hostIdTextBox";
            this.hostIdTextBox.ReadOnly = true;
            this.hostIdTextBox.Size = new System.Drawing.Size(300, 20);
            this.hostIdTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "User:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "User ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selfGearPictureBox1
            // 
            this.selfGearPictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.selfGearPictureBox1.Location = new System.Drawing.Point(68, 182);
            this.selfGearPictureBox1.Name = "selfGearPictureBox1";
            this.selfGearPictureBox1.Size = new System.Drawing.Size(300, 50);
            this.selfGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.selfGearPictureBox1.TabIndex = 12;
            this.selfGearPictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 50);
            this.label6.TabIndex = 14;
            this.label6.Text = "Preview:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // squadmateGearPictureBox1
            // 
            this.squadmateGearPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox1.Location = new System.Drawing.Point(488, 70);
            this.squadmateGearPictureBox1.Name = "squadmateGearPictureBox1";
            this.squadmateGearPictureBox1.Size = new System.Drawing.Size(300, 50);
            this.squadmateGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox1.TabIndex = 15;
            this.squadmateGearPictureBox1.TabStop = false;
            // 
            // squadmateGearPictureBox2
            // 
            this.squadmateGearPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox2.Location = new System.Drawing.Point(488, 126);
            this.squadmateGearPictureBox2.Name = "squadmateGearPictureBox2";
            this.squadmateGearPictureBox2.Size = new System.Drawing.Size(300, 50);
            this.squadmateGearPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox2.TabIndex = 16;
            this.squadmateGearPictureBox2.TabStop = false;
            // 
            // squadmateGearPictureBox3
            // 
            this.squadmateGearPictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox3.Location = new System.Drawing.Point(488, 182);
            this.squadmateGearPictureBox3.Name = "squadmateGearPictureBox3";
            this.squadmateGearPictureBox3.Size = new System.Drawing.Size(300, 50);
            this.squadmateGearPictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox3.TabIndex = 17;
            this.squadmateGearPictureBox3.TabStop = false;
            // 
            // minimizeTrayCheckbox
            // 
            this.minimizeTrayCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeTrayCheckbox.AutoSize = true;
            this.minimizeTrayCheckbox.Checked = true;
            this.minimizeTrayCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeTrayCheckbox.Location = new System.Drawing.Point(686, 35);
            this.minimizeTrayCheckbox.Name = "minimizeTrayCheckbox";
            this.minimizeTrayCheckbox.Size = new System.Drawing.Size(98, 17);
            this.minimizeTrayCheckbox.TabIndex = 18;
            this.minimizeTrayCheckbox.Text = "Minimize to tray";
            this.minimizeTrayCheckbox.UseVisualStyleBackColor = true;
            // 
            // squadmateNameTextBox1
            // 
            this.squadmateNameTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateNameTextBox1.Location = new System.Drawing.Point(382, 70);
            this.squadmateNameTextBox1.Name = "squadmateNameTextBox1";
            this.squadmateNameTextBox1.Size = new System.Drawing.Size(100, 50);
            this.squadmateNameTextBox1.TabIndex = 19;
            this.squadmateNameTextBox1.Text = "(none)";
            this.squadmateNameTextBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // squadmateNameTextBox2
            // 
            this.squadmateNameTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateNameTextBox2.Location = new System.Drawing.Point(382, 126);
            this.squadmateNameTextBox2.Name = "squadmateNameTextBox2";
            this.squadmateNameTextBox2.Size = new System.Drawing.Size(100, 50);
            this.squadmateNameTextBox2.TabIndex = 20;
            this.squadmateNameTextBox2.Text = "(none)";
            this.squadmateNameTextBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // squadmateNameTextBox3
            // 
            this.squadmateNameTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateNameTextBox3.Location = new System.Drawing.Point(382, 182);
            this.squadmateNameTextBox3.Name = "squadmateNameTextBox3";
            this.squadmateNameTextBox3.Size = new System.Drawing.Size(100, 50);
            this.squadmateNameTextBox3.TabIndex = 21;
            this.squadmateNameTextBox3.Text = "(none)";
            this.squadmateNameTextBox3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // showConsoleCheckBox
            // 
            this.showConsoleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showConsoleCheckBox.AutoSize = true;
            this.showConsoleCheckBox.Checked = true;
            this.showConsoleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showConsoleCheckBox.Location = new System.Drawing.Point(585, 12);
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.Size = new System.Drawing.Size(93, 17);
            this.showConsoleCheckBox.TabIndex = 22;
            this.showConsoleCheckBox.Text = "Show console";
            this.showConsoleCheckBox.UseVisualStyleBackColor = true;
            this.showConsoleCheckBox.CheckedChanged += new System.EventHandler(this.showConsoleCheckBox_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Fortnite Gear Overlay";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // debugOverlayCheckbox
            // 
            this.debugOverlayCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugOverlayCheckbox.AutoSize = true;
            this.debugOverlayCheckbox.Location = new System.Drawing.Point(585, 35);
            this.debugOverlayCheckbox.Name = "debugOverlayCheckbox";
            this.debugOverlayCheckbox.Size = new System.Drawing.Size(95, 17);
            this.debugOverlayCheckbox.TabIndex = 23;
            this.debugOverlayCheckbox.Text = "Debug overlay";
            this.debugOverlayCheckbox.UseVisualStyleBackColor = true;
            // 
            // updateNoticeLinkLabel
            // 
            this.updateNoticeLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateNoticeLinkLabel.Location = new System.Drawing.Point(385, 452);
            this.updateNoticeLinkLabel.Name = "updateNoticeLinkLabel";
            this.updateNoticeLinkLabel.Size = new System.Drawing.Size(399, 20);
            this.updateNoticeLinkLabel.TabIndex = 25;
            this.updateNoticeLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateNoticeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateNoticeLinkLabel_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 481);
            this.Controls.Add(this.updateNoticeLinkLabel);
            this.Controls.Add(this.debugOverlayCheckbox);
            this.Controls.Add(this.showConsoleCheckBox);
            this.Controls.Add(this.squadmateNameTextBox3);
            this.Controls.Add(this.squadmateNameTextBox2);
            this.Controls.Add(this.squadmateNameTextBox1);
            this.Controls.Add(this.minimizeTrayCheckbox);
            this.Controls.Add(this.squadmateGearPictureBox3);
            this.Controls.Add(this.squadmateGearPictureBox2);
            this.Controls.Add(this.squadmateGearPictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.selfGearPictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hostIdTextBox);
            this.Controls.Add(this.hostNameTextBox);
            this.Controls.Add(this.consoleLogTextBox);
            this.Controls.Add(this.showOverlayCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.downloadFrequencyNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uploadFrequencyNumericUpDown);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fortnite Gear Overlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.uploadFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown uploadFrequencyNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown downloadFrequencyNumericUpDown;
        private System.Windows.Forms.Label label2;
        private CheckBox showOverlayCheckbox;
        private TextBox consoleLogTextBox;
        private TextBox hostNameTextBox;
        private TextBox hostIdTextBox;
        private Label label3;
        private Label label4;
        private PictureBox selfGearPictureBox1;
        private Label label6;
        private PictureBox squadmateGearPictureBox1;
        private PictureBox squadmateGearPictureBox2;
        private PictureBox squadmateGearPictureBox3;
        private CheckBox minimizeTrayCheckbox;
        private Label squadmateNameTextBox1;
        private Label squadmateNameTextBox2;
        private Label squadmateNameTextBox3;
        private CheckBox showConsoleCheckBox;
        private NotifyIcon notifyIcon;
        private CheckBox debugOverlayCheckbox;
        private LinkLabel updateNoticeLinkLabel;
    }
}

