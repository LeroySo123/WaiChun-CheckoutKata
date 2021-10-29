using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using WaiChun_CheckoutKata.Controllers;
using WaiChun_CheckoutKata.DataBase;
using WaiChun_CheckoutKata.Models;
using Microsoft.AspNetCore.Mvc;

namespace WaiChun_CheckoutKata_Test
{
    public class Tests
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeController _homeController;

        public Tests()
        {
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

    }
}