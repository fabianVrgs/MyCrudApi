using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCrudApi.Data;
using MyCrudApi.models;

namespace MyCrudApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly MyCrudContext _context;
		public UserController(MyCrudContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return Json(await _context.User.ToArrayAsync());
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.User == null)
			{
				return NotFound();
			}

			var user = await _context.User.
				FirstOrDefaultAsync(m =>m.Id == id);
			if(user == null)
			{
				return NotFound();
			}
				return Json(user);
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Password")] User user)
		{
			if (ModelState.IsValid)
			{
				_context.Add(user);
				await _context.SaveChangesAsync();
				return Json(user);
			}
			return Json(user);
		}
		[HttpPatch("{id}")]
		public async Task<IActionResult> Update(int? id, [FromBody] User user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}

			_context.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return Json (user);		
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{

			var user = await _context.User.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.User.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
