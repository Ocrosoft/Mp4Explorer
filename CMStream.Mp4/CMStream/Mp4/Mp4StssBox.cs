namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4StssBox : Mp4FullBox
    {
        private int lookupCache;

        public Mp4StssBox() : base(0x10, Mp4BoxType.STSS)
        {
            this.Entries = new List<uint>();
        }

        public Mp4StssBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.STSS, 0L, stream)
        {
            this.lookupCache = 0;
            uint num = stream.ReadUInt32();
            this.Entries = new List<uint>((int) num);
            for (int i = 0; i < num; i++)
            {
                this.Entries.Add(stream.ReadUInt32());
            }
        }

        public bool IsSampleSync(uint sample)
        {
            int lookupCache = 0;
            if ((sample != 0) && (this.Entries.Count != 0))
            {
                if (this.Entries[this.lookupCache] <= sample)
                {
                    lookupCache = this.lookupCache;
                }
                while ((lookupCache < this.Entries.Count) && (this.Entries[lookupCache] <= sample))
                {
                    if (this.Entries[lookupCache] == sample)
                    {
                        this.lookupCache = lookupCache;
                        return true;
                    }
                    lookupCache++;
                }
            }
            return false;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            uint count = (uint) this.Entries.Count;
            stream.WriteUInt32(count);
            for (int i = 0; i < count; i++)
            {
                stream.WriteUInt32(this.Entries[i]);
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 0x10) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    return (base.Size + ((uint) (this.Entries.Count * 4)));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public int EntryCount =>
            this.Entries.Count;

        public List<uint> Entries { get; set; }
    }
}

