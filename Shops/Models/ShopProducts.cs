namespace Shops.Models
{
    public class ShopProducts
    {
        public ShopProducts(Product product, int cost, int numberOfProduct)
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