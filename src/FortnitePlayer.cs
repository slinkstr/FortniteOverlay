using System;
using System.Drawing;

namespace FortniteOverlay
{
    internal class FortnitePlayer
    {
        public string     Name         { get; set; }
        public string     UserId       { get; set; }
        public ReadyState State        { get; set; }
        public int        Index        { get; set; } = -1;
        public Bitmap     GearImage    { get; set; }
        public DateTime   GearModified { get; set; }
        public bool       IsFaded      { get; set; } = false;

        public string     UserIdTruncated => UserId.Substring(0, 5) + "..." + UserId.Substring(UserId.Length - 5, 5);

        public enum ReadyState
        {
            NotReady,
            Ready,
            SittingOut,
        }
    }
}
