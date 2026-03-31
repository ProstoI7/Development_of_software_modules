using System.Text;

namespace lab5
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("1. СОЗДАНИЕ ИЕРАРХИИ (COMPOSITE)");

            FolderItem localDrive = new FolderItem("C:");
            FolderItem myDocs = new FolderItem("MyDocuments");
            FolderItem images = new FolderItem("Images");

            FileItem textDoc = new FileItem("report.docx", 1024);
            FileItem photo1 = new FileItem("photo1.jpg", 2048);
            FileItem photo2 = new FileItem("photo2.png", 4096);

            images.Add(photo1);
            images.Add(photo2);
            myDocs.Add(textDoc);
            myDocs.Add(images);
            localDrive.Add(myDocs);

            Console.WriteLine($"Общий размер диска C: {localDrive.GetSize()} байт");
            Console.WriteLine($"Размер папки Images: {images.GetSize()} байт\n");


            // 2
            Console.WriteLine("2. ПОДКЛЮЧЕНИЕ ЧЕРЕЗ АДАПТЕРЫ (ADAPTER)");

            IFileSystem localFS = new FileSystemAdapter(localDrive, "NTFS Локально");

            FolderItem cloudDrive = new FolderItem("GoogleDriveRoot");
            IFileSystem cloudFS = new FileSystemAdapter(cloudDrive, "Google Drive API");


            // 3
            Console.WriteLine("3. ИСПОЛЬЗОВАНИЕ ФАСАДА (FACADE)");

            SyncFacade cloudManager = new SyncFacade(localFS, cloudFS);

            cloudManager.SyncFolder("Images", "CloudImagesBackup");

            cloudManager.Backup("MyDocuments", "CloudDocsBackup");

        }
    }
}

