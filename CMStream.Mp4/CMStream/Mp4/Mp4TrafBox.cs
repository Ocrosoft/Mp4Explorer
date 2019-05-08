namespace CMStream.Mp4
{
    using System;

    public class Mp4TrafBox : Mp4ContainerBox
    {
        public Mp4TrafBox() : base(8, Mp4BoxType.TRAF)
        {
        }

        public Mp4TrafBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.TRAF, 0L, stream, factory)
        {
        }
    }
}

