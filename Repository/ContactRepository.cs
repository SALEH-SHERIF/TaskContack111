using Contact_management.Contexts;
using Contact_management.Dtos;
using Contact_management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Contact_management.Repository
{
	public class ContactRepository : IContactRepository
	{
		private readonly ApplicationDbContext _context;

		public ContactRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		#region AddContactAsync
		public async Task<Contact> AddContactAsync(Contact contact)
		{
			if (string.IsNullOrEmpty(contact.UserId))
			{
				return null;
			}
			var userExists = await _context.Users.AnyAsync(u => u.Id == contact.UserId);
			if (!userExists)
			{
				return null;
			}
			await _context.Contacts.AddAsync(contact);
			await _context.SaveChangesAsync();
			return contact;
		}
		#endregion

		#region GetContactsByUserIdAsync
		public async Task<IEnumerable<Contact>> GetContactsByUserIdAsync(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return null;
			}
			var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
			if (!userExists)
			{
				return Enumerable.Empty<Contact>();
			}
			return await _context.Contacts
				.Where(c => c.UserId == userId)
				.ToListAsync();
		}

		#endregion

		#region GetContactByIdAsync
		public async Task<Contact?> GetContactByIdAsync(int id, string userId)
		{
			if (string.IsNullOrEmpty(userId) )
			{
				return null; 
			}
			var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
			if (!userExists)
			{
				return null;
			}
			return await _context.Contacts
				.Where(c => c.UserId == userId && c.Id == id)
				.FirstOrDefaultAsync();
		} 
		#endregion

		#region DeleteContactAsync
		public async Task<bool> DeleteContactAsync(int id, string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}
			var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
			if (!userExists)
			{
				return false;
			}
			var contact = await GetContactByIdAsync(id, userId);
			if (contact == null) return false;

			_context.Contacts.Remove(contact);
			await _context.SaveChangesAsync();
			return true;
		}
		#endregion

		// Bouns 
		#region GetContactsAsync
		public async Task<IEnumerable<Contact>> GetContactsAsync(string userid, ContactQueryDto contactQueryDto)
		{
			var query = _context.Contacts.AsQueryable();

			query = query.Where(c => c.UserId == userid);

			switch (contactQueryDto.SortBy.ToLower())
			{
				case "firstname":
					query = query.OrderBy(c => c.FirstName);
					break;
				case "lastname":
					query = query.OrderBy(c => c.LastName);
					break;
				case "birthdate":
					query = query.OrderBy(c => c.BirthDate);
					break;
				default:
					query = query.OrderBy(c => c.FirstName);
					break;
			}
			query = query.Skip((contactQueryDto.Page - 1) * contactQueryDto.PageSize).Take(contactQueryDto.PageSize);

			return await query.ToListAsync();
		} 
		#endregion
	}
}
