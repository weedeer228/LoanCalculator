namespace LoanCalculator.Models.Interfaces;

public interface IPayment
{
    public int Number { get; init; }
    public double Amount { get; init; }
}