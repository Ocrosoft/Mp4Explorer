namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4TfraBox : Mp4FullBox
    {
        public int Reserved;

        public Mp4TfraBox(uint trackId) : base(0x18, Mp4BoxType.TFRA, 0L, 1, 0)
        {
            this.TrackId = trackId;
            this.Reserved = 0;
            this.LengthSizeOfTrafNum = 3;
            this.LengthSizeOfTrunNum = 3;
            this.LengthSizeOfSampleNum = 3;
            this.Entries = new List<Mp4TfraEntry>();
        }

        public Mp4TfraBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.TFRA, 0L, stream)
        {
            this.TrackId = stream.ReadUInt32();
            uint num = stream.ReadUInt32();
            this.Reserved = ((int) (num >> 6)) & 0x3ffffff;
            this.LengthSizeOfTrafNum = (byte) ((num >> 4) & 3);
            this.LengthSizeOfTrunNum = (byte) ((num >> 2) & 3);
            this.LengthSizeOfSampleNum = (byte) (num & 3);
            uint num2 = stream.ReadUInt32();
            this.Entries = new List<Mp4TfraEntry>();
            for (int i = 0; i < num2; i++)
            {
                Mp4TfraEntry item = new Mp4TfraEntry();
                if (base.Version == 1)
                {
                    item.Time = stream.ReadUInt64();
                    item.MoofOffset = stream.ReadUInt64();
                }
                else
                {
                    item.Time = stream.ReadUInt32();
                    item.MoofOffset = stream.ReadUInt32();
                }
                switch (this.LengthSizeOfTrafNum)
                {
                    case 0:
                        item.TrafNumber = stream.ReadUInt08();
                        break;

                    case 1:
                        item.TrafNumber = stream.ReadUInt16();
                        break;

                    case 2:
                        item.TrafNumber = stream.ReadUInt24();
                        break;

                    case 3:
                        item.TrafNumber = stream.ReadUInt32();
                        break;
                }
                switch (this.LengthSizeOfTrunNum)
                {
                    case 0:
                        item.TrunNumber = stream.ReadUInt08();
                        break;

                    case 1:
                        item.TrunNumber = stream.ReadUInt16();
                        break;

                    case 2:
                        item.TrunNumber = stream.ReadUInt24();
                        break;

                    case 3:
                        item.TrunNumber = stream.ReadUInt32();
                        break;
                }
                switch (this.LengthSizeOfSampleNum)
                {
                    case 0:
                        item.SampleNumber = stream.ReadUInt08();
                        break;

                    case 1:
                        item.SampleNumber = stream.ReadUInt16();
                        break;

                    case 2:
                        item.SampleNumber = stream.ReadUInt24();
                        break;

                    case 3:
                        item.SampleNumber = stream.ReadUInt32();
                        break;
                }
                this.Entries.Add(item);
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.TrackId);
            uint num = (uint) (this.Reserved << 6);
            num |= (uint) (this.LengthSizeOfTrafNum << 4);
            num |= (uint) (this.LengthSizeOfTrunNum << 2);
            num |= this.LengthSizeOfSampleNum;
            stream.WriteUInt32(num);
            stream.WriteUInt32((uint) this.NumberOfEntry);
            for (int i = 0; i < this.Entries.Count; i++)
            {
                Mp4TfraEntry entry = this.Entries[i];
                if (base.Version == 1)
                {
                    stream.WriteUInt64(entry.Time);
                    stream.WriteUInt64(entry.MoofOffset);
                }
                else
                {
                    stream.WriteUInt32((uint) entry.Time);
                    stream.WriteUInt32((uint) entry.MoofOffset);
                }
                switch (this.LengthSizeOfTrafNum)
                {
                    case 0:
                        stream.WriteUInt08((byte) entry.TrafNumber);
                        break;

                    case 1:
                        stream.WriteUInt16((ushort) entry.TrafNumber);
                        break;

                    case 2:
                        stream.WriteUInt24(entry.TrafNumber);
                        break;

                    case 3:
                        stream.WriteUInt32(entry.TrafNumber);
                        break;
                }
                switch (this.LengthSizeOfTrunNum)
                {
                    case 0:
                        stream.WriteUInt08((byte) entry.TrunNumber);
                        break;

                    case 1:
                        stream.WriteUInt16((ushort) entry.TrunNumber);
                        break;

                    case 2:
                        stream.WriteUInt24(entry.TrunNumber);
                        break;

                    case 3:
                        stream.WriteUInt32(entry.TrunNumber);
                        break;
                }
                switch (this.LengthSizeOfSampleNum)
                {
                    case 0:
                        stream.WriteUInt08((byte) entry.SampleNumber);
                        break;

                    case 1:
                        stream.WriteUInt16((ushort) entry.SampleNumber);
                        break;

                    case 2:
                        stream.WriteUInt24(entry.SampleNumber);
                        break;

                    case 3:
                        stream.WriteUInt32(entry.SampleNumber);
                        break;
                }
            }
        }

        public override uint Size
        {
            get
            {
                if (((base.Size == 0x18) && (this.Entries != null)) && (this.Entries.Count > 0))
                {
                    int num = (((this.LengthSizeOfTrafNum + 1) + (this.LengthSizeOfTrunNum + 1)) + (this.LengthSizeOfSampleNum + 1)) + (2 * ((base.Version == 1) ? 8 : 4));
                    return (base.Size + ((uint) (this.Entries.Count * num)));
                }
                return base.Size;
            }
            set => 
                base.Size = value;
        }

        public uint TrackId { get; private set; }

        public byte LengthSizeOfTrafNum { get; private set; }

        public byte LengthSizeOfTrunNum { get; private set; }

        public byte LengthSizeOfSampleNum { get; private set; }

        public int NumberOfEntry =>
            this.Entries.Count;

        public List<Mp4TfraEntry> Entries { get; private set; }
    }
}

