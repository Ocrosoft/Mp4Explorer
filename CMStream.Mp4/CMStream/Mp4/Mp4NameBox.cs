namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4NameBox : Mp4Box
    {
        public Mp4NameBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.NAME)
        {
            byte[] buffer = new byte[size - this.HeaderSize];
            stream.Read(buffer, buffer.Length);
            this.Name = Encoding.ASCII.GetString(buffer);
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
    }
}

