namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Mp4Descriptor
    {
        public Mp4Descriptor(Mp4DescriptorTag tag, uint headerSize, uint payloadSize)
        {
            this.Tag = tag;
            this.HeaderSize = headerSize;
            this.PayloadSize = payloadSize;
            if ((headerSize < 2) || (headerSize > 5))
            {
                throw new Exception();
            }
        }

        public virtual void Write(Mp4Stream stream)
        {
            stream.WriteUInt08((byte) this.Tag);
            if (((this.HeaderSize - 1) > 8) || (this.HeaderSize < 2))
            {
                throw new Exception("Wrong value for HeaderSize");
            }
            uint payloadSize = this.PayloadSize;
            byte[] buffer = new byte[8];
            buffer[(int) ((IntPtr) (this.HeaderSize - 2))] = (byte) (payloadSize & 0x7f);
            for (int i = ((int) this.HeaderSize) - 3; i >= 0; i--)
            {
                payloadSize = payloadSize >> 7;
                buffer[i] = (byte) ((payloadSize & 0x7f) | 0x80);
            }
            stream.Write(buffer, ((int) this.HeaderSize) - 1);
            this.WriteFields(stream);
        }

        public abstract void WriteFields(Mp4Stream stream);

        public Mp4DescriptorTag Tag { get; set; }

        public uint HeaderSize { get; set; }

        public uint PayloadSize { get; set; }

        public uint Size =>
            (this.HeaderSize + this.PayloadSize);
    }
}

