namespace Contact_management.Dtos
{
	public class ContactDto
	{
		public int Id { get; set; } 
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string Email { get; set; } = null!;
		public DateTime BirthDate { get; set; }
	}
}
