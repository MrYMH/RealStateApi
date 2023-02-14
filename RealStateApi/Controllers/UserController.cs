using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealStateApi.Data;
using RealStateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealStateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _db;
        private readonly IConfiguration _config;

        public UserController(ApiDbContext db , IConfiguration config)
        {
            _db = db;
            _config= config;
        }

        //=======>/api/User/register
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            //check if email exist
            var userExist = _db.User.FirstOrDefault(u=>u.Email == user.Email);
            if(userExist != null)
            {
                return BadRequest("user already exists");
            }

            //add user to db
            _db.User.Add(user);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }


        //========>/api/User/Login
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            //check if user with email and address already exists
            var curr = _db.User.FirstOrDefault(u=>u.Email == user.Email && u.Password == user.Password);
            if(curr == null)
            {
                return NotFound();
            }

            //add(create) jwt
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentails = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials:credentails

                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);

        }


    }
}
