using ichi.Models;

namespace ichi.Repositories
{
    internal class PurchaseRepository
    {
        private readonly string _filePath;
        public PurchaseRepository()
        {
            _filePath = GetFilePath("purchase.txt");
            CreateFileIfItDoesNotExist();
        }

        public void SavePurchase(Purchase purchase)
        {
            File.AppendAllText(_filePath, purchase + Environment.NewLine);
        }
        public List<Purchase> GetAllPurchases()
        {
            var purchases = new List<Purchase>();
            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split('|');
                if (values.Length != 6)
                    continue;
                if (!int.TryParse(values[0], out int id))
                    continue;
                if (!int.TryParse(values[1], out int userId))
                    continue;
                if (!int.TryParse(values[2], out int bookId))
                    continue;
                if (!int.TryParse(values[4], out int vol))
                    continue;

                purchases.Add(new Purchase
                {
                    Id = id,
                    UserId = userId,
                    BookId = bookId,
                    BookName = values[3],
                    Vol = vol,
                });
            }

            return purchases;
        }

        private static string GetFilePath(string fileName)
        {
            return Path.GetFullPath(Path.Combine(
                AppContext.BaseDirectory, "..", "..", "..", "Data", fileName));
        }

        private void CreateFileIfItDoesNotExist()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            if (!File.Exists(_filePath))
                File.Create(_filePath).Close();
        }
    }
    }
