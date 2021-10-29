using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using WaiChun_CheckoutKata.Controllers;
using WaiChun_CheckoutKata.DataBase;
using WaiChun_CheckoutKata.Models;
using Microsoft.AspNetCore.Mvc;
using WaiChun_CheckoutKata.DTO;
using WaiChun_CheckoutKata.Services;
using System.Linq;

namespace WaiChun_CheckoutKata_Test
{
    public class Tests
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeController _homeController;
        private readonly CheckOutServices _checkOutServices;

        public Tests()
        {
            _checkOutServices = new CheckOutServices();
            _homeController = new HomeController(_logger);
        }

            [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetItemData()
        {
            //Arrange
            var expectedResult = 4;

            //Act
            ItemData _itemData = new ItemData();
            var testitemList = _itemData.GetItemData();
            var actualResult = testitemList.Count;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetPromotionsData()
        {
            //Arrange
            var expectedResult = 2;

            //Act
            PromotionData _promotionData = new PromotionData();
            var testpromotionList = _promotionData.GetPromotionsData();
            var actualResult = testpromotionList.Count;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetItemPriceCount()
        {
            //Arrange
            List<BasketItemModel> basketItemsList = new List<BasketItemModel>();
            basketItemsList.Add(new BasketItemModel() { ItemID = 1, ItemCount = 1 });
            basketItemsList.Add(new BasketItemModel() { ItemID = 2, ItemCount = 2 });
            basketItemsList.Add(new BasketItemModel() { ItemID = 1, ItemCount = 3 });
            var expectedResult = 6;


            //Act
            List<BasketItemPriceDTO> basketItemsWithPrice = _checkOutServices.GetItemPrice(basketItemsList);
            var actualResult = basketItemsWithPrice.Count;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestGetItemPriceItemSKUName()
        {
            //Arrange
            List<BasketItemModel> basketItemsList = new List<BasketItemModel>();
            basketItemsList.Add(new BasketItemModel() { ItemID = 1, ItemCount = 1 });
            basketItemsList.Add(new BasketItemModel() { ItemID = 2, ItemCount = 2 });
            basketItemsList.Add(new BasketItemModel() { ItemID = 1, ItemCount = 3 });
            var expectedResult = "B";


            //Act
            List<BasketItemPriceDTO> basketItemsWithPrice = _checkOutServices.GetItemPrice(basketItemsList);
            var actualResult = basketItemsWithPrice.Where(x => x.ItemID == 2).Select(x => x.ItemSKU).FirstOrDefault().ToString();

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestIsPromotionItemNoDiscount()
        {
            //Arrange
            List<BasketItemPriceDTO> itemList = new List<BasketItemPriceDTO>();
            itemList.Add(new BasketItemPriceDTO() { ItemID = 2, ItemSKU = "B", UnitPrice = 15 });
            itemList.Add(new BasketItemPriceDTO() { ItemID = 2, ItemSKU = "B", UnitPrice = 15 });

            var expectedResult = false;


            //Act
            bool actualResult = _checkOutServices.CalculatePromotionsPrice(itemList, out double itemPriceTotalinPromo);


            //Assert
            Assert.AreEqual(actualResult, expectedResult);

        }

        [Test]
        public void TestIsPromotionItemHaveDiscount()
        {
            //Arrange
            List<BasketItemPriceDTO> itemList = new List<BasketItemPriceDTO>();
            itemList.Add(new BasketItemPriceDTO() { ItemID = 2, ItemSKU = "B", UnitPrice = 15 });
            itemList.Add(new BasketItemPriceDTO() { ItemID = 2, ItemSKU = "B", UnitPrice = 15 });
            itemList.Add(new BasketItemPriceDTO() { ItemID = 2, ItemSKU = "B", UnitPrice = 15 });

            var expectedResult = true;


            //Act
            bool actualResult = _checkOutServices.CalculatePromotionsPrice(itemList, out double itemPriceTotalinPromo);


            //Assert
            Assert.AreEqual(actualResult, expectedResult);

        }
    }
}