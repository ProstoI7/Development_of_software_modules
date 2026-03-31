using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public interface IFileSystem
    {
        List<string> ListItems(string path);
        byte[] ReadFile(string path);
        void WriteFile(string path, byte[] data);
        void DeleteItem(string path);
    }

    public class FileSystemAdapter : IFileSystem
    {
        private FileSystemItem _root;
        private string _fsName;

        public FileSystemAdapter(FileSystemItem root, string fsName)
        {
            _root = root;
            _fsName = fsName;
        }

        private FileSystemItem FindItem(FileSystemItem current, string targetName)
        {
            if (current.Name == targetName) return current;

            if (current is FolderItem folder)
            {
                foreach (var child in folder.GetChildren())
                {
                    var found = FindItem(child, targetName);
                    if (found != null) return found;
                }
            }
            return null;
        }

        public List<string> ListItems(string path)
        {
            var item = FindItem(_root, path);
            if (item is FolderItem folder)
            {
                return folder.GetChildren().Select(c => c.Name).ToList();
            }
            return new List<string>();
        }

        public byte[] ReadFile(string path)
        {
            var item = FindItem(_root, path);
            if (item is FileItem file)
            {
                Console.WriteLine($"[{_fsName}] Чтение файла: {file.Name} ({file.GetSize()} байт)");
                return new byte[file.GetSize()];
            }
            throw new Exception($"[{_fsName}] Файл {path} не найден или это папка!");
        }

        public void WriteFile(string path, byte[] data)
        {
            Console.WriteLine($"[{_fsName}] Запись файла '{path}' размером {data.Length} байт.");
        }

        public void DeleteItem(string path)
        {
            Console.WriteLine($"[{_fsName}] Удаление элемента '{path}'.");
        }
    }
}
