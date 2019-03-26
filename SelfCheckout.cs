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
            // TODO
        }

        public void LooseItemAreaWeightChanged(int weightOfLooseItem)
        {
            //currentProduct.SetWeight();
            
        }

        public void BarcodeWasScanned(int barcode)
        {
            scannedProducts.Add(ProductsDAO.SearchUsingBarcode(barcode));
            baggingArea.SetExpectedWeight(scannedProducts.CalculateWeight());
        }

        public void BaggingAreaWeightChanged()
        {
            // TODO
        }

        public void UserPaid()
        {
            scannedProducts.Reset();
            baggingArea.Reset();
        }

        public string GetPromptForUser()
        {
            // TODO: Use the information we have to produce the correct message
            //       e.g. "Scan an item.", "Place item on scale.", etc.
            return "ERROR: Unknown state!";
        }

        public Product GetCurrentProduct()
        {
            return currentProduct;
        }
    }
}
