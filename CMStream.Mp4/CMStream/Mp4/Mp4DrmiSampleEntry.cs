namespace CMStream.Mp4
{
    using System;

    public class Mp4DrmiSampleEntry : Mp4EncvSampleEntry
    {
        public Mp4DrmiSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.DRMI, stream, factory)
        {
        }
    }
}

