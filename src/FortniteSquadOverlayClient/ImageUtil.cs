using System.Drawing;
using System.Windows.Forms;

namespace FortniteSquadOverlayClient
{
    internal class ImageUtil
    {
        public static void TakeScreenshot(ref Bitmap startingBitmap, Rectangle bounds)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }
            if (startingBitmap == null || startingBitmap.Width != bounds.Width || startingBitmap.Height != bounds.Height)
            {
                startingBitmap = new Bitmap(bounds.Width, bounds.Height);
            }

            using (Graphics g = Graphics.FromImage(startingBitmap))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }
        }
    }
}
