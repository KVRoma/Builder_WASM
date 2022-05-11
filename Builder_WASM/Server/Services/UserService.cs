using Builder_WASM.Shared.Entities;
using Builder_WASM.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Builder_WASM.Server.Services
{
    public class UserService : IUserService
    {
        private List<UserRegistered> _users = new List<UserRegistered>
        {
            new UserRegistered { Id = 1, Name = "Test", Role = "Admin", Password = "123" }
        };
        
        
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Name == model.Username && x.Password == model.Password);
            // return null if user not found
            if (user == null) return null!;
            // authentication successful so generate jwt token            
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
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
