using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Todo.Data
{
    public static class WithdrawlService
    {
        private static void SaveAll(Guid userId,List<WithdrawlItem> withdrawl)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string withdrawlFilePath = Utils.GetWithdrawlFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(withdrawl);
            File.WriteAllText(withdrawlFilePath, json);
        }



        public static List<WithdrawlItem> GetAll(Guid userId)
        {
            string appInventoryFilePath = Utils.GetWithdrawlFilePath(userId);
            if (!File.Exists(appInventoryFilePath))
            {
                return new List<WithdrawlItem>();
            }

            var json = File.ReadAllText(appInventoryFilePath);

            return JsonSerializer.Deserialize<List<WithdrawlItem>>(json);
        }



        public static List<WithdrawlItem> Create(Guid userId,Guid itemId, string itemName, int quantity, string takerName)
        {
            if (quantity <= 0)
            {
                throw new Exception("");
            }
            else if (quantity <= 0)
            {
                throw new Exception("Withdraw cannot be 0 or less");

            }
            else
            {
                List<WithdrawlItem> withdrawlItems = GetAll(userId);
                withdrawlItems.Add(new WithdrawlItem
                {
                    Quantity = quantity,
                    TakenBy = userId,
                    TakerName = takerName,
                    ItemId = itemId,
                    IsApproved = false,
                    ItemName = itemName,


                });
                SaveAll(userId, withdrawlItems);
                return withdrawlItems;

            }

            
        }



        public static List<WithdrawlItem> Delete(Guid userId,Guid id)
        {
            List<WithdrawlItem> withdrawlItems = GetAll(userId);
            WithdrawlItem itemDelete = withdrawlItems.FirstOrDefault(x => x.Id == id);

            if (itemDelete == null)
            {
                throw new Exception("Item not found.");
            }

            withdrawlItems.Remove(itemDelete);
            SaveAll(userId,withdrawlItems);
            return withdrawlItems;
        }







    }
}
