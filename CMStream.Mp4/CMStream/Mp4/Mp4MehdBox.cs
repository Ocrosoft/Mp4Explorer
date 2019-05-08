namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4MehdBox : Mp4FullBox
    {
        public Mp4MehdBox(uint fragmentDuration) : base(0x10, Mp4BoxType.MEHD)
        {
            this.FragmentDuration = fragmentDuration;
        }

        public Mp4MehdBox(uint size, Mp4Stream stream) : base(size, Mp4BoxType.MEHD, 0L, stream)
        {
            if (base.Version == 1)
            {
                this.FragmentDuration = stream.ReadUInt64();
            }
            else
            {
                this.FragmentDuration = stream.ReadUInt32();
            }
        }

        public override void WriteBody(Mp4Stream stream)
        {
            if (base.Version == 1)
            {
                stream.WriteUInt64(this.FragmentDuration);
            }
            else
            {
                stream.WriteUInt32((uint) this.FragmentDuration);
            }
        }

        public ulong FragmentDuration { get; private set; }
    }
}

