using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FortniteOverlay
{
    public partial class Form1 : Form
    {
        private int consoleHeight;
        private bool firstShow;

        public Form1()
        {
            InitializeComponent();
            consoleHeight = consoleLogTextBox.Size.Height;
            Text += " v" + Application.ProductVersion;
            Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            notifyIcon.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
            firstShow = true;
        }
        
        protected override bool ShowWithoutActivation
        {
            get
            {
                if(firstShow)
                {
                    firstShow = false;
                    return Program.config.StartMinimized;
                }
                return false;
            }
        }

        // ****************************************************************************************************
        // HELPER METHODS
        // ****************************************************************************************************
        public ProgramOptions CurrentProgramOptions()
        {
            return new ProgramOptions()
            {
                DebugOverlay = debugOverlayCheckbox.Checked
            };
        }

        public void Log(string message)
        {
            message = $"[{DateTime.Now}] {message.Replace("\n", Environment.NewLine)}" + Environment.NewLine;
            if (consoleLogTextBox.InvokeRequired)
            {
                consoleLogTextBox.Invoke(new MethodInvoker(delegate { consoleLogTextBox.AppendText(message); }));
            }
            else
            {
                consoleLogTextBox.AppendText(message);
            }
            Console.Write(message);
        }

        public void LogDebug(string message)
        {
            if(!(CurrentProgramOptions().DebugOverlay))
            {
                return;
            }

            message = $"[{DateTime.Now}] {message.Replace("\n", Environment.NewLine)}" + Environment.NewLine;
            if (consoleLogTextBox.InvokeRequired)
            {
                consoleLogTextBox.Invoke(new MethodInvoker(delegate { consoleLogTextBox.AppendText(message); }));
            }
            else
            {
                consoleLogTextBox.AppendText(message);
            }
            Console.Write(message);
        }

        public void MinimizeToTray()
        {
            ShowInTaskbar = false;
            notifyIcon.Visible = true;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        public void UnminimizeFromTray()
        {
            Show();
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        public void SetAlwaysOnTop(bool ontop)
        {
            TopMost = ontop;
        }

        public static void SetControlProperty(Control control, string propertyName, object data)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate { control.GetType().GetProperty(propertyName).SetValue(control, data); }));
            }
            else
            {
                control.GetType().GetProperty(propertyName).SetValue(control, data);
            }
        }

        public void SetHostName(string text)
        {
            SetControlProperty(previewLabel, "Text", text);
        }

        public void SetSelfGear(Bitmap bmp)
        {
            SetControlProperty(selfGearPictureBox1, "Image", bmp);
        }

        public void SetSquadGear(int index, Bitmap bmp)
        {
            switch (index)
            {
                case 0:
                    SetControlProperty(squadmate1GearPictureBox, "Image", bmp);
                    break;
                case 1:
                    SetControlProperty(squadmate2GearPictureBox, "Image", bmp);
                    break;
                case 2:
                    SetControlProperty(squadmate3GearPictureBox, "Image", bmp);
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
                    SetControlProperty(squadmate1NameLabel, "Text", name);
                    break;
                case 1:
                    SetControlProperty(squadmate2NameLabel, "Text", name);
                    break;
                case 2:
                    SetControlProperty(squadmate3NameLabel, "Text", name);
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadName");
            }
        }

        public void SetUpdateNotice(string text, string url = "")
        {
            updateNoticeLinkLabel.Text = text;
            updateNoticeLinkLabel.LinkArea = string.IsNullOrWhiteSpace(url) ? new LinkArea(0, 0) : new LinkArea(0, updateNoticeLinkLabel.Text.Length);
            updateNoticeLinkLabel.Tag = url;
            updateNoticeLinkLabel.LinkVisited = false;
        }

        public void ShowHideConsole(bool showConsole)
        {
            if (consoleLogTextBox.Visible == showConsole)
            {
                return;
            }

            int tableRow = mainTableLayoutPanel.GetRow(consoleLogTextBox);
            consoleLogTextBox.Visible = showConsole;
            mainTableLayoutPanel.RowStyles[tableRow].Height = showConsole ? consoleHeight : 0;
            Size = new Size(Size.Width, Size.Height + (showConsole ? consoleHeight : consoleHeight * -1));
        }

        public void ShowHideSortButtons(int squadmates)
        {
            SetControlProperty(squadmate1DownButton, "Visible", (squadmates > 1));
            SetControlProperty(squadmate2UpButton,   "Visible", (squadmates > 1));
            SetControlProperty(squadmate2DownButton, "Visible", (squadmates > 2));
            SetControlProperty(squadmate3UpButton,   "Visible", (squadmates > 2));
        }

        private static void SwapSquad(int indexA, int indexB)
        {
            var max = indexA > indexB ? indexA : indexB;
            if (Program.fortniters.Count < max + 1) { return; }
            var temp = Program.fortniters[indexA];
            Program.fortniters[indexA] = Program.fortniters[indexB];
            Program.fortniters[indexB] = temp;
            Program.UpdateFormElements();
        }

        // ****************************************************************************************************
        // CONTROL EVENT HANDLERS
        // ****************************************************************************************************
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Icon = null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowHideConsole(Program.config.ShowConsole);
            SetAlwaysOnTop(Program.config.AlwaysOnTop);
            if(Program.config.StartMinimized)
            {
                WindowState = FormWindowState.Minimized;
                MinimizeToTray();
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && Program.config.MinimizeToTray)
            {
                MinimizeToTray();
            }
        }

        private void editConfigButton_Click(object sender, EventArgs e)
        {
            FormCollection forms = Application.OpenForms;
            foreach(Form form in forms)
            {
                if (form.Name == "ConfigForm")
                {
                    form.Focus();
                    return;
                }
            }

            new ConfigForm().Show();
        }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                UnminimizeFromTray();
            }
        }
        private void previewLabel_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if (!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }
        private void selfGearPictureBox1_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }
        private void showConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;

            mainTableLayoutPanel.RowStyles[1].Height = (visible ? consoleHeight : 0);
            Size = new Size(Size.Width, Size.Height + (visible ? consoleHeight : consoleHeight * -1));
        }


        private void squadmate1DownButton_Click(object sender, EventArgs e)
        {
            SwapSquad(0, 1);
        }
        private void squadmate1GearPictureBox_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }
        private void squadmate1NameLabel_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if(!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }

        private void squadmate2DownButton_Click(object sender, EventArgs e)
        {
            SwapSquad(1, 2);
        }
        private void squadmate2GearPictureBox_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }
        private void squadmate2NameLabel_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if (!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }
        private void squadmate2UpButton_Click(object sender, EventArgs e)
        {
            SwapSquad(0, 1);
        }

        private void squadmate3GearPictureBox_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }
        private void squadmate3NameLabel_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if (!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }
        private void squadmate3UpButton_Click(object sender, EventArgs e)
        {
            SwapSquad(1, 2);
        }

        private void updateNoticeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var label = (LinkLabel)sender;
            updateNoticeLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(label.Tag.ToString());
        }
    }

    public class ProgramOptions
    {
        public bool DebugOverlay;
    }
}
