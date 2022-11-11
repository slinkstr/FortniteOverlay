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
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }

        public void SetDebugOverlay(Bitmap bmp)
        {
            SetControlProperty(debugPictureBox, "Image", bmp);
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

        public void PositionSquadGear(int index, int x, int y, int width, int height)
        {
            switch (index)
            {
                case 0:
                    SetControlProperty(squadmateGearPictureBox1, "Location", new Point(x, y));
                    SetControlProperty(squadmateGearPictureBox1, "Size", new Size(width, height));
                    break;
                case 1:
                    SetControlProperty(squadmateGearPictureBox2, "Location", new Point(x, y));
                    SetControlProperty(squadmateGearPictureBox2, "Size", new Size(width, height));
                    break;
                case 2:
                    SetControlProperty(squadmateGearPictureBox3, "Location", new Point(x, y));
                    SetControlProperty(squadmateGearPictureBox3, "Size", new Size(width, height));
                    break;
                default:
                    throw new Exception("Invalid index in SetSquadGear");
            }
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
    }
}
