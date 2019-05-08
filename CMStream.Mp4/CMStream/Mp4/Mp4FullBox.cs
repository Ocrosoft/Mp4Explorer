namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Mp4FullBox : Mp4Box
    {
        public const int FULL_BOX_HEADER_SIZE_32 = 12;
        public const int FULL_BOX_HEADER_SIZE_64 = 20;

        public Mp4FullBox(uint size, uint type) : this(size, type, 0L, 0, 0)
        {
        }

        public Mp4FullBox(uint size, uint type, Mp4Stream stream) : this(size, type, 0L, stream)
        {
        }

        public Mp4FullBox(uint size, uint type, ulong largeSize, Mp4Stream stream) : base(size, type, largeSize)
        {
            uint num = stream.ReadUInt32();
            this.Version = (num >> 0x18) & 0xff;
            this.Flags = num & 0xffffff;
        }

        public Mp4FullBox(uint size, uint type, ulong largeSize, uint version, uint flags) : base(size, type, largeSize)
        {
            this.Version = version;
            this.Flags = flags;
        }

        public override void WriteHeader(Mp4Stream stream)
        {
            base.WriteHeader(stream);
            stream.WriteUInt08((byte) this.Version);
            stream.WriteUInt24(this.Flags);
        }

        public uint Version { get; set; }

        public uint Flags { get; set; }

        public override int HeaderSize
        {
            get
            {
                if (this.Size == 1)
                {
                    return 20;
                }
                return 12;
            }
        }
    }
}

