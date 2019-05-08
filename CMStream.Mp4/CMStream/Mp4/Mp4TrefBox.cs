namespace CMStream.Mp4
{
    using System;

    public class Mp4TrefBox : Mp4ContainerBox
    {
        public Mp4TrefBox() : base(8, Mp4BoxType.TREF)
        {
        }

        public Mp4TrefBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.TREF, 0L, stream, factory)
        {
        }
    }
}

