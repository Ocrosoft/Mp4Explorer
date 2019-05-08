namespace CMStream.Mp4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Mp4ContainerBox : Mp4Box, IMp4ContainerBox
    {
        public const int BOX_LIST_WRITER_MAX_PADDING = 0x400;

        public Mp4ContainerBox(uint size, uint type) : this(size, type, 0L)
        {
        }

        public Mp4ContainerBox(uint size, uint type, ulong largeSize) : base(size, type, largeSize)
        {
            this.Children = new List<Mp4Box>();
        }

        public Mp4ContainerBox(uint size, uint type, ulong largeSize, Mp4Stream stream, Mp4BoxFactory factory) : base(size, type, largeSize)
        {
            if (this.HeaderSize == 12)
            {
                stream.Position += 4L;
            }
            this.ReadChildren(stream, factory, size - this.HeaderSize);
        }

        public void AddChild(Mp4Box child)
        {
            this.AddChild(child, -1);
        }

        public void AddChild(Mp4Box child, int position)
        {
            if (child.Parent != null)
            {
                throw new Mp4Exception("Child already have a parent");
            }
            if (position == -1)
            {
                this.Children.Add(child);
            }
            else
            {
                this.Children.Insert(position, child);
            }
            child.Parent = this;
            if (this.Size != 1)
            {
                this.Size += (child.Size != 1) ? child.Size : ((uint) child.LargeSize);
            }
            else
            {
                this.LargeSize += (child.Size != 1) ? ((ulong) child.Size) : ((ulong) ((uint) child.LargeSize));
            }
        }

        public T GetChild<T>(uint type) where T: Mp4Box => 
            (T)Enumerable.FirstOrDefault<Mp4Box>(this.Children, b => b.Type == type);

        public void ReadChildren(Mp4Stream stream, Mp4BoxFactory factory, long size)
        {
            this.Children = new List<Mp4Box>();
            Mp4Box item = null;
            long bytesAvailable = size;
            factory.PushContext(base.Type);
            while ((bytesAvailable > 0L) && ((item = factory.Read(stream, ref bytesAvailable)) != null))
            {
                item.Parent = this;
                this.Children.Add(item);
            }
            factory.PopContext();
        }

        public override void WriteBody(Mp4Stream stream)
        {
            this.WriteInnerBody(stream);
            foreach (Mp4Box box in this.Children)
            {
                long position = stream.Position;
                box.Write(stream);
                long num3 = stream.Position - position;
                if (num3 > box.Size)
                {
                    throw new Mp4Exception("Written more bytes than box size.");
                }
                if (num3 < box.Size)
                {
                    throw new Mp4Exception("Written fewer bytes than box size.");
                }
            }
        }

        public virtual void WriteInnerBody(Mp4Stream stream)
        {
        }

        public List<Mp4Box> Children { get; set; }
    }
}

