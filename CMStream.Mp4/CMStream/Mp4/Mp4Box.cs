namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Mp4Box
    {
        public const int BOX_HEADER_SIZE_32 = 8;
        public const int BOX_HEADER_SIZE_64 = 0x10;

        public Mp4Box(uint size, uint type) : this(size, type, 0L)
        {
        }

        public Mp4Box(uint size, uint type, ulong largeSize) : this(size, type, null, largeSize)
        {
        }

        public Mp4Box(uint size, uint type, byte[] extendedType, ulong largeSize)
        {
            this.Size = size;
            this.Type = type;
            this.LargeSize = largeSize;
            this.UserType = extendedType;
            if ((size == 1) && (largeSize == 0L))
            {
                throw new Mp4Exception("Large size must be specified");
            }
            if ((size == 0) && (largeSize != 0L))
            {
                throw new Mp4Exception("Large size can't be specified");
            }
            if ((type == Mp4BoxType.UUID) && (extendedType == null))
            {
                throw new Mp4Exception("Extended type must be specified");
            }
        }

        public override string ToString() => 
            (Mp4Util.FormatFourChars(this.Type) + "(" + ((this.Size != 1) ? ("Size:" + this.Size) : ("LargeSize" + this.LargeSize)) + ((this.Parent != null) ? (",Parent:" + Mp4Util.FormatFourChars(this.Parent.Type)) : "") + ")");

        public virtual void Write(Mp4Stream stream)
        {
            this.WriteHeader(stream);
            this.WriteBody(stream);
        }

        public abstract void WriteBody(Mp4Stream stream);
        public virtual void WriteHeader(Mp4Stream stream)
        {
            stream.WriteUInt32(this.Size);
            stream.WriteUInt32(this.Type);
            if (this.Size == 1)
            {
                stream.WriteUInt64(this.LargeSize);
            }
            if (this.UserType != null)
            {
                stream.Write(this.UserType, this.UserType.Length);
            }
        }

        public virtual uint Size { get; set; }

        public uint Type { get; set; }

        public virtual ulong LargeSize { get; set; }

        public byte[] UserType { get; set; }

        public virtual int HeaderSize
        {
            get
            {
                if (this.Size == 1)
                {
                    return 0x10;
                }
                return 8;
            }
        }

        public IMp4ContainerBox Parent { get; set; }
    }
}

