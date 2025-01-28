using FortniteOverlay.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FortniteOverlay.Util
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
