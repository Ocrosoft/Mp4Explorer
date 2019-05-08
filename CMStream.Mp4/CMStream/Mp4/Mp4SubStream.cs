namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SubStream : Mp4Stream
    {
        private Mp4Stream stream;
        private long offset;
        private long size;

        public Mp4SubStream(Mp4Stream stream, long offset, long size) : base(null)
        {
            this.stream = stream;
            this.offset = offset;
            this.size = size;
        }

        public override void Close()
        {
            this.stream.Close();
        }

        public override void CopyTo(Mp4Stream stream, int count)
        {
            stream.CopyTo(stream, count);
        }

        public override int ReadPartial(byte[] buffer, int offset, int count)
        {
            int num = 0;
            if (count == 0)
            {
                return 0;
            }
            if ((this.Position + count) > this.Length)
            {
                count = (int) (this.Length - this.Position);
            }
            if (count == 0)
            {
                throw new Exception("ERROR_EOS");
            }
            this.stream.Seek(this.offset + this.Position);
            num = this.stream.ReadPartial(buffer, offset, count);
            this.Position += num;
            return num;
        }

        public override void Seek(long position)
        {
            this.stream.Seek(this.offset + position);
        }

        public override int WritePartial(byte[] buffer, int offset, int count)
        {
            int num = 0;
            if (count == 0)
            {
                return 0;
            }
            if ((this.Position + count) > this.Length)
            {
                count = (int) (this.Length - this.Position);
            }
            if (count == 0)
            {
                throw new Exception("ERROR_EOS");
            }
            this.stream.Seek(this.offset + this.Position);
            num = this.stream.WritePartial(buffer, offset, count);
            this.Position += num;
            return num;
        }

        public override long Length =>
            this.size;

        public override long Position { get; set; }
    }
}

