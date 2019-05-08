namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MpegAudioSampleEntry : Mp4AudioSampleEntry
    {
        public Mp4MpegAudioSampleEntry(uint size, uint type, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, stream, factory)
        {
            this.Esds = base.GetChild<Mp4EsdsBox>(Mp4BoxType.ESDS);
            if ((this.Esds == null) && (base.QtVersion > 0))
            {
                this.Esds = this.FindChild<Mp4EsdsBox>("wave/esds");
            }
        }

        public Mp4EsdsBox Esds { get; private set; }

        public Mp4Mpeg4AudioObjectType Mpeg4AudioObjectType
        {
            get
            {
                Mp4EsDescriptor esDescriptor = this.Esds.EsDescriptor;
                if (esDescriptor != null)
                {
                    Mp4DecoderConfigDescriptor decoderConfigDescriptor = esDescriptor.DecoderConfigDescriptor;
                    if ((decoderConfigDescriptor != null) && (decoderConfigDescriptor.ObjectTypeIndication == Mp4ObjectTypeIndication.MPEG4_AUDIO))
                    {
                        Mp4DecoderSpecificInfoDescriptor decoderSpecificInfoDescriptor = decoderConfigDescriptor.DecoderSpecificInfoDescriptor;
                        if (((decoderSpecificInfoDescriptor != null) && (decoderSpecificInfoDescriptor.Info != null)) && (decoderSpecificInfoDescriptor.Info.Length >= 1))
                        {
                            byte num = (byte) (decoderSpecificInfoDescriptor.Info[0] >> 3);
                            if (num == 0x1f)
                            {
                                if (decoderSpecificInfoDescriptor.Info.Length < 2)
                                {
                                    return (Mp4Mpeg4AudioObjectType) 0;
                                }
                                num = (byte) (0x20 + (((decoderSpecificInfoDescriptor.Info[0] & 7) << 3) | ((decoderSpecificInfoDescriptor.Info[1] & 0xe0) >> 5)));
                            }
                            return (Mp4Mpeg4AudioObjectType) num;
                        }
                    }
                }
                return (Mp4Mpeg4AudioObjectType) 0;
            }
        }
    }
}

