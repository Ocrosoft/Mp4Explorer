namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4ElstEntry
    {
        public Mp4ElstEntry(ulong segmentDuration, long mediaTime, short mediaRate)
        {
            this.SegmentDuration = segmentDuration;
            this.MediaTime = mediaTime;
            this.MediaRate = mediaRate;
        }

        public ulong SegmentDuration { get; set; }

        public long MediaTime { get; set; }

        public short MediaRate { get; set; }
    }
}

