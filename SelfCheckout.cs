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

        public void BaggingAreaWeightChanged(int changed)
        {

        }

        public void UserPaid()
        {
            scannedProducts.Reset();
            baggingArea.Reset();
        }

        public string GetPromptForUser()
        {
            
            return "Place item in the bagging area";
        }

        public Product GetCurrentProduct()
        {
            return currentProduct;
        }
    }
}
