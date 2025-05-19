using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SAService.API.Manager.Interfaces;
using SAService.Application.DTOs;
using SAService.Application.Interfaces;
using SAService.Contracts.Constants;
using SAService.Domain.Entities;

namespace SAService.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class SystemLoginController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJwtService jwtService,
        ILogger<SystemLoginController> logger,
        ISystemLoginManager systemLoginMgr,
        RoleManager<IdentityRole<Guid>> roleManager
        ) : ControllerBase
    {
        private readonly IJwtService _jwtService = jwtService;
        private readonly ILogger<SystemLoginController> _logger = logger;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly ISystemLoginManager _systemLoginMgr = systemLoginMgr;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);


            var roleExists = await _roleManager.RoleExistsAsync(Role.User.ToString());
            if (!roleExists)
            {
                bool successRoleCreate = await _systemLoginMgr.CreateRole(Role.User.ToString());
                if (!successRoleCreate)
                {
                    return BadRequest("Can't create Role");
                }
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Role.User.ToString());
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    _logger.LogError("Error assigning role: {ErrorDescription}", error.Description);
                }
                return BadRequest(roleResult.Errors);
            }

            return Ok("User registered");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("AdminRegister")]
        public async Task<IActionResult> AdminRegister([FromBody] AdminRegisterDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            #region Check Role Exist
            var roleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!roleExists)
            {
                bool successRoleCreate = await _systemLoginMgr.CreateRole(request.Role);
                if (!successRoleCreate)
                {
                    return BadRequest("Can't create Role");
                }
            }
            #endregion

            var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    _logger.LogError("Error assigning role: {ErrorDescription}", error.Description);
                }
                return BadRequest(roleResult.Errors);
            }

            return Ok("User registered");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            try
            {
                _logger.LogInformation("Login API called at {Time}", DateTime.Now);

                // Attempt to sign in the user
                var result = await _signInManager.PasswordSignInAsync(requestDto.Username, requestDto.Password, false, false);
                if (!result.Succeeded)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                // Retrieve the user details
                var user = await _userManager.FindByNameAsync(requestDto.Username);
                if (user == null)
                {
                    return Unauthorized(new { message = "User not found." });
                }

                // Generate a JWT token for the user
                var token = await _jwtService.GenerateToken(user);

                _logger.LogInformation("Login API Finished at {Time}", DateTime.Now);

                // Return the token and user details
                return Ok(new
                {
                    message = "Login successful",
                    token,
                    user = new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.FirstName,
                        user.LastName
                    }
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred during login at {Time}", DateTime.Now);
                return BadRequest(e.Message);
            }
        }
    }
}
