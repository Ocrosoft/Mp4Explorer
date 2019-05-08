namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4UuidBox : Mp4Box
    {
        public byte[] Uuid;

        public Mp4UuidBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.UUID)
        {
            this.Uuid = new byte[0x10];
            stream.Read(this.Uuid, 0x10);
            this.Stream = stream;
            this.Position = stream.Position;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public Mp4Stream Stream { get; private set; }

        public long Position { get; private set; }
    }
}

