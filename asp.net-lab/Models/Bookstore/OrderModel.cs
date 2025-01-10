using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.net_lab.Models.Bookstore;

public class OrderModel
{
	public int OrderId { get; set; }

	public DateTime? OrderDate { get; set; }

	public int? CustomerId { get; set; }

	public int? ShippingMethodId { get; set; }

	public int? DestAddressId { get; set; }
	[Range(1, 6)]
	public int? StatusId { get; set; }
}
