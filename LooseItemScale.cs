using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class LooseItemScale
    {
        // Attributes
        private bool enabled;
        private SelfCheckout selfCheckout;

        // Operations
        public void Enable()
        {
            enabled = true;
        }

        public bool IsEnabled()
        {
            if (enabled == true)
                return true;
            else
                return false;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            this.selfCheckout = sc;
        }

        // NOTE: In reality the weight wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int weight)
        {
            
        }
    }
}
