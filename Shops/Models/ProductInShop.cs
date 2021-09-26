namespace Shops.Models
{
    public class ProductInShop
    {
        public ProductInShop(Product product, int cost, int numberOfProduct)
        {
            Product = product;
            Cost = cost;
            NumberOfProduct = numberOfProduct;
        }

        public Product Product { get; }
        public int Cost { get; set; }
        public int NumberOfProduct { get; set; }
    }
}