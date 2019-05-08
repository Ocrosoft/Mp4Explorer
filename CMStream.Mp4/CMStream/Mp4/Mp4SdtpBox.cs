namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4SdtpBox : Mp4FullBox
    {
        public Mp4SdtpBox() : base(12, Mp4BoxType.SDTP)
        {
            this.Entries = new List<Mp4SdtpEntry>();
        }

        public Mp4SdtpBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.SDTP, 0L, stream)
        {
            int capacity = ((int) size) - 12;
            this.Entries = new List<Mp4SdtpEntry>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                byte num3 = stream.ReadUInt08();
                int sampleDependsOn = (num3 & 0x30) >> 4;
                int sampleIsDependOn = (num3 & 12) >> 2;
                int sampleHasRedundancy = num3 & 3;
                this.Entries.Add(new Mp4SdtpEntry(sampleDependsOn, sampleIsDependOn, sampleHasRedundancy));
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            for (int i = 0; i < this.Entries.Count; i++)
            {
                byte num2 = 0;
                num2 = (byte) ((num2 | this.Entries[i].SampleDependsOn) << 2);
                num2 = (byte) ((num2 | this.Entries[i].SampleIsDependOn) << 2);
                num2 = (byte) (num2 | this.Entries[i].SampleHasRedundancy);
                stream.WriteUInt08(num2);
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 12) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    return (base.Size + ((uint) this.Entries.Count));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public List<Mp4SdtpEntry> Entries { get; private set; }
    }
}

