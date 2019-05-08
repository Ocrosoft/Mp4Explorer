namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4Track
    {
        public const uint TRACK_DEFAULT_MOVIE_TIMESCALE = 0x3e8;
        public const uint TRACK_FLAG_ENABLED = 1;
        public const uint TRACK_FLAG_IN_MOVIE = 2;
        public const uint TRACK_FLAG_IN_PREVIEW = 4;

        public Mp4Track(Mp4TrakBox box, Mp4Stream stream, uint movieTimeScale)
        {
            this.TrakBox = box;
            Mp4HdlrBox box2 = box.FindChild<Mp4HdlrBox>("mdia/hdlr");
            if (box2 != null)
            {
                uint handlerType = box2.HandlerType;
                if (handlerType == Mp4HandlerType.SOUN)
                {
                    this.Type = Mp4TrackType.AUDIO;
                }
                else if (handlerType == Mp4HandlerType.VIDE)
                {
                    this.Type = Mp4TrackType.VIDEO;
                }
                else if (handlerType == Mp4HandlerType.HINT)
                {
                    this.Type = Mp4TrackType.HINT;
                }
                else if ((handlerType == Mp4HandlerType.ODSM) || (handlerType == Mp4HandlerType.SDSM))
                {
                    this.Type = Mp4TrackType.SYSTEM;
                }
                else if ((handlerType == Mp4HandlerType.TEXT) || (handlerType == Mp4HandlerType.TX3G))
                {
                    this.Type = Mp4TrackType.TEXT;
                }
                else if (handlerType == Mp4HandlerType.JPEG)
                {
                    this.Type = Mp4TrackType.JPEG;
                }
            }
            Mp4ContainerBox stbl = box.FindChild<Mp4ContainerBox>("mdia/minf/stbl");
            if (stbl != null)
            {
                this.SampleTable = new Mp4BoxSampleTable(stbl, stream);
            }
        }

        public Mp4Track(Mp4TrackType type, Mp4SampleTable sampleTable, uint trackId, DateTime creationTime, DateTime modificationTime, ulong trackDuration, uint mediaTimeScale, ulong mediaDuration, string language, uint width, uint height)
        {
            uint sOUN;
            string str;
            this.Type = type;
            this.SampleTable = sampleTable;
            uint num = 0;
            if (type == Mp4TrackType.AUDIO)
            {
                num = 0x100;
            }
            switch (type)
            {
                case Mp4TrackType.AUDIO:
                    sOUN = Mp4HandlerType.SOUN;
                    str = "CM Stream MP4 Sound Handler";
                    break;

                case Mp4TrackType.VIDEO:
                    sOUN = Mp4HandlerType.VIDE;
                    str = "CM Stream MP4 Video Handler";
                    break;

                case Mp4TrackType.HINT:
                    sOUN = Mp4HandlerType.HINT;
                    str = "CM Stream MP4 Hint Handler";
                    break;

                case Mp4TrackType.TEXT:
                    sOUN = Mp4HandlerType.TEXT;
                    str = "CM Stream MP4 Text Handler";
                    break;

                default:
                    sOUN = 0;
                    str = null;
                    break;
            }
            this.TrakBox = new Mp4TrakBox(sampleTable, sOUN, str, trackId, creationTime, modificationTime, trackDuration, mediaTimeScale, mediaDuration, (ushort) num, language, width, height);
        }

        public Mp4Sample GetSample(int index) => 
            this.SampleTable?.GetSample(index);

        public Mp4SampleEntry GetSampleDescription(int index) => 
            this.SampleTable?.GetSampleDescription(index);

        public Mp4TrakBox TrakBox { get; set; }

        public Mp4TrackType Type { get; set; }

        public Mp4SampleTable SampleTable { get; set; }

        public uint Flags
        {
            get
            {
                if (this.TrakBox != null)
                {
                    Mp4TkhdBox box = this.TrakBox.FindChild<Mp4TkhdBox>("tkhd");
                    if (box != null)
                    {
                        return box.Flags;
                    }
                }
                return 0;
            }
        }

        public uint Id
        {
            get => 
                this.TrakBox.Id;
            set => 
                this.TrakBox.Id = value;
        }

        public uint HandlerType
        {
            get
            {
                if (this.TrakBox != null)
                {
                    Mp4HdlrBox box = this.TrakBox.FindChild<Mp4HdlrBox>("mdia/hdlr");
                    if (box != null)
                    {
                        return box.HandlerType;
                    }
                }
                return 0;
            }
        }

        public ulong Duration =>
            this.TrakBox.Duration;

        public int SampleCount
        {
            get
            {
                if (this.SampleTable == null)
                {
                    return 0;
                }
                return this.SampleTable.SampleCount;
            }
        }

        public int SampleDescriptionCount
        {
            get
            {
                if (this.SampleTable == null)
                {
                    return 0;
                }
                return this.SampleTable.SampleDescriptionCount;
            }
        }

        public uint MediaTimeScale
        {
            get
            {
                if (this.TrakBox == null)
                {
                    return 0;
                }
                return this.TrakBox.MediaTimeScale;
            }
        }

        public ulong MediaDuration
        {
            get
            {
                if (this.TrakBox == null)
                {
                    return 0L;
                }
                return this.TrakBox.MediaDuration;
            }
        }

        public string TrackLanguage
        {
            get
            {
                Mp4MdhdBox box = this.TrakBox.FindChild<Mp4MdhdBox>("mdia/mdhd");
                if (box != null)
                {
                    return box.Language;
                }
                return null;
            }
        }

        public uint Width =>
            this.TrakBox.Width;

        public uint Height =>
            this.TrakBox.Height;
    }
}

