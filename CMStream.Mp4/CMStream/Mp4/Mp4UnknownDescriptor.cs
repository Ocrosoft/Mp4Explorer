namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4UnknownDescriptor : Mp4Descriptor
    {
        public Mp4UnknownDescriptor(Mp4Stream stream, Mp4DescriptorTag tag, uint headerSize, uint payloadSize) : base(tag, headerSize, payloadSize)
        {
            this.Data = new byte[payloadSize];
            stream.Read(this.Data, (int) payloadSize);
        }

        public override void WriteFields(Mp4Stream stream)
        {
            stream.Write(this.Data, this.Data.Length);
        }

        public byte[] Data { get; private set; }
    }
}

