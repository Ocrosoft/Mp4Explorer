namespace CMStream.Mp4
{
    using System;

    public class Mp4MvexBox : Mp4ContainerBox
    {
        public Mp4MvexBox() : base(8, Mp4BoxType.MVEX)
        {
        }

        public Mp4MvexBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MVEX, 0L, stream, factory)
        {
        }
    }
}

