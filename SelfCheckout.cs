using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class SelfCheckout
    {
        // Attributes
        private Product currentProduct;
        private BaggingAreaScale baggingArea;
        private ScannedProducts scannedProducts;
        private LooseItemScale looseItemScale;


        // Constructor
        public SelfCheckout(BaggingAreaScale baggingArea, ScannedProducts scannedProducts, LooseItemScale looseItemScale)
        {
            this.baggingArea = baggingArea;
            this.scannedProducts = scannedProducts;
            this.looseItemScale = looseItemScale;
        }

        // Operations
        public void LooseProductSelected()
        {
            ProductsDAO.GetRandomLooseProduct();
            looseItemScale.Enable();
        }

        public void LooseItemAreaWeightChanged(int weightOfLooseItem)
        {
            currentProduct = ProductsDAO.GetRandomLooseProduct();
            currentProduct.SetWeight(weightOfLooseItem);
            scannedProducts.Add(currentProduct);
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
            looseItemScale.Disable();
        }

        public void BarcodeWasScanned(int barcode)
        {
            currentProduct = ProductsDAO.SearchUsingBarcode(barcode);
            scannedProducts.Add(currentProduct);
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
        }

        public void BaggingAreaWeightChanged()
        {
            currentProduct = null;
        }

        public void UserPaid()
        {
            scannedProducts.Reset();
            baggingArea.Reset();
        }

        public void UserRemoved(int index)
        {
            currentProduct = scannedProducts.GetProduct(index);
            scannedProducts.Remove(currentProduct);
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
            baggingArea.SetNewWeight(scannedProducts.CalculateWeight());
            currentProduct = null;
        }

        public string GetPromptForUser()
        {
            if (!baggingArea.IsWeightOk())
            {
                if (currentProduct == null)
                {
                    return "Please wait, assistant is on the way";
                }
                else
                {
                    return "Place the item in the bagging area";
                }
            }
            else if (looseItemScale.IsEnabled())
            {
                return "Place item on scale";
            }
            else if (currentProduct == null)
            {
                if (scannedProducts.HasItems())
                {
                    return "Scan an item or pay";
                }
                else
                {
                    return "Scan an item";
                }
            }
            else if (currentProduct != null)
            {
                return "Place the item in the bagging area";
            }
            return "ERROR: Unknown state!";
        }

        public Product GetCurrentProduct()
        {
            return currentProduct;
        }

        public void AdminOverrideWeight()
        {
            baggingArea.OverrideWeight();
        }
    }
}
