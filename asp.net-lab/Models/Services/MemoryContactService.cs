
using System.Reflection;

namespace asp.net_lab.Models.Services
{
	public class MemoryContactService : IContactService
	{
		private Dictionary<int, ContactModel> _contacts = new()
	{
		{
			1,
			new ContactModel()
			{
				Id = 1, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
				BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222",
				Category = Category.Business
			}
		},
		{
			2,
			new ContactModel()
			{
				Id = 2, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
				BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222",
				Category = Category.Family
			}
		},
		{
			3,
			new ContactModel()
			{
				Id = 3, FirstName = "Adam", LastName = "Abecki", Email = "aabecki@gmail.com",
				BirthDate = new DateOnly(2000, 10, 10), PhoneNumber = "+48 738 272 222",
				Category = Category.Friend
			}
		},
	};
		private int currentId = 3;
		public void Add(ContactModel model)
		{
			model.Id = ++currentId;
			_contacts.Add(model.Id, model);
		}
		public void Update(ContactModel model)
		{
			if (_contacts.ContainsKey(model.Id))
			{
				_contacts[model.Id] = model;
			}
		}
		public void Delete(int id)
		{
			_contacts.Remove(id);
		}

		public List<ContactModel> GetAll()
		{
			return _contacts.Values.ToList();
		}
		public ContactModel? GetById(int id)
		{
			return _contacts[id];
		}

		public List<OrganizationEntity> GetOrganizations()
		{
			throw new NotImplementedException();
		}
	}
}
