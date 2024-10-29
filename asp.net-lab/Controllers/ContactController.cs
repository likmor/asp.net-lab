using asp.net_lab.Models;
using asp.net_lab.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_lab.Controllers;

public class ContactController : Controller
{
	private readonly IContactService _contactService;

	public ContactController(IContactService contactService)
	{
		_contactService = contactService;
	}


	// GET
	public IActionResult Index()
	{
		return View(_contactService.GetAll());
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

		_contactService.Add(model);
		return RedirectToAction(nameof(Index));
	}
	public ActionResult Edit(int id)
	{
		return View(_contactService.GetById(id));
	}
	[HttpPost]
	public ActionResult Edit(ContactModel model)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}
		_contactService.Update(model);
		return RedirectToAction(nameof(Index));
	}
	public IActionResult Delete(int id)
	{
		_contactService.Delete(id);
		return RedirectToAction(nameof(Index));
	}
}