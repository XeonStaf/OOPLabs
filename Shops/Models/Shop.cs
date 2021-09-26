using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private int _money;
        private List<ProductInShop> _products;
        public Shop(string name, string address, int startMoney)
        {
            Name = name;
            _money = startMoney;
            Address = address;
            Id = Guid.NewGuid();
            _products = new List<ProductInShop>();
        }

        public string Name { get; }
        public string Address { get; }
        public Guid Id { get; }

        public void SupplyOrder(Order order)
        {
            foreach (ProductInShop orderProduct in order.OrderContent)
            {
                foreach (ProductInShop inShop in _products.Where(inShop => inShop.Product.Equals(orderProduct.Product)))
                {
                    inShop.Cost = orderProduct.Cost;
                    inShop.NumberOfProduct += orderProduct.NumberOfProduct;
                }

                _products.Add(new ProductInShop(orderProduct.Product, orderProduct.Cost, orderProduct.NumberOfProduct));
            }

            order = null;
        }

        public void ChangeCost(Product product, int newCost)
        {
            GetProduct(product.Id).Cost = newCost;
        }

        public bool CanBuy(Guid id)
        {
            return GetProduct(id).NumberOfProduct > 0;
        }

        public int GetCostOfTheProduct(Guid id)
        {
            return GetProduct(id).Cost;
        }

        public int GetAmountOfTheProduct(Guid id)
        {
            return GetProduct(id).NumberOfProduct;
        }

        public void MakeAPurchase(Client client, List<(Product product, int amount)> request)
        {
            int total = 0;
            foreach ((Product product, int amount) in request)
            {
                ProductInShop productInShop = this.GetProduct(product.Id);
                if (productInShop.NumberOfProduct < amount)
                    throw new CompanyManagerException($"There are not enough {product.Name} for purchase");
                total += productInShop.Cost * amount;
            }

            client.DraftMoney(total);
            this._money += total;
            foreach ((Product product, int amount) in request)
            {
                ProductInShop productInShop = this.GetProduct(product.Id);
                productInShop.NumberOfProduct -= amount;
            }
        }

        private ProductInShop GetProduct(Guid id)
        {
            foreach (ProductInShop item in _products.Where(item => item.Product.Id == id))
            {
                return item;
            }

            throw new CompanyManagerException("This product doesn't exists in this shop");
        }
    }
}