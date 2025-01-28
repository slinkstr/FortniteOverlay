using System;
using System.Drawing;
using System.Linq;
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

        public void SetSelfName(string name)
        {
            SetControlProperty(previewLabel, "Text", name);
            SetControlProperty(previewLabel, "ContextMenuStrip", string.IsNullOrWhiteSpace(name) ? null : contextMenuStrip1);

        }

        public void SetSelfGear(Bitmap bmp)
        {
            SetControlProperty(previewPictureBox, "Image", bmp);
            SetControlProperty(previewPictureBox, "ContextMenuStrip", bmp == null ? null : contextMenuStrip1);
        }

        public void SetSquadGear(int index, Bitmap bmp)
        {
            switch (index)
            {
                case 0:
                    SetControlProperty(squadmate1GearPictureBox, "Image", bmp);
                    SetControlProperty(squadmate1GearPictureBox, "ContextMenuStrip", bmp == null ? null : contextMenuStrip1);
                    break;
                case 1:
                    SetControlProperty(squadmate2GearPictureBox, "Image", bmp);
                    SetControlProperty(squadmate2GearPictureBox, "ContextMenuStrip", bmp == null ? null : contextMenuStrip1);
                    break;
                case 2:
                    SetControlProperty(squadmate3GearPictureBox, "Image", bmp);
                    SetControlProperty(squadmate3GearPictureBox, "ContextMenuStrip", bmp == null ? null : contextMenuStrip1);
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
                    SetControlProperty(squadmate1NameLabel, "ContextMenuStrip", string.IsNullOrWhiteSpace(name) ? null : contextMenuStrip1);
                    break;
                case 1:
                    SetControlProperty(squadmate2NameLabel, "Text", name);
                    SetControlProperty(squadmate2NameLabel, "ContextMenuStrip", string.IsNullOrWhiteSpace(name) ? null : contextMenuStrip1);
                    break;
                case 2:
                    SetControlProperty(squadmate3NameLabel, "Text", name);
                    SetControlProperty(squadmate3NameLabel, "ContextMenuStrip", string.IsNullOrWhiteSpace(name) ? null : contextMenuStrip1);
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadName");
            }
        }

        public void SetUpdateNotice(string text, string url = "")
        {
            SetControlProperty(updateNoticeLinkLabel, "Text", text);
            SetControlProperty(updateNoticeLinkLabel, "LinkArea", string.IsNullOrWhiteSpace(url) ? new LinkArea(0, 0) : new LinkArea(0, updateNoticeLinkLabel.Text.Length));
            SetControlProperty(updateNoticeLinkLabel, "Tag", url);
            SetControlProperty(updateNoticeLinkLabel, "LinkVisited", false);
        }

        public void ShowHideConsole(bool showConsole)
        {
            if(!Visible)
            {
                return;
            }
            if (consoleLogTextBox.Visible == showConsole)
            {
                return;
            }

            int tableRow = mainTableLayoutPanel.GetRow(consoleLogTextBox);
            consoleLogTextBox.Visible = showConsole;
            mainTableLayoutPanel.RowStyles[tableRow].Height = showConsole ? consoleHeight : 0;
            Size = new System.Drawing.Size(Size.Width, Size.Height + (showConsole ? consoleHeight : consoleHeight * -1));
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
            if (Program.CurrentSquad.Count < max + 1) { return; }
            var temp = Program.CurrentSquad[indexA];
            Program.CurrentSquad[indexA] = Program.CurrentSquad[indexB];
            Program.CurrentSquad[indexB] = temp;
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
        private void showConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;

            mainTableLayoutPanel.RowStyles[1].Height = (visible ? consoleHeight : 0);
            Size = new System.Drawing.Size(Size.Width, Size.Height + (visible ? consoleHeight : consoleHeight * -1));
        }

        private void squadmate1DownButton_Click(object sender, EventArgs e)
        {
            SwapSquad(0, 1);
        }
        private void squadmate2DownButton_Click(object sender, EventArgs e)
        {
            SwapSquad(1, 2);
        }
        private void squadmate2UpButton_Click(object sender, EventArgs e)
        {
            SwapSquad(0, 1);
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

        private FortnitePlayer FortniterAtUiIndex(int uiIndex)
        {
            if (uiIndex == 0)
            {
                return Program.LocalPlayer;
            }

            Label control = null;
            switch (uiIndex)
            {
                case 1:
                    control = squadmate1NameLabel;
                    break;
                case 2:
                    control = squadmate2NameLabel;
                    break;
                case 3:
                    control = squadmate3NameLabel;
                    break;
                default:
                    throw new Exception("Invalid index given to CopyName");
            }
            var fortniter = Program.CurrentSquad.FirstOrDefault(x => x.Name == control.Text);
            return fortniter;
        }

        private void CopyName(object sender, EventArgs e, int index)
        {
            var fortniter = FortniterAtUiIndex(index);
            if (fortniter == null)
            {
                return;
            }
            Clipboard.SetText(fortniter.Name);
        }

        private void CopyUID(object sender, EventArgs e, int index)
        {
            var fortniter = FortniterAtUiIndex(index);
            if (fortniter == null)
            {
                return;
            }
            Clipboard.SetText(fortniter.UserId);
        }

        private void CopyImage(object sender, EventArgs e, int index)
        {
            var fortniter = FortniterAtUiIndex(index);
            if (fortniter == null)
            {
                return;
            }
            Clipboard.SetDataObject(fortniter.GearImage);
        }

        private void OpenTracker(object sender, EventArgs e, int index)
        {
            var fortniter = FortniterAtUiIndex(index);
            if (fortniter == null)
            {
                return;
            }
            System.Diagnostics.Process.Start("https://fortnitetracker.com/profile/search?q=" + fortniter.UserId);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ToolStripMenuItem copyName    = new ToolStripMenuItem("Copy &name");
            ToolStripMenuItem copyUid     = new ToolStripMenuItem("Copy &UID");
            ToolStripMenuItem copyImage   = new ToolStripMenuItem("Copy &image");
            ToolStripMenuItem openTracker = new ToolStripMenuItem("Open in &FortniteTracker");

            var src = contextMenuStrip1.SourceControl;
            var index = src.Name.StartsWith("preview") ? 0 : int.Parse(src.Name.Substring(9, 1));

            copyName.Click += (snd, evtargs) => CopyName(snd, evtargs, index);
            copyUid.Click += (snd, evtargs) => CopyUID(snd, evtargs, index);
            copyImage.Click += (snd, evtargs) => CopyImage(snd, evtargs, index);
            openTracker.Click += (snd, evtargs) => OpenTracker(snd, evtargs, index);

            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.Add(copyName);
            contextMenuStrip1.Items.Add(copyUid);
            if (src is PictureBox && ((PictureBox)src).Image != null)
            {
                contextMenuStrip1.Items.Add(copyImage);
            }
            contextMenuStrip1.Items.Add(openTracker);
        }
    }

    public class ProgramOptions
    {
        public bool DebugOverlay;
    }
}
