using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using asp.net_lab.Models;

namespace asp.net_lab.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Calculator([FromQuery] Operator? op, [FromQuery] string? a, [FromQuery] string? b)
    {
        if (a is null || b is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b!";
            return View("CustomError");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "Nieznany operator!";
            return View("CustomError");
        }

        var n1 = double.Parse(a);
        var n2 = double.Parse(b);
        double output = 0;
        switch (op)
        {
            case Operator.Add:
            {
                output = n1 + n2;
                ViewBag.Operator = '+';
                break;
            }
            case Operator.Sub:
            {
                output = n1 - n2;
                ViewBag.Operator = '-';
                break;
            }
            case Operator.Mul:
            {
                output = n1 * n2;
                ViewBag.Operator = '*';
                break;
            }
            case Operator.Div:
            {
                output = n1 / n2;
                ViewBag.Operator = '/';
                break;
            }
        }

        ViewBag.A = n1;
        ViewBag.B = n2;

        ViewBag.Result = output;
        return View();
    }

    public IActionResult Age(DateTime birth, DateTime future)
    {
        
        // if (birth is null || future is null)
        // {
        //     ViewBag.ErrorMessage = "Niepoprawny format birth lub future!";
        //     return View("CustomError");
        // }
        if (birth > future)
        {
            ViewBag.ErrorMessage = "Birth nie może być większe niż future!";
            return View("CustomError");
        }

        var output = future.Year - birth.Year;
        if ((birth.Month == future.Month && birth.Day > future.Day) ||
            (birth.Month > future.Month))
        {
            output--;
        }

        ViewBag.Output = output;
        return View();
    }



public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    Add, Sub, Mul, Div
}