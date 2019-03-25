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

        // TODO: Use the class diagram for details of other operations
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

        // Operations

        public int CalculatePrice()
        {
            return 0;
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

        // TODO: Use the class diagram for details of operations
    }
}