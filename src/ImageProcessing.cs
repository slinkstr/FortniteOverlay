using System.Drawing;
using FortniteOverlay.Properties;

namespace FortniteOverlay
{
    internal class ImageProcessing
    {
        public static bool IsPlaying(Bitmap screenshot, PixelPositions positions)
        {
            if (screenshot == null) { return false; }
            var pix = screenshot.GetPixel(positions.ShieldIcon.X, positions.ShieldIcon.Y);
            if (pix.R < 190 || pix.G < 190 || pix.B < 190)
            {
                // debug: Shield indicator not detected (vals: {pix.R},{pix.G},{pix.B})
                return false;
            }

            return true;
        }

        public static bool IsSpectating(Bitmap screenshot, PixelPositions positions)
        {
            if (screenshot == null) { return false; };

            var pix = screenshot.GetPixel(positions.SpectatingText[0].X, positions.SpectatingText[0].Y);
            if (pix.R < 255 || pix.G < 255 || pix.B < 255)
            {
                return false;
            }

            pix = screenshot.GetPixel(positions.SpectatingText[1].X, positions.SpectatingText[1].Y);
            if (pix.R < 255 || pix.G < 255 || pix.B < 255)
            {
                return false;
            }

            // debug: Detected spectating text.
            return true;
        }

        public static Bitmap MarkStaleImage(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                int width = bmp.Width;
                int height = bmp.Height;

                Image outdated = Resources.outdated;

                Rectangle rect = new Rectangle(0, 0, width, height);
                using (Brush darken = new SolidBrush(Color.FromArgb(96, Color.Black)))
                {
                    g.FillRectangle(darken, rect);
                }

                var smallestSide = (height < width) ? height : width;
                var leftEdge = (width / 2) - (smallestSide / 2);
                var topEdge = (height / 2) - (smallestSide / 2);

                g.DrawImage(outdated, new Rectangle(leftEdge + (int)(smallestSide * 0.05),
                                                    topEdge + (int)(smallestSide * 0.05),
                                                    smallestSide - (int)(smallestSide * 0.10),
                                                    smallestSide - (int)(smallestSide * 0.10)));
            }
            return bmp;
        }

        public static Bitmap CropGear(Bitmap bmp, PixelPositions positions)
        {
            int slotSelected = -1;
            for (int i = 0; i < positions.Slots.Length; i++)
            {
                var pix = bmp.GetPixel(positions.Slots[i].X, positions.Slots[i].Y);

                if (pix.R < 255 || pix.G < 255 || pix.B < 255)
                {
                    // FIXME: wtf is this checking for?
                    if (pix.R != 127 || pix.G != 127 || pix.B != 127)
                    {
                        continue;
                    }
                }

                slotSelected = i;
                break;
            }

            // debug: Selected slot: {slotSelected}

            Bitmap cropped = new Bitmap(positions.SlotSize.Width * 5, positions.SlotSize.Height);
            using (Graphics g = Graphics.FromImage(cropped))
            {
                for (int i = 0; i < positions.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i) ? positions.SelectedSlotOffset : 0;
                    g.DrawImage(bmp,
                                new Rectangle(i * positions.SlotSize.Width, 0, positions.SlotSize.Width, positions.SlotSize.Height),
                                new Rectangle(positions.Slots[i].X, positions.Slots[i].Y + ofs, positions.SlotSize.Width, positions.SlotSize.Height),
                                GraphicsUnit.Pixel);
                }

                // keys (defunct)
                //g.DrawImage(bmp,
                //            new Rectangle(positions.SlotSize.Width * 5, 0, positions.SlotSize.Width, positions.SlotSize.Height),
                //            new Rectangle(positions.Keys.X, positions.Keys.Y, positions.SlotSize.Width, positions.SlotSize.Height),
                //            GraphicsUnit.Pixel);
            }

            return cropped;
        }

        public static void RenderDebugMarkers(ref Bitmap blankScreenshot, PixelPositions positions)
        {
            Pen pen = new Pen(Color.Red, 1);

            using (Graphics g = Graphics.FromImage(blankScreenshot))
            {
                for (int i = 0; i < positions.Slots.Length; i++)
                {
                    g.DrawRectangle(pen, new Rectangle(positions.Slots[i].X - 1, positions.Slots[i].Y - 1, positions.SlotSize.Width + 1, positions.SlotSize.Height + 1));
                }

                // keys are defunct
                //g.DrawRectangle(pen, new Rectangle(positions.Keys.X - 1, positions.Keys.Y - 1, positions.SlotSize.Width + 1, positions.SlotSize.Height + 1));

                g.DrawEllipse(pen, positions.ShieldIcon.X - 1, positions.ShieldIcon.Y - 1, 2, 2);

                g.DrawEllipse(pen, positions.SpectatingText[0].X - 1, positions.SpectatingText[0].Y - 1, 2, 2);
                g.DrawEllipse(pen, positions.SpectatingText[1].X - 1, positions.SpectatingText[1].Y - 1, 2, 2);
            }
        }
    }
}
