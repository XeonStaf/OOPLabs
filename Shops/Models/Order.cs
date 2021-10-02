using System.Collections.Generic;

namespace Shops.Models
{
    public class Order
    {
        public Order()
        {
            OrderContent = new List<ShopProducts>();
        }

        public List<ShopProducts> OrderContent { get; }

        public void AddProductToOrder(Product product, int cost, int numberOfProduct)
        {
            OrderContent.Add(new ShopProducts(product, cost, numberOfProduct));
        }
    }
}