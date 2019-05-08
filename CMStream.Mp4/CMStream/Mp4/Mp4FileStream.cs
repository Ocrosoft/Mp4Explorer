namespace CMStream.Mp4
{
    using System;
    using System.IO;

    public class Mp4FileStream : Mp4Stream
    {
        public Mp4FileStream(string filename, FileAccess access) : this(filename, FileMode.Open, access)
        {
        }

        public Mp4FileStream(string filename, FileMode mode, FileAccess access) : base(File.Open(filename, mode, access, FileShare.Read))
        {
        }
    }
}

