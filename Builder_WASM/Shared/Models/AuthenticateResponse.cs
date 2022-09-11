using Builder_WASM.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;        

        public AuthenticateResponse()
        {
        }
        public AuthenticateResponse(UserRegistered user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Role = user.Role;            
            Token = token;            
        }
    }
}
