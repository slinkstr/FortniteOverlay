using System;
using System.Drawing;
using System.Windows.Forms;

namespace FortniteOverlay
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }

        // ****************************************************************************************************
        // HELPER METHODS
        // ****************************************************************************************************
        public Image GetDebugOverlay()
        {
            return debugPictureBox.Image;
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

        // ****************************************************************************************************
        // CONTROL EVENT HANDLERS
        // ****************************************************************************************************
        private void OverlayForm_Resize(object sender, EventArgs e)
        {
            var form = (OverlayForm)sender;
            var width = form.Size.Width;
            var height = form.Size.Height;

            var imageSize = new Size((int)(width * 0.125), (int)(height * 0.05555555555));
            var firstImagePos = new Point((int)(width * 0.16796875), (int)(height * 0.08333333333));

            squadmateGearPictureBox1.Size = imageSize;
            squadmateGearPictureBox1.Location = firstImagePos;
            squadmateGearPictureBox2.Size = imageSize;
            squadmateGearPictureBox2.Location = Point.Add(firstImagePos, new Size(0, imageSize.Height));
            squadmateGearPictureBox3.Size = imageSize;
            squadmateGearPictureBox3.Location = Point.Add(firstImagePos, new Size(0, imageSize.Height * 2));
        }
    }
}
