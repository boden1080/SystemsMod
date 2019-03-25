using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_Checkout_Simulator
{
    class BaggingAreaScale
    {
        // Attributes
        private int weight;
        private int expected;
        private int allowedDifference;

        private SelfCheckout selfCheckout;

        // Operations
        public int GetCurrentWeight()
        {
            return 0;
        }

        public bool IsWeightOk()
        {
            // TODO
            return false;
        }

        public int GetExpectedWeight()
        {
            return expected;
        }

        public void SetExpectedWeight(int expected)
        {
            this.expected = expected;
        }

        public void OverrideWeight()
        {
            // TODO
        }

        public void Reset()
        {
            weight = 0;
            expected = 0;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            selfCheckout = sc;
        }

        // NOTE: In reality the difference wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int difference)
        {
            // TODO
        }
    }
}