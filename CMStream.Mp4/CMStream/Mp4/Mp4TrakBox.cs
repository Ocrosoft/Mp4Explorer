namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Mp4TrakBox : Mp4ContainerBox
    {
        public Mp4TrakBox(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.TRAK, 0L, stream, factory)
        {
            this.TkhdBox = this.FindChild<Mp4TkhdBox>("tkhd");
            this.MdhdBox = this.FindChild<Mp4MdhdBox>("mdia/mdhd");
        }

        public Mp4TrakBox(Mp4SampleTable sampleTable, uint hdlrType, string hdlrName, uint trackId, DateTime creationTime, DateTime modificationTime, ulong trackDuration, uint mediaTimeScale, ulong mediaDuration, ushort volume, string language, uint width, uint height) : base(8, Mp4BoxType.TRAK)
        {
            Mp4Box box4;
            this.TkhdBox = new Mp4TkhdBox(creationTime, modificationTime, trackId, trackDuration, volume, width, height);
            Mp4MdiaBox child = new Mp4MdiaBox();
            Mp4HdlrBox box2 = new Mp4HdlrBox(hdlrType, hdlrName);
            Mp4ContainerBox box3 = new Mp4ContainerBox(8, Mp4BoxType.MINF);
            if (hdlrType == Mp4HandlerType.VIDE)
            {
                box4 = new Mp4VmhdBox(0, 0, 0, 0);
            }
            else if (hdlrType == Mp4HandlerType.SOUN)
            {
                box4 = new Mp4SmhdBox(0);
            }
            else
            {
                box4 = new Mp4NmhdBox();
            }
            Mp4ContainerBox box5 = new Mp4ContainerBox(8, Mp4BoxType.DINF);
            Mp4Box box6 = new Mp4UrlBox();
            Mp4DrefBox box7 = new Mp4DrefBox(new Mp4Box[] { box6 });
            Mp4ContainerBox box8 = sampleTable?.GenerateStblAtom();
            box5.AddChild(box7);
            box3.AddChild(box4);
            box3.AddChild(box5);
            if (box8 != null)
            {
                box3.AddChild(box8);
            }
            this.MdhdBox = new Mp4MdhdBox(creationTime, modificationTime, mediaTimeScale, mediaDuration, language);
            child.AddChild(this.MdhdBox);
            child.AddChild(box2);
            child.AddChild(box3);
            base.AddChild(this.TkhdBox);
            base.AddChild(child);
        }

        public uint Id
        {
            get
            {
                if (this.TkhdBox == null)
                {
                    return 0;
                }
                return this.TkhdBox.TrackId;
            }
            set
            {
                if (this.TkhdBox != null)
                {
                    this.TkhdBox.TrackId = value;
                }
            }
        }

        public ulong Duration
        {
            get
            {
                if (this.TkhdBox == null)
                {
                    return 0L;
                }
                return this.TkhdBox.Duration;
            }
        }

        public uint MediaTimeScale
        {
            get
            {
                if (this.MdhdBox == null)
                {
                    return 0;
                }
                return this.MdhdBox.TimeScale;
            }
        }

        public ulong MediaDuration
        {
            get
            {
                if (this.MdhdBox == null)
                {
                    return 0L;
                }
                return this.MdhdBox.Duration;
            }
        }

        public Mp4TkhdBox TkhdBox { get; private set; }

        public Mp4MdhdBox MdhdBox { get; private set; }

        public uint Width
        {
            get
            {
                if (this.TkhdBox == null)
                {
                    return 0;
                }
                return this.TkhdBox.Width;
            }
            set
            {
                if (this.TkhdBox != null)
                {
                    this.TkhdBox.Width = value;
                }
            }
        }

        public uint Height
        {
            get
            {
                if (this.TkhdBox == null)
                {
                    return 0;
                }
                return this.TkhdBox.Height;
            }
            set
            {
                if (this.TkhdBox != null)
                {
                    this.TkhdBox.Height = value;
                }
            }
        }

        public List<ulong> ChunkOffsets
        {
            get
            {
                Mp4Box box = this.FindChild<Mp4Box>("mdia/minf/stbl/stco");
                if (box != null)
                {
                    Mp4StcoBox box2 = (Mp4StcoBox) box;
                    return box2.Entries.Cast<ulong>().ToList<ulong>();
                }
                box = this.FindChild<Mp4Box>("mdia/minf/stbl/co64");
                if (box == null)
                {
                    throw new Mp4Exception("Chunk offsets not found.");
                }
                Mp4Co64Box box3 = (Mp4Co64Box) box;
                return box3.Entries.ToList<ulong>();
            }
            set
            {
                List<ulong> list = value;
                Mp4Box box = this.FindChild<Mp4Box>("mdia/minf/stbl/stco");
                if (box != null)
                {
                    Mp4StcoBox box2 = (Mp4StcoBox) box;
                    if (box2.Entries.Count > list.Count)
                    {
                        throw new Mp4Exception("Out of range for chunk offsets");
                    }
                    for (int i = 0; i < box2.Entries.Count; i++)
                    {
                        box2.Entries[i] = (uint) list[i];
                    }
                }
                else
                {
                    box = this.FindChild<Mp4Box>("mdia/minf/stbl/co64");
                    if (box == null)
                    {
                        throw new Mp4Exception("Chunk offsets not found.");
                    }
                    Mp4Co64Box box3 = (Mp4Co64Box) box;
                    if (box3.Entries.Count > list.Count)
                    {
                        throw new Mp4Exception("Out of range for chunk offsets");
                    }
                    for (int i = 0; i < box3.Entries.Count; i++)
                    {
                        box3.Entries[i] = list[i];
                    }
                }
            }
        }
    }
}

