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
        private static PixelPositions MatchingPosition(Bitmap screenshot, List<PixelPositions> positions)
        {
            var dsrpos = positions.FirstOrDefault(x => x.Resolution[0] == screenshot.Width && x.Resolution[1] == screenshot.Height);
            if (dsrpos == null) { dsrpos = InterpolateResolution(positions.First(), screenshot.Width, screenshot.Height); }
            return dsrpos;
        }

        public static bool IsInGame(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return IsInGame(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static bool IsInGame(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            if (screenshot == null) { return false; }
            var scaledPos = ScalePositions(positions, hudScale);
            var pix = screenshot.GetPixel(scaledPos.ShieldIndicator[0], scaledPos.ShieldIndicator[1]);
            if (pix.R > 190 && pix.G > 190 && pix.B > 190)
            {
                return true;
            }
            else
            {
                Program.form.LogDebug($"Shield indicator not detected (vals: {pix.R}, {pix.G}, {pix.B})");
                return false;
            }
        }

        public static bool IsGoldBarsVisible(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return IsGoldBarsVisible(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static bool IsGoldBarsVisible(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            if (screenshot == null) { return false; }
            var scaledPos = ScalePositions(positions, hudScale);
            var pixelBrightness = screenshot.GetPixel(scaledPos.GoldBars[0][0], scaledPos.GoldBars[0][1]).GetBrightness();
            if(pixelBrightness < 0.58 || pixelBrightness > 0.75)
            {
                Program.form.LogDebug($"Gold bar not detected (Check #1, val: {pixelBrightness})");
                return false;
            }
            pixelBrightness = screenshot.GetPixel(scaledPos.GoldBars[1][0], scaledPos.GoldBars[1][1]).GetBrightness();
            if(pixelBrightness < 0.15 || pixelBrightness > 0.32)
            {
                Program.form.LogDebug($"Gold bar not detected (Check #2, val: {pixelBrightness})");
                return false;
            }
            return true;
        }

        public static bool IsSpectatingTextVisible(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return IsSpectatingTextVisible(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static bool IsSpectatingTextVisible(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            if (screenshot == null) { return false; }
            var scaledPos = ScalePositions(positions, hudScale);

            var pix = screenshot.GetPixel(scaledPos.SpectatingText[0][0], scaledPos.SpectatingText[0][1]);
            if (pix.R != 255 || pix.G != 255 || pix.B != 255)
            {
                Program.form.LogDebug("Spectating text not detected (Check 1)");
                return false;
            }

            pix = screenshot.GetPixel(scaledPos.SpectatingText[1][0], scaledPos.SpectatingText[1][1]);
            if (pix.R != 255 || pix.G != 255 || pix.B != 255)
            {
                Program.form.LogDebug("Spectating text not detected (Check 2)");
                return false;
            }

            Program.form.LogDebug("Detected spectating text.");
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
                var leftEdge     = (width  / 2) - (smallestSide / 2);
                var topEdge      = (height / 2) - (smallestSide / 2);

                g.DrawImage(outdated, new Rectangle(leftEdge     + (int)(smallestSide * 0.05),
                                                    topEdge      + (int)(smallestSide * 0.05),
                                                    smallestSide - (int)(smallestSide * 0.10),
                                                    smallestSide - (int)(smallestSide * 0.10)));
            }
            return bmp;
        }

        public static Bitmap RenderGear(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return RenderGear(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static Bitmap RenderGear(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            var scaledPos = ScalePositions(positions, hudScale);

            int slotSelected = 0;
            for (int i = 0; i < scaledPos.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(scaledPos.Slots[i][0], scaledPos.Slots[i][1]);

                //if (pix.R < 240 || pix.G < 240 || pix.B < 240)
                if (pix.R != 255 || pix.G != 255 || pix.B != 255)
                {
                    //if (!(pix.R > 120 && pix.R < 135) || !(pix.G > 120 && pix.G < 135) || !(pix.B > 120 && pix.B < 135))
                    if (pix.R != 127 || pix.G != 127 || pix.B != 127)
                    {
                        continue;
                    }
                }

                slotSelected = i + 1;
                break;
            }

            Program.form.LogDebug("Selected slot: " + slotSelected);

            Bitmap bitmap = new Bitmap((scaledPos.SlotSize * 6), scaledPos.SlotSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? scaledPos.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * scaledPos.SlotSize, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.Slots[i][0], scaledPos.Slots[i][1] + ofs, scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
                }

                // keys
                g.DrawImage(screenshot, new Rectangle(scaledPos.SlotSize * 5, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.Keys[0], scaledPos.Keys[1], scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
            }

            // Resize to save space
            bitmap = new Bitmap(bitmap, new Size(312, 52));

            return bitmap;
        }

        public static void RenderGearDebug(ref Bitmap blankScreenshot, List<PixelPositions> positions, int hudScale)
        {
            RenderGearDebug(ref blankScreenshot, MatchingPosition(blankScreenshot, positions), hudScale);
        }

        public static void RenderGearDebug(ref Bitmap blankScreenshot, PixelPositions positions, int hudScale)
        {
            Pen pen = new Pen(Color.Red, 1);

            var scaledPos = ScalePositions(positions, hudScale);

            using (Graphics g = Graphics.FromImage(blankScreenshot))
            {
                // Draw slot outlines
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    g.DrawRectangle(pen, new Rectangle(scaledPos.Slots[i][0] - 1, scaledPos.Slots[i][1] - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));
                }

                // Keys/crown
                g.DrawRectangle(pen, new Rectangle(scaledPos.Keys[0] - 1, scaledPos.Keys[1] - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));

                // Other points
                g.DrawEllipse(pen, scaledPos.Map[0] - 1, scaledPos.Map[1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.GoldBars[0][0] - 1, scaledPos.GoldBars[0][1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.GoldBars[1][0] - 1, scaledPos.GoldBars[1][1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.SpectatingText[0][0] - 1, scaledPos.SpectatingText[0][1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.SpectatingText[1][0] - 1, scaledPos.SpectatingText[1][1] - 1, 2, 2);
            }
        }

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

        public static PixelPositions InterpolateResolution(PixelPositions reference, int width, int height)
        {
            return new PixelPositions
            {
                Resolution = new int[2] { width, height },
                SelectedSlotOffset = (int)((double)reference.SelectedSlotOffset / (double)reference.Resolution[1] * height),
                SlotSize = (int)((double)reference.SlotSize / (double)reference.Resolution[0] * width),
                Slots = new int[5][]
                {
                    new int[] { (int)((double)reference.Slots[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[1][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[2][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[2][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[3][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[3][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Slots[4][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[4][1] / (double)reference.Resolution[1] * height) },
                },

                Map = new int[2] {
                    (int)((double)reference.Map[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.Map[1] / (double)reference.Resolution[1] * height),
                },
                GoldBars = new int[2][]
                {
                    new int[] { (int)((double)reference.GoldBars[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.GoldBars[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.GoldBars[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.GoldBars[1][1] / (double)reference.Resolution[1] * height) },
                },
                SpectatingText = new int[2][]
                {
                    new int[] { (int)((double)reference.SpectatingText[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.SpectatingText[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[1][1] / (double)reference.Resolution[1] * height) },
                },

                Keys = new int[2] {
                    (int)((double)reference.Keys[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.Keys[1] / (double)reference.Resolution[1] * height),
                }
            };
        }

        public static int[] ScaleAboutTopLeft(int x, int y, int width, int height, int scale)
        {
            return ScaleAboutPoint(x, y, 0, 0, width, height, scale);
        }

        public static int[] ScaleAboutBottomLeft(int x, int y, int width, int height, int scale)
        {
            return ScaleAboutPoint(x, y, 0, height, width, height, scale);
        }

        public static int[] ScaleAboutTopRight(int x, int y, int width, int height, int scale)
        {
            return ScaleAboutPoint(x, y, width, 0, width, height, scale);
        }

        public static int[] ScaleAboutBottomRight(int x, int y, int width, int height, int scale)
        {
            return ScaleAboutPoint(x, y, width, height, width, height, scale);
        }

        public static int[] ScaleAboutTopMiddle(int x, int y, int width, int height, int scale)
        {
            return ScaleAboutPoint(x, y, width / 2, 0, width, height, scale);
        }

        public static int[] ScaleAboutPoint(int x, int y, int scaleX, int scaleY, int width, int height, int scale)
        {
            int diffX = x - scaleX;
            int diffY = y - scaleY;
            double diffXScaled = diffX * ((double)scale / 100);
            double diffYScaled = diffY * ((double)scale / 100);
            int newX = x - (diffX - Convert.ToInt32(Math.Floor(diffXScaled)));
            int newY = y - (diffY - Convert.ToInt32(Math.Floor(diffYScaled)));
            return new int[] { newX, newY };
        }

        public static PixelPositions ScalePositions(PixelPositions pos, int scale)
        {
            if (scale == 100) { return pos; }
            if (scale < 25)   { throw new Exception("Invalid scale '" + scale + "', minimum is 25."); }
            if (scale > 125)  { throw new Exception("Invalid scale '" + scale + "', maximum is 125."); }

            var newPos = new PixelPositions
            {
                Resolution = pos.Resolution,
                SelectedSlotOffset = Convert.ToInt32(pos.SelectedSlotOffset * ((double)scale / 100)),
                SlotSize = Convert.ToInt32(pos.SlotSize * ((double)scale / 100)),
                Slots = new int[5][]
                {
                    ScaleAboutBottomRight(pos.Slots[0][0], pos.Slots[0][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutBottomRight(pos.Slots[1][0], pos.Slots[1][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutBottomRight(pos.Slots[2][0], pos.Slots[2][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutBottomRight(pos.Slots[3][0], pos.Slots[3][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutBottomRight(pos.Slots[4][0], pos.Slots[4][1], pos.Resolution[0], pos.Resolution[1], scale)
                },
                Map = ScaleAboutTopRight(pos.Map[0], pos.Map[1], pos.Resolution[0], pos.Resolution[1], scale),
                GoldBars = new int[2][]
                {
                    ScaleAboutBottomRight(pos.GoldBars[0][0], pos.GoldBars[0][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutBottomRight(pos.GoldBars[1][0], pos.GoldBars[1][1], pos.Resolution[0], pos.Resolution[1], scale),
                },
                SpectatingText = new int[2][]
                {
                    ScaleAboutTopMiddle(pos.SpectatingText[0][0], pos.SpectatingText[0][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutTopMiddle(pos.SpectatingText[1][0], pos.SpectatingText[1][1], pos.Resolution[0], pos.Resolution[1], scale),
                },

                Keys = ScaleAboutBottomRight(pos.Keys[0], pos.Keys[1], pos.Resolution[0], pos.Resolution[1], scale),
            };

            return newPos;
        }

        public static List<PixelPositions> KnownPositions()
        {
            List<PixelPositions> positions = new List<PixelPositions>()
            {
                new PixelPositions_1440p(),
                new PixelPositions_1080p(),
            };
            return positions;
        }

        public class PixelPositions
        {
            public int[] Resolution { get; set; }
            public int SelectedSlotOffset { get; set; }
            public int SlotSize { get; set; }
            public int[][] Slots { get; set; }

            public int[] Map { get; set; }
            public int[][] GoldBars { get; set; }
            public int[] ShieldIndicator { get; set; }
            public int[][] SpectatingText { get; set; }

            public int[] Keys { get; set; }
        }

        public class PixelPositions_1440p : PixelPositions
        {
            public PixelPositions_1440p()
            {
                Resolution            = new int[2] { 2560, 1440 };
                SelectedSlotOffset    = -13;
                SlotSize              = 104;
                Slots                 = new int[5][]
                {
                    new int[] { 1994, 1227 },
                    new int[] { 2103, 1227 },
                    new int[] { 2212, 1227 },
                    new int[] { 2321, 1227 },
                    new int[] { 2429, 1227 }
                };

                Map                   = new int[2] { 2512, 47 };
                GoldBars              = new int[2][]
                {
                    new int[] { 2518, 1149 },
                    new int[] { 2512, 1157 },
                };
                ShieldIndicator       = new int[2] { 45, 1267 };
                SpectatingText        = new int[2][]
                {
                    new int[] { 1181, 26 },
                    new int[] { 1200, 26 },
                };

                Keys                  = new int[2] { 2456, 1021  };
            }
        }

        public class PixelPositions_1080p : PixelPositions
        {
            public PixelPositions_1080p()
            {
                Resolution            = new int[2] { 1920, 1080 };
                SelectedSlotOffset    = -11;
                SlotSize              = 78;
                Slots                 = new int[5][]
                {
                    new int[] { 1496, 920 },
                    new int[] { 1577, 920 },
                    new int[] { 1659, 920 },
                    new int[] { 1741, 920 },
                    new int[] { 1822, 920 }
                };

                Map                   = new int[2] { 1884, 35  };
                GoldBars              = new int[2][]
                {
                    new int[] { 1889, 866 },
                    new int[] { 1885, 871 },
                };
                ShieldIndicator       = new int[2] { 33, 950 };
                SpectatingText        = new int[2][]
                {
                    new int[] { 885, 20 },
                    new int[] { 900, 20 },
                };

                Keys                  = new int[2] { 1842, 765 };
            }
        }
    }
}
