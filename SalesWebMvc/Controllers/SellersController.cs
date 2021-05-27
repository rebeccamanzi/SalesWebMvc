using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // declarar dependência ao serviço
        private readonly SellerService _sellerService;
        // fazer o construtor para injetar a dependência
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            // pega a lista do model (serviço)
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
