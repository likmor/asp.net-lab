using System.ComponentModel.DataAnnotations;

namespace asp.net_lab.Models
{
    public enum Category
    {
        [Display(Name = "Rodzina", Order = 1)]
        Family,
		[Display(Name = "Znajomi", Order = 2)]
		Friend,
		[Display(Name = "Kontakt zawodowy", Order = 3)]
		Business,

    }
}
