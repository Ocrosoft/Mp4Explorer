namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4IodsBox : Mp4FullBox
    {
        public Mp4IodsBox(Mp4ObjectDescriptor objectDescriptor) : base(12, Mp4BoxType.IODS)
        {
            this.ObjectDescriptor = objectDescriptor;
            if (this.ObjectDescriptor != null)
            {
                this.Size += this.ObjectDescriptor.Size;
            }
        }

        public Mp4IodsBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.IODS, 0L, stream)
        {
            Mp4Descriptor descriptor = new Mp4DescriptorFactory().Read(stream);
            this.ObjectDescriptor = descriptor as Mp4ObjectDescriptor;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (this.ObjectDescriptor != null)
            {
                this.ObjectDescriptor.Write(stream);
            }
        }

        public Mp4ObjectDescriptor ObjectDescriptor { get; private set; }
    }
}

