using System.ComponentModel.DataAnnotations;

namespace Contact_management.Dtos
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]

		public string email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		public string password { get; set; }
	}
}
