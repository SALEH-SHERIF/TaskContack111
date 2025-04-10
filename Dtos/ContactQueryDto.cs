using System.ComponentModel.DataAnnotations;

namespace Contact_management.Dtos
{
	public class ContactQueryDto
	{
		[Range(1, int.MaxValue)]
		[Required(ErrorMessage = "Please Enter num of page")]
		public int Page { get; set; } = 1;

		[Range(1, 100)]
		[Required(ErrorMessage = "Please Enter  page size")]
		public int PageSize { get; set; } = 10;

		[Required(ErrorMessage = "Please Enter sortBy ")]
		[RegularExpression("^(FirstName|LastName|BirthDate|Email)$", ErrorMessage = "SortBy must be one of the following: FirstName, LastName, BirthDate, Email.")]
		public string SortBy { get; set; } = "FirstName"; // Default value
		
	}
}
