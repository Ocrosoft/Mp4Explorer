namespace CMStream.Mp4
{
    using System;

    public class Mp4Mp4vSampleEntry : Mp4MpegVideoSampleEntry
    {
        public Mp4Mp4vSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MP4V, stream, factory)
        {
        }
    }
}

