namespace CMStream.Mp4
{
    using System;

    public class Mp4EncvSampleEntry : Mp4VisualSampleEntry
    {
        public Mp4EncvSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : this(size, Mp4BoxType.ENCV, stream, factory)
        {
        }

        public Mp4EncvSampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, stream, factory)
        {
        }
    }
}

