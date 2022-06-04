using Builder_WASM.Server.Services;
using Builder_WASM.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Builder_WASM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _user;        
        public AccountController(IUserService user)
        {
            _user = user;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _user.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            response.Message = "User authentication was successful";
            return Ok(response);
        }

        [HttpPut("changelogin/{id}")]
        public async Task<IActionResult> ChangeLogin(int id, AuthenticateRequestChangeLogin model)
        {
           if(id != model.Id) return BadRequest(new { message = "Username or password is incorrect" });

            string role = User.IsInRole("Admin") ? "Admin" : "User";

            var response = await _user.ChangeLogin(model, role);

            if (response == null) return BadRequest(new { message = "Username or password is incorrect" });


            return Ok(response);
        }

        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, AuthenticateRequestChangePassword model)
        {
            if (id != model.Id) return BadRequest(new { message = "Username or password is incorrect" });

            string role = User.IsInRole("Admin") ? "Admin" : "User";

            var response = await _user.ChangePassword(model, role);

            if (response == null) return BadRequest(new { message = "Username or password is incorrect" });


            return Ok(response);
        }
    }
}
