namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp48idBox : Mp4Box
    {
        public Mp48idBox(string value) : base(8, Mp4BoxType._DI8)
        {
            this.Value = value;
            this.Size += (uint) (this.Value.Length + 1);
        }

        public Mp48idBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType._DI8)
        {
            int count = ((int) size) - 8;
            byte[] buffer = new byte[count];
            stream.Read(buffer, count);
            buffer[count - 1] = 0;
            this.Value = Encoding.ASCII.GetString(buffer);
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (this.Size > 8)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(this.Value);
                stream.Write(bytes, bytes.Length + 1);
                uint num = this.Size - ((uint) ((8 + bytes.Length) + 1));
                while (num-- != 0)
                {
                    stream.WriteUInt08(0);
                }
            }
        }

        public string Value { get; private set; }
    }
}

