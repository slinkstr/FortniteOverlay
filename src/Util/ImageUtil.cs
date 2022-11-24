using FortniteOverlay.Properties;
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
            var scaledPos = ScalePositions(positions, hudScale);
            var pix = screenshot.GetPixel(scaledPos.Map[0], scaledPos.Map[1]);
            
            //if (pix.R < 250 || pix.G < 250 || pix.B < 250)
            if (pix.R != 255 || pix.G != 255 || pix.B != 255)
            {
                return false;
            }
            else
            {
                return true;
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
            var pix = screenshot.GetPixel(scaledPos.GoldBars[0], scaledPos.GoldBars[1]);
            if (pix.GetHue() > 52
             && pix.GetHue() < 62
             && pix.GetBrightness() > 0.70
             && pix.GetBrightness() < 0.95)
            {
                return true;
            }
            else
            {
                return false;
            }
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

            //if (pix.R < 250 || pix.G < 250 || pix.B < 250)
            if (pix.R != 255 || pix.G != 255 || pix.B != 255)
            {
                return false;
            }

            pix = screenshot.GetPixel(scaledPos.SpectatingText[1][0], scaledPos.SpectatingText[1][1]);

            //if (pix.R < 250 || pix.G < 250 || pix.B < 250)
            if (pix.R != 255 || pix.G != 255 || pix.B != 255)
            {
                return false;
            }

            return true;
        }

        public static bool IsCrownVisible(Bitmap screenshot, List<PixelPositions> positions, int hudScale, bool inventoryHotkey)
        {
            return IsCrownVisible(screenshot, MatchingPosition(screenshot, positions), hudScale, inventoryHotkey);
        }

        public static bool IsCrownVisible(Bitmap screenshot, PixelPositions positions, int hudScale, bool inventoryHotkey)
        {
            if (screenshot == null) { return false; }
            var scaledPos = ScalePositions(positions, hudScale);
            var pix = screenshot.GetPixel(scaledPos.Crown[0], scaledPos.Crown[1] + (inventoryHotkey ? 0 : scaledPos.InventoryHotkeyOffset));
            if (pix.GetHue() > 45
             && pix.GetHue() < 60
             && pix.GetSaturation() > 0.86)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static Bitmap RenderGear(Bitmap screenshot, List<PixelPositions> positions, int hudScale, bool inventoryHotkey)
        {
            return RenderGear(screenshot, MatchingPosition(screenshot, positions), hudScale, inventoryHotkey);
        }

        public static Bitmap RenderGear(Bitmap screenshot, PixelPositions positions, int hudScale, bool inventoryHotkey)
        {
            var scaledPos = ScalePositions(positions, hudScale);
            int hotkeyOffset = inventoryHotkey ? 0 : scaledPos.InventoryHotkeyOffset;

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

            Bitmap bitmap = new Bitmap((scaledPos.SlotSize * 6), scaledPos.SlotSize);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    var ofs = (slotSelected == i + 1) ? scaledPos.SelectedSlotOffset : 0;
                    g.DrawImage(screenshot, new Rectangle(i * scaledPos.SlotSize, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.Slots[i][0],      scaledPos.Slots[i][1] + ofs,               scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
                }

                // keys
                if (IsCrownVisible(screenshot, positions, hudScale, inventoryHotkey))
                {
                    g.DrawImage(screenshot, new Rectangle(scaledPos.SlotSize * 5, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.KeysWithCrown[0], scaledPos.KeysWithCrown[1] + hotkeyOffset, scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(screenshot, new Rectangle(scaledPos.SlotSize * 5, 0, scaledPos.SlotSize, scaledPos.SlotSize), new Rectangle(scaledPos.Keys[0],          scaledPos.Keys[1]          + hotkeyOffset, scaledPos.SlotSize, scaledPos.SlotSize), GraphicsUnit.Pixel);
                }
            }

            // Resize to save space
            bitmap = new Bitmap(bitmap, new Size(312, 52));

            return bitmap;
        }

        public static void RenderGearDebug(ref Bitmap blankScreenshot, List<PixelPositions> positions, int hudScale, bool inventoryHotkey)
        {
            RenderGearDebug(ref blankScreenshot, MatchingPosition(blankScreenshot, positions), hudScale, inventoryHotkey);
        }

        public static void RenderGearDebug(ref Bitmap blankScreenshot, PixelPositions positions, int hudScale, bool inventoryHotkey)
        {
            Pen pen = new Pen(Color.Red, 1);

            var scaledPos = ScalePositions(positions, hudScale);
            int crownOffset = inventoryHotkey ? 0 : scaledPos.InventoryHotkeyOffset;

            using (Graphics g = Graphics.FromImage(blankScreenshot))
            {
                // Draw slot outlines
                for (int i = 0; i < scaledPos.Slots.Length; i++)
                {
                    g.DrawRectangle(pen, new Rectangle(scaledPos.Slots[i][0] - 1, scaledPos.Slots[i][1] - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));
                }

                // Keys/crown
                g.DrawEllipse(pen, scaledPos.Crown[0] - 1, scaledPos.Crown[1] - 1 + crownOffset, 2, 2);
                g.DrawRectangle(pen, new Rectangle(scaledPos.KeysWithCrown[0] - 1, scaledPos.KeysWithCrown[1] + crownOffset - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));
                g.DrawRectangle(pen, new Rectangle(scaledPos.Keys[0]          - 1, scaledPos.Keys[1]          + crownOffset - 1, scaledPos.SlotSize + 1, scaledPos.SlotSize + 1));

                // Other points
                g.DrawEllipse(pen, scaledPos.Map[0] - 1, scaledPos.Map[1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.GoldBars[0] - 1, scaledPos.GoldBars[1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.SpectatingText[0][0] - 1, scaledPos.SpectatingText[0][1] - 1, 2, 2);
                g.DrawEllipse(pen, scaledPos.SpectatingText[1][0] - 1, scaledPos.SpectatingText[1][1] - 1, 2, 2);
            }
        }

        public static Bitmap TakeScreenshot(Rectangle bounds)
        {
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
                GoldBars = new int[2]
                {
                    (int)((double)reference.GoldBars[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.GoldBars[1] / (double)reference.Resolution[1] * height),
                },
                SpectatingText = new int[2][]
                {
                    new int[] { (int)((double)reference.SpectatingText[0][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[0][1] / (double)reference.Resolution[1] * height) },
                    new int[] { (int)((double)reference.SpectatingText[1][0] / (double)reference.Resolution[0] * width), (int)((double)reference.Slots[1][1] / (double)reference.Resolution[1] * height) },
                },

                InventoryHotkeyOffset = (int)((double)reference.InventoryHotkeyOffset / (double)reference.Resolution[1] * height),
                Keys = new int[2] {
                    (int)((double)reference.Keys[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.Keys[1] / (double)reference.Resolution[1] * height),
                },
                KeysWithCrown = new int[2]
                {
                    (int)((double)reference.KeysWithCrown[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.KeysWithCrown[1] / (double)reference.Resolution[1] * height),
                },
                Crown = new int[2]
                {
                    (int)((double)reference.Crown[0] / (double)reference.Resolution[0] * width),
                    (int)((double)reference.Crown[1] / (double)reference.Resolution[1] * height),
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
                GoldBars = ScaleAboutBottomRight(pos.GoldBars[0], pos.GoldBars[1], pos.Resolution[0], pos.Resolution[1], scale),
                SpectatingText = new int[2][]
                {
                    ScaleAboutTopMiddle(pos.SpectatingText[0][0], pos.SpectatingText[0][1], pos.Resolution[0], pos.Resolution[1], scale),
                    ScaleAboutTopMiddle(pos.SpectatingText[1][0], pos.SpectatingText[1][1], pos.Resolution[0], pos.Resolution[1], scale),
                },

                InventoryHotkeyOffset = Convert.ToInt32(pos.InventoryHotkeyOffset * ((double)scale / 100)),
                Keys = ScaleAboutBottomRight(pos.Keys[0], pos.Keys[1], pos.Resolution[0], pos.Resolution[1], scale),
                KeysWithCrown = ScaleAboutBottomRight(pos.KeysWithCrown[0], pos.KeysWithCrown[1], pos.Resolution[0], pos.Resolution[1], scale),
                Crown = ScaleAboutBottomRight(pos.Crown[0], pos.Crown[1], pos.Resolution[0], pos.Resolution[1], scale),
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
            public int[] GoldBars { get; set; }
            public int[][] SpectatingText { get; set; }

            public int InventoryHotkeyOffset { get; set; }
            public int[] Keys { get; set; }
            public int[] KeysWithCrown { get; set; }
            public int[] Crown { get; set; }
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
                    new int[] { 2009, 1227 },
                    new int[] { 2118, 1227 },
                    new int[] { 2227, 1227 },
                    new int[] { 2336, 1227 },
                    new int[] { 2444, 1227 }
                };

                Map                   = new int[2] { 2512, 47 };
                GoldBars              = new int[2] { 2518, 1153 };
                SpectatingText        = new int[2][]
                {
                    new int[] { 1181, 26 },
                    new int[] { 1200, 26 },
                };

                InventoryHotkeyOffset = 65;
                Keys                  = new int[2] { 2456, 927  };
                KeysWithCrown         = new int[2] { 2350, 944  };
                Crown                 = new int[2] { 2490, 1000 };
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
                    new int[] { 1507, 920 },
                    new int[] { 1589, 920 },
                    new int[] { 1670, 920 },
                    new int[] { 1752, 920 },
                    new int[] { 1833, 920 }
                };

                Map                   = new int[2] { 1884, 35  };
                GoldBars              = new int[2] { 1889, 866 };
                SpectatingText        = new int[2][]
                {
                    new int[] { 885, 20 },
                    new int[] { 900, 20 },
                };

                InventoryHotkeyOffset = 49;
                Keys                  = new int[2] { 1842, 694 };
                KeysWithCrown         = new int[2] { 1762, 707 };
                Crown                 = new int[2] { 1866, 750 };
            }
        }
    }
}
