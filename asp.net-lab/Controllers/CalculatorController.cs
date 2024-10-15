using asp.net_lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_lab.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Form()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Result([FromForm] Calculator model)
    {
        if (!model.IsValid())
        {
            ViewBag.ErrorMessage = "Data error!";
            return View("CustomError");
        }
        return View(model);
    }
}