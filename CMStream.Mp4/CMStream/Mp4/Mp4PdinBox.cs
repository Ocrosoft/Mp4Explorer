namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4PdinBox : Mp4FullBox
    {
        public Mp4PdinBox() : base(12, Mp4BoxType.PDIN)
        {
            this.Entries = new List<Mp4PdinEntry>();
        }

        public Mp4PdinBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.PDIN, stream)
        {
            int capacity = ((int) size) - this.HeaderSize;
            this.Entries = new List<Mp4PdinEntry>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                Mp4PdinEntry item = new Mp4PdinEntry {
                    Rate = stream.ReadUInt32(),
                    InitialDelay = stream.ReadUInt32()
                };
                this.Entries.Add(item);
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            for (int i = 0; i < this.Entries.Count; i++)
            {
                stream.WriteUInt32(this.Entries[i].Rate);
                stream.WriteUInt32(this.Entries[i].InitialDelay);
            }
        }

        public override uint Size
        {
            get => 
                ((uint) (this.HeaderSize + (4 * this.Entries.Count)));
            set => 
                base.Size = value;
        }

        public List<Mp4PdinEntry> Entries { get; set; }
    }
}

