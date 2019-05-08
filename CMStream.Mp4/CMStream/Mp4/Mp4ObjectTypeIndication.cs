namespace CMStream.Mp4
{
    using System;

    public enum Mp4ObjectTypeIndication
    {
        MPEG4_SYSTEM = 1,
        MPEG4_SYSTEM_COR = 2,
        MPEG4_TEXT = 8,
        MPEG4_VISUAL = 0x20,
        MPEG4_AUDIO = 0x40,
        MPEG2_VISUAL_SIMPLE = 0x60,
        MPEG2_VISUAL_MAIN = 0x61,
        MPEG2_VISUAL_SNR = 0x62,
        MPEG2_VISUAL_SPATIAL = 0x63,
        MPEG2_VISUAL_HIGH = 100,
        MPEG2_VISUAL_422 = 0x65,
        MPEG2_AAC_AUDIO_MAIN = 0x66,
        MPEG2_AAC_AUDIO_LC = 0x67,
        MPEG2_AAC_AUDIO_SSRP = 0x68,
        MPEG2_PART3_AUDIO = 0x69,
        MPEG1_VISUAL = 0x6a,
        MPEG1_AUDIO = 0x6b,
        JPEG = 0x6c
    }
}

