using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaiChun_CheckoutKata.Models
{
    public class ItemSKUViewModel
    {
        public List<ItemSKUModel> ItemSKUList { get; set; }
    }

    public class ItemSKUModel
    {
        public int ItemID { get; set; }
        public string ItemSKU { get; set; }
        public double UnitPrice { get; set; }
        public List<int> ItemQuantity { get; set; }
    }
}
