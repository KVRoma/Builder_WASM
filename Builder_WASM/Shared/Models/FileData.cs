using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Models
{
    public class FileData
    {
        public byte[]? Data { get; set; }
        public string FileType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Extension { get; set; } = string.Empty;
    }
}
