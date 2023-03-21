using LoanCalculator.Models.Interfaces;

namespace LoanCalculator.Models;

public class Payment : IPayment
{
    private double _debtBodyPayment = -228;
    private double _clearDebtBalance;
    private DateOnly _date;
    private double _percentages = -228;
    public readonly double rate;


    public Payment(int number, DateOnly date, double amount, double debtBalance, double rate)
    {
        Number = number;
        _date = date;
        Amount = amount;
        _clearDebtBalance = debtBalance;
        this.rate = rate;

    }

    public int Number { get; init; }
    public double Amount { get; init; }
    public string DebtBalance => String.Format("0:0.00", (_clearDebtBalance + Percentages));
    public string Date => _date.ToString("dd/MM/yyyy");



    public double Percentages
    {
        get
        {
            if (_percentages < 0)
                _percentages = Math.Round((_clearDebtBalance + Amount) * rate * 0.01 / 12, 2);
            return _percentages;
        }
    }

    public double DebtBodyPayment
    {
        get
        {
            if (_debtBodyPayment < 0)
                _debtBodyPayment = Math.Round(Amount - Percentages, 2);
            return _debtBodyPayment;
        }
    }

}