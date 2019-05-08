namespace CMStream.Mp4
{
    using System;

    public class Mp4MpegVideoSampleEntry : Mp4VisualSampleEntry
    {
        public Mp4MpegVideoSampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, stream, factory)
        {
        }
    }
}

