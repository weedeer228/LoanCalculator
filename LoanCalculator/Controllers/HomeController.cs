using LoanCalculator.Models;
using LoanCalculator.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoanCalculator.Controllers;

public class HomeController : Controller
{

    public IActionResult Calculate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Calculate(DebtData data)
    {
        ViewBag.Schedule = LoanCalculatorService.Instance.Calculate(data);
        return View();
    }
}