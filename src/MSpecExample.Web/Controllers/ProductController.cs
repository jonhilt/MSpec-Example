using System.Web.Mvc;
using MSpecExample.Web.Repositories;

namespace MSpecExample.Web.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Search(string searchTerm)
        {
            return View("SearchResults", _productRepository.FindProducts(searchTerm));
        }

        public ActionResult Search()
        {
            return View("Search");
        }
    }
}