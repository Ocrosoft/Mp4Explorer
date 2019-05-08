namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4HdlrBox : Mp4FullBox
    {
        public uint[] Reserved;

        public Mp4HdlrBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.HDLR, 0L, stream)
        {
            this.Reserved = new uint[3];
            this.PreDefined = stream.ReadUInt32();
            this.HandlerType = stream.ReadUInt32();
            this.Reserved[0] = stream.ReadUInt32();
            this.Reserved[1] = stream.ReadUInt32();
            this.Reserved[2] = stream.ReadUInt32();
            int count = ((int) size) - 0x1c;
            if (count > 0)
            {
                byte[] buffer = new byte[count];
                stream.Read(buffer, count);
                if (buffer[0] == (count - 1))
                {
                    this.Name = Encoding.UTF8.GetString(buffer, 1, buffer.Length - 1);
                }
                else
                {
                    this.Name = Encoding.UTF8.GetString(buffer);
                    int index = this.Name.IndexOf('\0');
                    if (index != -1)
                    {
                        this.Name = this.Name.Substring(0, index);
                    }
                }
            }
            else
            {
                this.Name = string.Empty;
            }
        }

        public Mp4HdlrBox(uint handlerType, string name) : base((uint) ((0x20 + name.Length) + 1), Mp4BoxType.HDLR)
        {
            uint num;
            this.Reserved = new uint[3];
            this.HandlerType = handlerType;
            this.Name = name;
            this.Reserved[2] = num = 0;
            this.Reserved[0] = this.Reserved[1] = num;
        }

        public override void WriteBody(Mp4Stream stream)
        {
            stream.WriteUInt32(0);
            stream.WriteUInt32(this.HandlerType);
            stream.WriteUInt32(this.Reserved[0]);
            stream.WriteUInt32(this.Reserved[1]);
            stream.WriteUInt32(this.Reserved[2]);
            int length = this.Name.Length;
            if ((0x20 + length) > this.Size)
            {
                length = (((int) this.Size) - 12) + 20;
            }
            if (length > 0)
            {
                stream.Write(Encoding.UTF8.GetBytes(this.Name), length);
            }
            int num2 = ((int) this.Size) - (0x20 + length);
            while (num2-- > 0)
            {
                stream.WriteUInt08(0);
            }
        }

        public uint PreDefined { get; set; }

        public uint HandlerType { get; set; }

        public string Name { get; set; }
    }
}

