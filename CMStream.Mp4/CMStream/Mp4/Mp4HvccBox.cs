namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class Mp4HvccBox : Mp4Box
    {
        public const byte AVC_PROFILE_BASELINE = 0x42;
        public const byte AVC_PROFILE_MAIN = 0x4d;
        public const byte AVC_PROFILE_EXTENDED = 0x58;
        public const byte AVC_PROFILE_HIGH = 100;
        public const byte AVC_PROFILE_HIGH_10 = 110;
        public const byte AVC_PROFILE_HIGH_422 = 0x7a;
        public const byte AVC_PROFILE_HIGH_444 = 0x90;

        public Mp4HvccBox() : base(15, Mp4BoxType.HVCC)
        {
        }

        public Mp4HvccBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.HVCC)
        {
            long position = stream.Position;
            this.RawBytes = new byte[size];
            stream.Read(this.RawBytes, this.RawBytes.Length);
            stream.Seek(position);
            this.ConfigurationVersion = stream.ReadUInt08();
            byte temp = stream.ReadUInt08();
            GeneralProfileSpace = ((byte)(temp & 0xc0)) >> 6;
            GeneralTierFlag = ((byte)(temp & 0x20)) >> 5;
            GeneralProfileIdc = (byte)(temp & 0x1f);
            GeneralProfileCompatibilityFlags = stream.ReadUInt32();
            GeneralConstraintIndicatorFlags = (ulong)(stream.ReadUInt32()) << 16;
            GeneralConstraintIndicatorFlags += stream.ReadUInt16();
            //GeneralConstraintIndicatorFlags |= (ulong)stream.ReadInt16();
            GeneralLevelIdc = stream.ReadUInt08();
            MinSpatialSegmentationIdc = (uint)(stream.ReadUInt16() & 0xff);
            ParallelismType = (byte)(stream.ReadUInt08() & 0x3);
            ChromaFormat = (byte)(stream.ReadUInt08() & 0x3);
            BitDepthLumaMinus8 = (byte)(stream.ReadUInt08() & 0x7);
            BitDepthChromaMinus8 = (byte)(stream.ReadUInt08() & 0x7);
            avgFrameRate = stream.ReadUInt16();
            temp = stream.ReadUInt08();
            constantFrameRate = ((byte)(temp & 0xc0)) >> 6;
            numTemporalLayers = ((byte)(temp & 0x38)) >> 3;
            temporalIdNested = (byte)(temp & 0x4) >> 2;
            lengthSizeMinusOne = (byte)(temp & 0x3);
            numOfArrays = stream.ReadUInt08();
            for (var i = 0; i < numOfArrays; i++)
            {
                byte ArrayCompleteness, NALUType;
                temp = stream.ReadUInt08();
                ArrayCompleteness = (byte)(temp & 0x80);
                NALUType = (byte)(temp & 0x3f);
                var NumNALUs = stream.ReadUInt16();
                if (NALUType == 0x20)
                {
                    VideoParameters = new List<byte[]>(NumNALUs);
                    for (var j = 0; j < NumNALUs; j++)
                    {
                        var NALULength = stream.ReadUInt16();
                        byte[] buffer = new byte[NALULength];
                        stream.Read(buffer, buffer.Length);
                        VideoParameters.Add(buffer);
                    }
                }
                else if (NALUType == 0x21)
                {
                    SequenceParameters = new List<byte[]>(NumNALUs);
                    for (var j = 0; j < NumNALUs; j++)
                    {
                        var NALULength = stream.ReadUInt16();
                        byte[] buffer = new byte[NALULength];
                        stream.Read(buffer, buffer.Length);
                        SequenceParameters.Add(buffer);
                    }
                }
                else if (NALUType == 0x22)
                {
                    PictureParameters = new List<byte[]>(NumNALUs);
                    for (var j = 0; j < NumNALUs; j++)
                    {
                        var NALULength = stream.ReadUInt16();
                        byte[] buffer = new byte[NALULength];
                        stream.Read(buffer, buffer.Length);
                        PictureParameters.Add(buffer);
                    }
                }
            }
        }

        public Mp4HvccBox(byte configVersion, byte profile, byte level, byte profileCompatibility, byte lengthSize, List<byte[]> sequenceParameters, List<byte[]> pictureParameters) : base(15, Mp4BoxType.AVCC)
        {
            // not impl
        }

        public override void WriteBody(Mp4Stream stream)
        {
            // not impl
            /*stream.WriteUInt08(this.ConfigurationVersion);
            stream.WriteUInt08(this.AVCProfileIndication);
            stream.WriteUInt08(this.AVCCompatibleProfiles);
            stream.WriteUInt08(this.AVCLevelIndication);
            byte num = 0xfc;
            if ((this.NaluLengthSize >= 1) && (this.NaluLengthSize <= 4))
            {
                num = (byte) (num | ((byte) (this.NaluLengthSize - 1)));
            }
            stream.WriteUInt08(num);
            this.NaluLengthSize = (byte) (1 + (num & 3));
            byte num2 = (byte) (this.SequenceParameters.Count & 0x31);
            stream.WriteUInt08((byte) (0xe0 | num2));
            for (int i = 0; i < num2; i++)
            {
                ushort length = (ushort) this.SequenceParameters[i].Length;
                stream.WriteUInt16(length);
                stream.Write(this.SequenceParameters[i], length);
            }
            byte count = (byte) this.PictureParameters.Count;
            stream.WriteUInt08(count);
            for (int j = 0; j < count; j++)
            {
                ushort length = (ushort) this.PictureParameters[j].Length;
                stream.WriteUInt16(length);
                stream.Write(this.PictureParameters[j], length);
            }*/
        }

        public byte ConfigurationVersion { get; private set; }
        public int GeneralProfileSpace { get; private set; }
        public int GeneralTierFlag { get; private set; }
        public byte GeneralProfileIdc { get; private set; }
        public uint GeneralProfileCompatibilityFlags { get; private set; }
        public ulong GeneralConstraintIndicatorFlags { get; private set; }
        public byte GeneralLevelIdc { get; private set; }
        public uint MinSpatialSegmentationIdc { get; private set; }
        public byte ParallelismType { get; private set; }
        public byte ChromaFormat { get; private set; }
        public byte BitDepthLumaMinus8 { get; private set; }
        public byte BitDepthChromaMinus8 { get; private set; }
        public ushort avgFrameRate { get; private set; }
        public int constantFrameRate { get; private set; }
        public int numTemporalLayers { get; private set; }
        public int temporalIdNested { get; private set; }
        public byte lengthSizeMinusOne { get; private set; }
        public byte numOfArrays { get; private set; }
        public List<byte[]> VideoParameters { get; private set; }
        public List<byte[]> SequenceParameters { get; private set; }
        public List<byte[]> PictureParameters { get; private set; }
        public byte[] RawBytes { get; private set; }
    }
}


/*
aligned(8) class HEVCDecoderConfigurationRecord {
	unsigned int(8) configurationVersion = 1;					// 配置版本，固定 0x01
	unsigned int(2) general_profile_space;						// 码流保留的配置空间，0x00 说明整个码流只有这里有配置信息
	unsigned int(1) general_tier_flag;							// tier 等级，Main tier 为 0x00，High tier 为 0x01
	unsigned int(5) general_profile_idc;						// profile 版本，Main 为 0x01，Main10 为 0x02，Main Still Picture 为 0x03
	unsigned int(32) general_profile_compatibility_flags;		// Main 为 0x60000000，Main10 为 0x20000000
	unsigned int(48) general_constraint_indicator_flags;		// 0x900000000000
	unsigned int(8) general_level_idc;							// 1080P都是4.1，4.1:0x7b, 5.1:0x99 ,5.2:0x9c ,6.1: 0xb7, 6.2:0xba
	bit(4) reserved = ‘1111’b;									// 0xf00
	unsigned int(12) min_spatial_segmentation_idc;				// 不知道干嘛的，设 0x00
	bit(6) reserved = ‘111111’b;								// 0xfc
	unsigned int(2) parallelismType;							// 多线程类型？0x00 表示未知
	bit(6) reserved = ‘111111’b;								// 0xfc
	unsigned int(2) chromaFormat;								// 色度取样，0x00：单色，0x01：YUV420，0x02：YUV422，0x03：YUV444
	bit(5) reserved = ‘11111’b;									// 0xf8
	unsigned int(3) bitDepthLumaMinus8;							// 我们都是 8bit 的，填 0x00
	bit(5) reserved = ‘11111’b;									// 0xf8
	unsigned int(3) bitDepthChromaMinus8;						// 我们都是 8bit 的，填 0x00
	bit(16) avgFrameRate;										// 平均帧率*10(0xfa00)，0x00 表示不知道平均帧率
	bit(2) constantFrameRate;									// 0x00 表示不知道是不是固定帧率
	bit(3) numTemporalLayers;									// 0x00 表示不知道支不支持 temporally scalable
	bit(1) temporalIdNested;									// 不知道干嘛的，设 0x00
	unsigned int(2) lengthSizeMinusOne; 						// 记录 NALU type 用的 bit 数  - 1，0x03
	unsigned int(8) numOfArrays;								// 数组的个数，VPS，SPS，PPS，0x03
	for (j=0; j < numOfArrays; j++) {
		bit(1) array_completeness;
		unsigned int(1) reserved = 0;
		unsigned int(6) NAL_unit_type;
		unsigned int(16) numNalus;
		for (i=0; i< numNalus; i++) {
			unsigned int(16) nalUnitLength;
			bit(8*nalUnitLength) nalUnit;
		}
	}
}
*/
