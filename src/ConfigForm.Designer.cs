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
            this.minimizeToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileLocationButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.autorunCheckBox = new System.Windows.Forms.CheckBox();
            this.secretKeyPanel = new System.Windows.Forms.Panel();
            this.secretKeyTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.uploadEndpointPanel = new System.Windows.Forms.Panel();
            this.uploadEndpointTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.imageLocationPanel = new System.Windows.Forms.Panel();
            this.imageLocationTextBoxEx = new FortniteOverlay.TextBoxEx();
            this.controlTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.hudScalePanel = new System.Windows.Forms.Panel();
            this.inventoryHotkeyCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.uploadIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.downloadIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.enableOverlayCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.hudScaleNumericUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.secretKeyPanel.SuspendLayout();
            this.uploadEndpointPanel.SuspendLayout();
            this.imageLocationPanel.SuspendLayout();
            this.controlTableLayoutPanel.SuspendLayout();
            this.hudScalePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadIntervalNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadIntervalNumericUpDown)).BeginInit();
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
            this.hudScaleNumericUpDown.Size = new System.Drawing.Size(227, 20);
            this.hudScaleNumericUpDown.TabIndex = 1;
            this.hudScaleNumericUpDown.Value = new decimal(new int[] {
            25,
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
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Secret key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Upload endpoint";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Image location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "HUD scale";
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(120, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 52);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadButton.Location = new System.Drawing.Point(237, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(111, 52);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelButton.Location = new System.Drawing.Point(354, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 52);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // minimizeToTrayCheckBox
            // 
            this.minimizeToTrayCheckBox.AutoSize = true;
            this.minimizeToTrayCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minimizeToTrayCheckBox.Location = new System.Drawing.Point(8, 344);
            this.minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
            this.minimizeToTrayCheckBox.Size = new System.Drawing.Size(228, 50);
            this.minimizeToTrayCheckBox.TabIndex = 9;
            this.minimizeToTrayCheckBox.Text = "Minimize to tray";
            this.minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileLocationButton
            // 
            this.openFileLocationButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openFileLocationButton.Location = new System.Drawing.Point(3, 3);
            this.openFileLocationButton.Name = "openFileLocationButton";
            this.openFileLocationButton.Size = new System.Drawing.Size(111, 52);
            this.openFileLocationButton.TabIndex = 0;
            this.openFileLocationButton.Text = "Open File Location";
            this.openFileLocationButton.UseVisualStyleBackColor = true;
            this.openFileLocationButton.Click += new System.EventHandler(this.openFileLocationButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.autorunCheckBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.minimizeToTrayCheckBox, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.secretKeyPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uploadEndpointPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.imageLocationPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.controlTableLayoutPanel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.hudScalePanel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.inventoryHotkeyCheckBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.showConsoleCheckBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.enableOverlayCheckBox, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49893F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49893F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49893F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49893F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49893F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50141F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50033F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50362F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(479, 460);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // autorunCheckBox
            // 
            this.autorunCheckBox.AutoSize = true;
            this.autorunCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autorunCheckBox.Location = new System.Drawing.Point(242, 344);
            this.autorunCheckBox.Name = "autorunCheckBox";
            this.autorunCheckBox.Size = new System.Drawing.Size(229, 50);
            this.autorunCheckBox.TabIndex = 10;
            this.autorunCheckBox.Text = "Run at system startup";
            this.autorunCheckBox.UseVisualStyleBackColor = true;
            this.autorunCheckBox.CheckedChanged += new System.EventHandler(this.autostartCheckBox_CheckedChanged);
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
            this.secretKeyPanel.Size = new System.Drawing.Size(469, 56);
            this.secretKeyPanel.TabIndex = 0;
            this.toolTip1.SetToolTip(this.secretKeyPanel, "Secret key to upload images to your server.  Keep this private!");
            // 
            // secretKeyTextBoxEx
            // 
            this.secretKeyTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secretKeyTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.secretKeyTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.secretKeyTextBoxEx.Name = "secretKeyTextBoxEx";
            this.secretKeyTextBoxEx.Size = new System.Drawing.Size(462, 20);
            this.secretKeyTextBoxEx.TabIndex = 1;
            this.secretKeyTextBoxEx.UseSystemPasswordChar = true;
            // 
            // uploadEndpointPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.uploadEndpointPanel, 2);
            this.uploadEndpointPanel.Controls.Add(this.label2);
            this.uploadEndpointPanel.Controls.Add(this.uploadEndpointTextBoxEx);
            this.uploadEndpointPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadEndpointPanel.Location = new System.Drawing.Point(5, 61);
            this.uploadEndpointPanel.Margin = new System.Windows.Forms.Padding(0);
            this.uploadEndpointPanel.Name = "uploadEndpointPanel";
            this.uploadEndpointPanel.Size = new System.Drawing.Size(469, 56);
            this.uploadEndpointPanel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.uploadEndpointPanel, "URL to upload images to. Must end with \".php\".");
            // 
            // uploadEndpointTextBoxEx
            // 
            this.uploadEndpointTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadEndpointTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.uploadEndpointTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.uploadEndpointTextBoxEx.Name = "uploadEndpointTextBoxEx";
            this.uploadEndpointTextBoxEx.Size = new System.Drawing.Size(462, 20);
            this.uploadEndpointTextBoxEx.TabIndex = 1;
            this.uploadEndpointTextBoxEx.TextChanged += new System.EventHandler(this.uploadEndpointTextBoxEx_TextChanged);
            // 
            // imageLocationPanel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.imageLocationPanel, 2);
            this.imageLocationPanel.Controls.Add(this.label3);
            this.imageLocationPanel.Controls.Add(this.imageLocationTextBoxEx);
            this.imageLocationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageLocationPanel.Location = new System.Drawing.Point(5, 117);
            this.imageLocationPanel.Margin = new System.Windows.Forms.Padding(0);
            this.imageLocationPanel.Name = "imageLocationPanel";
            this.imageLocationPanel.Size = new System.Drawing.Size(469, 56);
            this.imageLocationPanel.TabIndex = 2;
            this.toolTip1.SetToolTip(this.imageLocationPanel, "URL to look for uploaded images at, usually right next to the upload endpoint. Mu" +
        "st end with \"/\".");
            // 
            // imageLocationTextBoxEx
            // 
            this.imageLocationTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageLocationTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.imageLocationTextBoxEx.Location = new System.Drawing.Point(3, 20);
            this.imageLocationTextBoxEx.Name = "imageLocationTextBoxEx";
            this.imageLocationTextBoxEx.Size = new System.Drawing.Size(462, 20);
            this.imageLocationTextBoxEx.TabIndex = 1;
            this.imageLocationTextBoxEx.TextChanged += new System.EventHandler(this.imageLocationTextBoxEx_TextChanged);
            // 
            // controlTableLayoutPanel
            // 
            this.controlTableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.controlTableLayoutPanel, 2);
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.controlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.controlTableLayoutPanel.Controls.Add(this.cancelButton, 3, 0);
            this.controlTableLayoutPanel.Controls.Add(this.loadButton, 2, 0);
            this.controlTableLayoutPanel.Controls.Add(this.saveButton, 1, 0);
            this.controlTableLayoutPanel.Controls.Add(this.openFileLocationButton, 0, 0);
            this.controlTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlTableLayoutPanel.Location = new System.Drawing.Point(5, 397);
            this.controlTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.controlTableLayoutPanel.Name = "controlTableLayoutPanel";
            this.controlTableLayoutPanel.RowCount = 1;
            this.controlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlTableLayoutPanel.Size = new System.Drawing.Size(469, 58);
            this.controlTableLayoutPanel.TabIndex = 16;
            // 
            // hudScalePanel
            // 
            this.hudScalePanel.Controls.Add(this.label4);
            this.hudScalePanel.Controls.Add(this.hudScaleNumericUpDown);
            this.hudScalePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudScalePanel.Location = new System.Drawing.Point(5, 229);
            this.hudScalePanel.Margin = new System.Windows.Forms.Padding(0);
            this.hudScalePanel.Name = "hudScalePanel";
            this.hudScalePanel.Size = new System.Drawing.Size(234, 56);
            this.hudScalePanel.TabIndex = 5;
            this.toolTip1.SetToolTip(this.hudScalePanel, "The HUD scale you use in-game. NOTE: All calculations are designed for 100% and d" +
        "erived. Rounding errors may occur.");
            // 
            // inventoryHotkeyCheckBox
            // 
            this.inventoryHotkeyCheckBox.AutoSize = true;
            this.inventoryHotkeyCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inventoryHotkeyCheckBox.Location = new System.Drawing.Point(242, 232);
            this.inventoryHotkeyCheckBox.Name = "inventoryHotkeyCheckBox";
            this.inventoryHotkeyCheckBox.Size = new System.Drawing.Size(229, 50);
            this.inventoryHotkeyCheckBox.TabIndex = 6;
            this.inventoryHotkeyCheckBox.Text = "\"Map && Backpack Keys\" enabled";
            this.toolTip1.SetToolTip(this.inventoryHotkeyCheckBox, "Your in-game \"Map & Backpack Keys\" setting.");
            this.inventoryHotkeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.uploadIntervalNumericUpDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 173);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 56);
            this.panel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Upload interval (seconds)";
            // 
            // uploadIntervalNumericUpDown
            // 
            this.uploadIntervalNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadIntervalNumericUpDown.Location = new System.Drawing.Point(3, 19);
            this.uploadIntervalNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.uploadIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uploadIntervalNumericUpDown.Name = "uploadIntervalNumericUpDown";
            this.uploadIntervalNumericUpDown.Size = new System.Drawing.Size(228, 20);
            this.uploadIntervalNumericUpDown.TabIndex = 1;
            this.uploadIntervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.downloadIntervalNumericUpDown);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(239, 173);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 56);
            this.panel2.TabIndex = 4;
            // 
            // downloadIntervalNumericUpDown
            // 
            this.downloadIntervalNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadIntervalNumericUpDown.Location = new System.Drawing.Point(3, 19);
            this.downloadIntervalNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.downloadIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.downloadIntervalNumericUpDown.Name = "downloadIntervalNumericUpDown";
            this.downloadIntervalNumericUpDown.Size = new System.Drawing.Size(229, 20);
            this.downloadIntervalNumericUpDown.TabIndex = 1;
            this.downloadIntervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Download interval (seconds)";
            // 
            // showConsoleCheckBox
            // 
            this.showConsoleCheckBox.AutoSize = true;
            this.showConsoleCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showConsoleCheckBox.Location = new System.Drawing.Point(8, 288);
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.Size = new System.Drawing.Size(228, 50);
            this.showConsoleCheckBox.TabIndex = 7;
            this.showConsoleCheckBox.Text = "Show console";
            this.showConsoleCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableOverlayCheckBox
            // 
            this.enableOverlayCheckBox.AutoSize = true;
            this.enableOverlayCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enableOverlayCheckBox.Location = new System.Drawing.Point(242, 288);
            this.enableOverlayCheckBox.Name = "enableOverlayCheckBox";
            this.enableOverlayCheckBox.Size = new System.Drawing.Size(229, 50);
            this.enableOverlayCheckBox.TabIndex = 8;
            this.enableOverlayCheckBox.Text = "Enable overlay";
            this.enableOverlayCheckBox.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(479, 460);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
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
            this.controlTableLayoutPanel.ResumeLayout(false);
            this.hudScalePanel.ResumeLayout(false);
            this.hudScalePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uploadIntervalNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadIntervalNumericUpDown)).EndInit();
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
        private System.Windows.Forms.CheckBox autorunCheckBox;
        private System.Windows.Forms.NumericUpDown downloadIntervalNumericUpDown;
        private System.Windows.Forms.NumericUpDown uploadIntervalNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox inventoryHotkeyCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox showConsoleCheckBox;
        private System.Windows.Forms.CheckBox enableOverlayCheckBox;
    }
}