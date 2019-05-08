namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4TfraEntry
    {
        public Mp4TfraEntry()
        {
        }

        public Mp4TfraEntry(ulong time, ulong moofOffset, uint trafNumber, uint trunNumber, uint sampleNumber)
        {
            this.Time = time;
            this.MoofOffset = moofOffset;
            this.TrafNumber = trafNumber;
            this.TrunNumber = trunNumber;
            this.SampleNumber = sampleNumber;
        }

        public ulong Time { get; set; }

        public ulong MoofOffset { get; set; }

        public uint TrafNumber { get; set; }

        public uint TrunNumber { get; set; }

        public uint SampleNumber { get; set; }
    }
}

