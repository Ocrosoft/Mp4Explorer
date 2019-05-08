namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class Mp4SampleTable
    {
        protected Mp4SampleTable()
        {
        }

        public virtual Mp4ContainerBox GenerateStblAtom()
        {
            Mp4ContainerBox box = new Mp4ContainerBox(8, Mp4BoxType.STBL);
            Mp4StsdBox child = new Mp4StsdBox(this);
            Mp4StszBox box3 = new Mp4StszBox();
            Mp4StscBox box4 = new Mp4StscBox();
            Mp4SttsBox box5 = new Mp4SttsBox();
            Mp4StssBox box6 = new Mp4StssBox();
            Mp4CttsBox box7 = null;
            uint num = 0;
            uint num2 = 0;
            ulong item = 0L;
            uint samplesPerChunk = 0;
            uint descriptionIndex = 0;
            uint sampleDelta = 0;
            uint sampleCount = 0;
            uint sampleOffset = 0;
            uint num9 = 0;
            List<ulong> list = new List<ulong>();
            bool flag = false;
            int num10 = this.SampleCount;
            for (int i = 0; i < num10; i++)
            {
                Mp4Sample sample = this.GetSample(i);
                uint duration = sample.Duration;
                if ((duration != sampleDelta) && (sampleCount != 0))
                {
                    box5.Entries.Add(new Mp4SttsEntry(sampleCount, sampleDelta));
                    sampleCount = 0;
                }
                sampleCount++;
                sampleDelta = duration;
                uint ctsDelta = sample.CtsDelta;
                if ((ctsDelta != sampleOffset) && (num9 != 0))
                {
                    if (box7 == null)
                    {
                        box7 = new Mp4CttsBox();
                    }
                    box7.Entries.Add(new Mp4CttsEntry(num9, sampleOffset));
                    num9 = 0;
                }
                num9++;
                sampleOffset = ctsDelta;
                descriptionIndex = sample.DescriptionIndex;
                box3.Entries.Add((uint) sample.Size);
                if (sample.IsSync)
                {
                    box6.Entries.Add((uint) (i + 1));
                    if (i == 0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
                uint chunkIndex = 0;
                uint positionInChunk = 0;
                this.GetSampleChunkPosition((uint) i, out chunkIndex, out positionInChunk);
                if ((chunkIndex != num) && (samplesPerChunk != 0))
                {
                    list.Add(item);
                    item += num2;
                    box4.Entries.Add(new Mp4StscEntry(1, samplesPerChunk, descriptionIndex + 1));
                    samplesPerChunk = 0;
                    num2 = 0;
                }
                num = chunkIndex;
                num2 += (uint) sample.Size;
                samplesPerChunk++;
            }
            box5.Entries.Add(new Mp4SttsEntry(sampleCount, sampleDelta));
            if (box7 != null)
            {
                if (num9 == 0)
                {
                    throw new Exception("ASSERT(current_cts_delta_run != 0)");
                }
                box7.Entries.Add(new Mp4CttsEntry(num9, sampleOffset));
            }
            if (samplesPerChunk != 0)
            {
                list.Add(item);
                box4.Entries.Add(new Mp4StscEntry(1, samplesPerChunk, descriptionIndex + 1));
            }
            box.AddChild(child);
            box.AddChild(box3);
            box.AddChild(box4);
            box.AddChild(box5);
            if (box7 != null)
            {
                box.AddChild(box7);
            }
            if (!flag && (box6.Entries.Count != 0))
            {
                box.AddChild(box6);
            }
            uint count = (uint) list.Count;
            if (item <= 0xffffffffL)
            {
                uint[] entries = new uint[count];
                for (uint j = 0; j < count; j++)
                {
                    entries[j] = (uint) list[(int) j];
                }
                Mp4StcoBox box8 = new Mp4StcoBox(entries);
                box.AddChild(box8);
                return box;
            }
            Mp4Co64Box box9 = new Mp4Co64Box(list.ToArray());
            box.AddChild(box9);
            return box;
        }

        public uint GetNearestSyncSampleIndex(uint index) => 
            this.GetNearestSyncSampleIndex(index, true);

        public abstract uint GetNearestSyncSampleIndex(uint index, bool before);
        public abstract Mp4Sample GetSample(int sampleIndex);
        public abstract void GetSampleChunkPosition(uint sampleIndex, out uint chunkIndex, out uint positionInChunk);
        public abstract Mp4SampleEntry GetSampleDescription(int index);
        public abstract uint GetSampleIndexForTimeStamp(ulong ts);

        public abstract int SampleCount { get; }

        public abstract int SampleDescriptionCount { get; }
    }
}

