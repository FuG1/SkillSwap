using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using learn_asp.Models;
using learn_asp.Helpers;
using learn_asp.Data;
using System.Threading.Tasks;

namespace learn_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public SampleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            model.Password = PasswordHelper.HashPassword(model.Password);  
            _context.RegisteredUsers.Add(model);
            await _context.SaveChangesAsync();
            
            return Ok($"User {model.Username} registered successfully with id: {model.Id}");
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthorizModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.RegisteredUsers
                .FirstOrDefaultAsync(u => u.Username == model.Username);
            
            if (user == null || !PasswordHelper.VerifyPassword(user.Password, model.Password))
            {
                return Unauthorized("Wrong Password or Username");
            }

            return Ok("Login successful!");
        }

        [HttpGet("getUserForId/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.RegisteredUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} not found.");
            }
            
            return Ok(new { user.Username });
        }
    }
}