using System.Collections.Generic;

namespace Shops.Models
{
    public class Order
    {
        public Order()
        {
            OrderContent = new List<ProductInShop>();
        }

        public List<ProductInShop> OrderContent { get; }

        public void AddProductToOrder(Product product, int cost, int numberOfProduct)
        {
            OrderContent.Add(new ProductInShop(product, cost, numberOfProduct));
        }
    }
}