namespace CMStream.Mp4
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Mp4BoxExtensions
    {
        public static T FindChild<T>(this IMp4ContainerBox container, string path) where T: Mp4Box
        {
            IMp4ContainerBox box = container;
            while (path.Length >= 4)
            {
                string str;
                if (path.Length == 4)
                {
                    str = null;
                }
                else if (path[4] == '/')
                {
                    str = path.Substring(5);
                }
                else
                {
                    return default(T);
                }
                uint type = Mp4Util.Create(path[0], path[1], path[2], path[3]);
                Mp4Box child = box.GetChild<Mp4Box>(type);
                if (child == null)
                {
                    return default(T);
                }
                if (str != null)
                {
                    path = str;
                    box = child as IMp4ContainerBox;
                    if (box == null)
                    {
                        return default(T);
                    }
                }
                else
                {
                    return (T) child;
                }
            }
            return default(T);
        }
    }
}

