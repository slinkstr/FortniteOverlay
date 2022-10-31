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

        public void Log(string message)
        {
            message = $"[{DateTime.Now}] {message.Replace("\n", Environment.NewLine)}" + Environment.NewLine;
            setControlProperty(textBox1, "Text", textBox1.Text + message);
        }

        public int GetUpFreq()
        {
            return Convert.ToInt32(UpFreq.Value);
        }

        public int GetDownFreq()
        {
            return Convert.ToInt32(DownFreq.Value);
        }

        public void SetHostName(string text)
        {
            setControlProperty(hostNameTextBox, "Text", text);
        }

        public void SetHostId(string text)
        {
            setControlProperty(hostIdTextBox, "Text", text);
        }

        public void SetSquadGear(string text)
        {
            setControlProperty(currentSquadTextBox, "Text", text);
        }

        public void SetSelfGear(Bitmap bmp)
        {
            setControlProperty(selfGearPictureBox1, "Image", bmp);
        }

        public void SetSquadGear(int index, Bitmap bmp)
        {
            switch (index)
            {
                case 0:
                    setControlProperty(squadmateGearPictureBox1, "Image", bmp);
                    break;
                case 1:
                    setControlProperty(squadmateGearPictureBox2, "Image", bmp);
                    break;
                case 2:
                    setControlProperty(squadmateGearPictureBox3, "Image", bmp);
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadGear");
            }
        }

        public void SetSquadName(int index, string name)
        {
            switch (index)
            {
                case 0:
                    setControlProperty(squadmateNameTextBox1, "Text", name);
                    break;
                case 1:
                    setControlProperty(squadmateNameTextBox2, "Text", name);
                    break;
                case 2:
                    setControlProperty(squadmateNameTextBox3, "Text", name);
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadName");
            }
        }

        public void setControlProperty(Control control, string propertyName, object data)
        {
            if(control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate { control.GetType().GetProperty(propertyName).SetValue(control, data); }));
            }
            else
            {
                control.GetType().GetProperty(propertyName).SetValue(control, data);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.UpFreq = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.DownFreq = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.enableOverlayCheckbox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hostNameTextBox = new System.Windows.Forms.TextBox();
            this.hostIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.currentSquadTextBox = new System.Windows.Forms.TextBox();
            this.selfGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.squadmateGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox2 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox3 = new System.Windows.Forms.PictureBox();
            this.minimizeTrayCheckbox = new System.Windows.Forms.CheckBox();
            this.squadmateNameTextBox1 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox2 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UpFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // UpFreq
            // 
            this.UpFreq.Location = new System.Drawing.Point(12, 12);
            this.UpFreq.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.UpFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpFreq.Name = "UpFreq";
            this.UpFreq.Size = new System.Drawing.Size(120, 20);
            this.UpFreq.TabIndex = 1;
            this.UpFreq.Value = new decimal(new int[] {
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
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // DownFreq
            // 
            this.DownFreq.Location = new System.Drawing.Point(12, 38);
            this.DownFreq.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.DownFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DownFreq.Name = "DownFreq";
            this.DownFreq.Size = new System.Drawing.Size(120, 20);
            this.DownFreq.TabIndex = 3;
            this.DownFreq.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.DownFreq.ValueChanged += new System.EventHandler(this.DownFreq_ValueChanged);
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
            // enableOverlayCheckbox
            // 
            this.enableOverlayCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enableOverlayCheckbox.AutoSize = true;
            this.enableOverlayCheckbox.Location = new System.Drawing.Point(686, 12);
            this.enableOverlayCheckbox.Name = "enableOverlayCheckbox";
            this.enableOverlayCheckbox.Size = new System.Drawing.Size(98, 17);
            this.enableOverlayCheckbox.TabIndex = 5;
            this.enableOverlayCheckbox.Text = "Enable Overlay";
            this.enableOverlayCheckbox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 209);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(776, 229);
            this.textBox1.TabIndex = 6;
            this.textBox1.WordWrap = false;
            // 
            // hostNameTextBox
            // 
            this.hostNameTextBox.Location = new System.Drawing.Point(68, 70);
            this.hostNameTextBox.Name = "hostNameTextBox";
            this.hostNameTextBox.ReadOnly = true;
            this.hostNameTextBox.Size = new System.Drawing.Size(224, 20);
            this.hostNameTextBox.TabIndex = 7;
            // 
            // hostIdTextBox
            // 
            this.hostIdTextBox.Location = new System.Drawing.Point(68, 96);
            this.hostIdTextBox.Name = "hostIdTextBox";
            this.hostIdTextBox.ReadOnly = true;
            this.hostIdTextBox.Size = new System.Drawing.Size(224, 20);
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
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "User ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // currentSquadTextBox
            // 
            this.currentSquadTextBox.Location = new System.Drawing.Point(68, 122);
            this.currentSquadTextBox.Name = "currentSquadTextBox";
            this.currentSquadTextBox.ReadOnly = true;
            this.currentSquadTextBox.Size = new System.Drawing.Size(224, 20);
            this.currentSquadTextBox.TabIndex = 11;
            // 
            // selfGearPictureBox1
            // 
            this.selfGearPictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.selfGearPictureBox1.Location = new System.Drawing.Point(68, 163);
            this.selfGearPictureBox1.Name = "selfGearPictureBox1";
            this.selfGearPictureBox1.Size = new System.Drawing.Size(224, 40);
            this.selfGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.selfGearPictureBox1.TabIndex = 12;
            this.selfGearPictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Squad:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 40);
            this.label6.TabIndex = 14;
            this.label6.Text = "Preview:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // squadmateGearPictureBox1
            // 
            this.squadmateGearPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox1.Location = new System.Drawing.Point(560, 71);
            this.squadmateGearPictureBox1.Name = "squadmateGearPictureBox1";
            this.squadmateGearPictureBox1.Size = new System.Drawing.Size(224, 40);
            this.squadmateGearPictureBox1.TabIndex = 15;
            this.squadmateGearPictureBox1.TabStop = false;
            // 
            // squadmateGearPictureBox2
            // 
            this.squadmateGearPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox2.Location = new System.Drawing.Point(560, 117);
            this.squadmateGearPictureBox2.Name = "squadmateGearPictureBox2";
            this.squadmateGearPictureBox2.Size = new System.Drawing.Size(224, 40);
            this.squadmateGearPictureBox2.TabIndex = 16;
            this.squadmateGearPictureBox2.TabStop = false;
            // 
            // squadmateGearPictureBox3
            // 
            this.squadmateGearPictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.squadmateGearPictureBox3.Location = new System.Drawing.Point(560, 163);
            this.squadmateGearPictureBox3.Name = "squadmateGearPictureBox3";
            this.squadmateGearPictureBox3.Size = new System.Drawing.Size(224, 40);
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
            this.squadmateNameTextBox1.Location = new System.Drawing.Point(454, 71);
            this.squadmateNameTextBox1.Name = "squadmateNameTextBox1";
            this.squadmateNameTextBox1.Size = new System.Drawing.Size(100, 40);
            this.squadmateNameTextBox1.TabIndex = 19;
            this.squadmateNameTextBox1.Text = "(none)";
            this.squadmateNameTextBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // squadmateNameTextBox2
            // 
            this.squadmateNameTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateNameTextBox2.Location = new System.Drawing.Point(454, 117);
            this.squadmateNameTextBox2.Name = "squadmateNameTextBox2";
            this.squadmateNameTextBox2.Size = new System.Drawing.Size(100, 40);
            this.squadmateNameTextBox2.TabIndex = 20;
            this.squadmateNameTextBox2.Text = "(none)";
            this.squadmateNameTextBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // squadmateNameTextBox3
            // 
            this.squadmateNameTextBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateNameTextBox3.Location = new System.Drawing.Point(454, 163);
            this.squadmateNameTextBox3.Name = "squadmateNameTextBox3";
            this.squadmateNameTextBox3.Size = new System.Drawing.Size(100, 40);
            this.squadmateNameTextBox3.TabIndex = 21;
            this.squadmateNameTextBox3.Text = "(none)";
            this.squadmateNameTextBox3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.squadmateNameTextBox3);
            this.Controls.Add(this.squadmateNameTextBox2);
            this.Controls.Add(this.squadmateNameTextBox1);
            this.Controls.Add(this.minimizeTrayCheckbox);
            this.Controls.Add(this.squadmateGearPictureBox3);
            this.Controls.Add(this.squadmateGearPictureBox2);
            this.Controls.Add(this.squadmateGearPictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selfGearPictureBox1);
            this.Controls.Add(this.currentSquadTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hostIdTextBox);
            this.Controls.Add(this.hostNameTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.enableOverlayCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DownFreq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpFreq);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Fortnite Gear Overlay";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UpFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown UpFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown DownFreq;
        private System.Windows.Forms.Label label2;
        private CheckBox enableOverlayCheckbox;
        private TextBox textBox1;
        private TextBox hostNameTextBox;
        private TextBox hostIdTextBox;
        private Label label3;
        private Label label4;
        private TextBox currentSquadTextBox;
        private PictureBox selfGearPictureBox1;
        private Label label5;
        private Label label6;
        private PictureBox squadmateGearPictureBox1;
        private PictureBox squadmateGearPictureBox2;
        private PictureBox squadmateGearPictureBox3;
        private CheckBox minimizeTrayCheckbox;
        private Label squadmateNameTextBox1;
        private Label squadmateNameTextBox2;
        private Label squadmateNameTextBox3;
    }
}

