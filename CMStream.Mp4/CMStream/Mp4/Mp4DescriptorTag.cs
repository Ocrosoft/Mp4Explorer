namespace CMStream.Mp4
{
    using System;

    public enum Mp4DescriptorTag
    {
        OD = 1,
        IOD = 2,
        MP4_OD = 0x11,
        MP4_IOD = 0x10,
        ES = 3,
        ES_ID_INC = 14,
        ES_ID_REF = 15,
        DECODER_CONFIG = 4,
        DECODER_SPECIFIC_INFO = 5,
        SL_CONFIG = 6,
        IPMP_DESCRIPTOR_POINTER = 10,
        IPMP_DESCRIPTOR = 11
    }
}

