namespace CMStream.Mp4
{
    using System;

    public class Mp4StblBox : Mp4ContainerBox
    {
        public Mp4StblBox() : base(8, Mp4BoxType.STBL)
        {
        }

        public Mp4StblBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.STBL, 0L, stream, factory)
        {
        }
    }
}

