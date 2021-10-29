using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaiChun_CheckoutKata.Models;

namespace WaiChun_CheckoutKata.DataBase
{
    public class ItemData
    {
        public List<ItemSKUModel> GetItemData()
        {
            List<int> itemCount = Enumerable.Range(1, 10).ToList();

            List<ItemSKUModel> ItemDataList = new List<ItemSKUModel>();
            ItemDataList.Add(new ItemSKUModel() { ItemID = 1, ItemSKU = "A", UnitPrice = 10, ItemQuantity = itemCount });
            ItemDataList.Add(new ItemSKUModel() { ItemID = 2, ItemSKU = "B", UnitPrice = 15, ItemQuantity = itemCount });
            ItemDataList.Add(new ItemSKUModel() { ItemID = 3, ItemSKU = "C", UnitPrice = 40, ItemQuantity = itemCount });
            ItemDataList.Add(new ItemSKUModel() { ItemID = 4, ItemSKU = "D", UnitPrice = 55, ItemQuantity = itemCount });

            return ItemDataList;
        }
    }
}
