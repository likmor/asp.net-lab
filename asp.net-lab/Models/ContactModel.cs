﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp.net_lab.Models;

public class ContactModel
{
    [HiddenInput] public int Id { get; set; }

    [Required(ErrorMessage = "Musisz podać imię!")]
    [MaxLength(length: 20, ErrorMessage = "Imię nie może być dłuższe niż 20 znaków!")]
    [MinLength(length: 2)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Musisz podać nazwisko!")]
    [MaxLength(length: 20, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków!")]
    [MinLength(length: 2)]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }

	[Display(Name = "Adres e-mail")]

	[EmailAddress] public string Email { get; set; }
    [DataType(DataType.Date)] public DateOnly BirthDate { get; set; }

    [Phone]
    //[RegularExpression("\\d\\d\\ \\d\\d\\d \\d\\d\\d \\d\\d\\d", ErrorMessage = "Wpisz numer wg wzoru: +xx: xxx xxx xxx")]
	
    [Display(Name = "Telefon")]

	public string PhoneNumber { get; set; }
    public Category Category { get; set; }
    
    [Display(Name = "Organizacja")]
    public int OrganizationId { get; set; }
    public OrganizationEntity? Organization { get; set; }
    
    [ValidateNever]
    public List<SelectListItem> Organizations { get; set; }
}