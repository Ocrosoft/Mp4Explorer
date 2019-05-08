namespace CMStream.Mp4
{
    using System;

    public class Mp4NmhdBox : Mp4FullBox
    {
        public Mp4NmhdBox() : base(12, Mp4BoxType.NMHD)
        {
        }

        public Mp4NmhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.NMHD, 0L, stream)
        {
        }

        public override void WriteBody(Mp4Stream stream)
        {
        }
    }
}

