namespace asp.net_lab.Models;

public class ContactMapper
{
    public static ContactEntity ToEntity(ContactModel model)
    {
        return new ContactEntity()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Category = model.Category,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Created = DateTime.Now,
            Organization = model.Organization,
            OrganizationId = model.OrganizationId
        };
    }
    public static ContactModel FromEntity(ContactEntity entity)
    {
        return new ContactModel()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Category = entity.Category,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Organization = entity.Organization,
            OrganizationId = entity.OrganizationId
        };
    }
}