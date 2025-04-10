using Contact_management.Dtos;
using Contact_management.Models;
using Contact_management.Repository;
using Microsoft.EntityFrameworkCore;

namespace Contact_management.Services
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _repository;

		public ContactService(IContactRepository repository)
		{
			_repository = repository;
		}
		#region AddContactAsync
    	public async Task<ContactDto> AddContactAsync(AddContactDto dto, string userId)
		{
			var contact = new Contact
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				PhoneNumber = dto.PhoneNumber,
				Email = dto.Email,
				BirthDate = dto.BirthDate,
				UserId = userId

			};

			var added = await _repository.AddContactAsync(contact);

			return new ContactDto
			{
				Id = added.Id,
				FirstName = added.FirstName,
				LastName = added.LastName,
				PhoneNumber = added.PhoneNumber,
				Email = added.Email,
				BirthDate = added.BirthDate
			};
		}
		#endregion

		#region GetUserContactsAsync
		public async Task<IEnumerable<ContactDto>> GetUserContactsAsync(string userId)
		{

			var contacts = await _repository.GetContactsByUserIdAsync(userId);
			if (contacts == null)
			{
				return Enumerable.Empty<ContactDto>();
			}
			return contacts.Select(c => new ContactDto
			{
				Id = c.Id,
				FirstName = c.FirstName,
				LastName = c.LastName,
				PhoneNumber = c.PhoneNumber,
				Email = c.Email,
				BirthDate = c.BirthDate
			});
		}

		#endregion

		#region GetContactByIdAsync
		public async Task<ContactDto?> GetContactByIdAsync(int id, string userId)
		{
			var contact = await _repository.GetContactByIdAsync(id, userId);
			if (contact == null) return null;

			return new ContactDto
			{
				Id = contact.Id,
				FirstName = contact.FirstName,
				LastName = contact.LastName,
				PhoneNumber = contact.PhoneNumber,
				Email = contact.Email,
				BirthDate = contact.BirthDate
			};
		}
		#endregion

		#region DeleteContactAsync
		public async Task<bool> DeleteContactAsync(int id, string userId)
		{
			return await _repository.DeleteContactAsync(id, userId);
		}
		#endregion

		#region GetContactsAsync
		public async Task<IEnumerable<Contact>> GetContactsAsync(string userId, ContactQueryDto contactQueryDto)
		{
			return await _repository.GetContactsAsync(userId, contactQueryDto);
		} 
		#endregion

	}
}
