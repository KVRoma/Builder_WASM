using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DDescription
    {
        public int Id { get; set; }
        [Display(Name = "Description")]
        public string NameDescription { get; set; } = "";
        [Display(Name = "Rate")]
        public decimal RateDescription { get; set; }

        public int DItemId { get; set; }
        public DItem? DItem { get; set; } = new DItem();
    }
}
