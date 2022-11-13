using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortniteOverlay
{
    public partial class Form1 : Form
    {
        private int consoleHeight;

        public Form1()
        {
            InitializeComponent();
            Text += " v" + Application.ProductVersion;
            consoleHeight = consoleLogTextBox.Size.Height;
        }

        public ProgramOptions ProgramOptions()
        {
            return new ProgramOptions()
            {
                UploadFrequency = Convert.ToInt32(uploadFrequencyNumericUpDown.Value),
                DownloadFrequency = Convert.ToInt32(downloadFrequencyNumericUpDown.Value),
                ShowConsole = showConsoleCheckBox.Checked,
                EnableOverlay = showOverlayCheckbox.Checked,
                MinimizeToTray = minimizeTrayCheckbox.Checked,
                DebugOverlay = debugOverlayCheckbox.Checked,
                HUDScale = Convert.ToInt32(hudScaleNumericUpDown.Value),
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
                    SetControlProperty(squadmateGearPictureBox1, "Image", bmp);
                    break;
                case 1:
                    SetControlProperty(squadmateGearPictureBox2, "Image", bmp);
                    break;
                case 2:
                    SetControlProperty(squadmateGearPictureBox3, "Image", bmp);
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
                    SetControlProperty(squadmateNameTextBox1, "Text", name);
                    break;
                case 1:
                    SetControlProperty(squadmateNameTextBox2, "Text", name);
                    break;
                case 2:
                    SetControlProperty(squadmateNameTextBox3, "Text", name);
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadName");
            }
        }

        public void SetHUDScale(int scale)
        {
            SetControlProperty(hudScaleNumericUpDown, "Value", (decimal)scale);
        }

        public void SetUpdateNotice(string text, string url)
        {
            updateNoticeLinkLabel.Text = text;
            updateNoticeLinkLabel.LinkArea = new LinkArea(0, updateNoticeLinkLabel.Text.Length);
            updateNoticeLinkLabel.Tag = url;
            updateNoticeLinkLabel.LinkVisited = false;
        }

        public void SetControlProperty(Control control, string propertyName, object data)
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

        private void showConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;

            mainTableLayoutPanel.RowStyles[1].Height = (visible ? consoleHeight : 0);
            Size = new Size(Size.Width, Size.Height + (visible ? consoleHeight : consoleHeight * -1));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && minimizeTrayCheckbox.Checked)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
        }

        private void form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Icon = null;
        }

        private void updateNoticeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var label = (LinkLabel)sender;
            updateNoticeLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start(label.Tag.ToString());
        }

        private void squadmateNameTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if(!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }

        private void squadmateNameTextBox2_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if (!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }

        private void squadmateNameTextBox3_DoubleClick(object sender, EventArgs e)
        {
            var label = (Label)sender;
            if (!string.IsNullOrWhiteSpace(label.Text)) { Clipboard.SetText(label.Text); }
        }

        private void squadmateGearPictureBox1_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }

        private void squadmateGearPictureBox2_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
        }

        private void squadmateGearPictureBox3_DoubleClick(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            if (box.Image != null) { Clipboard.SetDataObject(box.Image); }
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
    }

    public class ProgramOptions
    {
        public int UploadFrequency;
        public int DownloadFrequency;
        public bool ShowConsole;
        public bool EnableOverlay;
        public bool MinimizeToTray;
        public bool DebugOverlay;
        public int HUDScale;
    }
}
