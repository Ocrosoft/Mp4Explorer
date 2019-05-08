namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4FtypBox : Mp4Box
    {
        public Mp4FtypBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.FTYP)
        {
            this.MajorBrand = stream.ReadUInt32();
            this.MinorVersion = stream.ReadUInt32();
            int capacity = (int) ((size - 0x10) / 4);
            this.CompatibleBrands = new List<uint>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                this.CompatibleBrands.Add(stream.ReadUInt32());
            }
        }

        public Mp4FtypBox(uint majorBrand, uint minorVersion, uint[] compatibleBrands) : base((uint) (0x10 + (4 * compatibleBrands.Length)), Mp4BoxType.FTYP)
        {
            this.MajorBrand = majorBrand;
            this.MinorVersion = minorVersion;
            this.CompatibleBrands = new List<uint>(compatibleBrands);
        }

        public override string ToString()
        {
            object obj2 = string.Empty;
            return (string.Concat(new object[] { obj2, "ftyp(MajorBrand:", Mp4Util.FormatFourChars(this.MajorBrand), ",MinorVersion:", this.MinorVersion }) + ((this.CompatibleBrands.Count > 0) ? (",CompatibleBrands:" + Mp4Util.FormatFourChars(this.CompatibleBrands)) : "") + ")");
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(this.MajorBrand);
            stream.WriteUInt32(this.MinorVersion);
            foreach (uint num in this.CompatibleBrands)
            {
                stream.WriteUInt32(num);
            }
        }

        public uint MajorBrand { get; set; }

        public uint MinorVersion { get; set; }

        public List<uint> CompatibleBrands { get; set; }
    }
}

