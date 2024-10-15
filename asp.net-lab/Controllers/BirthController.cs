using asp.net_lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_lab.Controllers;

public class BirthController : Controller
{
    // GET
    public IActionResult Form()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Result([FromForm] Birth birth)
    {
        if (!birth.IsValid())
        {
            ViewBag.ErrorMessage = "Data error!";
            return View("CustomError");
        }
        return View(birth);
    }
}