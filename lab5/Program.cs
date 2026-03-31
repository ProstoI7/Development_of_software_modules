using System.Text;

namespace lab5
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("ЭТАП 1. Паттерн «Компоновщик»\n");

            FileItem file1 = new FileItem("document.txt", 1024);
            FileItem file2 = new FileItem("photo.jpg", 5120);
            FileItem file3 = new FileItem("video.mp4", 20480);

            FolderItem root = new FolderItem("Root");
            FolderItem docs = new FolderItem("Docs");
            FolderItem media = new FolderItem("Media");

            FolderItem albums = new FolderItem("Albums");

            docs.Add(file1);
            media.Add(file2);
            media.Add(file3);
            media.Add(albums);

            root.Add(docs);
            root.Add(media);

            Console.WriteLine("РАЗМЕРЫ ЭЛЕМЕНТОВ ФАЙЛОВОЙ СИСТЕМЫ:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Размер файла 'video.mp4': {file3.GetSize()} байт");
            Console.WriteLine($"Размер папки 'Media': {media.GetSize()} байт");
            Console.WriteLine($"Размер папки 'Docs': {docs.GetSize()} байт");
            Console.WriteLine($"Размер КОРНЕВОЙ ПАПКИ: {root.GetSize()} байт\n");


            Console.WriteLine("\nЭТАП 2. Паттерн «Адаптер»\n");

            IFileSystem localFS = new FileSystemAdapter(root, "Локальный Диск");

            Console.WriteLine("1. Метод ListItems - содержимое папки 'Media':");
            foreach (var item in localFS.ListItems("Media"))
            {
                Console.WriteLine($"     {item}");
            }

            Console.WriteLine("\n2. Метод ReadFile - чтение файла:");
            byte[] fileData = localFS.ReadFile("video.mp4");

            Console.WriteLine("\n3. Метод WriteFile - запись файла:");
            localFS.WriteFile("newfile.txt", new byte[] { 1, 2, 3 });

            Console.WriteLine("\n4. Метод DeleteItem - удаление элемента:");
            localFS.DeleteItem("document.txt");


            Console.WriteLine("\n\nЭТАП 3. Паттерн «Фасад»\n");

            FolderItem cloudRoot = new FolderItem("CloudRoot");
            IFileSystem cloudFS = new FileSystemAdapter(cloudRoot, "Облако");

            SyncFacade facade = new SyncFacade(localFS, cloudFS);

            Console.WriteLine("ДЕМОНСТРАЦИЯ 1: Синхронизация папки 'Media' в облако");
            facade.SyncFolder("Media", "CloudMedia");

            Console.WriteLine("ДЕМОНСТРАЦИЯ 2: Резервное копирование папки 'Docs'");
            facade.Backup("Docs", "CloudDocsBackup");

        }
    }

}

