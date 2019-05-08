namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4EsDescriptor : Mp4Descriptor
    {
        public const int FLAG_STREAM_DEPENDENCY = 1;
        public const int FLAG_URL = 2;
        public const int FLAG_OCR_STREAM = 4;

        public Mp4EsDescriptor(Mp4Stream stream, uint headerSize, uint payloadSize) : base(Mp4DescriptorTag.ES, headerSize, payloadSize)
        {
            Mp4Descriptor descriptor;
            long position = stream.Position;
            this.EsId = stream.ReadUInt16();
            byte num2 = stream.ReadUInt08();
            this.Flags = (uint) ((num2 >> 5) & 7);
            this.StreamPriority = (byte) (num2 & 0x1f);
            if ((this.Flags & 1) != 0)
            {
                this.DependsOn = stream.ReadUInt16();
            }
            else
            {
                this.DependsOn = 0;
            }
            if ((this.Flags & 2) != 0)
            {
                byte count = stream.ReadUInt08();
                if (count != 0)
                {
                    byte[] buffer = new byte[count + 1];
                    stream.Read(buffer, count);
                    buffer[count] = 0;
                    this.Url = Encoding.ASCII.GetString(buffer, 0, count);
                }
            }
            if ((this.Flags & 2) != 0)
            {
                this.OcrEsId = stream.ReadUInt16();
            }
            else
            {
                this.OcrEsId = 0;
            }
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
            stream.WriteUInt16(this.EsId);
            byte num = (byte) (this.StreamPriority | ((byte) (this.Flags << 5)));
            stream.WriteUInt08(num);
            if ((this.Flags & 1) != 0)
            {
                stream.WriteUInt16(this.DependsOn);
            }
            if ((this.Flags & 2) != 0)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(this.Url);
                stream.WriteUInt08((byte) bytes.Length);
                stream.Write(bytes, bytes.Length);
                stream.WriteUInt08(0);
            }
            if ((this.Flags & 4) != 0)
            {
                stream.WriteUInt16(this.OcrEsId);
            }
            foreach (Mp4Descriptor descriptor in this.SubDescriptors)
            {
                descriptor.Write(stream);
            }
        }

        public ushort EsId { get; private set; }

        public ushort OcrEsId { get; private set; }

        public uint Flags { get; private set; }

        public byte StreamPriority { get; private set; }

        public ushort DependsOn { get; private set; }

        public string Url { get; private set; }

        public List<Mp4Descriptor> SubDescriptors { get; private set; }

        public Mp4DecoderConfigDescriptor DecoderConfigDescriptor =>
            (this.SubDescriptors.Find(d => d.Tag == Mp4DescriptorTag.DECODER_CONFIG) as Mp4DecoderConfigDescriptor);
    }
}

