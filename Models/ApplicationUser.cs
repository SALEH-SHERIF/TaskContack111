using Microsoft.AspNetCore.Identity;

namespace Contact_management.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<Contact>? Contacts { get; set; }
	}
}
