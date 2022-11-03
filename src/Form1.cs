using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortniteOverlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void showConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;
            var textBoxHeight = consoleLogTextBox.Size.Height;
            consoleLogTextBox.Visible = visible;

            Size = new Size(Size.Width, Size.Height + (visible ? 200 : textBoxHeight*-1));
        }
    }
}
