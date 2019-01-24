using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Blog.Dtos;
using Blog.Entities;
using Blog.Services.Exceptions;
using Blog.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthenticationController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody]UserDto userDto)
        //{
        //    var user = _userService.Authenticate(userDto.Email, userDto.Password);

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    // return basic user info (without password) and token to store client side
        //    return Ok(new
        //    {
        //        Id = user.Id,
        //        Username = user.Email,
        //        DisplayName = user.DisplayName,
        //        Token = tokenString
        //    });
        //}

        ////[AllowAnonymous]
        ////[HttpPost("register")]
        //public IActionResult Register([FromBody]UserDto userDto)
        //{
        //    // map dto to entity
        //    var user = _mapper.Map<Model>(userDto);

        //    try
        //    {
        //        // save 
        //        _userService.Create(user, userDto.Password);
        //        return Ok();
        //    }
        //    catch (LoginException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        ////[AllowAnonymous]
        ////[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var user = _userService.GetById(id);
        //    var userDto = _mapper.Map<UserDto>(user);
        //    return Ok(userDto);
        //}
    }
}