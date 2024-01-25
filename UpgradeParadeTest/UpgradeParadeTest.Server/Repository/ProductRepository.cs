using Microsoft.EntityFrameworkCore;
using UpgradeParadeTest.Server.Data;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Models;

namespace UpgradeParadeTest.Server.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.Products.Count() == 0)
            {
                var products = new List<Product>
    {
        new Product
        {
            Id = new Guid(),
            ProductPrice = 220,
            ProductName = "Pizza",
            ProductImg = Uri.EscapeUriString("https://images.unsplash.com/photo-1513104890138-7c749659a591?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8fA%3D%3D")
        },
        new Product
        {
            Id = new Guid(),
            ProductPrice = 190,
            ProductName = "Cola Zero x12",
            ProductImg = Uri.EscapeUriString("https://aquamarket.ua/17183-large_default/coca-cola-zero-upakovka-12-sht-po-025-l-koka-kola-zero-voda-solodka-nizkokalorijna-sklo.jpg")
        },
        new Product
        {
            Id = new Guid(),
            ProductPrice = 250,
            ProductName = "Big Burger",
            ProductImg = Uri.EscapeUriString("https://instafood.com.ua/images/full_blog/burger-xxxl-ish-5c546b9c99130-min-5cde9299b51a9.png?1609925664")
        },
        new Product
        {
            Id = new Guid(),
            ProductPrice = 300,
            ProductName = "Nuggets",
            ProductImg = Uri.EscapeUriString("https://cdn.metro-online.com/-/media/Project/MCW/UA_Metro/Blog/2021/08_2021/Dva-retsepty-kuryachykh-nahetsiv/459x459_1.png?h=440&iar=0&w=440&rev=173314d1c26b43cf96e9b86eaa762c00&hash=B63BEB84666BFF94F43D6B12419EF607")
        },
        new Product
        {
            Id = new Guid(),
            ProductPrice = 420,
            ProductName = "Borsch",
            ProductImg = Uri.EscapeUriString("https://zemliak.com/uploads/all/1b/62/a7/1b62a705aec2fef61a2d98e7dceff0ad.jpg")
        }
    };
                _dbContext.Products.AddRange(products);
                _dbContext.SaveChanges();
            }
        }

        public async Task<Product> GetProductByName(string productName)
        {
            var prod = await _dbContext.Products.FirstAsync(i=>i.ProductName==productName);
            return prod;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var prods = await _dbContext.Products.ToListAsync();
            return prods;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var prods = _dbContext.Products.ToList();
            return prods;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(i => i.Id == id);
            return product;
        }
    }
}
