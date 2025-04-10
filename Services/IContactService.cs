using Contact_management.Dtos;
using Contact_management.Models;

namespace Contact_management.Services
{
	public interface IContactService
	{
		Task<ContactDto> AddContactAsync(AddContactDto dto, string userId);
		Task<IEnumerable<ContactDto>> GetUserContactsAsync(string userId);
		Task<ContactDto?> GetContactByIdAsync(int id, string userId);
		Task<bool> DeleteContactAsync(int id, string userId);
		// Bouns 
		Task<IEnumerable<Contact>> GetContactsAsync(string userId, ContactQueryDto contactQueryDto);
	}
}
