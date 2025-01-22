using asp.net_lab.Models.Bookstore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp.net_lab.Models.Services
{
	public interface IBookstoreService
	{
		Task<PagedListAsync<CustomerModel>> GetCustomerListPaginatedAsync(int page, int size);
		Task<PagedListAsync<OrderModel>> GetCustomerOrdersByIdAsync(int customerId, int page, int size);
		Task<OrderModel?> GetOrderByIdAsync(int orderId);
		Task<List<SelectListItem>> GetOrderStatusListAsync();
		Task AddCustomerAsync(CustomerModel model);
		Task UpdateOrderAsync(OrderModel model);


	}
}
