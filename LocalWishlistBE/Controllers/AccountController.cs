using AutoMapper;
using LocalWishlistBE.JwtFeatures;
using LocalWishlistBE.Models;
using LocalWishlistBE.Models.DTO;
using LocalWishlistBE.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LocalWishlistBE.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private IRepositoryWrapper _repoWrapper;
        private readonly JwtHandler _jwtHandler;

        public AccountController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler, IRepositoryWrapper repoWrapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _repoWrapper = repoWrapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            User user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> UserCreate([FromBody] UserForRegistrationDto userForRegistration)
        {

            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            User userBbdd = await _userManager.FindByNameAsync(userForRegistration.Email);

            if (userBbdd != null)
                return BadRequest("Este mail ya esta registrado");

            var user = _mapper.Map<User>(userForRegistration);

            try
            {
                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Algo ha ido mal al guardar el usuario nuevo");
            }
        }
    }
}
