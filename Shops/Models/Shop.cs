using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private int _shopsMoney;
        private List<ShopProducts> _products;
        public Shop(string name, string address, int startMoney)
        {
            Name = name;
            _shopsMoney = startMoney;
            Address = address;
            Id = Guid.NewGuid();
            _products = new List<ShopProducts>();
        }

        public string Name { get; }
        public string Address { get; }
        public Guid Id { get; }

        public void SupplyOrder(Order order)
        {
            foreach (ShopProducts orderProduct in order.OrderContent)
            {
                foreach (ShopProducts inShop in _products.Where(inShop => inShop.Product.Equals(orderProduct.Product)))
                {
                    inShop.Cost = orderProduct.Cost;
                    inShop.NumberOfProduct += orderProduct.NumberOfProduct;
                }

                _products.Add(new ShopProducts(orderProduct.Product, orderProduct.Cost, orderProduct.NumberOfProduct));
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
                ShopProducts shopProducts = this.GetProduct(product.Id);
                if (shopProducts.NumberOfProduct < amount)
                    throw new CompanyManagerException($"There are not enough {product.Name} for purchase");
                total += shopProducts.Cost * amount;
            }

            client.WithdrawnMoney(total);
            this._shopsMoney += total;
            foreach ((Product product, int amount) in request)
            {
                ShopProducts shopProducts = this.GetProduct(product.Id);
                shopProducts.NumberOfProduct -= amount;
            }
        }

        private ShopProducts GetProduct(Guid id)
        {
            foreach (ShopProducts item in _products.Where(item => item.Product.Id == id))
            {
                return item;
            }

            throw new CompanyManagerException("This product doesn't exists in this shop");
        }
    }
}