namespace Milk.Vending.Interfaces
{
    public interface IVendingMachine
    {
        public int[] CalculateChange(decimal purchaseAmount, decimal tenderAmount);

    }
}