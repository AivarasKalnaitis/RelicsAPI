using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RelicsAPI.Auth;
using RelicsAPI.Auth.Model;
using RelicsAPI.Data.DTOs.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelicsAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<User> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(registerUserDTO.Email);

            if (user != null)
                return BadRequest("Such email already in use !");

            var newUser = new User
            {
                Email = registerUserDTO.Email,
                UserName = registerUserDTO.UserName
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, registerUserDTO.Password);

            if (!createdUserResult.Succeeded)
                return BadRequest("Name already in use !");

            await _userManager.AddToRoleAsync(newUser, UserRoles.Customer);

            return CreatedAtAction(nameof(Register), _mapper.Map<UserDTO>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return BadRequest("User email or password is invalid.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!isPasswordValid)
                return BadRequest("User email or password is invalid.");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);

            return Ok(accessToken);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken(TokenRequest tokenRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }

            var result = await _tokenManager.VerifyAndGenerateToken(tokenRequest);

            if (result == null)
                BadRequest("Invalid tokens");

            return Ok(result);
        }
    }
}
