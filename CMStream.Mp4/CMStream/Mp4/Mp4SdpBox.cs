namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class Mp4SdpBox : Mp4Box
    {
        public Mp4SdpBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.SDP_, 0L)
        {
            int count = ((int) size) - 8;
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

        public string Text { get; set; }
    }
}

