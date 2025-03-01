using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using learn_asp.Models;
using learn_asp.Helpers;
using learn_asp.Data;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!IsEmailValid(model.Email))
            {
                return BadRequest("Provided email is not valid");
            }
            
            if (await _context.RegisteredUsers.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict("Email already exists");
            }
            
            model.Password = PasswordHashProvider.HashPassword(model.Password);
            _context.RegisteredUsers.Add(model);
            await _context.SaveChangesAsync();
            
            return Ok($"User {model.Username} registered successfully with id: {model.Id}");
        }

        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthorizModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisterModel? user;
            if (model.UsernameOrEmail.Contains("@"))
            {
                user = await _context.RegisteredUsers
                    .FirstOrDefaultAsync(u => u.Email == model.UsernameOrEmail);
            }
            else
            {
                user = await _context.RegisteredUsers
                    .FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail);
            }
            
            if (user == null || !PasswordHashProvider.VerifyPassword(user.Password, model.Password))
            {
                return Unauthorized();
            }

            return Ok();
        }
        
        // Обновлённая проверка валидности email по синтаксису с использованием регулярного выражения
        private bool IsEmailValid(string email)
        {
            // Проверочный формат: user@service.domain (например, user@gmail.com)
            bool isValidFormat = System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            
            // При необходимости, можно дополнительно проверить существование почты через Aspose.Email:
            // var validator = new Aspose.Email.EmailAddressValidator();
            // return isValidFormat && validator.Validate(email);
            
            return isValidFormat;
        }
    }
}