namespace CMStream.Mp4
{
    using System;

    public class Mp4EncaSampleEntry : Mp4AudioSampleEntry
    {
        public Mp4EncaSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : this(size, Mp4BoxType.ENCA, stream, factory)
        {
        }

        public Mp4EncaSampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, stream, factory)
        {
        }
    }
}

