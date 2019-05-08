namespace CMStream.Mp4
{
    using System;

    public class Mp4Mp4sSampleEntry : Mp4MpegSystemSampleEntry
    {
        public Mp4Mp4sSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MP4S, stream, factory)
        {
        }
    }
}

