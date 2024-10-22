using asp.net_lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_lab.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Id = 1, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
                BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222"
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
                BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222"
            }
        },
        {
            3,
            new ContactModel()
            {
                Id = 3, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
                BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222"
            }
        },
    };

    private static int currentId = 3;

    // GET
    public IActionResult Index()
    {
        return View(_contacts);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }
}