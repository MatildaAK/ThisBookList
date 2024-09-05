using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpGet]
        [Route("GetUsers")]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUser")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user != null)
            {
                return Ok(user);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Registration")]

        public async Task<IActionResult> Registration(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUser = _context.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null)
            {
                _context.Users.Add(new Models.User
                {
                    Name = userDTO.Name,
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                });

                await _context.SaveChangesAsync();
                return Ok("användaren registrerad!");
            }
            else
            {
                return BadRequest("Användare finns redan med denna Email");
            }
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user != null)
            {
                var claims = new[]
                {
              new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim("UserId", user.UserId.ToString()),
              new Claim("Email", user.Email.ToString())
          };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn
                    );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { Token = tokenValue, User = user });

                //return Ok(user);
            }
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                return NotFound("Användare hittades inte.");
            }

            user.Name = userDTO.Name;
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;

            await _context.SaveChangesAsync();

            return Ok("Användaren uppdaterades!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                return NotFound("Användare hittades inte.");
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok("Användaren togs bort!");
        }

    }
}