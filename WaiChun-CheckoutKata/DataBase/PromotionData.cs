using System.Collections.Generic;
using WaiChun_CheckoutKata.Models;

namespace WaiChun_CheckoutKata.DataBase
{
    public class PromotionData
    {
        public List<PromotionsModel> GetPromotionsData()
        {
            List<PromotionsModel> PromotionDataList = new List<PromotionsModel>();
            PromotionDataList.Add(new PromotionsModel() { ItemID = 2, PromotionItemCount = 3, Discount = 40, IsPercent = false });
            PromotionDataList.Add(new PromotionsModel() { ItemID = 4, PromotionItemCount = 2, Discount = 25, IsPercent = true });

            return PromotionDataList;
        }
    }
}
