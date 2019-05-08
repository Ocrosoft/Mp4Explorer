namespace CMStream.Mp4
{
    using System;

    public class Mp4MinfBox : Mp4ContainerBox
    {
        public Mp4MinfBox() : base(8, Mp4BoxType.MINF)
        {
        }

        public Mp4MinfBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MINF, 0L, stream, factory)
        {
        }
    }
}

