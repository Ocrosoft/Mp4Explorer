namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4EsdsBox : Mp4FullBox
    {
        public Mp4EsdsBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.ESDS, 0L, stream)
        {
            Mp4Descriptor descriptor = new Mp4DescriptorFactory().Read(stream);
            this.EsDescriptor = descriptor as Mp4EsDescriptor;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (this.EsDescriptor != null)
            {
                this.EsDescriptor.Write(stream);
            }
        }

        public Mp4EsDescriptor EsDescriptor { get; private set; }
    }
}

