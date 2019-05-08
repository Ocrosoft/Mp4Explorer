namespace CMStream.Mp4
{
    using System;
    using System.IO;

    public class Mp4Stream
    {
        private Stream stream;

        public Mp4Stream(Stream stream)
        {
            this.stream = stream;
        }

        public virtual void Close()
        {
            this.stream.Close();
        }

        public virtual void CopyTo(Mp4Stream stream, int count)
        {
            byte[] buffer = new byte[count];
            this.stream.Read(buffer, 0, count);
            stream.Write(buffer, count);
        }

        public void Read(byte[] buffer, int count)
        {
            if (count != 0)
            {
                int num;
                for (int i = 0; count != 0; i += num)
                {
                    num = this.ReadPartial(buffer, i, count);
                    if ((num == 0) || (num > count))
                    {
                        throw new Exception("ERROR_INTERNAL");
                    }
                    count -= num;
                }
            }
        }

        public double ReadDouble()
        {
            byte[] buffer = new byte[8];
            this.Read(buffer, 8);
            return Mp4Util.BytesToDoubleBE(buffer);
        }

        public short ReadInt16()
        {
            byte[] buffer = new byte[2];
            this.Read(buffer, 2);
            return (short) Mp4Util.BytesToUInt16BE(buffer);
        }

        public int ReadInt32()
        {
            byte[] buffer = new byte[4];
            this.Read(buffer, 4);
            return (int) Mp4Util.BytesToUInt32BE(buffer);
        }

        public long ReadInt64()
        {
            byte[] buffer = new byte[8];
            this.Read(buffer, 8);
            return (long) Mp4Util.BytesToUInt64BE(buffer);
        }

        public virtual int ReadPartial(byte[] buffer, int offset, int count) => 
            this.stream.Read(buffer, offset, count);

        public byte ReadUInt08()
        {
            byte[] buffer = new byte[1];
            this.Read(buffer, 1);
            return buffer[0];
        }

        public ushort ReadUInt16()
        {
            byte[] buffer = new byte[2];
            this.Read(buffer, 2);
            return Mp4Util.BytesToUInt16BE(buffer);
        }

        public uint ReadUInt24()
        {
            byte[] buffer = new byte[3];
            this.Read(buffer, 3);
            return Mp4Util.BytesToUInt24BE(buffer);
        }

        public uint ReadUInt32()
        {
            byte[] buffer = new byte[4];
            this.Read(buffer, 4);
            return Mp4Util.BytesToUInt32BE(buffer);
        }

        public ulong ReadUInt64()
        {
            byte[] buffer = new byte[8];
            this.Read(buffer, 8);
            return Mp4Util.BytesToUInt64BE(buffer);
        }

        public virtual void Seek(long position)
        {
            this.stream.Seek(position, SeekOrigin.Begin);
        }

        public void Write(byte[] buffer, int length)
        {
            if (length != 0)
            {
                int num;
                for (int i = 0; length != 0; i += num)
                {
                    num = this.WritePartial(buffer, i, length);
                    if ((num == 0) || (num > length))
                    {
                        throw new Exception("ERROR_INTERNAL");
                    }
                    length -= num;
                }
            }
        }

        public void WriteDouble(double value)
        {
            byte[] bytes = new byte[8];
            Mp4Util.BytesFromDoubleBE(bytes, value);
            this.Write(bytes, 8);
        }

        public void WriteInt16(short value)
        {
            this.WriteUInt16((ushort) value);
        }

        public virtual int WritePartial(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
            return count;
        }

        public void WriteUInt08(byte value)
        {
            this.Write(new byte[] { value }, 1);
        }

        public void WriteUInt16(ushort value)
        {
            byte[] bytes = new byte[2];
            Mp4Util.BytesFromUInt16BE(bytes, value);
            this.Write(bytes, 2);
        }

        public void WriteUInt24(uint value)
        {
            byte[] bytes = new byte[3];
            Mp4Util.BytesFromUInt24BE(bytes, value);
            this.Write(bytes, 3);
        }

        public void WriteUInt32(uint value)
        {
            byte[] bytes = new byte[4];
            Mp4Util.BytesFromUInt32BE(bytes, value);
            this.Write(bytes, 4);
        }

        public void WriteUInt64(ulong value)
        {
            byte[] bytes = new byte[8];
            Mp4Util.BytesFromUInt64BE(bytes, value);
            this.Write(bytes, 8);
        }

        public virtual long Length =>
            this.stream.Length;

        public virtual long Position
        {
            get => 
                this.stream.Position;
            set => 
                stream.Position = value;
        }
    }
}

