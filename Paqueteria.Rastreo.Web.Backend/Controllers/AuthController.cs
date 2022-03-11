using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.Rastreo.Web.Backend.Models.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Paqueteria.Rastreo.Web.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User _User = new User();
        public IConfiguration _Configuration { get; }

        public AuthController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto Request)
        {
            CreatePasswordHash(Request.Password,out byte[] PasswordHash,out byte[] PasswordSalt);
            _User.Username = Request.Username;
            _User.PasswordHash = PasswordHash;
            _User.PasswordSalt = PasswordSalt;
            return Ok(_User);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto Request)
        {
            if(_User.Username != Request.Username)
                return BadRequest("User not found");
            if (!VerifyPasswordHash(Request.Password, _User.PasswordHash, _User.PasswordSalt))
                return BadRequest("Wrong password");
            var _Token = CreateToken(_User);
            return Ok(_Token);
        }
        private string CreateToken(User User)
        {
            List<Claim> _Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _User.Username)
            };
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _Configuration.GetSection("AppSettings:Token").Value));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var Token = new JwtSecurityToken(
                claims:_Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: Creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);
            return jwt;
        }
        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            }
        }
        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var _ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return _ComputedHash.SequenceEqual(PasswordHash);
            }
        }

    }
}
