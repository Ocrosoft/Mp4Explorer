namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4StscEntry
    {
        public Mp4StscEntry()
        {
        }

        public Mp4StscEntry(uint firstChunk, uint samplesPerChunk, uint sampleDescriptionIndex)
        {
            this.FirstChunk = firstChunk;
            this.SamplesPerChunk = samplesPerChunk;
            this.SampleDescriptionIndex = sampleDescriptionIndex;
        }

        public uint FirstChunk { get; set; }

        public uint SamplesPerChunk { get; set; }

        public uint SampleDescriptionIndex { get; set; }

        public uint FirstSample { get; set; }

        public uint ChunkCount { get; set; }
    }
}

