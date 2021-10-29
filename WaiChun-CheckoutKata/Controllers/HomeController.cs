using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using WaiChun_CheckoutKata.DataBase;
using WaiChun_CheckoutKata.Models;
using WaiChun_CheckoutKata.Services;

namespace WaiChun_CheckoutKata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ItemSKUViewModel _itemSKUViewModel = new ItemSKUViewModel();
            ItemData _itemData = new ItemData();
            var itemList = _itemData.GetItemData();
            _itemSKUViewModel.ItemSKUList = itemList;

            return View(_itemSKUViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult check([FromBody] List<BasketItemModel> basketItems)
        {
            CheckOutServices checkOutServices = new CheckOutServices();
            double TotalPrice = checkOutServices.CheckOutTotal(basketItems);
            return Ok(TotalPrice);
        }

    }
}
