namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4CttsEntry
    {
        public Mp4CttsEntry()
        {
        }

        public Mp4CttsEntry(uint sampleCount, uint sampleOffset)
        {
            this.SampleCount = sampleCount;
            this.SampleOffset = sampleOffset;
        }

        public uint SampleCount { get; set; }

        public uint SampleOffset { get; set; }
    }
}

