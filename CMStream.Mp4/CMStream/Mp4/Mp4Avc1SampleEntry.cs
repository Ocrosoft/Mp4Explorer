namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4Avc1SampleEntry : Mp4VisualSampleEntry
    {
        public Mp4Avc1SampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.AVC1, stream, factory)
        {
            this.AvccBox = base.GetChild<Mp4AvccBox>(Mp4BoxType.AVCC);
        }

        public Mp4Avc1SampleEntry(ushort width, ushort height, ushort depth, string compressorName, Mp4AvccBox avcc) : base(Mp4BoxType.AVC1, width, height, depth, compressorName)
        {
            base.AddChild(avcc);
            this.AvccBox = avcc;
            this.Size += avcc.Size + 8;
        }

        public Mp4AvccBox AvccBox { get; private set; }

        public byte Profile =>
            this.AvccBox.AVCProfileIndication;

        public byte ProfileCompatibility =>
            this.AvccBox.AVCCompatibleProfiles;

        public byte Level =>
            this.AvccBox.AVCLevelIndication;

        public byte NaluLengthSize =>
            this.AvccBox.NaluLengthSize;
    }
}

