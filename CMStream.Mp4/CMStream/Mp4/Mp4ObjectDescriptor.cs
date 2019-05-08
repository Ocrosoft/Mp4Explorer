namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4ObjectDescriptor : Mp4Descriptor
    {
        public Mp4ObjectDescriptor(Mp4Stream stream, Mp4DescriptorTag tag, uint headerSize, uint payloadSize) : base(tag, headerSize, payloadSize)
        {
            Mp4Descriptor descriptor;
            long position = stream.Position;
            ushort num2 = stream.ReadUInt16();
            this.ObjectDescriptorId = (ushort) (num2 >> 6);
            this.UrlFlag = (num2 & 0x20) != 0;
            if (this.UrlFlag)
            {
                byte count = stream.ReadUInt08();
                byte[] buffer = new byte[0x100];
                stream.Read(buffer, count);
                buffer[count] = 0;
                this.Url = Encoding.ASCII.GetString(buffer, 0, count);
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
            throw new NotImplementedException();
        }

        public ushort ObjectDescriptorId { get; set; }

        public bool UrlFlag { get; set; }

        public string Url { get; set; }

        public List<Mp4Descriptor> SubDescriptors { get; set; }
    }
}

