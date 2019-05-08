namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public class Mp4SdtpEntry
    {
        public int Reserved;

        public Mp4SdtpEntry(int sampleDependsOn, int sampleIsDependOn, int sampleHasRedundancy)
        {
            this.SampleDependsOn = sampleDependsOn;
            this.SampleIsDependOn = sampleIsDependOn;
            this.SampleHasRedundancy = sampleHasRedundancy;
        }

        public int SampleDependsOn { get; set; }

        public int SampleIsDependOn { get; set; }

        public int SampleHasRedundancy { get; set; }
    }
}

