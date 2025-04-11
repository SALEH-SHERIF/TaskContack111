using System.ComponentModel.DataAnnotations;

namespace Contact_management.Dtos
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Username is required.")]
		[StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
		public string userName { get; set; }

		[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.Example=user@gmail.com ")]
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
		[StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(10, ErrorMessage = "Password must be at least 6 characters long and at most 10 characters ", MinimumLength = 6)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required.")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string confirmPassword { get; set; }
	}
}
