namespace CMStream.Mp4
{
    using System;

    public class Mp4MpegSystemSampleEntry : Mp4SampleEntry
    {
        public Mp4MpegSystemSampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, stream, factory)
        {
        }
    }
}

