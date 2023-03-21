using LoanCalculator.Models;
using LoanCalculator.Models.Interfaces;

namespace LoanCalculator.Interfaces;

public interface IScheduleCreator
{
    public Schedule CreateSchedule(IEnumerable<IPayment> payments, double totalOverpayment);
}