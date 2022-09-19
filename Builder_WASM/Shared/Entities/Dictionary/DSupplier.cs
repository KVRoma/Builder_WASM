using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DSupplier
    {
        public int Id { get; set; }
        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "The Name field cannot be null !!! Please, write the name. ")]
        public string NameSupplier { get; set; } = "";

        [Display(Name = "Address")]
        [Required(ErrorMessage = "The Address field cannot be null !!! Please, write the Address. ")]
        public string AddressSupplier { get; set; } = "";

        public List<DItem>? DItems { get; set; } = new List<DItem>();
        
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
