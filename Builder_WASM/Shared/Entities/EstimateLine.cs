using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class EstimateLine
    {
        public int Id { get; set; }
        [Display(Name = "Groupe")]
        public string Groupe { get; set; } = "";
        [Display(Name = "Item")]
        public string Item { get; set; } = "";
        [Display(Name = "Description")]
        public string Description { get; set; } = "";
        [Display(Name = "Rate")]
        public decimal Rate { get; set; } = 0m;
        [Display(Name = "Count")]
        public decimal Count { get; set; } = 0m;
        [Display(Name = "Price")]
        public decimal Price { get { return decimal.Round(Rate * Count, 2); } }
        public EstimateLineType Type { get; set; }

        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; } = new Estimate();
    }
}
