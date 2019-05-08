namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Mp4BoxSampleTable : Mp4SampleTable
    {
        public Mp4BoxSampleTable(Mp4ContainerBox stbl, Mp4Stream sampleStream)
        {
            this.SampleStream = sampleStream;
            this.StscAtom = stbl.GetChild<Mp4StscBox>(Mp4BoxType.STSC);
            this.StcoAtom = stbl.GetChild<Mp4StcoBox>(Mp4BoxType.STCO);
            this.StszAtom = stbl.GetChild<Mp4StszBox>(Mp4BoxType.STSZ);
            this.SttsAtom = stbl.GetChild<Mp4SttsBox>(Mp4BoxType.STTS);
            this.CttsAtom = stbl.GetChild<Mp4CttsBox>(Mp4BoxType.CTTS);
            this.StsdAtom = stbl.GetChild<Mp4StsdBox>(Mp4BoxType.STSD);
            this.StssAtom = stbl.GetChild<Mp4StssBox>(Mp4BoxType.STSS);
            this.Co64Atom = stbl.GetChild<Mp4Co64Box>(Mp4BoxType.CO64);
        }

        public override uint GetNearestSyncSampleIndex(uint index, bool before)
        {
            throw new NotImplementedException();
        }

        public override Mp4Sample GetSample(int index)
        {
            ulong chunkOffset;
            Mp4Sample sample = new Mp4Sample();
            if ((this.StcoAtom == null) && (this.Co64Atom == null))
            {
                throw new Exception("INVALID_FORMAT");
            }
            index++;
            this.StscAtom.GetChunkForSample((uint) index, out uint num, out uint num2, out uint num3);
            if (num2 > index)
            {
                throw new Exception("ERROR_INTERNAL");
            }
            try
            {
                if (this.StcoAtom != null)
                {
                    chunkOffset = this.StcoAtom.GetChunkOffset(num);
                }
                else
                {
                    chunkOffset = this.Co64Atom.GetChunkOffset((int) num);
                }
            }
            catch
            {
                return null;
            }
            for (uint i = ((uint) index) - num2; i < index; i++)
            {
                uint num7 = this.StszAtom.GetSampleSize(i);
                chunkOffset += num7;
            }
            sample.DescriptionIndex = num3 - 1;
            uint ctsOffset = 0;
            ulong dts = 0L;
            uint duration = 0;
            this.SttsAtom.GetDts(index, out dts, out duration);
            sample.Duration = duration;
            sample.Dts = dts;
            if (this.CttsAtom == null)
            {
                sample.Cts = dts;
            }
            else
            {
                ctsOffset = this.CttsAtom.GetCtsOffset((uint) index);
                sample.Cts = dts + ctsOffset;
            }
            uint sampleSize = this.StszAtom.GetSampleSize((uint) index);
            sample.Size = (int) sampleSize;
            if (this.StssAtom == null)
            {
                sample.IsSync = true;
            }
            else
            {
                sample.IsSync = this.StssAtom.IsSampleSync((uint) index);
            }
            sample.Offset = (int) chunkOffset;
            sample.DataStream = this.SampleStream;
            return sample;
        }

        public override void GetSampleChunkPosition(uint sampleIndex, out uint chunkIndex, out uint positionInChunk)
        {
            throw new NotImplementedException();
        }

        public override Mp4SampleEntry GetSampleDescription(int index) => 
            this.StsdAtom?.GetSampleEntry(index);

        public override uint GetSampleIndexForTimeStamp(ulong ts)
        {
            throw new NotImplementedException();
        }

        public Mp4Stream SampleStream { get; set; }

        public Mp4StscBox StscAtom { get; set; }

        public Mp4StcoBox StcoAtom { get; set; }

        public Mp4StszBox StszAtom { get; set; }

        public Mp4SttsBox SttsAtom { get; set; }

        public Mp4CttsBox CttsAtom { get; set; }

        public Mp4StsdBox StsdAtom { get; set; }

        public Mp4StssBox StssAtom { get; set; }

        public Mp4Co64Box Co64Atom { get; set; }

        public override int SampleCount
        {
            get
            {
                if (this.StszAtom == null)
                {
                    return 0;
                }
                return (int) this.StszAtom.SampleCount;
            }
        }

        public override int SampleDescriptionCount
        {
            get
            {
                if (this.StsdAtom == null)
                {
                    return 0;
                }
                return this.StsdAtom.Entries.Count;
            }
        }
    }
}

