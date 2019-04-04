using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    public partial class UserInterface : Form
    {
        // Attributes
        SelfCheckout selfCheckout;
        BarcodeScanner barcodeScanner;
        BaggingAreaScale baggingAreaScale;
        LooseItemScale looseItemScale;
        ScannedProducts scannedProducts;

        // Constructor
        public UserInterface()
        {
            InitializeComponent();

            // NOTE: This is where we set up all the objects,
            // and create the various relationships between them.

            baggingAreaScale = new BaggingAreaScale();
            scannedProducts = new ScannedProducts();
            barcodeScanner = new BarcodeScanner();
            looseItemScale = new LooseItemScale();

            selfCheckout = new SelfCheckout(baggingAreaScale, scannedProducts, looseItemScale);
            barcodeScanner.LinkToSelfCheckout(selfCheckout);
            baggingAreaScale.LinkToSelfCheckout(selfCheckout);
            looseItemScale.LinkToSelfCheckout(selfCheckout);

            UpdateDisplay();
        }

        // Operations
        private void UserScansProduct(object sender, EventArgs e)
        {
            barcodeScanner.BarcodeDetected();
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaCorrect(object sender, EventArgs e)
        {
            baggingAreaScale.WeightChangeDetected(selfCheckout.GetCurrentProduct().GetWeight());
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaIncorrect(object sender, EventArgs e)
        {
            // NOTE: We are pretending to put down an item with the wrong weight.
            // To simulate this we'll use a random number, here's one for you to use.
            int weight = new Random().Next(20, 100);
            baggingAreaScale.WeightChangeDetected(weight);
            UpdateDisplay();
        }

        private void UserSelectsALooseProduct(object sender, EventArgs e)
        {
            selfCheckout.LooseProductSelected();
            UpdateDisplay();
        }

        private void UserWeighsALooseProduct(object sender, EventArgs e)
        {
            // NOTE: We are pretending to weigh a banana or whatever here.
            // To simulate this we'll use a random number, here's one for you to use.
            int weight = new Random().Next(20, 100);
            looseItemScale.WeightChangeDetected(weight);
            UpdateDisplay();
        }

        private void AdminOverridesWeight(object sender, EventArgs e)
        {
            // TODO
            selfCheckout.AdminOverrideWeight();
            UpdateDisplay();
        }

        private void UserChoosesToPay(object sender, EventArgs e)
        {
            selfCheckout.UserPaid();
            UpdateDisplay();
        }

        private void lbBasket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbBasket.SelectedIndex >= 0)
            {
                btnRemoveProduct.Enabled = true;
            }
            else
            {
                btnRemoveProduct.Enabled = false;
            }
        }

        private void UserRemovesProduct(object sender, EventArgs e)
        {
            btnAdminRemove.Enabled = true;
        }

        private void AdminConfirmation(object sender, EventArgs e)
        {
            int index = lbBasket.SelectedIndex;
            selfCheckout.UserRemoved(index);
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            lbBasket.Items.Clear();
            string currentName;
            int x;
            bool alreadyAdded;
            foreach(Product p in scannedProducts.GetProducts())
            {
                currentName = p.GetName();
                x = scannedProducts.numberOfOccurances(currentName);
                alreadyAdded = false;
                foreach(string s in lbBasket.Items)
                {
                    if(s.Substring(0, currentName.Length) == currentName)
                    {
                        alreadyAdded = true;
                    }
                }
                if(alreadyAdded == false)
                {
                    lbBasket.Items.Add(currentName + " x" + x);
                }
            }
            lblScreen.Text = selfCheckout.GetPromptForUser();
            lblTotalPrice.Text = string.Format("{0:C2}", (Convert.ToDecimal(scannedProducts.CalculatePrice()) / 100));
            lblBaggingAreaCurrentWeight.Text = string.Format("{0:0.00}", (Convert.ToDecimal(baggingAreaScale.GetCurrentWeight())));
            lblBaggingAreaExpectedWeight.Text = string.Format("{0:0.00}", (Convert.ToDecimal(baggingAreaScale.GetExpectedWeight())));
            // Reset buttons
            btnAdminOverridesWeight.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = false;
            btnRemoveProduct.Enabled = false;
            btnAdminRemove.Enabled = false;
            // Enabling/Disabling buttons
            if (!baggingAreaScale.IsWeightOk())
            {
                if (selfCheckout.GetCurrentProduct() == null)
                {
                    btnAdminOverridesWeight.Enabled = true;
                }
                else
                {
                    btnUserPutsProductInBaggingAreaCorrect.Enabled = true;
                    btnUserPutsProductInBaggingAreaIncorrect.Enabled = true;
                }
            }
            else if (looseItemScale.IsEnabled())
            {
                btnUserWeighsLooseProduct.Enabled = true;
            }
            else if (selfCheckout.GetCurrentProduct() == null)
            {
                btnUserScansBarcodeProduct.Enabled = true;
                btnUserSelectsLooseProduct.Enabled = true;
                if (scannedProducts.HasItems())
                {
                    btnUserChooseToPay.Enabled = true;
                }
            }
            else if (selfCheckout.GetCurrentProduct() != null)
            {
                btnUserPutsProductInBaggingAreaCorrect.Enabled = true;
                btnUserPutsProductInBaggingAreaIncorrect.Enabled = true;
            }
        }
    }
}
