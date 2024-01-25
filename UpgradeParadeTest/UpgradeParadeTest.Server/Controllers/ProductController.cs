using Microsoft.AspNetCore.Mvc;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _iProductRepository;
        public ProductController(IProductRepository _iProductRepository)
        {
            this._iProductRepository = _iProductRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var prods = await _iProductRepository.GetAllProductsAsync();
            return Ok(prods);
        }

    }
}
