namespace asp.net_lab.Models;

public class Birth
{
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsValid()
    {
        return Name != null && BirthDate < DateTime.UtcNow && BirthDate != DateTime.MinValue;
    }

    public int Age
    {
        get
        {
            DateTime current = DateTime.UtcNow;
            var output = current.Year - BirthDate.Year;
            if ((BirthDate.Month == current.Month && BirthDate.Day > current.Day) ||
                (BirthDate.Month > current.Month))
            {
                output--;
            }

            return output;
        }
    }
}