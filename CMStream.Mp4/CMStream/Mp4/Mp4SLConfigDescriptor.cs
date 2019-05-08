namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SLConfigDescriptor : Mp4Descriptor
    {
        public Mp4SLConfigDescriptor(uint headerSize) : base(Mp4DescriptorTag.SL_CONFIG, headerSize, 1)
        {
            this.Predefined = 2;
        }

        public Mp4SLConfigDescriptor(Mp4Stream stream, uint headerSize, uint payloadSize) : base(Mp4DescriptorTag.SL_CONFIG, headerSize, payloadSize)
        {
            this.Predefined = stream.ReadUInt08();
        }

        public override void WriteFields(Mp4Stream stream)
        {
            stream.WriteUInt08(this.Predefined);
        }

        public byte Predefined { get; private set; }
    }
}

