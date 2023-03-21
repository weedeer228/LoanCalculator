
namespace LoanCalculator.Models;

public class Schedule
{
    public IEnumerable<Payment> Payments { get; init; } = new List<Payment>();
    
    public double TotalOverPayment { get; init; }
    
    
}