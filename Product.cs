using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class Product
    {
        // Attributes
        protected string name;
        protected int barcode;
        protected int weightInGrams;

        // Operations
        public string GetName()
        {
            return name;
            
        }

        public int GetBarcode()
        {
            return barcode;
        }

        public bool IsLooseProduct()
        {
            if(weightInGrams != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetWeight()
        {
            return weightInGrams;
        }

        public virtual int CalculatePrice()
        {
            return 0;
        }

        public void SetWeight(int weightInGrams)
        {
            this.weightInGrams = weightInGrams;

        }
    }

    class PackagedProduct : Product
    {
        // Attributes
        private int priceInPence;
        // Constructor
        public PackagedProduct(int barcode, string name, int priceInPence, int weightInGrams)
        {
            this.barcode = barcode;
            this.name = name;
            this.priceInPence = priceInPence;
            this.weightInGrams = weightInGrams;
        }
        public override int CalculatePrice()
        {
            return priceInPence;
        }

        public bool isLooseProduct()
        {
            return false;
        }
    }

    class LooseProduct : Product
    {
        // Attributes
        private int pencePer100g;
        // Constructor
        public LooseProduct(int barcode, string name, int pencePer100g)
        {
            this.barcode = barcode;
            this.name = name;
            this.pencePer100g = pencePer100g;
        }

        // Operations
        public int GetPencePer100g()
        {
            return pencePer100g;
        }

        public override int CalculatePrice()
        {
            double weightIn100Grams = Convert.ToDouble(weightInGrams) / 100;
            return Convert.ToInt32(weightIn100Grams * pencePer100g);
        }

        public bool isLooseProduct()
        {
            return true;
        }
    }
}
