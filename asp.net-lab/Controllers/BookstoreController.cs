using asp.net_lab.Models;
using asp.net_lab.Models.Bookstore;
using asp.net_lab.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp.net_lab.Controllers
{
	public class BookstoreController : Controller
	{
		private readonly IBookstoreService _bookstoreService;

		public BookstoreController(IBookstoreService gravityService)
		{
			_bookstoreService = gravityService;
		}
		public async Task<ActionResult> Index(int page = 1, int size = 10)
		{
			return View(await _bookstoreService.GetCustomerListPaginatedAsync(page, size));
		}
		public async Task<ActionResult> OrderList(int customerId, int page = 1, int size = 10)
		{
			return View(await _bookstoreService.GetCustomerOrdersByIdAsync(customerId, page, size));
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> EditOrder(int orderId)
		{
			var statusList = await _bookstoreService.GetOrderStatusListAsync();
			ViewBag.StatusList = statusList;
			return View(await _bookstoreService.GetOrderByIdAsync(orderId));
		}
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> UpdateOrder(OrderModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			await _bookstoreService.UpdateOrderAsync(model);
			return RedirectToAction(nameof(EditOrder), new { orderId = model.OrderId });
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> AddCustomer()
		{
			return View();
		}
		[HttpPost]
		[Authorize]
		public async Task<ActionResult> AddCustomer(CustomerModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await _bookstoreService.AddCustomerAsync(model);
			return RedirectToAction(nameof(Index));
		}

	}
}
