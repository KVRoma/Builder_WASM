using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class UserRegistered
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;


        [JsonIgnore]
        public string Password { get; set; } = string.Empty;

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        [JsonIgnore]
        public List<UserMessage> Messages { get; set; } = new List<UserMessage>();  

        /// <summary>
        /// Copies all fields except the password
        /// </summary>
        /// <param name="user"></param>
        public void CopyWithoutPassword(UserRegistered user)
        {
            Name = user.Name;
            Role = user.Role;
            CompanyId = user.CompanyId;
            Company = user.Company;
            Messages = user.Messages;
        }
    }
}
