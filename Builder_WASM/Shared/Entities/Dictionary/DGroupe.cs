using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DGroupe
    {
        public int Id { get; set; }
        [Display(Name = "Group")]
        public string NameGroupe { get; set; } = "";
        public EstimateLineType Type { get; set; }

        public List<DItem> DItems { get; set; } = new List<DItem>();
    }
}
