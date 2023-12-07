using JWTDemo002.Model;
using JWTDemo002.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace JWTDemo002.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("insert")]
        public async Task<IActionResult> Insert(UserDto userDto)
        {
            try
            {
                var res = await _userService.InsertAsync(userDto);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var res = await _userService.GetAllAsync();
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var res = await _userService.GetAsync(id);
                if (res == null)
                {
                    return BadRequest("User not found");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            try
            {

                var res = await _userService.LoginAsync(request);
                return StatusCode((int)res.StatusCode, JsonConvert.SerializeObject(res));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
