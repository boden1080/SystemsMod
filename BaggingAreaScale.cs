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
        private double weightTolerance;

        private SelfCheckout selfCheckout;

        // Operations
        public int GetCurrentWeight()
        {
            return weight;
        }

        public bool IsWeightOk()
        {
            weightTolerance = (weight / 100) * 2;
            if ((expected >= weight - weightTolerance) && (expected <= weight + weightTolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetExpectedWeight()
        {
            return expected;
        }

        public void SetExpectedWeight(int expected)
        {
            this.expected = expected;
            this.expected -= allowedDifference;
        }

        public void SetNewWeight(int expected)
        {
            weight = expected;
        }

        public void OverrideWeight()
        {
            allowedDifference += expected - weight;
            expected = weight;
        }

        public void Reset()
        {
            weight = 0;
            expected = 0;
            allowedDifference = 0;
        }

        public void LinkToSelfCheckout(SelfCheckout sc)
        {
            selfCheckout = sc;
        }

        // NOTE: In reality the difference wouldn't be passed in here, the
        //       scale would detect the change and notify the self checkout
        public void WeightChangeDetected(int weight)
        {
            this.weight += weight;
            selfCheckout.BaggingAreaWeightChanged();
        }
    }
}
