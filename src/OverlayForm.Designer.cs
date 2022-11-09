using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FortniteOverlay
{
    partial class OverlayForm
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

        // https://stackoverflow.com/a/72831565
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_LAYERED = 0x80000;
                const int WS_EX_TRANSPARENT = 0x20;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.squadmateGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox2 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox3 = new System.Windows.Forms.PictureBox();
            this.debugPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // squadmateGearPictureBox1
            // 
            this.squadmateGearPictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox1.Location = new System.Drawing.Point(430, 120);
            this.squadmateGearPictureBox1.Name = "squadmateGearPictureBox1";
            this.squadmateGearPictureBox1.Size = new System.Drawing.Size(300, 80);
            this.squadmateGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox1.TabIndex = 2;
            this.squadmateGearPictureBox1.TabStop = false;
            // 
            // squadmateGearPictureBox2
            // 
            this.squadmateGearPictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox2.Location = new System.Drawing.Point(430, 200);
            this.squadmateGearPictureBox2.Name = "squadmateGearPictureBox2";
            this.squadmateGearPictureBox2.Size = new System.Drawing.Size(300, 80);
            this.squadmateGearPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox2.TabIndex = 3;
            this.squadmateGearPictureBox2.TabStop = false;
            // 
            // squadmateGearPictureBox3
            // 
            this.squadmateGearPictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.squadmateGearPictureBox3.Location = new System.Drawing.Point(430, 280);
            this.squadmateGearPictureBox3.Name = "squadmateGearPictureBox3";
            this.squadmateGearPictureBox3.Size = new System.Drawing.Size(300, 80);
            this.squadmateGearPictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox3.TabIndex = 4;
            this.squadmateGearPictureBox3.TabStop = false;
            // 
            // debugPictureBox
            // 
            this.debugPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugPictureBox.Location = new System.Drawing.Point(0, 0);
            this.debugPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.debugPictureBox.Name = "debugPictureBox";
            this.debugPictureBox.Size = new System.Drawing.Size(2560, 1440);
            this.debugPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.debugPictureBox.TabIndex = 5;
            this.debugPictureBox.TabStop = false;
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(2560, 1440);
            this.Controls.Add(this.squadmateGearPictureBox3);
            this.Controls.Add(this.squadmateGearPictureBox2);
            this.Controls.Add(this.squadmateGearPictureBox1);
            this.Controls.Add(this.debugPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverlayForm";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.Text = "OverlayForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox squadmateGearPictureBox1;
        private System.Windows.Forms.PictureBox squadmateGearPictureBox2;
        private System.Windows.Forms.PictureBox squadmateGearPictureBox3;
        private PictureBox debugPictureBox;
    }
}