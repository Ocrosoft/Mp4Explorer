﻿namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MdatBox : Mp4Box
    {
        public Mp4MdatBox(uint size, ulong largeSize, Mp4Stream stream) : base(size, Mp4BoxType.MDAT, largeSize)
        {
            this.Stream = stream;
            this.Position = stream.Position;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            byte[] data = this.Data;
            stream.Write(data, data.Length);
        }

        public Mp4Stream Stream { get; set; }

        public long Position { get; set; }

        public byte[] Data
        {
            get
            {
                int num = (this.Size == 0) ? ((int) (this.Stream.Length - this.Position)) : ((this.Size == 1) ? ((int) this.LargeSize) : ((int) this.Size));
                if (num > 0x7fffffff)
                {
                    throw new Mp4Exception("Can't hold so much large data in memory");
                }
                int headerSize = this.HeaderSize;
                byte[] buffer = new byte[num - headerSize];
                this.Stream.Position = this.Position;
                this.Stream.ReadPartial(buffer, headerSize, buffer.Length);
                return buffer;
            }
        }
    }
}

