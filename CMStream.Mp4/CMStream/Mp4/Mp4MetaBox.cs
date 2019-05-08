namespace CMStream.Mp4
{
    using System;

    public class Mp4MetaBox : Mp4ContainerBox
    {
        public Mp4MetaBox() : base(12, Mp4BoxType.META)
        {
        }

        public Mp4MetaBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.META, 0L, stream, factory)
        {
        }

        public override int HeaderSize =>
            (12 + ((this.Size == 1) ? 8 : 0));
    }
}

