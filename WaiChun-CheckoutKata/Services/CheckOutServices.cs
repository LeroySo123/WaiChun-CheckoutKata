using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
