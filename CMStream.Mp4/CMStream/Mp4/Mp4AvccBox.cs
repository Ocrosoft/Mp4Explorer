namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4AvccBox : Mp4Box
    {
        public const byte AVC_PROFILE_BASELINE = 0x42;
        public const byte AVC_PROFILE_MAIN = 0x4d;
        public const byte AVC_PROFILE_EXTENDED = 0x58;
        public const byte AVC_PROFILE_HIGH = 100;
        public const byte AVC_PROFILE_HIGH_10 = 110;
        public const byte AVC_PROFILE_HIGH_422 = 0x7a;
        public const byte AVC_PROFILE_HIGH_444 = 0x90;

        public Mp4AvccBox() : base(15, Mp4BoxType.AVCC)
        {
            this.ConfigurationVersion = 1;
            this.AVCProfileIndication = 0;
            this.AVCLevelIndication = 0;
            this.AVCCompatibleProfiles = 0;
            this.NaluLengthSize = 0;
        }

        public Mp4AvccBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.AVCC)
        {
            long position = stream.Position;
            this.RawBytes = new byte[size];
            stream.Read(this.RawBytes, this.RawBytes.Length);
            stream.Seek(position);
            this.ConfigurationVersion = stream.ReadUInt08();
            this.AVCProfileIndication = stream.ReadUInt08();
            this.AVCCompatibleProfiles = stream.ReadUInt08();
            this.AVCLevelIndication = stream.ReadUInt08();
            byte num2 = stream.ReadUInt08();
            this.NaluLengthSize = (byte) (1 + (num2 & 3));
            byte capacity = (byte) (stream.ReadUInt08() & 0x1f);
            this.SequenceParameters = new List<byte[]>(capacity);
            for (uint i = 0; i < capacity; i++)
            {
                byte[] buffer = new byte[stream.ReadUInt16()];
                stream.Read(buffer, buffer.Length);
                this.SequenceParameters.Add(buffer);
            }
            byte num6 = stream.ReadUInt08();
            this.PictureParameters = new List<byte[]>();
            for (uint j = 0; j < num6; j++)
            {
                byte[] buffer = new byte[stream.ReadUInt16()];
                stream.Read(buffer, buffer.Length);
                this.PictureParameters.Add(buffer);
            }
        }

        public Mp4AvccBox(byte configVersion, byte profile, byte level, byte profileCompatibility, byte lengthSize, List<byte[]> sequenceParameters, List<byte[]> pictureParameters) : base(15, Mp4BoxType.AVCC)
        {
            this.ConfigurationVersion = configVersion;
            this.AVCProfileIndication = profile;
            this.AVCLevelIndication = level;
            this.AVCCompatibleProfiles = profileCompatibility;
            this.NaluLengthSize = lengthSize;
            int num = 0;
            this.SequenceParameters = new List<byte[]>();
            for (num = 0; num < sequenceParameters.Count; num++)
            {
                this.SequenceParameters.Add(sequenceParameters[num]);
                this.Size += (uint) (2 + sequenceParameters[num].Length);
            }
            this.PictureParameters = new List<byte[]>();
            for (num = 0; num < pictureParameters.Count; num++)
            {
                this.PictureParameters.Add(pictureParameters[num]);
                this.Size += (uint) (2 + pictureParameters[num].Length);
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt08(this.ConfigurationVersion);
            stream.WriteUInt08(this.AVCProfileIndication);
            stream.WriteUInt08(this.AVCCompatibleProfiles);
            stream.WriteUInt08(this.AVCLevelIndication);
            byte num = 0xfc;
            if ((this.NaluLengthSize >= 1) && (this.NaluLengthSize <= 4))
            {
                num = (byte) (num | ((byte) (this.NaluLengthSize - 1)));
            }
            stream.WriteUInt08(num);
            this.NaluLengthSize = (byte) (1 + (num & 3));
            byte num2 = (byte) (this.SequenceParameters.Count & 0x31);
            stream.WriteUInt08((byte) (0xe0 | num2));
            for (int i = 0; i < num2; i++)
            {
                ushort length = (ushort) this.SequenceParameters[i].Length;
                stream.WriteUInt16(length);
                stream.Write(this.SequenceParameters[i], length);
            }
            byte count = (byte) this.PictureParameters.Count;
            stream.WriteUInt08(count);
            for (int j = 0; j < count; j++)
            {
                ushort length = (ushort) this.PictureParameters[j].Length;
                stream.WriteUInt16(length);
                stream.Write(this.PictureParameters[j], length);
            }
        }

        public byte ConfigurationVersion { get; private set; }

        public byte AVCProfileIndication { get; private set; }

        public byte AVCLevelIndication { get; private set; }

        public byte AVCCompatibleProfiles { get; private set; }

        public byte NaluLengthSize { get; private set; }

        public List<byte[]> SequenceParameters { get; private set; }

        public List<byte[]> PictureParameters { get; private set; }

        public byte[] RawBytes { get; private set; }
    }
}

