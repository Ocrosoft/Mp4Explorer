namespace CMStream.Mp4
{
    using System;

    public class Mp4MdiaBox : Mp4ContainerBox
    {
        public Mp4MdiaBox() : base(8, Mp4BoxType.MDIA)
        {
        }

        public Mp4MdiaBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MDIA, 0L, stream, factory)
        {
        }
    }
}

