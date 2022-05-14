using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        [Display(Name = "TID")]
        public string TID { get; set; } = "";
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DatePayment { get; set; } = DateTime.Now;
        [Display(Name = "Amount")]
        public decimal AmountPayment { get; set; }
        [Display(Name = "Method")]
        public string MethodPayment { get; set; } = "";
        [Display(Name = "Percent")]
        public decimal PercentMethodPayment { get; set; }
        public decimal TotalPayment
        {
            get
            {
                return decimal.Round(AmountPayment - (AmountPayment * (PercentMethodPayment / 100m)), 2);
            }
        }

        public int EstimateId { get; set; }
        public Estimate Estimate { set; get; } = new Estimate();
    }
}
