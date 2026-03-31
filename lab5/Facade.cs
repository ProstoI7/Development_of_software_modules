using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class SyncFacade
    {
        private IFileSystem _sourceFS;
        private IFileSystem _targetFS;

        public SyncFacade(IFileSystem source, IFileSystem target)
        {
            _sourceFS = source;
            _targetFS = target;
        }

        public void SyncFolder(string sourcePath, string targetPath)
        {
            Console.WriteLine($"\n--- ЗАПУСК СИНХРОНИЗАЦИИ: [{sourcePath}] -> [{targetPath}] ---");

            var items = _sourceFS.ListItems(sourcePath);
            if (items.Count == 0)
            {
                Console.WriteLine("Исходная папка пуста или не найдена.");
                return;
            }

            foreach (var item in items)
            {
                try
                {
                    byte[] data = _sourceFS.ReadFile(item);
                    _targetFS.WriteFile(item, data);
                }
                catch
                {
                    Console.WriteLine($"Пропуск '{item}' (является вложенной папкой).");
                }
            }
            Console.WriteLine("--- СИНХРОНИЗАЦИЯ ЗАВЕРШЕНА ---\n");
        }

        public void Backup(string sourcePath, string backupPath)
        {
            Console.WriteLine($"\n--- ЗАПУСК РЕЗЕРВНОГО КОПИРОВАНИЯ ---");
            Console.WriteLine("Создание снимка файловой системы...");
            SyncFolder(sourcePath, backupPath);
            Console.WriteLine("--- БЭКАП УСПЕШНО СОЗДАН ---\n");
        }
    }
}
