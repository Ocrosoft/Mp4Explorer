namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TrunEntry
    {
        public Mp4TrunEntry()
        {
        }

        public Mp4TrunEntry(uint sampleDuration, uint sampleSize, uint sampleFlags, uint sampleCompositionTimeOffset)
        {
            this.SampleDuration = sampleDuration;
            this.SampleSize = sampleSize;
            this.SampleFlags = sampleFlags;
            this.SampleCompositionTimeOffset = sampleCompositionTimeOffset;
        }

        public uint SampleDuration { get; set; }

        public uint SampleSize { get; set; }

        public uint SampleFlags { get; set; }

        public uint SampleCompositionTimeOffset { get; set; }
    }
}

