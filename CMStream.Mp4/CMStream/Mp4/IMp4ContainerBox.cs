namespace CMStream.Mp4
{
    using System;

    public interface IMp4ContainerBox
    {
        T GetChild<T>(uint type) where T: Mp4Box;

        uint Type { get; set; }
    }
}

