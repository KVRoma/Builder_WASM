using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Today;
        public bool ReadByAdmin { get; set; } = false;
        public bool ReadByUser { get; set; } = false;
        public string Message { get; set; } = string.Empty;

        public int UserRegisteredId { get; set; }
        public UserRegistered? UserRegistered { get; set; }
    }
}
