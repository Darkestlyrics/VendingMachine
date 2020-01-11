using System;
using System.Collections.Generic;
using Milk.Vending.Interfaces;

namespace Milk.Vending.Classes {
    public class VendingMachine : IVendingMachine {

        private readonly int[] _denominations;

        /// <summary>
        /// Initialise a new Vending Machine
        /// </summary>
        public VendingMachine( int[] denominations)
        {
            this._denominations = denominations;
            SortAndReverseDenominations();
        }

        /// <summary>
        /// Calculates the Change to be given
        /// </summary>
        /// <param name="purchaseAmount">The amount of the Purchase</param>
        /// <param name="tenderAmount">The amount Tendered</param>
        /// <returns>&lt;see cref="Dictionary{int, int}" /&gt;</returns>
        public int[] CalculateChange(decimal purchaseAmount, decimal tenderAmount)
        {
            List<int> res = new List<int>();
            int reserve = GetDifferenceInCents(tenderAmount, purchaseAmount);
            if (reserve < 0)
                throw new ArgumentException("Tendered Amount Cannot be less than Purchase Amount");
            foreach (var denomination in _denominations)
            {
                if (reserve == 0)
                    break;
                int num = reserve / denomination;
                if (num > 0)
                {
                    reserve -= denomination * num;
                    for (int i = 0; i < num ; i++)
                    {
                        res.Add(denomination);
                    }
                }
            }
            return res.ToArray();
        }

        /// <summary>
        /// Ensures that the Denominations are sorted correctly
        /// </summary>
        private void SortAndReverseDenominations() {
            Array.Sort(_denominations);
            Array.Reverse(_denominations);
        }

        /// <summary>
        /// Calculates the difference in amounts in cents
        /// </summary>
        /// <param name="a">The first Amount</param>
        /// <param name="b">The second amount</param>
        /// <returns></returns>
        private int GetDifferenceInCents(decimal a, decimal b) {
            var c = a - b;
            return (int)(c * 100);
        }
    }
}
