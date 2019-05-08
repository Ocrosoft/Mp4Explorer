namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MdhdBox : Mp4FullBox
    {
        public Mp4MdhdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.MDHD, 0L, stream)
        {
            if (base.Version == 0)
            {
                this.CreationTime = stream.ReadUInt32();
                this.ModificationTime = stream.ReadUInt32();
                this.TimeScale = stream.ReadUInt32();
                this.Duration = stream.ReadUInt32();
            }
            else
            {
                this.CreationTime = stream.ReadUInt64();
                this.ModificationTime = stream.ReadUInt64();
                this.TimeScale = stream.ReadUInt32();
                this.Duration = stream.ReadUInt64();
            }
            byte[] buffer = new byte[2];
            stream.Read(buffer, 2);
            byte num = (byte) (((byte) (buffer[0] >> 2)) & 0x1f);
            byte num2 = (byte) ((((byte) (buffer[0] & 3)) << 3) | ((buffer[1] >> 5) & 7));
            byte num3 = (byte) (buffer[1] & 0x1f);
            if (((num != 0) && (num2 != 0)) && (num3 != 0))
            {
                char[] chArray = new char[] { (char) (num + 0x60), (char) (num2 + 0x60), (char) (num3 + 0x60) };
                this.Language = new string(chArray);
            }
            else
            {
                this.Language = "```";
            }
        }

        public Mp4MdhdBox(DateTime creationTime, DateTime modificationTime, uint timeScale, ulong duration, string language) : base(0x2c, Mp4BoxType.MDHD, 0L, 1, 0)
        {
            this.CreationTime = Mp4Util.ConvertTime(creationTime);
            this.ModificationTime = Mp4Util.ConvertTime(modificationTime);
            this.TimeScale = timeScale;
            this.Duration = duration;
            this.Language = language;
            if (language.Length != 3)
            {
                throw new Mp4Exception("Language must be of length 3.");
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (base.Version == 0)
            {
                stream.WriteUInt32((uint) this.CreationTime);
                stream.WriteUInt32((uint) this.ModificationTime);
                stream.WriteUInt32(this.TimeScale);
                stream.WriteUInt32((uint) this.Duration);
            }
            else
            {
                stream.WriteUInt64(this.CreationTime);
                stream.WriteUInt64(this.ModificationTime);
                stream.WriteUInt32(this.TimeScale);
                stream.WriteUInt64(this.Duration);
            }
            byte num = (byte) (this.Language[0] - '`');
            byte num2 = (byte) (this.Language[1] - '`');
            byte num3 = (byte) (this.Language[2] - '`');
            stream.WriteUInt08((byte) ((num << 2) | (num2 >> 3)));
            stream.WriteUInt08((byte) ((num2 << 5) | num3));
            stream.WriteUInt16(0);
        }

        public ulong CreationTime { get; set; }

        public ulong ModificationTime { get; set; }

        public uint TimeScale { get; set; }

        public ulong Duration { get; set; }

        public string Language { get; set; }

        public ushort PreDefined { get; set; }
    }
}

