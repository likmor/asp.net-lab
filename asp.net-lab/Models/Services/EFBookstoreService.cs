using asp.net_lab.Models.Bookstore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace asp.net_lab.Models.Services;

public class EFBookstoreService : IBookstoreService
{
	private readonly BookstoreContext _context;

	public EFBookstoreService(BookstoreContext context)
	{
		_context = context;
	}

	public async Task<PagedListAsync<CustomerModel>> GetCustomerListPaginatedAsync(int page, int size)
	{
		return new PagedListAsync<CustomerModel>((page, size) => _context.Customers
		.Select(c => new CustomerModel
		{
			CustomerId = c.CustomerId,
			FirstName = c.FirstName,
			LastName = c.LastName,
			Email = c.Email,
			OrderCount = c.CustOrders.Count(),
			Country = string.Join(", ", c.CustomerAddresses
				.Select(ca => ca.Address.Country.CountryName).ToList())
		})
		.OrderBy(x => x.CustomerId)
		.Skip((page - 1) * size)
		.Take(size)
		.ToList(),
		await _context.Customers.CountAsync(),
		page,
		size
		);

	}

	public async Task<PagedListAsync<CustOrder>> GetCustomerOrdersByIdAsync(int customerId, int page, int size)
	{
		return new PagedListAsync<CustOrder>((page, size) => _context.CustOrders
		.Where(id => id.CustomerId == customerId)
		.OrderBy(x => x.OrderId)
		.Skip((page - 1) * size)
		.Take(size)
		.ToList(),
		await _context.CustOrders
		.Where(id => id.CustomerId == customerId)
		.CountAsync(),
		page,
		size);

	}

	public async Task<OrderModel?> GetOrderByIdAsync(int orderId)
	{
		return await _context.CustOrders
			.Where(x => x.OrderId == orderId)
			.Select(x => new OrderModel
			{
				OrderId = x.OrderId,
				OrderDate = x.OrderDate,
				CustomerId = x.CustomerId,
				DestAddressId = x.DestAddressId,
				ShippingMethodId = x.ShippingMethodId,
				StatusId = x.OrderHistories.OrderByDescending(x => x.StatusDate).FirstOrDefault().StatusId,
			}).FirstOrDefaultAsync();
	}
	public async Task<List<SelectListItem>> GetOrderStatusListAsync()
	{
		return await _context.OrderStatuses
			.Select(s => new SelectListItem
			{
				Value = s.StatusId.ToString(),
				Text = $"{s.StatusId} - {s.StatusValue}"
			})
			.ToListAsync();
	}
	public async Task AddCustomerAsync(CustomerModel model)
	{
		var customer = new Customer
		{
			FirstName = model.FirstName,
			LastName = model.LastName,
			Email = model.Email,
		};
		_context.Customers.Add(customer);
		await _context.SaveChangesAsync();
	}
	public async Task UpdateOrderAsync(OrderModel model)
	{
		var corder = await _context.CustOrders
			.Where(x => x.OrderId == model.OrderId)
			.Select(x => new OrderModel
			{
				OrderId = x.OrderId,
				OrderDate = x.OrderDate,
				CustomerId = x.CustomerId,
				DestAddressId = x.DestAddressId,
				ShippingMethodId = x.ShippingMethodId,
				StatusId = x.OrderHistories.OrderByDescending(x => x.StatusDate).FirstOrDefault().StatusId,
			}).FirstOrDefaultAsync();
		if (corder.StatusId >= 4)
		{
			return;
		}
		var order = new OrderHistory
		{
			OrderId = model.OrderId,
			StatusId = model.StatusId,
			StatusDate = DateTime.Now,
		};
		await _context.OrderHistories.AddAsync(order);
		await _context.SaveChangesAsync();
	}

}
public class PagedListAsync<T>
{
	public IList<T> Data { get; }
	public IList<int> Pages { get; }
	public int TotalItems { get; }
	public int TotalPages { get; }
	public int Page { get; }
	public int Size { get; }
	public bool IsFirst { get; }
	public bool IsLast { get; }
	public bool IsNext { get; }
	public bool IsPrevious { get; }
	public bool ShowNear { get; }

	public PagedListAsync(Func<int, int, IList<T>> dataGenerator, int totalItems, int page, int size)
	{
		TotalItems = totalItems;
		Size = size;
		TotalPages = (int)Math.Ceiling((double)totalItems / size);
		Page = ClipPage(page);
		IsFirst = Page <= 1;
		IsLast = Page >= TotalPages;
		IsNext = !IsLast;
		IsPrevious = !IsFirst;
		Data = dataGenerator(Page, Size);
		Pages = GeneratePages();
	}
	private IList<int> GeneratePages()
	{
		var pages = new SortedSet<int>();
		if (TotalPages > 2)
		{
			for (int i = 1; i <= 3; i++)
			{
				pages.Add(i);
			}
			for (int i = TotalPages - 2; i <= TotalPages; i++)
			{
				pages.Add(i);
			}
		}

		pages.Add(Page);
		if (IsPrevious)
		{
			pages.Add(Page - 1);
		}
		if (IsNext)
		{
			pages.Add(Page + 1);
		}

		var list = pages.ToList();
		var tempList = new List<int>(list);
		int count = list.Count;
		int offset = 0;
		for (int i = 0; i < count - 1; i++)
		{
			if (list[i + 1] - list[i] > 1)
			{
				Console.WriteLine($"{list[i + 1]}, {list[i]}");
				tempList.Insert(i + 1 + offset++, -(list[i + 1] + list[i]) / 2);
			}
		}
		return tempList;
	}

	private int ClipPage(int page)
	{
		int totalPages = TotalPages;
		if (page > totalPages)
		{
			return totalPages;
		}
		if (page < 1)
		{
			return 1;
		}
		return page;
	}

}