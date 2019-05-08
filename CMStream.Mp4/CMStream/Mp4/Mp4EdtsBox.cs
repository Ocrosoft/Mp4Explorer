namespace CMStream.Mp4
{
    using System;

    public class Mp4EdtsBox : Mp4ContainerBox
    {
        public Mp4EdtsBox() : base(8, Mp4BoxType.EDTS)
        {
        }

        public Mp4EdtsBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.EDTS, 0L, stream, factory)
        {
        }
    }
}

