using Contact_management.Dtos;
using Contact_management.Models;

namespace Contact_management.Repository
{
	public interface IContactRepository
	{
		Task<Contact> AddContactAsync(Contact contact);
		Task<IEnumerable<Contact>> GetContactsByUserIdAsync(string userId);
		Task<Contact?> GetContactByIdAsync(int id, string userId);
		Task<bool> DeleteContactAsync(int id, string userId);

		// Bouns
		Task<IEnumerable<Contact>> GetContactsAsync(string userid ,  ContactQueryDto contactQueryDto);
	}
}
