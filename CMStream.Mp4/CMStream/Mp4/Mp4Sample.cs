namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4Sample
    {
        public Mp4Sample()
        {
            this.IsSync = true;
        }

        public Mp4Sample(Mp4Stream dataStream, long offset, int size, uint duration, uint descriptionIndex, ulong dts, uint ctsDelta, bool isSync)
        {
            this.DataStream = dataStream;
            this.Offset = offset;
            this.Size = size;
            this.Duration = duration;
            this.DescriptionIndex = descriptionIndex;
            this.Dts = dts;
            this.CtsDelta = ctsDelta;
            this.IsSync = isSync;
        }

        public void ReadData(byte[] data)
        {
            if (this.DataStream == null)
            {
                throw new Exception("FAILURE");
            }
            if (data.Length != 0)
            {
                this.DataStream.Seek(this.Offset);
                this.DataStream.Read(data, this.Size);
            }
        }

        public Mp4Stream DataStream { get; set; }

        public long Offset { get; set; }

        public int Size { get; set; }

        public uint Duration { get; set; }

        public uint DescriptionIndex { get; set; }

        public ulong Dts { get; set; }

        public uint CtsDelta { get; set; }

        public bool IsSync { get; set; }

        public ulong Cts
        {
            get => 
                (this.Dts + this.CtsDelta);
            set
            {
                ulong num = value;
                this.CtsDelta = (num > this.Dts) ? ((uint) (num - this.Dts)) : 0;
            }
        }
    }
}

