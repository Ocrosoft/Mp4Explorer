namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mp4MoovBox : Mp4ContainerBox
    {
        public Mp4MoovBox() : base(8, Mp4BoxType.MOOV)
        {
        }

        public Mp4MoovBox(uint size, ulong largeSize, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.MOOV, largeSize, stream, factory)
        {
        }

        public List<Mp4TrakBox> GetTrackBoxes() => 
            (from b in base.Children
                where b is Mp4TrakBox
                select b).Cast<Mp4TrakBox>().ToList<Mp4TrakBox>();
    }
}

