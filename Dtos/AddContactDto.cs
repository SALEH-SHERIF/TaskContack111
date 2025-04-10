using System.ComponentModel.DataAnnotations;

namespace Contact_management.Dtos
{
	public class AddContactDto
	{
		[Required(ErrorMessage = "First Name is required.")]
		[StringLength(8, ErrorMessage = "First Name must be less than 8 characters.")]
		public string FirstName { get; set; } = null!;
	
		[Required(ErrorMessage = "Last Name is required.")]
		[StringLength(8, ErrorMessage = "Last Name must be less than 8 characters.")]
		public string LastName { get; set; } = null!;
		
		[Required(ErrorMessage = "Phone Number is required.")]
		public string PhoneNumber { get; set; } = null!;

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Please enter a valid Email.")]
		public string Email { get; set; } = null!;
		
		[Required(ErrorMessage = "Birth Date is required.")]
		[DataType(DataType.Date)]
		[Range(typeof(DateTime), "1/1/1960", "1/1/2015", ErrorMessage = "BirthDate must be in the past start from 1/1/1960 to end 1/1/2015 .")]
		public DateTime BirthDate { get; set; }
	}
}
