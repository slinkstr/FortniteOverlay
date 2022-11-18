using System.Windows.Forms;

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
            this.uploadFrequencyLabel = new System.Windows.Forms.Label();
            this.downloadFrequencyNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.downloadFrequencyLabel = new System.Windows.Forms.Label();
            this.showOverlayCheckbox = new System.Windows.Forms.CheckBox();
            this.consoleLogTextBox = new System.Windows.Forms.TextBox();
            this.selfGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.previewLabel = new System.Windows.Forms.Label();
            this.squadmateGearPictureBox1 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox2 = new System.Windows.Forms.PictureBox();
            this.squadmateGearPictureBox3 = new System.Windows.Forms.PictureBox();
            this.squadmateNameTextBox1 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox2 = new System.Windows.Forms.Label();
            this.squadmateNameTextBox3 = new System.Windows.Forms.Label();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.debugOverlayCheckbox = new System.Windows.Forms.CheckBox();
            this.updateNoticeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.infoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.previewTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.configTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.uploadTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.downloadTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editConfigButton = new System.Windows.Forms.Button();
            this.squadmatesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.squadmate2ArrowTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.squadmate2DownButton = new System.Windows.Forms.Button();
            this.squadmate2UpButton = new System.Windows.Forms.Button();
            this.squadmate1ArrowTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.squadmate1DownButton = new System.Windows.Forms.Button();
            this.squadmate3ArrowTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.squadmate3UpButton = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.uploadFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadFrequencyNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).BeginInit();
            this.infoTableLayoutPanel.SuspendLayout();
            this.previewTableLayoutPanel.SuspendLayout();
            this.configTableLayoutPanel.SuspendLayout();
            this.uploadTableLayoutPanel.SuspendLayout();
            this.downloadTableLayoutPanel.SuspendLayout();
            this.squadmatesTableLayoutPanel.SuspendLayout();
            this.squadmate2ArrowTableLayoutPanel.SuspendLayout();
            this.squadmate1ArrowTableLayoutPanel.SuspendLayout();
            this.squadmate3ArrowTableLayoutPanel.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadFrequencyNumericUpDown
            // 
            this.uploadFrequencyNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadFrequencyNumericUpDown.Location = new System.Drawing.Point(3, 5);
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
            this.uploadFrequencyNumericUpDown.Size = new System.Drawing.Size(52, 20);
            this.uploadFrequencyNumericUpDown.TabIndex = 1;
            this.uploadFrequencyNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // uploadFrequencyLabel
            // 
            this.uploadFrequencyLabel.AutoSize = true;
            this.uploadFrequencyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadFrequencyLabel.Location = new System.Drawing.Point(61, 0);
            this.uploadFrequencyLabel.Name = "uploadFrequencyLabel";
            this.uploadFrequencyLabel.Size = new System.Drawing.Size(111, 30);
            this.uploadFrequencyLabel.TabIndex = 2;
            this.uploadFrequencyLabel.Text = "Upload Frequency (sec)";
            this.uploadFrequencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // downloadFrequencyNumericUpDown
            // 
            this.downloadFrequencyNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFrequencyNumericUpDown.Location = new System.Drawing.Point(3, 5);
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
            this.downloadFrequencyNumericUpDown.Size = new System.Drawing.Size(52, 20);
            this.downloadFrequencyNumericUpDown.TabIndex = 3;
            this.downloadFrequencyNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // downloadFrequencyLabel
            // 
            this.downloadFrequencyLabel.AutoSize = true;
            this.downloadFrequencyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadFrequencyLabel.Location = new System.Drawing.Point(61, 0);
            this.downloadFrequencyLabel.Name = "downloadFrequencyLabel";
            this.downloadFrequencyLabel.Size = new System.Drawing.Size(111, 30);
            this.downloadFrequencyLabel.TabIndex = 4;
            this.downloadFrequencyLabel.Text = "Download Frequency (sec)";
            this.downloadFrequencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // showOverlayCheckbox
            // 
            this.showOverlayCheckbox.AutoSize = true;
            this.showOverlayCheckbox.Checked = true;
            this.showOverlayCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOverlayCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showOverlayCheckbox.Location = new System.Drawing.Point(178, 33);
            this.showOverlayCheckbox.Name = "showOverlayCheckbox";
            this.showOverlayCheckbox.Size = new System.Drawing.Size(169, 24);
            this.showOverlayCheckbox.TabIndex = 5;
            this.showOverlayCheckbox.Text = "Enable Overlay";
            this.showOverlayCheckbox.UseVisualStyleBackColor = true;
            // 
            // consoleLogTextBox
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.consoleLogTextBox, 2);
            this.consoleLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleLogTextBox.Location = new System.Drawing.Point(3, 241);
            this.consoleLogTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.consoleLogTextBox.Multiline = true;
            this.consoleLogTextBox.Name = "consoleLogTextBox";
            this.consoleLogTextBox.ReadOnly = true;
            this.consoleLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.consoleLogTextBox.Size = new System.Drawing.Size(778, 200);
            this.consoleLogTextBox.TabIndex = 6;
            this.consoleLogTextBox.WordWrap = false;
            // 
            // selfGearPictureBox1
            // 
            this.selfGearPictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.selfGearPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selfGearPictureBox1.Location = new System.Drawing.Point(70, 0);
            this.selfGearPictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.selfGearPictureBox1.Name = "selfGearPictureBox1";
            this.selfGearPictureBox1.Size = new System.Drawing.Size(280, 80);
            this.selfGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.selfGearPictureBox1.TabIndex = 12;
            this.selfGearPictureBox1.TabStop = false;
            this.selfGearPictureBox1.DoubleClick += new System.EventHandler(this.selfGearPictureBox1_DoubleClick);
            // 
            // previewLabel
            // 
            this.previewLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewLabel.Location = new System.Drawing.Point(3, 0);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(64, 80);
            this.previewLabel.TabIndex = 14;
            this.previewLabel.Text = "Preview:";
            this.previewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.previewLabel.DoubleClick += new System.EventHandler(this.previewLabel_DoubleClick);
            // 
            // squadmateGearPictureBox1
            // 
            this.squadmateGearPictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.squadmateGearPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateGearPictureBox1.Location = new System.Drawing.Point(80, 0);
            this.squadmateGearPictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateGearPictureBox1.Name = "squadmateGearPictureBox1";
            this.squadmateGearPictureBox1.Size = new System.Drawing.Size(308, 79);
            this.squadmateGearPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox1.TabIndex = 15;
            this.squadmateGearPictureBox1.TabStop = false;
            this.squadmateGearPictureBox1.DoubleClick += new System.EventHandler(this.squadmateGearPictureBox1_DoubleClick);
            // 
            // squadmateGearPictureBox2
            // 
            this.squadmateGearPictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.squadmateGearPictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateGearPictureBox2.Location = new System.Drawing.Point(80, 79);
            this.squadmateGearPictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateGearPictureBox2.Name = "squadmateGearPictureBox2";
            this.squadmateGearPictureBox2.Size = new System.Drawing.Size(308, 79);
            this.squadmateGearPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox2.TabIndex = 16;
            this.squadmateGearPictureBox2.TabStop = false;
            this.squadmateGearPictureBox2.DoubleClick += new System.EventHandler(this.squadmateGearPictureBox2_DoubleClick);
            // 
            // squadmateGearPictureBox3
            // 
            this.squadmateGearPictureBox3.BackColor = System.Drawing.SystemColors.Control;
            this.squadmateGearPictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateGearPictureBox3.Location = new System.Drawing.Point(80, 158);
            this.squadmateGearPictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateGearPictureBox3.Name = "squadmateGearPictureBox3";
            this.squadmateGearPictureBox3.Size = new System.Drawing.Size(308, 80);
            this.squadmateGearPictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.squadmateGearPictureBox3.TabIndex = 17;
            this.squadmateGearPictureBox3.TabStop = false;
            this.squadmateGearPictureBox3.DoubleClick += new System.EventHandler(this.squadmateGearPictureBox3_DoubleClick);
            // 
            // squadmateNameTextBox1
            // 
            this.squadmateNameTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateNameTextBox1.Location = new System.Drawing.Point(0, 0);
            this.squadmateNameTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateNameTextBox1.Name = "squadmateNameTextBox1";
            this.squadmateNameTextBox1.Size = new System.Drawing.Size(80, 79);
            this.squadmateNameTextBox1.TabIndex = 19;
            this.squadmateNameTextBox1.Text = "squadmate1";
            this.squadmateNameTextBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.squadmateNameTextBox1.DoubleClick += new System.EventHandler(this.squadmateNameTextBox1_DoubleClick);
            // 
            // squadmateNameTextBox2
            // 
            this.squadmateNameTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateNameTextBox2.Location = new System.Drawing.Point(0, 79);
            this.squadmateNameTextBox2.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateNameTextBox2.Name = "squadmateNameTextBox2";
            this.squadmateNameTextBox2.Size = new System.Drawing.Size(80, 79);
            this.squadmateNameTextBox2.TabIndex = 20;
            this.squadmateNameTextBox2.Text = "squadmate2";
            this.squadmateNameTextBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.squadmateNameTextBox2.DoubleClick += new System.EventHandler(this.squadmateNameTextBox2_DoubleClick);
            // 
            // squadmateNameTextBox3
            // 
            this.squadmateNameTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmateNameTextBox3.Location = new System.Drawing.Point(0, 158);
            this.squadmateNameTextBox3.Margin = new System.Windows.Forms.Padding(0);
            this.squadmateNameTextBox3.Name = "squadmateNameTextBox3";
            this.squadmateNameTextBox3.Size = new System.Drawing.Size(80, 80);
            this.squadmateNameTextBox3.TabIndex = 21;
            this.squadmateNameTextBox3.Text = "squadmate3";
            this.squadmateNameTextBox3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.squadmateNameTextBox3.DoubleClick += new System.EventHandler(this.squadmateNameTextBox3_DoubleClick);
            // 
            // showConsoleCheckBox
            // 
            this.showConsoleCheckBox.AutoSize = true;
            this.showConsoleCheckBox.Checked = true;
            this.showConsoleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showConsoleCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showConsoleCheckBox.Location = new System.Drawing.Point(3, 33);
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.Size = new System.Drawing.Size(169, 24);
            this.showConsoleCheckBox.TabIndex = 22;
            this.showConsoleCheckBox.Text = "Show console";
            this.showConsoleCheckBox.UseVisualStyleBackColor = true;
            this.showConsoleCheckBox.CheckedChanged += new System.EventHandler(this.showConsoleCheckBox_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "FortniteOverlay";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // debugOverlayCheckbox
            // 
            this.debugOverlayCheckbox.AutoSize = true;
            this.debugOverlayCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugOverlayCheckbox.Location = new System.Drawing.Point(3, 63);
            this.debugOverlayCheckbox.Name = "debugOverlayCheckbox";
            this.debugOverlayCheckbox.Size = new System.Drawing.Size(169, 24);
            this.debugOverlayCheckbox.TabIndex = 23;
            this.debugOverlayCheckbox.Text = "Debug overlay";
            this.debugOverlayCheckbox.UseVisualStyleBackColor = true;
            // 
            // updateNoticeLinkLabel
            // 
            this.updateNoticeLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateNoticeLinkLabel.Location = new System.Drawing.Point(356, 441);
            this.updateNoticeLinkLabel.Name = "updateNoticeLinkLabel";
            this.updateNoticeLinkLabel.Size = new System.Drawing.Size(422, 20);
            this.updateNoticeLinkLabel.TabIndex = 25;
            this.updateNoticeLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateNoticeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateNoticeLinkLabel_LinkClicked);
            // 
            // infoTableLayoutPanel
            // 
            this.infoTableLayoutPanel.ColumnCount = 1;
            this.infoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoTableLayoutPanel.Controls.Add(this.previewTableLayoutPanel, 0, 2);
            this.infoTableLayoutPanel.Controls.Add(this.configTableLayoutPanel, 0, 0);
            this.infoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.infoTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoTableLayoutPanel.Name = "infoTableLayoutPanel";
            this.infoTableLayoutPanel.RowCount = 3;
            this.infoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.infoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.infoTableLayoutPanel.Size = new System.Drawing.Size(350, 238);
            this.infoTableLayoutPanel.TabIndex = 27;
            // 
            // previewTableLayoutPanel
            // 
            this.previewTableLayoutPanel.ColumnCount = 2;
            this.previewTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.previewTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.previewTableLayoutPanel.Controls.Add(this.selfGearPictureBox1, 1, 0);
            this.previewTableLayoutPanel.Controls.Add(this.previewLabel, 0, 0);
            this.previewTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewTableLayoutPanel.Location = new System.Drawing.Point(0, 158);
            this.previewTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.previewTableLayoutPanel.Name = "previewTableLayoutPanel";
            this.previewTableLayoutPanel.RowCount = 1;
            this.previewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.previewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.previewTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.previewTableLayoutPanel.Size = new System.Drawing.Size(350, 80);
            this.previewTableLayoutPanel.TabIndex = 0;
            // 
            // configTableLayoutPanel
            // 
            this.configTableLayoutPanel.ColumnCount = 2;
            this.configTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.configTableLayoutPanel.Controls.Add(this.uploadTableLayoutPanel, 0, 0);
            this.configTableLayoutPanel.Controls.Add(this.downloadTableLayoutPanel, 1, 0);
            this.configTableLayoutPanel.Controls.Add(this.showConsoleCheckBox, 0, 1);
            this.configTableLayoutPanel.Controls.Add(this.showOverlayCheckbox, 1, 1);
            this.configTableLayoutPanel.Controls.Add(this.editConfigButton, 1, 2);
            this.configTableLayoutPanel.Controls.Add(this.debugOverlayCheckbox, 0, 2);
            this.configTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.configTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.configTableLayoutPanel.Name = "configTableLayoutPanel";
            this.configTableLayoutPanel.RowCount = 3;
            this.configTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.configTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.configTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.configTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.configTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.configTableLayoutPanel.Size = new System.Drawing.Size(350, 90);
            this.configTableLayoutPanel.TabIndex = 0;
            // 
            // uploadTableLayoutPanel
            // 
            this.uploadTableLayoutPanel.ColumnCount = 2;
            this.uploadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.uploadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.uploadTableLayoutPanel.Controls.Add(this.uploadFrequencyLabel, 1, 0);
            this.uploadTableLayoutPanel.Controls.Add(this.uploadFrequencyNumericUpDown, 0, 0);
            this.uploadTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.uploadTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.uploadTableLayoutPanel.Name = "uploadTableLayoutPanel";
            this.uploadTableLayoutPanel.RowCount = 1;
            this.uploadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uploadTableLayoutPanel.Size = new System.Drawing.Size(175, 30);
            this.uploadTableLayoutPanel.TabIndex = 0;
            // 
            // downloadTableLayoutPanel
            // 
            this.downloadTableLayoutPanel.ColumnCount = 2;
            this.downloadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.downloadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.downloadTableLayoutPanel.Controls.Add(this.downloadFrequencyNumericUpDown, 0, 0);
            this.downloadTableLayoutPanel.Controls.Add(this.downloadFrequencyLabel, 1, 0);
            this.downloadTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadTableLayoutPanel.Location = new System.Drawing.Point(175, 0);
            this.downloadTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.downloadTableLayoutPanel.Name = "downloadTableLayoutPanel";
            this.downloadTableLayoutPanel.RowCount = 1;
            this.downloadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.downloadTableLayoutPanel.Size = new System.Drawing.Size(175, 30);
            this.downloadTableLayoutPanel.TabIndex = 0;
            // 
            // editConfigButton
            // 
            this.editConfigButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editConfigButton.Location = new System.Drawing.Point(178, 63);
            this.editConfigButton.Name = "editConfigButton";
            this.editConfigButton.Size = new System.Drawing.Size(169, 24);
            this.editConfigButton.TabIndex = 26;
            this.editConfigButton.Text = "Edit Config";
            this.editConfigButton.UseVisualStyleBackColor = true;
            this.editConfigButton.Click += new System.EventHandler(this.editConfigButton_Click);
            // 
            // squadmatesTableLayoutPanel
            // 
            this.squadmatesTableLayoutPanel.ColumnCount = 3;
            this.squadmatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.squadmatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.squadmatesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmate2ArrowTableLayoutPanel, 2, 1);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmate1ArrowTableLayoutPanel, 2, 0);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateGearPictureBox1, 1, 0);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateNameTextBox3, 0, 2);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateGearPictureBox3, 1, 2);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateNameTextBox2, 0, 1);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateGearPictureBox2, 1, 1);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmateNameTextBox1, 0, 0);
            this.squadmatesTableLayoutPanel.Controls.Add(this.squadmate3ArrowTableLayoutPanel, 2, 2);
            this.squadmatesTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmatesTableLayoutPanel.Location = new System.Drawing.Point(353, 3);
            this.squadmatesTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.squadmatesTableLayoutPanel.Name = "squadmatesTableLayoutPanel";
            this.squadmatesTableLayoutPanel.RowCount = 3;
            this.squadmatesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.squadmatesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.squadmatesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.squadmatesTableLayoutPanel.Size = new System.Drawing.Size(428, 238);
            this.squadmatesTableLayoutPanel.TabIndex = 28;
            // 
            // squadmate2ArrowTableLayoutPanel
            // 
            this.squadmate2ArrowTableLayoutPanel.ColumnCount = 1;
            this.squadmate2ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.squadmate2ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.squadmate2ArrowTableLayoutPanel.Controls.Add(this.squadmate2DownButton, 0, 1);
            this.squadmate2ArrowTableLayoutPanel.Controls.Add(this.squadmate2UpButton, 0, 0);
            this.squadmate2ArrowTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate2ArrowTableLayoutPanel.Location = new System.Drawing.Point(388, 79);
            this.squadmate2ArrowTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.squadmate2ArrowTableLayoutPanel.Name = "squadmate2ArrowTableLayoutPanel";
            this.squadmate2ArrowTableLayoutPanel.RowCount = 2;
            this.squadmate2ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate2ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate2ArrowTableLayoutPanel.Size = new System.Drawing.Size(40, 79);
            this.squadmate2ArrowTableLayoutPanel.TabIndex = 24;
            // 
            // squadmate2DownButton
            // 
            this.squadmate2DownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate2DownButton.Location = new System.Drawing.Point(3, 39);
            this.squadmate2DownButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.squadmate2DownButton.Name = "squadmate2DownButton";
            this.squadmate2DownButton.Size = new System.Drawing.Size(34, 34);
            this.squadmate2DownButton.TabIndex = 2;
            this.squadmate2DownButton.Text = "↓";
            this.squadmate2DownButton.UseVisualStyleBackColor = true;
            this.squadmate2DownButton.Visible = false;
            this.squadmate2DownButton.Click += new System.EventHandler(this.squadmate2DownButton_Click);
            // 
            // squadmate2UpButton
            // 
            this.squadmate2UpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate2UpButton.Location = new System.Drawing.Point(3, 6);
            this.squadmate2UpButton.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.squadmate2UpButton.Name = "squadmate2UpButton";
            this.squadmate2UpButton.Size = new System.Drawing.Size(34, 33);
            this.squadmate2UpButton.TabIndex = 1;
            this.squadmate2UpButton.Text = "↑";
            this.squadmate2UpButton.UseVisualStyleBackColor = true;
            this.squadmate2UpButton.Visible = false;
            this.squadmate2UpButton.Click += new System.EventHandler(this.squadmate2UpButton_Click);
            // 
            // squadmate1ArrowTableLayoutPanel
            // 
            this.squadmate1ArrowTableLayoutPanel.ColumnCount = 1;
            this.squadmate1ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.squadmate1ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.squadmate1ArrowTableLayoutPanel.Controls.Add(this.squadmate1DownButton, 0, 1);
            this.squadmate1ArrowTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate1ArrowTableLayoutPanel.Location = new System.Drawing.Point(388, 0);
            this.squadmate1ArrowTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.squadmate1ArrowTableLayoutPanel.Name = "squadmate1ArrowTableLayoutPanel";
            this.squadmate1ArrowTableLayoutPanel.RowCount = 2;
            this.squadmate1ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate1ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate1ArrowTableLayoutPanel.Size = new System.Drawing.Size(40, 79);
            this.squadmate1ArrowTableLayoutPanel.TabIndex = 23;
            // 
            // squadmate1DownButton
            // 
            this.squadmate1DownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate1DownButton.Location = new System.Drawing.Point(3, 39);
            this.squadmate1DownButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.squadmate1DownButton.Name = "squadmate1DownButton";
            this.squadmate1DownButton.Size = new System.Drawing.Size(34, 34);
            this.squadmate1DownButton.TabIndex = 0;
            this.squadmate1DownButton.Text = "↓";
            this.squadmate1DownButton.UseVisualStyleBackColor = true;
            this.squadmate1DownButton.Visible = false;
            this.squadmate1DownButton.Click += new System.EventHandler(this.squadmate1DownButton_Click);
            // 
            // squadmate3ArrowTableLayoutPanel
            // 
            this.squadmate3ArrowTableLayoutPanel.ColumnCount = 1;
            this.squadmate3ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.squadmate3ArrowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.squadmate3ArrowTableLayoutPanel.Controls.Add(this.squadmate3UpButton, 0, 0);
            this.squadmate3ArrowTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate3ArrowTableLayoutPanel.Location = new System.Drawing.Point(388, 158);
            this.squadmate3ArrowTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.squadmate3ArrowTableLayoutPanel.Name = "squadmate3ArrowTableLayoutPanel";
            this.squadmate3ArrowTableLayoutPanel.RowCount = 2;
            this.squadmate3ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate3ArrowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.squadmate3ArrowTableLayoutPanel.Size = new System.Drawing.Size(40, 80);
            this.squadmate3ArrowTableLayoutPanel.TabIndex = 22;
            // 
            // squadmate3UpButton
            // 
            this.squadmate3UpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.squadmate3UpButton.Location = new System.Drawing.Point(3, 6);
            this.squadmate3UpButton.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.squadmate3UpButton.Name = "squadmate3UpButton";
            this.squadmate3UpButton.Size = new System.Drawing.Size(34, 34);
            this.squadmate3UpButton.TabIndex = 1;
            this.squadmate3UpButton.Text = "↑";
            this.squadmate3UpButton.UseVisualStyleBackColor = true;
            this.squadmate3UpButton.Visible = false;
            this.squadmate3UpButton.Click += new System.EventHandler(this.squadmate3UpButton_Click);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.consoleLogTextBox, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.updateNoticeLinkLabel, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.squadmatesTableLayoutPanel, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.infoTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.Padding = new System.Windows.Forms.Padding(3);
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(784, 464);
            this.mainTableLayoutPanel.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 464);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FortniteOverlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.uploadFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadFrequencyNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selfGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.squadmateGearPictureBox3)).EndInit();
            this.infoTableLayoutPanel.ResumeLayout(false);
            this.previewTableLayoutPanel.ResumeLayout(false);
            this.configTableLayoutPanel.ResumeLayout(false);
            this.configTableLayoutPanel.PerformLayout();
            this.uploadTableLayoutPanel.ResumeLayout(false);
            this.uploadTableLayoutPanel.PerformLayout();
            this.downloadTableLayoutPanel.ResumeLayout(false);
            this.downloadTableLayoutPanel.PerformLayout();
            this.squadmatesTableLayoutPanel.ResumeLayout(false);
            this.squadmate2ArrowTableLayoutPanel.ResumeLayout(false);
            this.squadmate1ArrowTableLayoutPanel.ResumeLayout(false);
            this.squadmate3ArrowTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown uploadFrequencyNumericUpDown;
        private System.Windows.Forms.Label uploadFrequencyLabel;
        private System.Windows.Forms.NumericUpDown downloadFrequencyNumericUpDown;
        private System.Windows.Forms.Label downloadFrequencyLabel;
        private CheckBox showOverlayCheckbox;
        private TextBox consoleLogTextBox;
        private PictureBox selfGearPictureBox1;
        private Label previewLabel;
        private PictureBox squadmateGearPictureBox1;
        private PictureBox squadmateGearPictureBox2;
        private PictureBox squadmateGearPictureBox3;
        private Label squadmateNameTextBox1;
        private Label squadmateNameTextBox2;
        private Label squadmateNameTextBox3;
        private CheckBox showConsoleCheckBox;
        private NotifyIcon notifyIcon;
        private CheckBox debugOverlayCheckbox;
        private LinkLabel updateNoticeLinkLabel;
        private TableLayoutPanel infoTableLayoutPanel;
        private TableLayoutPanel previewTableLayoutPanel;
        private TableLayoutPanel configTableLayoutPanel;
        private TableLayoutPanel uploadTableLayoutPanel;
        private TableLayoutPanel downloadTableLayoutPanel;
        private TableLayoutPanel squadmatesTableLayoutPanel;
        private TableLayoutPanel mainTableLayoutPanel;
        private TableLayoutPanel squadmate2ArrowTableLayoutPanel;
        private Button squadmate2DownButton;
        private Button squadmate2UpButton;
        private TableLayoutPanel squadmate1ArrowTableLayoutPanel;
        private Button squadmate1DownButton;
        private TableLayoutPanel squadmate3ArrowTableLayoutPanel;
        private Button squadmate3UpButton;
        private Button editConfigButton;
    }
}

