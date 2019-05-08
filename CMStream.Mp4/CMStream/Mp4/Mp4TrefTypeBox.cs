namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4TrefTypeBox : Mp4Box
    {
        public Mp4TrefTypeBox(uint type) : base(8, type)
        {
        }

        public Mp4TrefTypeBox(uint type, uint size, Mp4Stream stream) : base(size, type)
        {
            int capacity = ((int) size) - this.HeaderSize;
            this.TrackIds = new List<uint>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                uint item = stream.ReadUInt32();
                this.TrackIds.Add(item);
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            for (int i = 0; i < this.TrackIds.Count; i++)
            {
                stream.WriteUInt32(this.TrackIds[i]);
            }
        }

        public List<uint> TrackIds { get; set; }
    }
}

