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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
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
            ((System.ComponentModel.ISupportInitialize)(this.hudScaleNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // hudScaleNumericUpDown
            // 
            this.hudScaleNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hudScaleNumericUpDown.Location = new System.Drawing.Point(15, 175);
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
            this.hudScaleNumericUpDown.Size = new System.Drawing.Size(376, 20);
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
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Secret Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Upload Endpoint";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Image Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "HUD Scale";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(154, 286);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(235, 286);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 11;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(316, 286);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
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
            this.imageLocationTextBoxEx.Location = new System.Drawing.Point(15, 125);
            this.imageLocationTextBoxEx.Name = "imageLocationTextBoxEx";
            this.imageLocationTextBoxEx.Size = new System.Drawing.Size(376, 20);
            this.imageLocationTextBoxEx.TabIndex = 2;
            this.imageLocationTextBoxEx.TextChanged += new System.EventHandler(this.imageLocationTextBoxEx_TextChanged);
            // 
            // uploadEndpointTextBoxEx
            // 
            this.uploadEndpointTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadEndpointTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.uploadEndpointTextBoxEx.Location = new System.Drawing.Point(15, 75);
            this.uploadEndpointTextBoxEx.Name = "uploadEndpointTextBoxEx";
            this.uploadEndpointTextBoxEx.Size = new System.Drawing.Size(376, 20);
            this.uploadEndpointTextBoxEx.TabIndex = 1;
            this.uploadEndpointTextBoxEx.TextChanged += new System.EventHandler(this.uploadEndpointTextBoxEx_TextChanged);
            // 
            // secretKeyTextBoxEx
            // 
            this.secretKeyTextBoxEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secretKeyTextBoxEx.BorderColor = System.Drawing.Color.Transparent;
            this.secretKeyTextBoxEx.Location = new System.Drawing.Point(15, 25);
            this.secretKeyTextBoxEx.Name = "secretKeyTextBoxEx";
            this.secretKeyTextBoxEx.Size = new System.Drawing.Size(376, 20);
            this.secretKeyTextBoxEx.TabIndex = 0;
            this.secretKeyTextBoxEx.UseSystemPasswordChar = true;
            // 
            // minimizeToTrayCheckBox
            // 
            this.minimizeToTrayCheckBox.AutoSize = true;
            this.minimizeToTrayCheckBox.Location = new System.Drawing.Point(15, 225);
            this.minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
            this.minimizeToTrayCheckBox.Size = new System.Drawing.Size(102, 17);
            this.minimizeToTrayCheckBox.TabIndex = 13;
            this.minimizeToTrayCheckBox.Text = "Minimize to Tray";
            this.minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileLocationButton
            // 
            this.openFileLocationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openFileLocationButton.Location = new System.Drawing.Point(12, 286);
            this.openFileLocationButton.Name = "openFileLocationButton";
            this.openFileLocationButton.Size = new System.Drawing.Size(115, 23);
            this.openFileLocationButton.TabIndex = 14;
            this.openFileLocationButton.Text = "Open File Location";
            this.openFileLocationButton.UseVisualStyleBackColor = true;
            this.openFileLocationButton.Click += new System.EventHandler(this.openFileLocationButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 321);
            this.Controls.Add(this.openFileLocationButton);
            this.Controls.Add(this.minimizeToTrayCheckBox);
            this.Controls.Add(this.imageLocationTextBoxEx);
            this.Controls.Add(this.uploadEndpointTextBoxEx);
            this.Controls.Add(this.secretKeyTextBoxEx);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hudScaleNumericUpDown);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hudScaleNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}