namespace CMStream.Mp4
{
    using System;

    public class Mp4DinfBox : Mp4ContainerBox
    {
        public Mp4DinfBox() : base(8, Mp4BoxType.DINF)
        {
        }

        public Mp4DinfBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.DINF, 0L, stream, factory)
        {
        }
    }
}

