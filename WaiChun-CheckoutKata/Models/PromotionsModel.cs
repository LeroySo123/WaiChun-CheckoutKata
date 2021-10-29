using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaiChun_CheckoutKata.Models
{
    public class PromotionsModel
    {
        public int ItemID { get; set; }
        public int PromotionItemCount { get; set; }
        public int Discount { get; set; }
        public bool IsPercent { get; set; }
    }
}
