using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Models
{
    public class ResponseAPI<T>
    {
        private string message = string.Empty;
        public T Response { get; set; }
        public string Message 
        {
            get { return message.Split(":")[1]?.Split("}")[0] ?? ""; }
            set { message = value; } 
        }
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; } 
    }
}
