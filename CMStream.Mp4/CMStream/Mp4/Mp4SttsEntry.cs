namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SttsEntry
    {
        public Mp4SttsEntry(uint sampleCount, uint sampleDelta)
        {
            this.SampleCount = sampleCount;
            this.SampleDelta = sampleDelta;
        }

        public uint SampleCount { get; set; }

        public uint SampleDelta { get; set; }
    }
}

