﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Todo.Data
{
    public static class ApprovedItemService
    {

        private static void SaveAll(Guid userId,List<ApprovedItem> approvedItems)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string approvedFilePath = Utils.GetApprovedFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(approvedItems);
            File.WriteAllText(approvedFilePath, json);
        }



        public static List<ApprovedItem> GetAll(Guid userId)
        {
            string appApprovedFilePath = Utils.GetApprovedFilePath(userId);
            if (!File.Exists(appApprovedFilePath))
            {
                return new List<ApprovedItem>();
            }

            var json = File.ReadAllText(appApprovedFilePath);

            return JsonSerializer.Deserialize<List<ApprovedItem>>(json);
        }



        public static List<ApprovedItem> Create(Guid userId, string itemName, Guid itemid, int quantity, string takerName, Guid approverID, string approverName, bool isApproved)
        {
            
            List<ApprovedItem> approvedItems = GetAll(userId);
            approvedItems.Add(new ApprovedItem
            {
                Quantity = quantity,
                ItemId= itemid,
                TakenBy = userId,
                TakerName = takerName,
                IsApproved = isApproved,
                ItemName = itemName,
                ApprovedBy = approverID,
                ApproverName = approverName,



            });
            SaveAll(userId,approvedItems);
            return approvedItems;
        }



    }
}
