using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static bool IsMapVisible(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return IsMapVisible(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static bool IsMapVisible(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            if (screenshot == null) { return false; }
            Color pureWhite = Color.FromArgb(255, 255, 255);
            var scaledPos = ScalePositions(positions, hudScale);
            var pix = screenshot.GetPixel(scaledPos.Map[0], scaledPos.Map[1]);
            if (pix != pureWhite) { return false; }
            return true;
        }

        public static Bitmap MarkStaleImage(Bitmap bmp)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                int width = bmp.Width;
                int height = bmp.Height;

                Image outdated = null;
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FortniteOverlay.outdated.png"))
                {
                    outdated = Image.FromStream(stream);
                }

                Rectangle rect = new Rectangle(0, 0, width, height);
                using (Brush darken = new SolidBrush(Color.FromArgb(128, Color.Black)))
                {
                    g.FillRectangle(darken, rect);
                }
                g.DrawImage(outdated, new Rectangle(width / 2 - height / 2, height - (int)(height * 0.95), (int)(height * 0.90), (int)(height * 0.90)));
            }
            return bmp;
        }

        public static Bitmap RenderGear(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return RenderGear(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static Bitmap RenderGear(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color fadedWhite = Color.FromArgb(127, 127, 127);

            var scaledPos = ScalePositions(positions, hudScale);

            int slotSelected = 0;
            for (int i = 0; i < scaledPos.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(scaledPos.Slots[i][0], scaledPos.Slots[i][1]);
                if (pix == pureWhite || pix == fadedWhite)
                {
                    slotSelected = i + 1;
                }
            }

            Bitmap bitmap = new Bitmap((scaledPos.SlotSize * 6), scaledPos.SlotSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? scaledPos.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * scaledPos.SlotSize, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.Slots[i][0], scaledPos.Slots[i][1] + ofs, scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
                }

                // keys
                Color yellowish = Color.FromArgb(244, 219, 93);
                var pix = screenshot.GetPixel(scaledPos.CrownPos[0], scaledPos.CrownPos[1]);
                bool crownVisible = yellowish.GetHue() - 10 < pix.GetHue() && yellowish.GetHue() + 10 > pix.GetHue();
                if (crownVisible) { g.DrawImage(screenshot, new Rectangle(scaledPos.SlotSize * 5, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.KeyPosCrown[0], scaledPos.KeyPosCrown[1], scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel); }
                else              { g.DrawImage(screenshot, new Rectangle(scaledPos.SlotSize * 5, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.KeyPos[0],      scaledPos.KeyPos[1],      scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel); }
            }

            // Resize to save space
            bitmap = new Bitmap(bitmap, new Size(312, 52));

            return bitmap;
        }

        public static Bitmap RenderGearDebug(Bitmap screenshot, List<PixelPositions> positions, int hudScale)
        {
            return RenderGearDebug(screenshot, MatchingPosition(screenshot, positions), hudScale);
        }

        public static Bitmap RenderGearDebug(Bitmap screenshot, PixelPositions positions, int hudScale)
        {
            Bitmap bitmap = new Bitmap(screenshot.Width, screenshot.Height);
            Pen pen = new Pen(Color.Red, 1);

            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color fadedWhite = Color.FromArgb(127, 127, 127);

            var scaledPos = ScalePositions(positions, hudScale);

            int slotSelected = 0;
            for (int i = 0; i < scaledPos.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(scaledPos.Slots[i][0], scaledPos.Slots[i][1]);
                if (pix == pureWhite || pix == fadedWhite)
                {
                    slotSelected = i + 1;
                }
            }

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? scaledPos.SelectedSlotOffset : 0;
                    g.DrawRectangle(pen, new Rectangle(scaledPos.Slots[i][0] - 1, scaledPos.Slots[i][1] - 1 + ofs, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));
                }

                // keys
                Color yellowish = Color.FromArgb(244, 219, 93);
                var pix = screenshot.GetPixel(scaledPos.CrownPos[0], scaledPos.CrownPos[1]);

                bool crownVisible = yellowish.GetHue() - 10 < pix.GetHue() && yellowish.GetHue() + 10 > pix.GetHue();
                Program.form.Log((crownVisible ? "CROWN!" : "") + $"Crown position hue: {pix.GetHue()}");
                if (crownVisible) { g.DrawRectangle(pen, new Rectangle(scaledPos.KeyPosCrown[0] - 1, scaledPos.KeyPosCrown[1] - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1)); }
                else              { g.DrawRectangle(pen, new Rectangle(scaledPos.KeyPos[0] - 1,      scaledPos.KeyPos[1] - 1,      scaledPos.SlotSize + 1, scaledPos.SlotSize + 1)); }

                // Draw points of interest
                // Map
                g.DrawEllipse(pen, scaledPos.Map[0] - 1, scaledPos.Map[1] - 1, 2, 2);
                // Crown
                g.DrawEllipse(pen, scaledPos.CrownPos[0] - 1, scaledPos.CrownPos[1] - 1, 2, 2);
            }

            return bitmap;
        }

        public static Bitmap TakeScreenshot()
        {
            Rectangle bounds = MiscUtil.GetWindowPosition(Program.fortniteProcess);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }
            return bitmap;
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
                KeyPos = new int[2] {
                    (int)((double)reference.KeyPos[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.KeyPos[1] / (double)reference.Resolution[1] * height),
                },
                KeyPosCrown = new int[2]
                {
                    (int)((double)reference.KeyPosCrown[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.KeyPosCrown[1] / (double)reference.Resolution[1] * height),
                },
                CrownPos = new int[2]
                {
                    (int)((double)reference.CrownPos[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.CrownPos[1] / (double)reference.Resolution[1] * height),
                }
            };
        }

        public static int[] ScaleFromTopLeft(int x, int y, int width, int height, int scale)
        {
            int newX = Convert.ToInt32(Math.Floor(x * ((double)scale / 100)));
            int newY = Convert.ToInt32(Math.Floor(y * ((double)scale / 100)));
            return new int[] { newX, newY };
        }

        public static int[] ScaleFromBottomLeft(int x, int y, int width, int height, int scale)
        {
            int newX = Convert.ToInt32(Math.Floor(x * ((double)scale / 100)));
            int newY = Convert.ToInt32(Math.Floor(height - (height - y) * ((double)scale / 100)));
            return new int[] { newX, newY };
        }

        public static int[] ScaleFromTopRight(int x, int y, int width, int height, int scale)
        {
            int newX = Convert.ToInt32(Math.Floor(width - (width - x) * ((double)scale / 100)));
            int newY = Convert.ToInt32(Math.Floor(y * ((double)scale / 100)));
            return new int[] { newX, newY };
        }

        public static int[] ScaleFromBottomRight(int x, int y, int width, int height, int scale)
        {
            Console.WriteLine("ScaleFromBottomRight - X: " + (width - (width - x) * ((double)scale / 100)) + ", Y: " + (height - (height - y) * ((double)scale / 100)));
            int newX = Convert.ToInt32(Math.Floor(width - (width - x) * ((double)scale / 100)));
            int newY = Convert.ToInt32(Math.Floor(height - (height - y) * ((double)scale / 100)));
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
                    ScaleFromBottomRight(pos.Slots[0][0], pos.Slots[0][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleFromBottomRight(pos.Slots[1][0], pos.Slots[1][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleFromBottomRight(pos.Slots[2][0], pos.Slots[2][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleFromBottomRight(pos.Slots[3][0], pos.Slots[3][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleFromBottomRight(pos.Slots[4][0], pos.Slots[4][1], pos.Resolution[0], pos.Resolution[1], scale)
                },
                Map = ScaleFromTopRight(pos.Map[0], pos.Map[1], pos.Resolution[0], pos.Resolution[1], scale),
                KeyPos = ScaleFromBottomRight(pos.KeyPos[0], pos.KeyPos[1], pos.Resolution[0], pos.Resolution[1], scale),
                KeyPosCrown = ScaleFromBottomRight(pos.KeyPosCrown[0], pos.KeyPosCrown[1], pos.Resolution[0], pos.Resolution[1], scale),
                CrownPos = ScaleFromBottomRight(pos.CrownPos[0], pos.CrownPos[1], pos.Resolution[0], pos.Resolution[1], scale),
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
            public int[] KeyPos { get; set; }
            public int[] KeyPosCrown { get; set; }
            public int[] CrownPos { get; set; }
        }

        public class PixelPositions_1440p : PixelPositions
        {
            public PixelPositions_1440p()
            {
                Resolution         = new int[2] { 2560, 1440 };
                SelectedSlotOffset = -13;
                SlotSize           = 104;
                Slots              = new int[5][]
                {
                    new int[] { 2009, 1227 },
                    new int[] { 2118, 1227 },
                    new int[] { 2227, 1227 },
                    new int[] { 2336, 1227 },
                    new int[] { 2444, 1227 }
                };
                Map                = new int[2] { 2512, 47   };
                KeyPos             = new int[2] { 2456, 927  };
                KeyPosCrown        = new int[2] { 2350, 944  };
                CrownPos           = new int[2] { 2490, 1000 };
            }
        }

        public class PixelPositions_1080p : PixelPositions
        {
            public PixelPositions_1080p()
            {
                Resolution         = new int[2] { 1920, 1080 };
                SelectedSlotOffset = -11;
                SlotSize           = 78;
                Slots              = new int[5][]
                {
                    new int[] { 1507, 920 },
                    new int[] { 1589, 920 },
                    new int[] { 1670, 920 },
                    new int[] { 1752, 920 },
                    new int[] { 1833, 920 }
                };
                Map                = new int[2] { 1884, 35  };
                KeyPos             = new int[2] { 1842, 694 };
                KeyPosCrown        = new int[2] { 1762, 707 };
                CrownPos           = new int[2] { 1866, 750 };
            }
        }
    }
}
