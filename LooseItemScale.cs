using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class LooseItemScale
    {
        // Attributes


        // Operations
        public void Enable()
        {
            // TODO
        }

        public bool IsEnabled()
        {
            // TODO
            return false;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            // TODO
        }

        // NOTE: In reality the weight wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int weight)
        {
            // TODO
        }
    }
}