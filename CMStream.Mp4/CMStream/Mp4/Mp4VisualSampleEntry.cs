namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4VisualSampleEntry : Mp4SampleEntry
    {
        public byte[] Predefined2;

        public Mp4VisualSampleEntry(uint size, uint format, Mp4Stream stream, Mp4BoxFactory factory) : base(size, format)
        {
            this.Predefined2 = new byte[12];
            uint fieldsSize = this.GetFieldsSize();
            this.ReadFields(stream);
            base.ReadChildren(stream, factory, (long) ((size - 8) - fieldsSize));
        }

        public Mp4VisualSampleEntry(uint format, ushort width, ushort height, ushort depth, string compressorName) : base(8, format)
        {
            this.Predefined2 = new byte[12];
            this.Predefined1 = 0;
            this.Reserved2 = 0;
            this.Width = width;
            this.Height = height;
            this.HorizResolution = 0x480000;
            this.VertResolution = 0x480000;
            this.Reserved3 = 0;
            this.FrameCount = 1;
            this.CompressorName = compressorName;
            this.Depth = depth;
            this.Predefined3 = 0xffff;
            this.Size += 70;
        }

        public override uint GetFieldsSize() => 
            (base.GetFieldsSize() + 70);

        public override void ReadFields(Mp4Stream stream)
        {
            base.ReadFields(stream);
            this.Predefined1 = stream.ReadUInt16();
            this.Reserved2 = stream.ReadUInt16();
            stream.Read(this.Predefined2, this.Predefined2.Length);
            this.Width = stream.ReadUInt16();
            this.Height = stream.ReadUInt16();
            this.HorizResolution = stream.ReadUInt32();
            this.VertResolution = stream.ReadUInt32();
            this.Reserved3 = stream.ReadUInt32();
            this.FrameCount = stream.ReadUInt16();
            byte[] buffer = new byte[0x20];
            stream.Read(buffer, 0x20);
            int count = buffer[0];
            if (count < 0x20)
            {
                this.CompressorName = Encoding.ASCII.GetString(buffer, 1, count);
            }
            this.Depth = stream.ReadUInt16();
            this.Predefined3 = stream.ReadUInt16();
        }

        public override void WriteInnerBody(Mp4Stream stream)
        {
            base.WriteInnerBody(stream);
            stream.WriteUInt16(this.Predefined1);
            stream.WriteUInt16(this.Reserved2);
            stream.Write(this.Predefined2, this.Predefined2.Length);
            stream.WriteUInt16(this.Width);
            stream.WriteUInt16(this.Height);
            stream.WriteUInt32(this.HorizResolution);
            stream.WriteUInt32(this.VertResolution);
            stream.WriteUInt32(this.Reserved3);
            stream.WriteUInt16(this.FrameCount);
            byte[] buffer = new byte[0x20];
            int length = this.CompressorName.Length;
            if (length > 0x1f)
            {
                length = 0x1f;
            }
            buffer[0] = (byte) length;
            for (int i = 0; i < length; i++)
            {
                buffer[i + 1] = (byte) this.CompressorName[i];
            }
            for (int j = length + 1; j < 0x20; j++)
            {
                buffer[j] = 0;
            }
            stream.Write(buffer, 0x20);
            stream.WriteUInt16(this.Depth);
            stream.WriteUInt16(this.Predefined3);
        }

        public ushort Predefined1 { get; private set; }

        public ushort Reserved2 { get; private set; }

        public ushort Width { get; private set; }

        public ushort Height { get; private set; }

        public uint HorizResolution { get; private set; }

        public uint VertResolution { get; private set; }

        public uint Reserved3 { get; private set; }

        public ushort FrameCount { get; private set; }

        public string CompressorName { get; private set; }

        public ushort Depth { get; private set; }

        public ushort Predefined3 { get; private set; }
    }
}

