namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4RtpBox : Mp4Box
    {
        public Mp4RtpBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.RTP_, 0L)
        {
            this.DescriptionFormat = stream.ReadUInt32();
            int count = ((int) size) - 12;
            if (count > 0)
            {
                byte[] buffer = new byte[count];
                stream.Read(buffer, count);
                this.Text = Encoding.ASCII.GetString(buffer);
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            throw new NotImplementedException();
        }

        public uint DescriptionFormat { get; set; }

        public string Text { get; set; }
    }
}

