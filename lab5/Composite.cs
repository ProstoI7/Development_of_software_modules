using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public abstract class FileSystemItem
    {
        public string Name { get; set; }

        protected FileSystemItem(string name)
        {
            Name = name;
        }

        public abstract long GetSize();
        public abstract void Add(FileSystemItem item);
        public abstract void Remove(FileSystemItem item);
        public abstract FileSystemItem GetChild(int index);
    }

    public class FileItem : FileSystemItem
    {
        private long _size;

        public FileItem(string name, long size) : base(name)
        {
            _size = size;
        }

        public override long GetSize()
        {
            return _size;
        }

        public override void Add(FileSystemItem item) => throw new InvalidOperationException("К файлу нельзя добавить элементы.");
        public override void Remove(FileSystemItem item) => throw new InvalidOperationException("Из файла нельзя удалить элементы.");
        public override FileSystemItem GetChild(int index) => throw new InvalidOperationException("У файла нет дочерних элементов.");
    }

    public class FolderItem : FileSystemItem
    {
        private List<FileSystemItem> _children = new List<FileSystemItem>();

        public FolderItem(string name) : base(name) { }

        public override long GetSize()
        {
            long totalSize = 0;
            foreach (var child in _children)
            {
                totalSize += child.GetSize();
            }
            return totalSize;
        }

        public override void Add(FileSystemItem item) => _children.Add(item);
        public override void Remove(FileSystemItem item) => _children.Remove(item);
        public override FileSystemItem GetChild(int index) => _children[index];

        public IReadOnlyList<FileSystemItem> GetChildren() => _children.AsReadOnly();
    }
}
