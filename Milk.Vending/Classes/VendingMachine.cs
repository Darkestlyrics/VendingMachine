using System;
using System.Collections.Generic;
using Milk.Vending.Interfaces;

namespace Milk.Vending.Classes {
    public class VendingMachine : IVendingMachine {

        private readonly Dictionary<int, int> _denominations;

        /// <summary>
        /// Initialise a new Vending Machine
        /// </summary>
        public VendingMachine(Dictionary<int, int> denominations) {
            _denominations = denominations;
            SortAndReverseDenominations();
        }

        /// <summary>
        /// Calculates the Change to be given
        /// </summary>
        /// <param name="purchaseAmount">The amount of the Purchase</param>
        /// <param name="tenderAmount">The amount Tendered</param>
        /// <returns>&lt;see cref="Dictionary{int, int}" /&gt;</returns>
        public int[] CalculateChange(decimal purchaseAmount, decimal tenderAmount) {
            List<int> res = new List<int>();
            int reserve = GetDifferenceInCents(tenderAmount, purchaseAmount);
            if (reserve < 0)
                throw new ArgumentException("Tendered Amount Cannot be less than Purchase Amount");
            foreach (var denomination in _denominations) {
                int denominationValue = denomination.Value;
                int denominationCount = denomination.Key;

                if (reserve == 0)
                    break;
                if (HasCoins(denomination)) {
                    int tempAmount = reserve / denominationCount;
                    if (denominationValue < tempAmount) {
                        withdrawFromCashBox(denomination, denominationValue, ref reserve, res);
                    } else {
                        withdrawFromCashBox(denomination, tempAmount, ref reserve, res);
                    }
                }
            }
            return res.ToArray();

        }


        private bool HasCoins(KeyValuePair<int, int> denomination) {
            return denomination.Value > 0;

        }


        /// <summary>
        /// Withdraw from Cashbox
        /// </summary>
        /// <param name="denomination">The denomination to use</param>
        /// <param name="amount"></param>
        /// <param name="reserve"></param>
        /// <param name="res">The </param>
        private void withdrawFromCashBox(KeyValuePair<int, int> denomination, int amount, ref int reserve, List<int> res) {
            reserve -= denomination.Key * amount;
            for (int i = 0; i < amount; i++) {
                res.Add(denomination.Key);
            }
        }

        /// <summary>
        /// Ensures that the Denominations are sorted correctly
        /// </summary>
        private void SortAndReverseDenominations() {
            //Ar;
            //Array.Reverse(_denominations);
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
