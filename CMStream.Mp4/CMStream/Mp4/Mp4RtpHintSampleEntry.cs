namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4RtpHintSampleEntry : Mp4SampleEntry
    {
        public Mp4RtpHintSampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.RTP_)
        {
            uint fieldsSize = this.GetFieldsSize();
            this.ReadFields(stream);
            base.ReadChildren(stream, factory, (long) ((size - 8) - fieldsSize));
        }

        public override uint GetFieldsSize() => 
            (base.GetFieldsSize() + 8);

        public override void ReadFields(Mp4Stream stream)
        {
            base.ReadFields(stream);
            this.HintTrackVersion = stream.ReadUInt16();
            this.HighestCompatibleVersion = stream.ReadUInt16();
            this.MaxPacketSize = stream.ReadUInt32();
        }

        public override void WriteInnerBody(Mp4Stream stream)
        {
            base.WriteInnerBody(stream);
            stream.WriteUInt16(this.HintTrackVersion);
            stream.WriteUInt16(this.HighestCompatibleVersion);
            stream.WriteUInt32(this.MaxPacketSize);
        }

        public ushort HintTrackVersion { get; private set; }

        public ushort HighestCompatibleVersion { get; private set; }

        public uint MaxPacketSize { get; private set; }
    }
}

