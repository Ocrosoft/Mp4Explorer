namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4DecoderSpecificInfoDescriptor : Mp4Descriptor
    {
        public Mp4DecoderSpecificInfoDescriptor(Mp4Stream stream, uint headerSize, uint payloadSize) : base(Mp4DescriptorTag.DECODER_SPECIFIC_INFO, headerSize, payloadSize)
        {
            this.Info = new byte[payloadSize];
            stream.Read(this.Info, (int) payloadSize);
        }

        public override void WriteFields(Mp4Stream stream)
        {
            if ((base.PayloadSize != 0) && (this.Info != null))
            {
                stream.Write(this.Info, this.Info.Length);
            }
        }

        public byte[] Info { get; private set; }
    }
}

