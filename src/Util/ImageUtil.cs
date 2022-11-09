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

        public static bool IsMapVisible(Bitmap screenshot, List<PixelPositions> positions)
        {
            return IsMapVisible(screenshot, MatchingPosition(screenshot, positions).Map);
        }

        public static bool IsMapVisible(Bitmap screenshot, int[][] mapPos)
        {
            if (screenshot == null) { return false; }
            if (mapPos == null) { throw new Exception("'mapPos' given to IsMapVisible was null."); }

            Color pureWhite = Color.FromArgb(255, 255, 255);

            if (screenshot.GetPixel(mapPos[0][0], mapPos[0][1]) != pureWhite) { return false; }
            if (screenshot.GetPixel(mapPos[1][0], mapPos[1][1]) != pureWhite) { return false; }
            if (screenshot.GetPixel(mapPos[2][0], mapPos[2][1]) != pureWhite) { return false; }
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

        public static Bitmap RenderGear(Bitmap screenshot, List<PixelPositions> positions)
        {
            return RenderGear(screenshot, MatchingPosition(screenshot, positions));
        }

        public static Bitmap RenderGear(Bitmap screenshot, PixelPositions positions)
        {
            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color fadedWhite = Color.FromArgb(127, 127, 127);

            int slotSelected = 0;
            for (int i = 0; i < positions.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(positions.Slots[i][0], positions.Slots[i][1]);
                if (pix == pureWhite || pix == fadedWhite)
                {
                    slotSelected = i + 1;
                }
            }

            Bitmap bitmap = new Bitmap((positions.SlotSize * 6), positions.SlotSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < positions.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? positions.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * positions.SlotSize, 0, positions.SlotSize, positions.SlotSize), new Rectangle(positions.Slots[i][0], positions.Slots[i][1] + ofs, positions.SlotSize, positions.SlotSize), GraphicsUnit.Pixel);
                }

                // keys
                Color yellowish = Color.FromArgb(244, 219, 93);
                var pix = screenshot.GetPixel(positions.CrownPos[0], positions.CrownPos[1]);
                if (yellowish.GetHue() - 10 < pix.GetHue() && yellowish.GetHue() + 10 > pix.GetHue())
                {
                    g.DrawImage(screenshot, new Rectangle(positions.SlotSize * 5, 0, positions.SlotSize, positions.SlotSize), new Rectangle(positions.KeyPosCrown[0], positions.KeyPosCrown[1], positions.SlotSize, positions.SlotSize), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(screenshot, new Rectangle(positions.SlotSize * 5, 0, positions.SlotSize, positions.SlotSize), new Rectangle(positions.KeyPos[0], positions.KeyPos[1], positions.SlotSize, positions.SlotSize), GraphicsUnit.Pixel);
                }
            }

            return bitmap;
        }

        public static Bitmap RenderGearDebug(Bitmap screenshot, List<PixelPositions> positions)
        {
            return RenderGearDebug(screenshot, MatchingPosition(screenshot, positions));
        }

        public static Bitmap RenderGearDebug(Bitmap screenshot, PixelPositions positions)
        {
            Bitmap bitmap = new Bitmap(screenshot.Width, screenshot.Height);
            Pen pen = new Pen(Color.Red, 1);

            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color fadedWhite = Color.FromArgb(127, 127, 127);

            int slotSelected = 0;
            for (int i = 0; i < positions.Slots.Length; i++)
            {
                var pix = screenshot.GetPixel(positions.Slots[i][0], positions.Slots[i][1]);
                if (pix == pureWhite || pix == fadedWhite)
                {
                    slotSelected = i + 1;
                }
            }

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < positions.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? positions.SelectedSlotOffset : 0;
                    g.DrawRectangle(pen, new Rectangle(positions.Slots[i][0], positions.Slots[i][1] + ofs, positions.SlotSize, positions.SlotSize));
                }

                // keys
                Color yellowish = Color.FromArgb(244, 219, 93);
                var pix = screenshot.GetPixel(positions.CrownPos[0], positions.CrownPos[1]);
                g.DrawRectangle(pen, new Rectangle(positions.CrownPos[0], positions.CrownPos[1], 1, 1));
                Program.form.Log($"Crown position hue: {pix.GetHue()}, tolerance: {yellowish.GetHue() - 10} - {yellowish.GetHue() + 10}");
                if (yellowish.GetHue() - 10 < pix.GetHue() && yellowish.GetHue() + 10 > pix.GetHue())
                {
                    g.DrawRectangle(pen, new Rectangle(positions.KeyPosCrown[0], positions.KeyPosCrown[1], positions.SlotSize, positions.SlotSize));
                }
                else
                {
                    g.DrawRectangle(pen, new Rectangle(positions.KeyPos[0], positions.KeyPos[1], positions.SlotSize, positions.SlotSize));
                }

                // Draw points of interest
                // Map
                g.DrawRectangle(pen, new Rectangle(positions.Map[0][0], positions.Map[0][1], 1, 1));
                g.DrawRectangle(pen, new Rectangle(positions.Map[1][0], positions.Map[1][1], 1, 1));
                g.DrawRectangle(pen, new Rectangle(positions.Map[2][0], positions.Map[2][1], 1, 1));

                // Crown

            }

            return bitmap;
        }

        public static Bitmap TakeScreenshot()
        {
            Rectangle bounds = MiscUtil.GetWindowPosition(Program.fortniteProcess);
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                throw new Exception($"Error capturing screenshot. Width: {bounds.Width}, Height: {bounds.Height}, X: {bounds.X}, Y: {bounds.Y}.");
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
                Map = new int[3][]
                {
                    new int[] { (int)((double)reference.Map[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Map[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[1][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.Map[2][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Map[2][1] / (double)reference.Resolution[1] * height) },
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
            public int[][] Map { get; set; }
            public int[] KeyPos { get; set; }
            public int[] KeyPosCrown { get; set; }
            public int[] CrownPos { get; set; }
        }

        public class PixelPositions_1440p : PixelPositions
        {
            public PixelPositions_1440p()
            {
                Resolution = new int[2] { 2560, 1440 };
                SelectedSlotOffset = -13;
                SlotSize = 104;
                Slots = new int[5][]
                {
                    new int[] { 2009, 1227 },
                    new int[] { 2118, 1227 },
                    new int[] { 2227, 1227 },
                    new int[] { 2336, 1227 },
                    new int[] { 2444, 1227 }
                };
                Map = new int[3][]
                {
                    new int[] { 2543, 18 },
                    new int[] { 2538, 31 },
                    new int[] { 2511, 45 }
                };
                KeyPos = new int[2] { 2456, 927 };
                KeyPosCrown = new int[2] { 2350, 944 };
                CrownPos = new int[2] { 2490, 1000 };
            }
        }

        public class PixelPositions_1080p : PixelPositions
        {
            public PixelPositions_1080p()
            {
                Resolution = new int[2] { 1920, 1080 };
                SelectedSlotOffset = -11;
                SlotSize = 78;
                Slots = new int[5][]
                {
                    new int[] { 1507, 920 },
                    new int[] { 1589, 920 },
                    new int[] { 1670, 920 },
                    new int[] { 1752, 920 },
                    new int[] { 1833, 920 }
                };
                Map = new int[3][]
                {
                    new int[] { 1907, 13 },
                    new int[] { 1904, 23 },
                    new int[] { 1883, 33 }
                };
                KeyPos = new int[2] { 1842, 694 };
                KeyPosCrown = new int[2] { 1762, 707 };
                CrownPos = new int[2] { 1866, 750 };
            }
        }
    }
}
