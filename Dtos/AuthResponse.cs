using System.Text.Json.Serialization;

namespace Contact_management.Dtos
{
	public class AuthResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<string>? Errors { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // if token is null => not apper  , otherwise token is show 
		public string? Token { get; set; }
	}
}
