using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Models;

public class DebtData
{
    public int Term { get; set; }
    public double Amount { get; set; }
    public double Rate { get; set; }
    public PercentType PercentType { get; set; }
}

public enum PercentType
{
    ByYear,
    ByDay,

}