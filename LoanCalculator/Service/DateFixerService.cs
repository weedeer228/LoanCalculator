namespace LoanCalculator.Service;

public class DateFixer
{
    public static DateOnly CheckDate(int day, int month, int year)
    {
        if (day < 29) return new DateOnly(year, month, day);
        try
        {
            var result = new DateOnly(year, month, day);
            return result;
        }
        catch (System.ArgumentOutOfRangeException)
        {
            return CheckDate(day - 1, month, year);
        }
    }
}