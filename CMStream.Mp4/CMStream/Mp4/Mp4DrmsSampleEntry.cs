namespace CMStream.Mp4
{
    using System;

    public class Mp4DrmsSampleEntry : Mp4EncaSampleEntry
    {
        public Mp4DrmsSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.DRMS, stream, factory)
        {
        }
    }
}

