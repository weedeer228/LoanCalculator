using LoanCalculator.Interfaces;
using LoanCalculator.Models;
using LoanCalculator.Models.Interfaces;

namespace LoanCalculator.Service;

public class ScheduleService : IScheduleCreator
{
    public Schedule CreateSchedule(IEnumerable<IPayment> payments, double totalOverpayment) =>
        new Schedule() { Payments = payments.Select(p => p as Payment)!, TotalOverPayment = totalOverpayment };
}