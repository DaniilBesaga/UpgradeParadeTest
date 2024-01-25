using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using UpgradeParadeTest.Server.Interfaces;
using UpgradeParadeTest.Server.Repository;

namespace UpgradeParadeTest.Server.Controllers
{
    public class PaymentsController : Controller
    {
        private IOrderRepository _orderRepository;
        public PaymentsController(IOrderRepository orderRepository)
        {
            StripeConfiguration.ApiKey = "sk_test_v0rQRqenSs7uso5uewLvjI7b00A9HmbCmN";
            _orderRepository = orderRepository;
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var prods = _orderRepository.GetAllOrders().SelectMany(x => x.Products)
        .GroupBy(product => product).Where(group => group.Count() > 1);

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var group in prods)
            {
                var product = group.Key;
                var count = group.Count();

                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        // Set the unit amount and currency based on your product
                        UnitAmount = (long?)product.ProductPrice,
                        Currency = "uah",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.ProductName, // Set the product name based on your product
                        },
                    },
                    Quantity = count, // Set the quantity of this product based on the count
                };

                lineItems.Add(lineItem);
            }

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:7278/success",
                CancelUrl = "http://localhost:7278/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
