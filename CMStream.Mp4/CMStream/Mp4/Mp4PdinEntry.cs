namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4PdinEntry
    {
        public Mp4PdinEntry()
        {
        }

        public Mp4PdinEntry(uint rate, uint initialDelay)
        {
            this.Rate = rate;
            this.InitialDelay = initialDelay;
        }

        public uint Rate { get; set; }

        public uint InitialDelay { get; set; }
    }
}

