using LoanCalculator.Models;
using LoanCalculator.Models.Interfaces;

namespace LoanCalculator.Service;

public sealed class LoanCalculatorService
{
    private static LoanCalculatorService? _calculator;
    private static ScheduleService? _scheduleService;
    public static double totalDebt;

    public static LoanCalculatorService Instance
    {
        get
        {
            if (_calculator is null)
                _calculator = new();
            return _calculator;
        }
    }

    private ScheduleService ScheduleService
    {
        get
        {
            if (_scheduleService is null)
                _scheduleService = new();
            return _scheduleService;
        }
    }

    public Schedule Calculate(double amount, int term, double rate, PercentType type)
    {
        rate = CalculateRate(rate, type);
        var payments = Instance.CalculatePayments(amount, term, rate);
        totalDebt = amount;
        var enumerable = payments as IPayment[] ?? payments.ToArray();
        var totalOverPayment = Instance.CalculateTotalOverpayment(amount, enumerable);
        return ScheduleService.CreateSchedule(enumerable, totalOverPayment);
    }
    public Schedule Calculate(DebtData data) => Calculate(data.Amount, data.Term, data.Rate, data.PercentType);

    private double CalculateRate(double rate, PercentType type) => type is PercentType.ByYear ? rate : rate * 365;


    private IEnumerable<IPayment> CalculatePayments(double amount, int term, double rate)
    {
        List<Payment> result = new();
        var paymentsAmount = CalculatePaymentsAmount(amount, term, rate);
        var dates = CalculatePaymentsDates(term);
        var debtBalances = CalculateDebtBalances(amount, paymentsAmount, term, rate);
        for (int i = 0; i < term; i++)
            result.Add(new Payment(i + 1, dates[i], paymentsAmount, debtBalances[i], rate));
        return result;
    }

    private double CalculatePaymentsAmount(double amount, int term, double rate)
    {
        var coefficient = CalculateAnnuityCoefficient(term, rate);
        return Math.Round(amount * coefficient, 2);
    }

    private double CalculatePercentageForCurrentMonth(double totalDebt, double rate) => Math.Round(totalDebt * rate * 0.01 / 12);
    ///
    private DateOnly[] CalculatePaymentsDates(int term)
    {
        DateOnly[] result = new DateOnly[term];
        var now = DateOnly.FromDateTime(DateTime.Now);
        var month = now.Month;
        var year = now.Year;
        var day = now.Day;

        for (int i = 0; i < term; i++)
        {
            month += i > 0 ? 1 : 0;
            if (month > 12)
            {
                month = 1;
                year += 1;
            }

            result[i] = DateFixer.CheckDate(day, month, year);
        }

        // Массив в качестве возвращаемого значения тк далее следует перебор по индексу, а массив-самая быстрая коллекция для этого;
        return result;
    }

    private double[] CalculateDebtBalances(double totalAmount, double monthAmount, int term, double rate)
    {
        var result = new double[term];
        for (int i = 0; i < term; i++)
        {
            totalAmount = totalAmount - monthAmount;
            result[i] = Math.Round(totalAmount, 2);
        }

        return result;
    }

    private double CalculateTotalOverpayment(double amount, IEnumerable<IPayment> payments) =>
        Math.Round(payments.Sum(p => p.Amount) - amount);


    private double CalculateAnnuityCoefficient(int term, double rate)
    {
        var monthRate = rate * 0.01 / 12;
        var constPart = Math.Pow((1 + monthRate), term);
        var nomerator = monthRate * constPart;
        var denomerator = constPart - 1;
        return nomerator / denomerator;
    }
}