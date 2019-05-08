namespace CMStream.Mp4
{
    using System;

    public class Mp4Mp4aSampleEntry : Mp4MpegAudioSampleEntry
    {
        public Mp4Mp4aSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MP4A, stream, factory)
        {
        }
    }
}

