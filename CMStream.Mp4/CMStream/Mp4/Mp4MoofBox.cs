namespace CMStream.Mp4
{
    using System;

    public class Mp4MoofBox : Mp4ContainerBox
    {
        public Mp4MoofBox() : base(8, Mp4BoxType.MOOF)
        {
        }

        public Mp4MoofBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MOOF, 0L, stream, factory)
        {
        }
    }
}

