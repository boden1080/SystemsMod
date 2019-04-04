using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class ScannedProducts
    {
        // Attributes
        private List<Product> products = new List<Product>();


        // Operations
        public List<Product> GetProducts()
        {
            return products;
        }

        public int CalculateWeight()
        {
            int weight = 0;
            foreach(Product p in products)
            {
                weight += p.GetWeight();
            }
            return weight;
        }

        public int CalculatePrice()
        {
            int price = 0;
            foreach (Product p in products)
            {
                price += p.CalculatePrice();
            }
            return price;
        }

        public void Reset()
        {
            products.Clear();
        }

        public void Add(Product p)
        {
            products.Add(p);
        }

        public bool HasItems()
        {
            // TODO
            return false;
        }
    }
}
