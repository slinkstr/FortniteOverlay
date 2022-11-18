using FortniteOverlay.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortniteOverlay
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            LoadConfigFromFile();
        }

        private void uploadEndpointTextBoxEx_TextChanged(object sender, EventArgs e)
        {
            TextBoxEx textBox = (TextBoxEx)sender;
            if (MiscUtil.uploadEndpointRegex.Match(textBox.Text).Success)
            {
                textBox.BorderColor = Color.Green;
            }
            else
            {
                textBox.BorderColor = Color.Red;
            }
        }

        private void imageLocationTextBoxEx_TextChanged(object sender, EventArgs e)
        {
            TextBoxEx textBox = (TextBoxEx)sender;
            if (MiscUtil.imageLocationRegex.Match(textBox.Text).Success)
            {
                textBox.BorderColor = Color.Green;
            }
            else
            {
                textBox.BorderColor = Color.Red;
            }
        }

        private void LoadConfigFromFile()
        {
            ProgramConfig cfg;
            try
            {
                cfg = MiscUtil.GetConfig();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            secretKeyTextBoxEx.Text = cfg.SecretKey;
            uploadEndpointTextBoxEx.Text = cfg.UploadEndpoint;
            imageLocationTextBoxEx.Text = cfg.ImageLocation;
            hudScaleNumericUpDown.Value = Math.Min(Math.Max(25, cfg.HUDScale), 150);
            minimizeToTrayCheckBox.Checked = cfg.MinimizeToTray;
        }

        private void SaveConfigToFile()
        {
            ProgramConfig cfg = new ProgramConfig()
            {
                SecretKey = secretKeyTextBoxEx.Text,
                UploadEndpoint = uploadEndpointTextBoxEx.Text,
                ImageLocation = imageLocationTextBoxEx.Text,
                HUDScale = (int)hudScaleNumericUpDown.Value,
                MinimizeToTray = minimizeToTrayCheckBox.Checked,
            };

            MiscUtil.SaveConfig(cfg);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveConfigToFile();
            Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            LoadConfigFromFile();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFileLocationButton_Click(object sender, EventArgs e)
        {
            MiscUtil.OpenConfigLocation();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var cfg = MiscUtil.GetConfig();
                MiscUtil.VerifyConfig(cfg);
                Program.config = cfg;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "FortniteOverlay", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }

    // https://stackoverflow.com/a/39420512
    public class TextBoxEx : TextBox
    {
        const int WM_NCPAINT = 0x85;
        const uint RDW_INVALIDATE = 0x1;
        const uint RDW_IUPDATENOW = 0x100;
        const uint RDW_FRAME = 0x400;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);
        Color borderColor = Color.Blue;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero,
                    RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT && BorderColor != Color.Transparent &&
                BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
            {
                var hdc = GetWindowDC(this.Handle);
                using (var g = Graphics.FromHdcInternal(hdc))
                using (var p = new Pen(BorderColor))
                {
                    g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
                }
                ReleaseDC(this.Handle, hdc);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero,
                   RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        }
    }
}
