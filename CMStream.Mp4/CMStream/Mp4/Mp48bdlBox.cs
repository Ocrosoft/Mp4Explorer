namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp48bdlBox : Mp4Box
    {
        public Mp48bdlBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.LDB8)
        {
            this.Encoding = stream.ReadUInt32();
            this.EncodingVersion = stream.ReadUInt32();
            this.BundleData = new byte[(size - 8) - 8];
            stream.Read(this.BundleData, this.BundleData.Length);
        }

        public Mp48bdlBox(uint encoding, uint encodingVersion, byte[] data) : base(Mp4BoxType.LDB8, (uint) (0x10 + data.Length))
        {
            this.Encoding = encoding;
            this.EncodingVersion = encodingVersion;
            this.BundleData = data;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.Encoding);
            stream.WriteUInt32(this.EncodingVersion);
            stream.Write(this.BundleData, this.BundleData.Length);
        }

        public uint Encoding { get; private set; }

        public uint EncodingVersion { get; private set; }

        public byte[] BundleData { get; private set; }
    }
}

