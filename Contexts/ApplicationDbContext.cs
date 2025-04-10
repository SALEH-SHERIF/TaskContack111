using Contact_management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Contact_management.Contexts
{
	public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options) { }

        public DbSet<Contact> Contacts { get; set;}
    }
}
