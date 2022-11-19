namespace FortniteOverlay
{
    partial class ConfigForm
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
            this.hudScaleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.imageLocationTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.uploadEndpointTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.secretKeyTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.minimizeToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileLocationButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.secretKeyPanel = new System.Windows.Forms.Panel();
            this.uploadEndpointPanel = new System.Windows.Forms.Panel();
            this.imageLocationPanel = new System.Windows.Forms.Panel();
            this.hudScalePanel = new System.Windows.Forms.Panel();
            this.controlTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.hudScaleNumericUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.secretKeyPanel.SuspendLayout();
            this.uploadEndpointPanel.SuspendLayout();
            this.imageLocationPanel.SuspendLayout();
            this.hudScalePanel.SuspendLayout();
            this.controlTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // hudScaleNumericUpDown
            // 
            this.hudScaleNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hudScaleNumericUpDown.Location = new System.Drawing.Point(3, 20);
            this.hudScaleNumericUpDown.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.hudScaleNumericUpDown.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.hudScaleNumericUpDown.Name = "hudScaleNumericUpDown";
            this.hudScaleNumericUpDown.Size = new System.Drawing.Size(327, 20);
            this.hudScaleNumericUpDown.TabIndex = 3;
            this.hudScaleNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Secret Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Upload Endpoint";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Image Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "HUD Scale";
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(66, 50);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadButton.Location = new System.Drawing.Point(75, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(66, 50);
            this.loadButton.TabIndex = 11;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelButton.Location = new System.Drawing.Point(147, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(68, 50);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // imageLocationTextBoxEx
            // 
            this.imageLocationTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageLocationTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.imageLocationTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.imageLocationTextBoxEx.Name = "imageLocationTextBoxEx";
            this.imageLocationTextBoxEx.Size = new System.Drawing.Size(327, 20);
            this.imageLocationTextBoxEx.TabIndex = 2;
            this.imageLocationTextBoxEx.TextChanged += new System.EventHandler(this.imageLocationTextBoxEx_TextChanged);
            // 
            // uploadEndpointTextBoxEx
            // 
            this.uploadEndpointTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadEndpointTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.uploadEndpointTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.uploadEndpointTextBoxEx.Name = "uploadEndpointTextBoxEx";
            this.uploadEndpointTextBoxEx.Size = new System.Drawing.Size(327, 20);
            this.uploadEndpointTextBoxEx.TabIndex = 1;
            this.uploadEndpointTextBoxEx.TextChanged += new System.EventHandler(this.uploadEndpointTextBoxEx_TextChanged);
            // 
            // secretKeyTextBoxEx
            // 
            this.secretKeyTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secretKeyTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.secretKeyTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.secretKeyTextBoxEx.Name = "secretKeyTextBoxEx";
            this.secretKeyTextBoxEx.Size = new System.Drawing.Size(327, 20);
            this.secretKeyTextBoxEx.TabIndex = 0;
            this.secretKeyTextBoxEx.UseSystemPasswordChar = true;
            // 
            // minimizeToTrayCheckBox
            // 
            this.minimizeToTrayCheckBox.AutoSize = true;
            this.minimizeToTrayCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minimizeToTrayCheckBox.Location = new System.Drawing.Point(8, 212);
            this.minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
            this.minimizeToTrayCheckBox.Size = new System.Drawing.Size(110, 45);
            this.minimizeToTrayCheckBox.TabIndex = 13;
            this.minimizeToTrayCheckBox.Text = "Minimize to Tray";
            this.toolTip1.SetToolTip(this.minimizeToTrayCheckBox, "Whether or not to minimize the program to the system tray.");
            this.minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileLocationButton
            // 
            this.openFileLocationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openFileLocationButton.Location = new System.Drawing.Point(8, 263);
            this.openFileLocationButton.Name = "openFileLocationButton";
            this.openFileLocationButton.Size = new System.Drawing.Size(110, 50);
            this.openFileLocationButton.TabIndex = 14;
            this.openFileLocationButton.Text = "Open File Location";
            this.openFileLocationButton.UseVisualStyleBackColor = true;
            this.openFileLocationButton.Click += new System.EventHandler(this.openFileLocationButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.secretKeyPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.openFileLocationButton, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.uploadEndpointPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.minimizeToTrayCheckBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.imageLocationPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.hudScalePanel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.controlTableLayoutPanel, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 321);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // secretKeyPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.secretKeyPanel, 2);
            this.secretKeyPanel.Controls.Add(this.label1);
            this.secretKeyPanel.Controls.Add(this.secretKeyTextBoxEx);
            this.secretKeyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secretKeyPanel.Location = new System.Drawing.Point(5, 5);
            this.secretKeyPanel.Margin = new System.Windows.Forms.Padding(0);
            this.secretKeyPanel.Name = "secretKeyPanel";
            this.secretKeyPanel.Size = new System.Drawing.Size(334, 51);
            this.secretKeyPanel.TabIndex = 0;
            this.toolTip1.SetToolTip(this.secretKeyPanel, "Secret key to upload images to your server.  Keep this private!");
            // 
            // uploadEndpointPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.uploadEndpointPanel, 2);
            this.uploadEndpointPanel.Controls.Add(this.label2);
            this.uploadEndpointPanel.Controls.Add(this.uploadEndpointTextBoxEx);
            this.uploadEndpointPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadEndpointPanel.Location = new System.Drawing.Point(5, 56);
            this.uploadEndpointPanel.Margin = new System.Windows.Forms.Padding(0);
            this.uploadEndpointPanel.Name = "uploadEndpointPanel";
            this.uploadEndpointPanel.Size = new System.Drawing.Size(334, 51);
            this.uploadEndpointPanel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.uploadEndpointPanel, "URL to upload images to. Must end with \".php\".");
            // 
            // imageLocationPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.imageLocationPanel, 2);
            this.imageLocationPanel.Controls.Add(this.label3);
            this.imageLocationPanel.Controls.Add(this.imageLocationTextBoxEx);
            this.imageLocationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageLocationPanel.Location = new System.Drawing.Point(5, 107);
            this.imageLocationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.imageLocationPanel.Name = "imageLocationPanel";
            this.imageLocationPanel.Size = new System.Drawing.Size(334, 51);
            this.imageLocationPanel.TabIndex = 2;
            this.toolTip1.SetToolTip(this.imageLocationPanel, "URL to look for uploaded images at, usually right next to the upload endpoint. Mu" +
        "st end with \"/\".");
            // 
            // hudScalePanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.hudScalePanel, 2);
            this.hudScalePanel.Controls.Add(this.label4);
            this.hudScalePanel.Controls.Add(this.hudScaleNumericUpDown);
            this.hudScalePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudScalePanel.Location = new System.Drawing.Point(5, 158);
            this.hudScalePanel.Margin = new System.Windows.Forms.Padding(0);
            this.hudScalePanel.Name = "hudScalePanel";
            this.hudScalePanel.Size = new System.Drawing.Size(334, 51);
            this.hudScalePanel.TabIndex = 3;
            this.toolTip1.SetToolTip(this.hudScalePanel, "The HUD scale you use in-game. NOTE: All calculations are designed for 100% and d" +
        "erived. Rounding errors may occur.");
            // 
            // controlTableLayoutPanel
            // 
            this.controlTableLayoutPanel.ColumnCount = 3;
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.controlTableLayoutPanel.Controls.Add(this.cancelButton, 2, 0);
            this.controlTableLayoutPanel.Controls.Add(this.saveButton, 0, 0);
            this.controlTableLayoutPanel.Controls.Add(this.loadButton, 1, 0);
            this.controlTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlTableLayoutPanel.Location = new System.Drawing.Point(121, 260);
            this.controlTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.controlTableLayoutPanel.Name = "controlTableLayoutPanel";
            this.controlTableLayoutPanel.RowCount = 1;
            this.controlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.controlTableLayoutPanel.Size = new System.Drawing.Size(218, 56);
            this.controlTableLayoutPanel.TabIndex = 16;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 0;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 0;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 321);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hudScaleNumericUpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.secretKeyPanel.ResumeLayout(false);
            this.secretKeyPanel.PerformLayout();
            this.uploadEndpointPanel.ResumeLayout(false);
            this.uploadEndpointPanel.PerformLayout();
            this.imageLocationPanel.ResumeLayout(false);
            this.imageLocationPanel.PerformLayout();
            this.hudScalePanel.ResumeLayout(false);
            this.hudScalePanel.PerformLayout();
            this.controlTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown hudScaleNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
        private TextBoxEx secretKeyTextBoxEx;
        private TextBoxEx uploadEndpointTextBoxEx;
        private TextBoxEx imageLocationTextBoxEx;
        private System.Windows.Forms.CheckBox minimizeToTrayCheckBox;
        private System.Windows.Forms.Button openFileLocationButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel secretKeyPanel;
        private System.Windows.Forms.Panel uploadEndpointPanel;
        private System.Windows.Forms.Panel imageLocationPanel;
        private System.Windows.Forms.Panel hudScalePanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel controlTableLayoutPanel;
    }
}