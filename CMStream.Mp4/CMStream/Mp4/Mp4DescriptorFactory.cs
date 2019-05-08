namespace CMStream.Mp4
{
    using System;

    public class Mp4DescriptorFactory
    {
        public Mp4Descriptor Read(Mp4Stream stream)
        {
            if ((stream.Length - stream.Position) == 0L)
            {
                return null;
            }
            Mp4Descriptor descriptor = null;
            long position = stream.Position;
            Mp4DescriptorTag tag = (Mp4DescriptorTag) stream.ReadUInt08();
            uint payloadSize = 0;
            uint headerSize = 1;
            uint num4 = 4;
            byte num5 = 0;
            do
            {
                headerSize++;
                num5 = stream.ReadUInt08();
                payloadSize = (payloadSize << 7) + ((uint) (num5 & 0x7f));
            }
            while ((--num4 != 0) && ((num5 & 0x80) != 0));
            switch (tag)
            {
                case Mp4DescriptorTag.OD:
                case Mp4DescriptorTag.MP4_OD:
                    descriptor = new Mp4ObjectDescriptor(stream, tag, headerSize, payloadSize);
                    break;

                case Mp4DescriptorTag.ES:
                    descriptor = new Mp4EsDescriptor(stream, headerSize, payloadSize);
                    break;

                case Mp4DescriptorTag.DECODER_CONFIG:
                    descriptor = new Mp4DecoderConfigDescriptor(stream, headerSize, payloadSize);
                    break;

                case Mp4DescriptorTag.DECODER_SPECIFIC_INFO:
                    descriptor = new Mp4DecoderSpecificInfoDescriptor(stream, headerSize, payloadSize);
                    break;

                case Mp4DescriptorTag.SL_CONFIG:
                    if (payloadSize != 1)
                    {
                        throw new Exception("INVALID_FORMAT");
                    }
                    descriptor = new Mp4SLConfigDescriptor(stream, headerSize, payloadSize);
                    break;

                default:
                    descriptor = new Mp4UnknownDescriptor(stream, tag, headerSize, payloadSize);
                    break;
            }
            stream.Seek((position + headerSize) + payloadSize);
            return descriptor;
        }
    }
}

