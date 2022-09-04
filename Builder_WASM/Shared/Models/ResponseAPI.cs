using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Models
{
    public class ResponseAPI<T>
    {
        public T Response { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; } 
    }
}
