namespace CMStream.Mp4
{
    using System;

    public class Mp4Exception : Exception
    {
        public Mp4Exception()
        {
        }

        public Mp4Exception(string message) : base(message)
        {
        }

        public Mp4Exception(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

