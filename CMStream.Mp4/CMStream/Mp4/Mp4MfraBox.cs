namespace CMStream.Mp4
{
    using System;

    public class Mp4MfraBox : Mp4ContainerBox
    {
        public Mp4MfraBox() : base(8, Mp4BoxType.MFRA)
        {
        }

        public Mp4MfraBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MFRA, 0L, stream, factory)
        {
        }
    }
}

