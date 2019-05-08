namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4Movie
    {
        public Mp4Movie() : this(0)
        {
        }

        public Mp4Movie(uint timeScale)
        {
            this.MoovBox = new Mp4MoovBox();
            this.MvhdBox = new Mp4MvhdBox(DateTime.Now, DateTime.Now, timeScale, 0L, 0x10000, 0x100);
            this.MoovBox.AddChild(this.MvhdBox);
            this.Tracks = new List<Mp4Track>();
        }

        public Mp4Movie(Mp4MoovBox moov, Mp4Stream sampleStream)
        {
            if (moov != null)
            {
                uint timeScale;
                this.MoovBox = moov;
                this.MvhdBox = moov.GetChild<Mp4MvhdBox>(Mp4BoxType.MVHD);
                if (this.MvhdBox != null)
                {
                    timeScale = this.MvhdBox.TimeScale;
                }
                else
                {
                    timeScale = 0;
                }
                this.Tracks = new List<Mp4Track>();
                foreach (Mp4TrakBox box in moov.GetTrackBoxes())
                {
                    Mp4Track item = new Mp4Track(box, sampleStream, timeScale);
                    this.Tracks.Add(item);
                }
            }
        }

        public Mp4MoovBox MoovBox { get; private set; }

        public Mp4MvhdBox MvhdBox { get; private set; }

        public uint DurationMs
        {
            get
            {
                if (this.MvhdBox != null)
                {
                    return (uint) Mp4Util.ConvertTime(this.MvhdBox.Duration, this.TimeScale, 0x3e8);
                }
                return 0;
            }
        }

        public uint TimeScale
        {
            get
            {
                if (this.MvhdBox != null)
                {
                    return this.MvhdBox.TimeScale;
                }
                return 0;
            }
        }

        public List<Mp4Track> Tracks { get; private set; }
    }
}

