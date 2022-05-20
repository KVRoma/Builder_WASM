using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DContractor
    {
        public int Id { get; set; }
        [Display(Name = "Contractor name")]
        [Required(ErrorMessage = "The field cannot be null !!! Please, write the name contractor. ")]
        public string Name { get; set; } = "";
        [Display(Name = "Contractor phone")]
        public string Phone { get; set; } = "";
        [Display(Name = "Contractor specialty")]
        public string Specialty { get; set; } = "";
        [Display(Name = "Contractor specialty")]
        public string SpecName
        {
            get { return Name + ((Specialty != "") ? (" (") : ("")) + Specialty + ((Specialty != "") ? (") ") : ("")); }
        }
        [Required]
        public string Color { get; set; } = "Black";

        public int CompanyId { get; set; }
        public Company Company { get; set; } = new Company();
    }
}
