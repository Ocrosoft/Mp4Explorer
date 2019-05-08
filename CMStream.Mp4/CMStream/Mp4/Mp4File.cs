namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4File
    {
        public Mp4File(Mp4Movie movie)
        {
            this.Movie = movie;
            this.MoovIsBeforeMdat = true;
            this.Boxes = new List<Mp4Box>();
        }

        public Mp4File(Mp4Stream stream) : this(stream, new Mp4BoxFactory(), false)
        {
        }

        public Mp4File(Mp4Stream stream, Mp4BoxFactory factory) : this(stream, factory, false)
        {
        }

        public Mp4File(Mp4Stream stream, Mp4BoxFactory factory, bool moovOnly)
        {
            this.Boxes = new List<Mp4Box>();
            bool flag = true;
            while (flag)
            {
                long position = stream.Position;
                Mp4Box item = factory.Read(stream);
                if (item != null)
                {
                    this.Boxes.Add(item);
                    if (item.Type == Mp4BoxType.MOOV)
                    {
                        this.Movie = new Mp4Movie((Mp4MoovBox) item, stream);
                        if (moovOnly)
                        {
                            flag = false;
                        }
                    }
                    else if (item.Type == Mp4BoxType.FTYP)
                    {
                        this.FileType = (Mp4FtypBox) item;
                    }
                    else if ((item.Type == Mp4BoxType.MDAT) && (this.Movie == null))
                    {
                        this.MoovIsBeforeMdat = false;
                    }
                }
                else
                {
                    flag = false;
                }
            }
        }

        public List<Mp4Box> Boxes { get; private set; }

        public Mp4Movie Movie { get; private set; }

        public Mp4FtypBox FileType { get; private set; }

        public bool MoovIsBeforeMdat { get; private set; }
    }
}

