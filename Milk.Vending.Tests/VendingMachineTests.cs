using System;
using System.Collections;
using System.Collections.Generic;
using Milk.Vending.Classes;
using Xunit;

namespace Milk.Vending.Tests {
    public class VendingMachineTests {

        [Fact(DisplayName = "Expect to Return an Object")]
        public void CanCreateTest() {
            // Arrange
            var vendingMachine = new VendingMachine(new int[0]);

            // Act

            // Assert
            Assert.IsType<VendingMachine>(vendingMachine);
            Assert.NotNull(vendingMachine);
        }


        [Theory(DisplayName = "Expect to Return Change")]
        [ClassData(typeof(TestDataGenerator))]
        public void StandardTest(string currency, int[] denoms) {
            // Arrange
            var vendingMachine = new VendingMachine(denoms);
            var purchaseAmount = 1.35M;
            var tenderAmount = 2.00M;
            int[] expected = null;
            switch (currency) {
                case "USD":
                    expected = new[]
                    {
                        25,
                        25,
                        10,
                        5
                    };
                    break;
                case "GBP":
                    expected = new[]
                    {
                        50,
                        10,
                        5
                    };
                    break;
            }

            // Act
            var result = vendingMachine.CalculateChange(
                purchaseAmount,
                tenderAmount);

            // Assert
            Assert.Equal(result, expected);
        }

        [Theory(DisplayName = "Expect to Return empty")]
        [ClassData(typeof(TestDataGenerator))]
        public void ZeroTest(string currency, int[] denoms) {
            // Arrange
            var vendingMachine = new VendingMachine(denoms);
            decimal purchaseAmount = 2.00M;
            decimal tenderAmount = 2.00M;
            int[] expected = new int[0];

            // Act
            var result = vendingMachine.CalculateChange(
                purchaseAmount,
                tenderAmount);

            // Assert
            Assert.Equal(result, expected);
        }


        [Theory(DisplayName = "Expect to throw an exception")]
        [ClassData(typeof(TestDataGenerator))]
        public void NegativeTest(string currency, int[] denoms) {
            // Arrange
            var vendingMachine = new VendingMachine(denoms);
            decimal purchaseAmount = 2.00M;
            decimal tenderAmount = 1.35M;

            // Assert
            Assert.Throws<ArgumentException>(() => vendingMachine.CalculateChange(
                purchaseAmount,
                tenderAmount));
        }
    }
    public class TestDataGenerator : IEnumerable<object[]> {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "GBP" ,new [] {1, 2, 5, 10, 20, 50}},
            new object[] { "USD" ,new [] {1, 5, 10, 25 }}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }




}
