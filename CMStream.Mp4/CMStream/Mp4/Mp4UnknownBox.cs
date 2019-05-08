namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4UnknownBox : Mp4Box
    {
        public Mp4UnknownBox(uint size, uint type, ulong largeSize, Mp4Stream stream) : base(size, type, largeSize)
        {
            this.Stream = stream;
            this.Position = stream.Position;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if ((this.Stream == null) || (this.Size < 8))
            {
                throw new Exception("FAILURE");
            }
            long position = this.Stream.Position;
            this.Stream.Seek(this.Position);
            int count = ((int) this.Size) - this.HeaderSize;
            this.Stream.CopyTo(stream, count);
            this.Stream.Seek(position);
        }

        public Mp4Stream Stream { get; private set; }

        public long Position { get; private set; }
    }
}

