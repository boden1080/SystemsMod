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

            //button enabled/disabled
            btnUserWeighsLooseProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;

            

            UpdateDisplay();
        }

        // Operations
        private void UserScansProduct(object sender, EventArgs e)
        {
            barcodeScanner.BarcodeDetected();
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserPutsProductInBaggingAreaCorrect.Enabled = true;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = true;
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaCorrect(object sender, EventArgs e)
        {
            baggingAreaScale.WeightChangeDetected(selfCheckout.GetCurrentProduct().GetWeight());
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserSelectsLooseProduct.Enabled = true;
            btnUserChooseToPay.Enabled = true;
            UpdateDisplay();
        }

        private void UserPutsProductInBaggingAreaIncorrect(object sender, EventArgs e)
        {
            // NOTE: We are pretending to put down an item with the wrong weight.
            // To simulate this we'll use a random number, here's one for you to use.
            int weight = new Random().Next(20, 100);

            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = true;
            selfCheckout.BaggingAreaWeightChanged(weight);
            UpdateDisplay();
        }

        private void UserSelectsALooseProduct(object sender, EventArgs e)
        {
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnUserScansBarcodeProduct.Enabled = false;
            btnUserSelectsLooseProduct.Enabled = false;
            btnUserWeighsLooseProduct.Enabled = true;
            btnUserChooseToPay.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;
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
            btnUserPutsProductInBaggingAreaCorrect.Enabled = false;
            btnUserPutsProductInBaggingAreaIncorrect.Enabled = false;
            btnAdminOverridesWeight.Enabled = false;
            btnUserScansBarcodeProduct.Enabled = true;
            btnUserSelectsLooseProduct.Enabled = true;
            btnUserChooseToPay.Enabled = true;
            UpdateDisplay();
        }

        private void UserChoosesToPay(object sender, EventArgs e)
        {
            selfCheckout.UserPaid();
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            lbBasket.Items.Clear();
            foreach(Product p in scannedProducts.GetProducts())
            {
                lbBasket.Items.Add(p.GetName());
            }
            lblScreen.Text = selfCheckout.GetPromptForUser();
            lblTotalPrice.Text = Convert.ToString(scannedProducts.CalculatePrice());
            lblBaggingAreaCurrentWeight.Text = Convert.ToString(baggingAreaScale.GetCurrentWeight());
            lblBaggingAreaExpectedWeight.Text = Convert.ToString(baggingAreaScale.GetExpectedWeight());
            // button updates do later
            // TODO: use all the information we have to update the UI:
            //     - set whether buttons are enabled
            //     - set label texts
            //     - refresh the scanned products list box
        }

       
    }
}
