namespace CMStream.Mp4
{
    using System;

    public static class Mp4HandlerType
    {
        public static readonly uint SOUN = Mp4Util.Create('s', 'o', 'u', 'n');
        public static readonly uint VIDE = Mp4Util.Create('v', 'i', 'd', 'e');
        public static readonly uint HINT = Mp4Util.Create('h', 'i', 'n', 't');
        public static readonly uint MDIR = Mp4Util.Create('m', 'd', 'i', 'r');
        public static readonly uint TEXT = Mp4Util.Create('t', 'e', 'x', 't');
        public static readonly uint TX3G = Mp4Util.Create('t', 'x', '3', 'g');
        public static readonly uint JPEG = Mp4Util.Create('j', 'p', 'e', 'g');
        public static readonly uint ODSM = Mp4Util.Create('o', 'd', 's', 'm');
        public static readonly uint SDSM = Mp4Util.Create('s', 'd', 's', 'm');
    }
}

