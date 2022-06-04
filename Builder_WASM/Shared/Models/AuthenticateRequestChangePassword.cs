using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Models
{
    public class AuthenticateRequestChangePassword
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        
        
        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
       
    }
}
