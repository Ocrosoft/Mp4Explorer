namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SampleEntry : Mp4ContainerBox
    {
        public byte[] Reserved;

        public Mp4SampleEntry(uint size, uint type) : base(size, type)
        {
            this.Reserved = new byte[6];
        }

        public Mp4SampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type)
        {
            this.Reserved = new byte[6];
            uint fieldsSize = this.GetFieldsSize();
            this.ReadFields(stream);
            base.ReadChildren(stream, factory, (long) ((size - 8) - fieldsSize));
        }

        public virtual uint GetFieldsSize() => 
            8;

        public virtual void ReadFields(Mp4Stream stream)
        {
            stream.Read(this.Reserved, this.Reserved.Length);
            this.DataReferenceIndex = stream.ReadUInt16();
        }

        public override void WriteInnerBody(Mp4Stream stream)
        {
            stream.Write(this.Reserved, this.Reserved.Length);
            stream.WriteUInt16(this.DataReferenceIndex);
        }

        public ushort DataReferenceIndex { get; set; }
    }
}

