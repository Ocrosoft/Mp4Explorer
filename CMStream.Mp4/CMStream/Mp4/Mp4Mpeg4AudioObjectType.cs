namespace CMStream.Mp4
{
    using System;

    public enum Mp4Mpeg4AudioObjectType
    {
        AAC_MAIN = 1,
        AAC_LC = 2,
        AAC_SSR = 3,
        AAC_LTP = 4,
        SBR = 5,
        AAC_SCALABLE = 6,
        TWINVQ = 7,
        ER_AAC_LC = 0x11,
        ER_AAC_LTP = 0x13,
        ER_AAC_SCALABLE = 20,
        ER_TWINVQ = 0x15,
        ER_BSAC = 0x16,
        ER_AAC_LD = 0x17,
        LAYER_1 = 0x20,
        LAYER_2 = 0x21,
        LAYER_3 = 0x22
    }
}

