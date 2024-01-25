using Microsoft.AspNetCore.Mvc;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Models;
using static System.Net.Mime.MediaTypeNames;

namespace UpgradeParadeTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        public OrderController(IOrderRepository OrderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _OrderRepository = OrderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOrderById(Guid id)
        {
            try
            {
                var image = await _OrderRepository.GetOrderById(id);

                if (image == null)
                    return NotFound();

                return Ok(image);
            }
            catch (Exception ex)
            {
                string err = ex.Message + "\n" + ex.StackTrace;
                return BadRequest(err);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest();

                order.Id = Guid.NewGuid();
                var customers = _customerRepository.GetAllCustomers();
                if (customers != null)
                {
                    foreach(var cust in customers)
                    {
                        if(cust.Email==order.Customer.Email && cust.Name==order.Customer.Name)
                        {
                            order.Customer = cust;
                            break;
                        }
                    }
                    if(order.CustomerId == Guid.Empty)
                    {
                        order.CustomerId = Guid.NewGuid();
                        await _customerRepository.AddCustomerAsync(order.Customer);
                    }
                }
                else
                {
                    order.Customer.Id = Guid.NewGuid();
                    await _customerRepository.AddCustomerAsync(order.Customer);
                }

                foreach(var str in order.Products.Select(x => x.ProductName))
                {
                    switch (str)
                    {
                        case "Pizza":
                            var pr = await _productRepository.GetProductByName("Pizza");
                            var a = order.Products.Find(x => x.ProductName == "Pizza");
                            a = pr;
                            break;
                        case "Cola Zero x12":
                            pr = await _productRepository.GetProductByName("Cola Zero x12");
                            a = order.Products.Find(x => x.ProductName == "Cola Zero x12");
                            a = pr;
                            break;
                        case "Big Burger":
                            pr = await _productRepository.GetProductByName("Big Burger");
                            a = order.Products.Find(x => x.ProductName == "Big Burger");
                            a = pr;
                            break;
                        case "Nuggets":
                            pr = await _productRepository.GetProductByName("Nuggets");
                            a = order.Products.Find(x => x.ProductName == "Nuggets");
                            a = pr;
                            break;
                        case "Borsch":
                            pr = await _productRepository.GetProductByName("Borsch");
                            a = order.Products.Find(x => x.ProductName == "Borsch");
                            a = pr;
                            break;
                    }
                }
                order.TotalPrice = order.Products.Select(x=>x.ProductPrice).Sum();
                await _OrderRepository.AddOrderAsync(order);

                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                string err = ex.Message + "\n" + ex.StackTrace;
                return BadRequest(err);
            }
        }

    }
}
