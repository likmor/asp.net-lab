﻿namespace asp.net_lab.Models.Services
{
	public interface IContactService
	{
		void Add(ContactModel contact);
		void Update(ContactModel contact);
		void Delete(int id);
		List<ContactModel> GetAll();
		ContactModel? GetById(int id);

		List<OrganizationEntity> GetOrganizations();


	}
}
