namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Mp4CustomSampleTable : Mp4SampleTable
    {
        public const uint CUSTOM_SAMPLE_TABLE_DEFAULT_CHUNK_SIZE = 10;
        private List<Mp4Sample> samples;
        private uint chunkSize;
        private List<SampleDescriptionHolder> sampleDescriptions;

        public Mp4CustomSampleTable() : this(10)
        {
        }

        public Mp4CustomSampleTable(uint chunkSize)
        {
            this.samples = new List<Mp4Sample>();
            this.sampleDescriptions = new List<SampleDescriptionHolder>();
            this.chunkSize = (chunkSize != 0) ? chunkSize : 10;
        }

        public virtual void AddSample(Mp4Stream dataStream, ulong offset, uint size, uint duration, uint descriptionIndex, ulong dts, uint ctsDelta, bool sync)
        {
            if (this.samples.Count > 0)
            {
                Mp4Sample sample = this.samples[this.samples.Count - 1];
                if (dts == 0L)
                {
                    if (sample.Duration == 0)
                    {
                        throw new Exception("INVALID_PARAMETERS");
                    }
                    dts = sample.Dts + sample.Duration;
                }
                else if (sample.Duration == 0)
                {
                    if (dts <= sample.Dts)
                    {
                        throw new Exception("INVALID_PARAMETERS");
                    }
                    sample.Duration = (uint) (dts - sample.Dts);
                }
                else if (dts != (sample.Dts + sample.Duration))
                {
                    throw new Exception("INVALID_PARAMETERS");
                }
            }
            Mp4Sample item = new Mp4Sample(dataStream, (long) offset, (int) size, duration, descriptionIndex, dts, ctsDelta, sync);
            this.samples.Add(item);
        }

        public virtual void AddSampleDescription(Mp4SampleEntry description)
        {
            this.AddSampleDescription(description, true);
        }

        public virtual void AddSampleDescription(Mp4SampleEntry description, bool transferOwnership)
        {
            this.sampleDescriptions.Add(new SampleDescriptionHolder(description, transferOwnership));
        }

        public override uint GetNearestSyncSampleIndex(uint index, bool before)
        {
            if (before)
            {
                for (int j = (int) index; j >= 0; j--)
                {
                    if (this.samples[j].IsSync)
                    {
                        return (uint) j;
                    }
                }
                return 0;
            }
            uint count = (uint) this.samples.Count;
            for (uint i = index; i < count; i++)
            {
                if (this.samples[(int) i].IsSync)
                {
                    return i;
                }
            }
            return (uint) this.samples.Count;
        }

        public override Mp4Sample GetSample(int index) => 
            this.samples[index];

        public override void GetSampleChunkPosition(uint sampleIndex, out uint chunkIndex, out uint positionInChunk)
        {
            chunkIndex = 0;
            positionInChunk = 0;
            if (sampleIndex >= this.samples.Count)
            {
                throw new Exception("OUT_OF_RANGE");
            }
            if (this.chunkSize == 0)
            {
                throw new Exception("INVALID_STATE");
            }
            chunkIndex = sampleIndex / this.chunkSize;
            positionInChunk = sampleIndex % this.chunkSize;
        }

        public override Mp4SampleEntry GetSampleDescription(int index)
        {
            try
            {
                return this.sampleDescriptions[index].SampleDescription;
            }
            catch
            {
                return null;
            }
        }

        public override uint GetSampleIndexForTimeStamp(ulong ts)
        {
            throw new NotSupportedException();
        }

        public override int SampleCount =>
            this.samples.Count;

        public override int SampleDescriptionCount =>
            this.sampleDescriptions.Count;

        private class SampleDescriptionHolder
        {
            public SampleDescriptionHolder(Mp4SampleEntry description, bool isOwned)
            {
                this.SampleDescription = description;
                this.IsOwned = isOwned;
            }

            public Mp4SampleEntry SampleDescription { get; set; }

            public bool IsOwned { get; set; }
        }
    }
}

