using ichi.Models;
using ichi.Repositories;

namespace ichi.Services
{
    internal class PurchaseService
    {
        private readonly PurchaseRepository _purchaseRepository;
        public PurchaseService(PurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public Purchase CreatePurchase(User user, Book book, int vol
            //, int quantity
            )
        {
            //if (quantity <= 0)
            //    throw new ArgumentException("Quantity must be greater than zero.");

            List<Purchase> purchases = _purchaseRepository.GetAllPurchases();
            Purchase purchase = new Purchase();
            purchase.Id = GetNextId(purchases);
            purchase.UserId = user.Id;
            purchase.BookId = book.Id;
            purchase.BookName = book.Name;
            purchase.Vol = book.Vol;

            _purchaseRepository.SavePurchase(purchase);
            return purchase;
        }
        public List<Purchase> GetOrdersForUser(int userId)
        {
            List<Purchase> result = new List<Purchase>();
            List<Purchase> purchases = _purchaseRepository.GetAllPurchases();

            foreach (Purchase purchase in purchases)
            {
                if (purchase.UserId == userId)
                    result.Add(purchase);
            }

            return result;
        }

        public List<Purchase> GetAllPurchases()
        {
            return _purchaseRepository.GetAllPurchases();
        }

        private static int GetNextId(List<Purchase> purchases)
        {
            int biggestId = 0;
            foreach (Purchase purchase in purchases)
            {
                if (purchase.Id > biggestId)
                    biggestId = purchase.Id;
            }
            return biggestId + 1;
        }


    }
}
