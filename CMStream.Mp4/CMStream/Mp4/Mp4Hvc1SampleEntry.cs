namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4Hvc1SampleEntry : Mp4VisualSampleEntry
    {
        public Mp4Hvc1SampleEntry(uint size, Mp4Stream stream, Mp4BoxFactory factory) : base(size, Mp4BoxType.HVC1, stream, factory)
        {
            this.HvccBox = base.GetChild<Mp4HvccBox>(Mp4BoxType.HVCC);
        }

        public Mp4Hvc1SampleEntry(ushort width, ushort height, ushort depth, string compressorName, Mp4HvccBox hvcc) : base(Mp4BoxType.HVC1, width, height, depth, compressorName)
        {
            base.AddChild(hvcc);
            this.HvccBox = hvcc;
            this.Size += hvcc.Size + 8;
        }

        public Mp4HvccBox HvccBox { get; private set; }

        /*public byte Profile =>
            this.HvccBox.AVCProfileIndication;

        public byte ProfileCompatibility =>
            this.HvccBox.HVCCompatibleProfiles;

        public byte Level =>
            this.HvccBox.AVCLevelIndication;

        public byte NaluLengthSize =>
            this.HvccBox.NaluLengthSize;*/
    }
}

