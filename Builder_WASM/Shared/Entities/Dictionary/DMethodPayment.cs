using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DMethodPayment
    {
        public int Id { get; set; }
        [Display(Name = "Percent")]
        public decimal PercentMethod { get; set; }
        [Display(Name = "Name method payment")]
        [Required(ErrorMessage = "The field cannot be null !!! Please, write the name method. ")]
        public string NameMethod { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
