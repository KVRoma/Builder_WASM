using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities.Dictionary
{
    public class DItem
    {
        public int Id { get; set; }
        [Display(Name = "Item")]
        public string NameItem { get; set; } = "";

        public int DGroupeId { get; set; }
        public DGroupe? DGroupe { get; set; }

        [Display(Name = "Supplier")]
        public int? DSapplierId { get; set; }
        public DSupplier? DSupplier { get; set; }
        public List<DDescription> DDescriptions { get; set; } = new List<DDescription>();
    }
}
