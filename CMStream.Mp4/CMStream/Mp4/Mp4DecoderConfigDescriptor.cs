namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4DecoderConfigDescriptor : Mp4Descriptor
    {
        public Mp4DecoderConfigDescriptor(Mp4Stream stream, uint headerSize, uint payloadSize) : base(Mp4DescriptorTag.DECODER_CONFIG, headerSize, payloadSize)
        {
            Mp4Descriptor descriptor;
            long position = stream.Position;
            this.ObjectTypeIndication = (Mp4ObjectTypeIndication) stream.ReadUInt08();
            byte num2 = stream.ReadUInt08();
            this.StreamType = (byte) ((num2 >> 2) & 0x3f);
            this.UpStream = (num2 & 2) != 0;
            this.BufferSize = stream.ReadUInt24();
            this.MaxBitrate = stream.ReadUInt32();
            this.AverageBitrate = stream.ReadUInt32();
            long offset = stream.Position;
            this.SubDescriptors = new List<Mp4Descriptor>();
            Mp4DescriptorFactory factory = new Mp4DescriptorFactory();
            Mp4SubStream stream2 = new Mp4SubStream(stream, offset, (long) (payloadSize - ((uint) (offset - position))));
            while ((descriptor = factory.Read(stream2)) != null)
            {
                this.SubDescriptors.Add(descriptor);
            }
        }

        public override void WriteFields(Mp4Stream stream)
        {
            stream.WriteUInt08((byte) this.ObjectTypeIndication);
            byte num = (byte) (((this.StreamType << 2) | (this.UpStream ? 2 : 0)) | 1);
            stream.WriteUInt08(num);
            stream.WriteUInt24(this.BufferSize);
            stream.WriteUInt32(this.MaxBitrate);
            stream.WriteUInt32(this.AverageBitrate);
            foreach (Mp4Descriptor descriptor in this.SubDescriptors)
            {
                descriptor.Write(stream);
            }
        }

        public byte StreamType { get; private set; }

        public Mp4ObjectTypeIndication ObjectTypeIndication { get; private set; }

        public bool UpStream { get; private set; }

        public uint BufferSize { get; private set; }

        public uint MaxBitrate { get; private set; }

        public uint AverageBitrate { get; private set; }

        public List<Mp4Descriptor> SubDescriptors { get; private set; }

        public Mp4DecoderSpecificInfoDescriptor DecoderSpecificInfoDescriptor =>
            (this.SubDescriptors.Find(d => d.Tag == Mp4DescriptorTag.DECODER_SPECIFIC_INFO) as Mp4DecoderSpecificInfoDescriptor);
    }
}

