using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaiChun_CheckoutKata.DTO
{
    public class BasketItemPriceDTO
    {
        public int ItemID { get; set; }
        public string ItemSKU { get; set; }
        public double UnitPrice { get; set; }
    }
}
