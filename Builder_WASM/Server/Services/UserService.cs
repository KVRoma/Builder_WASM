using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Builder_WASM.Server.Services
{
    public class UserService : IUserService
    {
        
        //private List<UserRegistered> _users = new List<UserRegistered>
        //{
        //    new UserRegistered { Id = 1, Name = "Test", Role = "Admin", Password = "123" },
        //    new UserRegistered { Id = 2, Name = "Test2", Role = "User", Password = "123" }
        //};
        private readonly IUnitOfWork _context;
        public UserService(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task <AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            //var user = _users.SingleOrDefault(x => x.Name == model.Username && x.Password == model.Password);
            var result = await _context.UserRegisteredRepository.GetAsync(x=>x.Name == model.Username && x.Password == model.Password);
            var user = result.FirstOrDefault();
            // return null if user not found
            if (user == null) return null!;
            // authentication successful so generate jwt token            
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<ResponseMessage> ChangeLogin(AuthenticateRequestChangeLogin model, string role)
        {
            var result = await _context.UserRegisteredRepository.GetByIdAsync(model.Id);
            if (result == null) return null!;
                        
            UserMessage message = new UserMessage()
            {
                Message = role + ": Change login: " + result.Name + " -> " + model.Username,
            };
            result.Name = model.Username;
            result.Messages.Add(message);

            _context.UserRegisteredRepository.Update(result);
            await _context.SaveAsync();

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.Message = "Change login was success!";

            return responseMessage;
        }

        public async Task<ResponseMessage> ChangePassword(AuthenticateRequestChangePassword model, string role)
        {
            var result = await _context.UserRegisteredRepository.GetByIdAsync(model.Id);
            if (result == null) return null!;

            UserMessage message = new UserMessage()
            {
                Message = role + ": Change password."
            };
            result.Password = model.NewPassword;
            result.Messages.Add(message);

            _context.UserRegisteredRepository.Update(result);
            await _context.SaveAsync();

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.Message = "Change password was success!";

            return responseMessage;
        }


        // helper methods
        private string generateJwtToken(UserRegistered user)
        {          

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);                       

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow, 
                    claims: claimsIdentity.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
