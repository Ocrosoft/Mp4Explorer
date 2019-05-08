namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4UrnBox : Mp4FullBox
    {
        public Mp4UrnBox() : base(12, Mp4BoxType.URN_, 0L, 0, 1)
        {
        }

        public Mp4UrnBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.URN_, 0L, stream)
        {
            throw new NotImplementedException();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (((base.Flags & 1) == 0) && (this.Size > 12))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(this.Location);
                stream.Write(bytes, bytes.Length + 1);
                int num = ((int) this.Size) - ((12 + bytes.Length) + 1);
                while (num-- != 0)
                {
                    stream.WriteUInt08(0);
                }
            }
        }

        public string Location { get; private set; }
    }
}

