using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net_lab.Models.Bookstore;

public class CustomerModel
{
	[HiddenInput] public int CustomerId { get; set; }

	[Required(ErrorMessage = "Musisz podać imię!")]
	[MaxLength(length: 20, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków!")]
	[MinLength(length: 2, ErrorMessage = "Imię nie może być krótsze niż 2 znaki!")]
	[Display(Name = "Imię")]

	public string? FirstName { get; set; }

	[Required(ErrorMessage = "Musisz podać nazwisko!")]
	[MaxLength(length: 40, ErrorMessage = "Nazwisko nie może być dłuższe niż 40 znaków!")]
	[MinLength(length: 2, ErrorMessage = "Nazwisko nie może być krótsze niż 2 znaki!")]
	[Display(Name = "Nazwisko")]

	public string? LastName { get; set; }

	[Required(ErrorMessage = "Musisz podać adres e-mail!")]
	[EmailAddress]
	[Display(Name = "Adres e-mail")]
	public string? Email { get; set; }

	[Display(Name = "Liczba zamówień")]

	public int? OrderCount { get; set; }

	[Display(Name = "Kraj")]

	public string? Country { get; set; }
}
