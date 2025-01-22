using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace asp.net_lab.Models.Bookstore;

public class OrderModel
{
	[HiddenInput] public int CustomerId { get; set; }

	[Display(Name = "Id zamówienia")]
	public int OrderId { get; set; }

	[Display(Name = "Data zamówienia")]

	public DateTime? OrderDate { get; set; }

	[Display(Name = "Status zamówienia")]

	[Range(1, 6)]
	public int? StatusId { get; set; }
}
