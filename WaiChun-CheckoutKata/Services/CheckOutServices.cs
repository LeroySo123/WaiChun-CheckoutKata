using System.Collections.Generic;
using System.Linq;
using WaiChun_CheckoutKata.DataBase;
using WaiChun_CheckoutKata.DTO;
using WaiChun_CheckoutKata.Models;

namespace WaiChun_CheckoutKata.Services
{
    public class CheckOutServices
    {
        public double CheckOutTotal(List<BasketItemModel> basketItemsList)
        {
            double totalPrice = 0;
            List<BasketItemPriceDTO> basketItemsWithPrice = GetItemPrice(basketItemsList);
            List<BasketItemTotalPriceDTO> itemTotalPrice = CalculateItemTotalPrice(basketItemsWithPrice);
            totalPrice = itemTotalPrice.Select(x => x.TotalPrice).Sum();
            return totalPrice;
        }

        public List<BasketItemPriceDTO> GetItemPrice(List<BasketItemModel> basketItemsList)
        {
            List<BasketItemPriceDTO> basketItemPriceDTOList = new List<BasketItemPriceDTO>();
            ItemData itemData = new ItemData();
            var itemList = itemData.GetItemData();
            foreach (BasketItemModel basketItem in basketItemsList)
            {
                for (int i = 0; i < basketItem.ItemCount; i++)
                {
                    BasketItemPriceDTO basketItemPrice = new BasketItemPriceDTO();
                    var ItemDetail = itemList.Where(x => x.ItemID == basketItem.ItemID).FirstOrDefault();
                    basketItemPrice.ItemID = basketItem.ItemID;
                    basketItemPrice.ItemSKU = ItemDetail.ItemSKU;
                    basketItemPrice.UnitPrice = ItemDetail.UnitPrice;
                    basketItemPriceDTOList.Add(basketItemPrice);
                }
            }
            return basketItemPriceDTOList;
        }

        public List<BasketItemTotalPriceDTO> CalculateItemTotalPrice(List<BasketItemPriceDTO> basketItemPriceDTOList)
        {
            List<BasketItemTotalPriceDTO> basketItemTotalPriceList = new List<BasketItemTotalPriceDTO>();
            var GroupItemList = basketItemPriceDTOList.GroupBy(g => g.ItemSKU).ToList();
            foreach (var ItemSKU in GroupItemList)
            {
                BasketItemTotalPriceDTO basketitemTotalPrice = new BasketItemTotalPriceDTO();
                double itemTotalPrice;
                var itemList = basketItemPriceDTOList.Where(x => x.ItemSKU == ItemSKU.Key).ToList();
                bool isPromotion = CalculatePromotionsPrice(itemList, out double itemPriceTotalinPromo);
                if (isPromotion)
                {
                    itemTotalPrice = itemPriceTotalinPromo;
                }
                else
                {
                    itemTotalPrice = itemList.Select(x => x.UnitPrice).Sum();
                }
                basketitemTotalPrice.ItemID = itemList.Select(s => s.ItemID).FirstOrDefault();
                basketitemTotalPrice.ItemSKU = ItemSKU.Key;
                basketitemTotalPrice.TotalPrice = itemTotalPrice;
                basketItemTotalPriceList.Add(basketitemTotalPrice);
            }

            return basketItemTotalPriceList;
        }

        public bool CalculatePromotionsPrice(List<BasketItemPriceDTO> itemList, out double itemPriceTotal)
        {
            bool isPromotion = false;
            itemPriceTotal = 0;

            int ItemID = itemList.Select(s => s.ItemID).FirstOrDefault();

            PromotionData promotionData = new PromotionData();
            var promotionList = promotionData.GetPromotionsData();

            PromotionsModel promotionDetail = promotionList.Where(x => x.ItemID == ItemID).FirstOrDefault();
            if (promotionDetail != null)
            {
                double itemUnitPrice = itemList.Select(s => s.UnitPrice).FirstOrDefault();

                int itemQuantity = itemList.Count();
                int div = itemQuantity / promotionDetail.PromotionItemCount;
                int mod = itemQuantity % promotionDetail.PromotionItemCount;

                if (div != 0)
                {
                    double promotionsPriceTotal = 0;
                    bool IsPercent = promotionDetail.IsPercent;
                    if (IsPercent)
                    {
                        double DiscountPercent = (double)(promotionDetail.Discount / 100D);
                        promotionsPriceTotal = (itemUnitPrice * (promotionDetail.PromotionItemCount * div)) * (1 - DiscountPercent);
                    }
                    else
                    {
                        promotionsPriceTotal = div * promotionDetail.Discount;
                    }
                    double modPriceTotal = mod * itemUnitPrice;
                    itemPriceTotal = promotionsPriceTotal + modPriceTotal;
                    isPromotion = true;
                }

            }
            return isPromotion;
        }
    }
}
