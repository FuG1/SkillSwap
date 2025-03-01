using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using learn_asp.Data;
using learn_asp.Models;
using System.Threading.Tasks;

namespace learn_asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.RegisteredUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user.Username);
        }
    }
}