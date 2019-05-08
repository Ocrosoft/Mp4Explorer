namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4AudioSampleEntry : Mp4SampleEntry
    {
        private uint sampleRate;
        private ushort channelCount;

        public Mp4AudioSampleEntry(uint size, uint format, Mp4Stream stream, Mp4BoxFactory factory) : base(size, format)
        {
            this.ReadFields(stream);
            uint fieldsSize = this.GetFieldsSize();
            base.ReadChildren(stream, factory, (long) ((size - 8) - fieldsSize));
        }

        public Mp4AudioSampleEntry(uint format, uint sampleRate, ushort sampleSize, ushort channelCount) : base(8, format)
        {
            this.QtVersion = 0;
            this.QtRevision = 0;
            this.QtVendor = 0;
            this.ChannelCount = channelCount;
            this.SampleSize = sampleSize;
            this.QtCompressionId = 0;
            this.QtPacketSize = 0;
            this.SampleRate = sampleRate;
            this.QtV1SamplesPerPacket = 0;
            this.QtV1BytesPerPacket = 0;
            this.QtV1BytesPerFrame = 0;
            this.QtV1BytesPerSample = 0;
            this.QtV2StructSize = 0;
            this.QtV2SampleRate64 = 0.0;
            this.QtV2ChannelCount = 0;
            this.QtV2Reserved = 0;
            this.QtV2BitsPerChannel = 0;
            this.QtV2FormatSpecificFlags = 0;
            this.QtV2BytesPerAudioPacket = 0;
            this.QtV2LPCMFramesPerAudioPacket = 0;
            this.Size += 20;
        }

        public override uint GetFieldsSize()
        {
            uint num = base.GetFieldsSize() + 20;
            if (this.QtVersion == 1)
            {
                return (num + 0x10);
            }
            if (this.QtVersion == 2)
            {
                num += (uint) (0x24 + ((this.QtV2Extension != null) ? this.QtV2Extension.Length : 0));
            }
            return num;
        }

        public override void ReadFields(Mp4Stream stream)
        {
            base.ReadFields(stream);
            this.QtVersion = stream.ReadUInt16();
            this.QtRevision = stream.ReadUInt16();
            this.QtVendor = stream.ReadUInt32();
            this.ChannelCount = stream.ReadUInt16();
            this.SampleSize = stream.ReadUInt16();
            this.QtCompressionId = stream.ReadUInt16();
            this.QtPacketSize = stream.ReadUInt16();
            this.SampleRate = stream.ReadUInt32();
            if (this.QtVersion == 1)
            {
                this.QtV1SamplesPerPacket = stream.ReadUInt32();
                this.QtV1BytesPerPacket = stream.ReadUInt32();
                this.QtV1BytesPerFrame = stream.ReadUInt32();
                this.QtV1BytesPerSample = stream.ReadUInt32();
            }
            else if (this.QtVersion == 2)
            {
                uint num2;
                uint num3;
                this.QtV2StructSize = stream.ReadUInt32();
                this.QtV2SampleRate64 = stream.ReadDouble();
                this.QtV2ChannelCount = stream.ReadUInt32();
                this.QtV2Reserved = stream.ReadUInt32();
                this.QtV2BitsPerChannel = stream.ReadUInt32();
                this.QtV2FormatSpecificFlags = stream.ReadUInt32();
                this.QtV2BytesPerAudioPacket = stream.ReadUInt32();
                this.QtV2LPCMFramesPerAudioPacket = stream.ReadUInt32();
                if (this.QtV2StructSize > 0x48)
                {
                    uint num = this.QtV2StructSize - 0x48;
                    this.QtV2Extension = new byte[num];
                    stream.Read(this.QtV2Extension, (int) num);
                }
                this.QtV1BytesPerSample = num2 = 0;
                this.QtV1BytesPerFrame = num3 = num2;
                this.QtV1SamplesPerPacket = this.QtV1BytesPerPacket = num3;
            }
            else
            {
                this.QtV1SamplesPerPacket = 0;
                this.QtV1BytesPerPacket = 0;
                this.QtV1BytesPerFrame = 0;
                this.QtV1BytesPerSample = 0;
                this.QtV2StructSize = 0;
                this.QtV2SampleRate64 = 0.0;
                this.QtV2ChannelCount = 0;
                this.QtV2Reserved = 0;
                this.QtV2BitsPerChannel = 0;
                this.QtV2FormatSpecificFlags = 0;
                this.QtV2BytesPerAudioPacket = 0;
                this.QtV2LPCMFramesPerAudioPacket = 0;
            }
        }

        public override void WriteInnerBody(Mp4Stream stream)
        {
            base.WriteInnerBody(stream);
            stream.WriteUInt16(this.QtVersion);
            stream.WriteUInt16(this.QtRevision);
            stream.WriteUInt32(this.QtVendor);
            stream.WriteUInt16(this.ChannelCount);
            stream.WriteUInt16(this.SampleSize);
            stream.WriteUInt16(this.QtCompressionId);
            stream.WriteUInt16(this.QtPacketSize);
            stream.WriteUInt32(this.SampleRate);
            if (this.QtVersion == 1)
            {
                stream.WriteUInt32(this.QtV1SamplesPerPacket);
                stream.WriteUInt32(this.QtV1BytesPerPacket);
                stream.WriteUInt32(this.QtV1BytesPerFrame);
                stream.WriteUInt32(this.QtV1BytesPerSample);
            }
            else if (this.QtVersion == 2)
            {
                stream.WriteUInt32(this.QtV2StructSize);
                stream.WriteDouble(this.QtV2SampleRate64);
                stream.WriteUInt32(this.QtV2ChannelCount);
                stream.WriteUInt32(this.QtV2Reserved);
                stream.WriteUInt32(this.QtV2BitsPerChannel);
                stream.WriteUInt32(this.QtV2FormatSpecificFlags);
                stream.WriteUInt32(this.QtV2BytesPerAudioPacket);
                stream.WriteUInt32(this.QtV2LPCMFramesPerAudioPacket);
                if (this.QtV2Extension != null)
                {
                    stream.Write(this.QtV2Extension, this.QtV2Extension.Length);
                }
            }
        }

        public ushort QtVersion { get; private set; }

        public ushort QtRevision { get; private set; }

        public uint QtVendor { get; private set; }

        public ushort ChannelCount
        {
            get
            {
                if (this.QtVersion == 2)
                {
                    return (ushort) this.QtV2ChannelCount;
                }
                return this.channelCount;
            }
            private set => 
                this.channelCount = value;
        }

        public ushort SampleSize { get; private set; }

        public ushort QtCompressionId { get; private set; }

        public ushort QtPacketSize { get; private set; }

        public uint SampleRate
        {
            get
            {
                if (this.QtVersion == 2)
                {
                    return (uint) this.QtV2SampleRate64;
                }
                return this.sampleRate;
            }
            private set
            {
                if (this.QtVersion == 2)
                {
                    this.QtV2SampleRate64 = value;
                }
                else
                {
                    this.sampleRate = value;
                }
            }
        }

        public uint QtV1SamplesPerPacket { get; private set; }

        public uint QtV1BytesPerPacket { get; private set; }

        public uint QtV1BytesPerFrame { get; private set; }

        public uint QtV1BytesPerSample { get; private set; }

        public uint QtV2StructSize { get; private set; }

        public double QtV2SampleRate64 { get; private set; }

        public uint QtV2ChannelCount { get; private set; }

        public uint QtV2Reserved { get; private set; }

        public uint QtV2BitsPerChannel { get; private set; }

        public uint QtV2FormatSpecificFlags { get; private set; }

        public uint QtV2BytesPerAudioPacket { get; private set; }

        public uint QtV2LPCMFramesPerAudioPacket { get; private set; }

        public byte[] QtV2Extension { get; private set; }
    }
}

