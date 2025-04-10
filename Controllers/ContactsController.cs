using Contact_management.Contexts;
using Contact_management.Dtos;
using Contact_management.Models;
using Contact_management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Contact_management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly IContactService _service;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public ContactsController(IContactService service, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
		{
			_service = service;
			_context = dbContext;
			_userManager = userManager;
		}


		// Get UserId from Token 
		private string GetUserId() =>
			User.FindFirstValue(ClaimTypes.NameIdentifier)!;

		#region AddContact
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddContact(AddContactDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			// Get UserId From Token
			var userId = GetUserId();

			var result = await _service.AddContactAsync(dto, userId);
			if (result == null)
			{
				return BadRequest(new { message = "User not found or invalid data." });
			}
			return Ok(new { success = "Done", result });
		}
		#endregion

		#region GetAllContacts
		[HttpGet("GetAllContent")]
		[Authorize]
		public async Task<IActionResult> GetAllContacts()
		{
			var result = await _service.GetUserContactsAsync(GetUserId());
			if (result == null)
			{
				return NotFound(new { message = "Not Found Data." });
			}
			return Ok(new { success = "Done", result });
		}
		#endregion

		#region GetContact
		[Authorize]
        [HttpGet("{id}")]
		public async Task<IActionResult> GetContact(int id)
		{
			var contact = await _service.GetContactByIdAsync(id, GetUserId());
			if (contact == null)
			{
				return NotFound("Contact not found ");
			}

			return Ok(new { success = "Done", contact });
		} 
		#endregion

		#region DeleteContact
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteContact(int id)
		{

			var contact = await _service.GetContactByIdAsync(id, GetUserId());
			if (contact == null)
			{
				return NotFound("Contact not found or you cannot delete this.");
			}

			var deleted = await _service.DeleteContactAsync(id, GetUserId());
			if (!deleted) return NotFound("Delete Failed");
			return Ok("Delete Succeus");
		}
		#endregion

		#region GetContactsSorting
		[HttpGet("GetContactsSorting")]
		public async Task<IActionResult> GetContactsSorting([FromQuery] ContactQueryDto contactQueryDto)
		{
			// validate pageSize and pageNumber
			if (contactQueryDto.PageSize <= 0 || contactQueryDto.Page <= 0)
			{
				return BadRequest("Invalid page size or page number.");
			}

			var contacts = await _service.GetContactsAsync(GetUserId(), contactQueryDto);
			if (contacts == null)
				return NotFound("Not Found Contact");
			return Ok(new { success = "Done", contacts });
		} 
		#endregion

	}
}
