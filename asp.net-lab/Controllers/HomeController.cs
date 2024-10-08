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

    public IActionResult Calculator([FromQuery] string op, [FromQuery] string a, [FromQuery] string b)
    {
        double n1 = double.Parse(a);
        double n2 = double.Parse(b);
        double output = 0;
        switch (op)
        {
            case "add":
            {
                output = n1 + n2;
                break;
            }
            case "sub":
            {
                output = n1 - n2;
                break;
            }
            case "mul":
            {
                output = n1 * n2;
                break;
            }
            case "div":
            {
                output = n1 / n2;
                break;
            }
        }
        ViewBag.Result = output;
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