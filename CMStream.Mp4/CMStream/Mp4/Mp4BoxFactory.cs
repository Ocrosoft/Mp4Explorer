namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;

    public class Mp4BoxFactory
    {
        private Stack<uint> contextStack = new Stack<uint>();

        public uint GetContext()
        {
            if (this.contextStack.Count > 0)
            {
                return this.contextStack.Peek();
            }
            return 0;
        }

        public void PopContext()
        {
            this.contextStack.Pop();
        }

        public void PushContext(uint context)
        {
            this.contextStack.Push(context);
        }

        public virtual Mp4Box Read(Mp4Stream stream)
        {
            long bytesAvailable = stream.Length - stream.Position;
            return this.Read(stream, ref bytesAvailable);
        }

        public virtual Mp4Box Read(Mp4Stream stream, ref long bytesAvailable)
        {
            Mp4Box box = null;
            if (bytesAvailable < 8L)
            {
                return null;
            }
            long position = stream.Position;
            uint size = stream.ReadUInt32();
            ulong largeSize = 0L;
            ulong num5 = size;
            uint type = stream.ReadUInt32();
            long num2 = position + size;
            switch (num5)
            {
                case 0L:
                    if (stream.Length >= position)
                    {
                        num5 = (ulong) (stream.Length - position);
                    }
                    num2 = (long) (((ulong) position) + num5);
                    break;

                case 1L:
                    if (bytesAvailable < 0x10L)
                    {
                        stream.Seek(position);
                        throw new Exception("INVALID_FORMAT");
                    }
                    largeSize = stream.ReadUInt64();
                    num5 = largeSize;
                    num2 = (long) (((ulong) position) + largeSize);
                    break;
            }
            if (((num5 > 0L) && (num5 < 8L)) || ((long)num5 > bytesAvailable))
            {
                stream.Seek(position);
                throw new Exception("INVALID_FORMAT");
            }
            if (type == Mp4BoxType.MOOV)
            {
                box = new Mp4MoovBox(size, largeSize, stream, this);
            }
            else if (type == Mp4BoxType.MVHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4MvhdBox(size, stream);
            }
            else if (type == Mp4BoxType.MFHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4MfhdBox(size, stream);
            }
            else if (type == Mp4BoxType.TRAK)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4TrakBox(size, stream, this);
            }
            else if (type == Mp4BoxType.HDLR)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4HdlrBox(size, stream);
            }
            else if (type == Mp4BoxType.TKHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4TkhdBox(size, stream);
            }
            else if (type == Mp4BoxType.TFHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4TfhdBox(size, stream);
            }
            else if (type == Mp4BoxType.TRUN)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4TrunBox(size, stream);
            }
            else if (type == Mp4BoxType.MDHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4MdhdBox(size, stream);
            }
            else if (type == Mp4BoxType.STSD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4StsdBox(size, stream, this);
            }
            else if (type == Mp4BoxType.STSC)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4StscBox(size, stream);
            }
            else if (type == Mp4BoxType.STCO)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4StcoBox(size, stream);
            }
            else if (type == Mp4BoxType.CO64)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4Co64Box(size, stream);
            }
            else if (type == Mp4BoxType.STSZ)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4StszBox(size, stream);
            }
            else if (type == Mp4BoxType.STTS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4SttsBox(size, stream);
            }
            else if (type == Mp4BoxType.CTTS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4CttsBox(size, stream);
            }
            else if (type == Mp4BoxType.STSS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4StssBox(size, stream);
            }
            else if (type == Mp4BoxType.IODS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4IodsBox(size, stream);
            }
            else if (type == Mp4BoxType.ESDS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4EsdsBox(size, stream);
            }
            else if (type == Mp4BoxType.MP4S)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4Mp4sSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.ENCA)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4EncaSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.ENCV)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4EncvSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.DRMS)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4DrmsSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.DRMI)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4DrmiSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.MP4A)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4Mp4aSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.MP4V)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4Mp4vSampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.AVC1)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4Avc1SampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.HVC1 || type == Mp4BoxType.HEV1)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4Hvc1SampleEntry(size, stream, this);
                }
            }
            else if (type == Mp4BoxType.ALAC)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                if (this.GetContext() == Mp4BoxType.STSD)
                {
                    box = new Mp4AudioSampleEntry(size, Mp4BoxType.ALAC, stream, this);
                }
            }
            else if (type == Mp4BoxType.AVCC)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4AvccBox(size, stream);
            }
            else if (type == Mp4BoxType.HVCC)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4HvccBox(size, stream);
            }
            else if (type == Mp4BoxType.UUID)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4UuidBox(size, stream);
            }
            else if (type == Mp4BoxType._DI8)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp48idBox(size, stream);
            }
            else if (type == Mp4BoxType.LDB8)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp48bdlBox(size, stream);
            }
            else if (type == Mp4BoxType.DREF)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4DrefBox(size, stream, this);
            }
            else if (type == Mp4BoxType.URL_)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4UrlBox(size, stream);
            }
            else if (type == Mp4BoxType.ELST)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4ElstBox(size, stream);
            }
            else if (type == Mp4BoxType.VMHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4VmhdBox(size, stream);
            }
            else if (type == Mp4BoxType.SMHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4SmhdBox(size, stream);
            }
            else if (type == Mp4BoxType.NMHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4NmhdBox(size, stream);
            }
            else if (type == Mp4BoxType.HMHD)
            {
                if (size == 1)
                {
                    throw new Exception("INVALID_FORMAT");
                }
                box = new Mp4HmhdBox(size, stream);
            }
            else if ((type != Mp4BoxType.FRMA) && (type != Mp4BoxType.SCHM))
            {
                if (type == Mp4BoxType.FTYP)
                {
                    if (size == 1)
                    {
                        throw new Exception("Invalid size");
                    }
                    box = new Mp4FtypBox(size, stream);
                }
                else if (type == Mp4BoxType.RTP_)
                {
                    if (size == 1)
                    {
                        throw new Exception("INVALID_FORMAT");
                    }
                    if (this.GetContext() == Mp4BoxType.STSD)
                    {
                        box = new Mp4RtpHintSampleEntry(size, stream, this);
                    }
                    else
                    {
                        box = new Mp4RtpBox(size, stream);
                    }
                }
                else if (type == Mp4BoxType.TIMS)
                {
                    if (size == 1)
                    {
                        throw new Exception("INVALID_FORMAT");
                    }
                    box = new Mp4TimsBox(size, stream);
                }
                else if (type == Mp4BoxType.SDP_)
                {
                    if (size == 1)
                    {
                        throw new Exception("INVALID_FORMAT");
                    }
                    box = new Mp4SdpBox(size, stream);
                }
                else if (((((type != Mp4BoxType.IKMS) && (type != Mp4BoxType.ISFM)) && ((type != Mp4BoxType.ISLT) && (type != Mp4BoxType.ODHE))) && (((type != Mp4BoxType.OHDR) && (type != Mp4BoxType.ODDA)) && ((type != Mp4BoxType.ODAF) && (type != Mp4BoxType.GRPI)))) && (type != Mp4BoxType.IPRO))
                {
                    if ((((type == Mp4BoxType.HINT) || (type == Mp4BoxType.CDSC)) || ((type == Mp4BoxType.CDSC) || (type == Mp4BoxType.SYNC))) || (((type == Mp4BoxType.MPOD) || (type == Mp4BoxType.DPND)) || (((type == Mp4BoxType.IPIR) || (type == Mp4BoxType.ALIS)) || (type == Mp4BoxType.CHAP))))
                    {
                        if (this.GetContext() == Mp4BoxType.TREF)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4TrefTypeBox(type, size, stream);
                        }
                    }
                    else if ((((type == Mp4BoxType.HNTI) || (type == Mp4BoxType.SCHI)) || ((type == Mp4BoxType.SINF) || (type == Mp4BoxType.UDTA))) || (((type == Mp4BoxType.ILST) || (type == Mp4BoxType.MDRI)) || (type == Mp4BoxType.WAVE)))
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4ContainerBox(size, type, largeSize, stream, this);
                    }
                    else if (type == Mp4BoxType.TREF)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4TrefBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.DINF)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4DinfBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.MEHD)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4MehdBox(size, stream);
                    }
                    else if (type == Mp4BoxType.TREX)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4TrexBox(size, stream);
                    }
                    else if (type == Mp4BoxType.MVEX)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4MvexBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.EDTS)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4EdtsBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.MOOF)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4MoofBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.TRAF)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4TrafBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.MFRA)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4MfraBox(size, stream, this);
                    }
                    else if (type == Mp4BoxType.MDIA)
                    {
                        if (size == 1)
                        {
                            throw new Exception("INVALID_FORMAT");
                        }
                        box = new Mp4MdiaBox(size, stream, this);
                    }
                    else if ((type == Mp4BoxType.ODRM) || (type == Mp4BoxType.ODKM))
                    {
                        box = new Mp4ContainerBox(size, type, largeSize, stream, this);
                    }
                    else if (type != Mp4BoxType.WIDE)
                    {
                        if (type == Mp4BoxType.MDAT)
                        {
                            box = new Mp4MdatBox(size, largeSize, stream);
                        }
                        else if ((type == Mp4BoxType.FREE) || (type == Mp4BoxType.SKIP))
                        {
                            box = new Mp4FreeBox(size, largeSize, stream);
                        }
                        else if (type == Mp4BoxType.NAME)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4NameBox(size, stream);
                        }
                        else if (type == Mp4BoxType.TFRA)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4TfraBox(size, stream);
                        }
                        else if (type == Mp4BoxType.MFRO)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4MfroBox(size, stream);
                        }
                        else if (type == Mp4BoxType.PDIN)
                        {
                            if (size == 1)
                            {
                                throw new Exception("Invalid size");
                            }
                            box = new Mp4PdinBox(size, stream);
                        }
                        else if (type == Mp4BoxType.STBL)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4StblBox(size, stream, this);
                        }
                        else if (type == Mp4BoxType.MINF)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4MinfBox(size, stream, this);
                        }
                        else if (type == Mp4BoxType.SDTP)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4SdtpBox(size, stream);
                        }
                        else if (type == Mp4BoxType.META)
                        {
                            if (size == 1)
                            {
                                throw new Exception("INVALID_FORMAT");
                            }
                            box = new Mp4MetaBox(size, stream, this);
                        }
                    }
                }
            }
            if (box == null)
            {
                uint num7 = 8;
                if (num5 == 1L)
                {
                    num7 += 8;
                }
                stream.Seek(position + num7);
                box = new Mp4UnknownBox(size, type, largeSize, stream);
            }
            bytesAvailable -= (long)num5;
            stream.Seek(num2);
            return box;
        }
    }
}

