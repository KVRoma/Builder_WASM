using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class Estimate
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateEstimate { get; set; } = DateTime.Now;
        public string Description { get; set; } = "";
        public string Note { get; set; } = "";

        [Display(Name = "Material Subtotal")]
        public decimal MaterialSubtotal { get; set; }
        [Display(Name = "TAX percent")]
        public int TAXpercent { get; set; }
        [Display(Name = "TAX")]
        public decimal MaterialTax
        {
            get { return decimal.Round(MaterialSubtotal * (TAXpercent / 100m), 2); }        //0.12m    
        }
        [Display(Name = "Material Discount")]
        public decimal MaterialDiscount { get; set; }
        [Display(Name = "Material Total")]
        public decimal MaterialTotal
        {
            get { return (MaterialSubtotal + MaterialTax) - MaterialDiscount; }
        }




        [Display(Name = "Labour Subtotal")]
        public decimal LabourSubtotal { get; set; }
        [Display(Name = "GST percent")]
        public int GSTpercent { get; set; }
        [Display(Name = "GST")]
        public decimal LabourTax
        {
            get { return decimal.Round(LabourSubtotal * (GSTpercent / 100m), 2); }     //0.05m
        }
        [Display(Name = "Labour Discount")]
        public decimal LabourDiscount { get; set; }
        [Display(Name = "Labours Total")]
        public decimal LabourTotal
        {
            get { return (LabourSubtotal + LabourTax) - LabourDiscount; }
        }


        [Display(Name = "Project Total")]
        public decimal ProjectTotal
        {
            get
            {
                return MaterialTotal + LabourTotal;
            }
        }
        //***************************************************************************For Credits********
        public bool FinancingYesNo { get; set; }
        /// <summary>
        /// Для ручного ввода депозиту
        /// </summary>
        public decimal FinancingUser { get; set; }
        public decimal AmountPaidByCreditCard { get; set; }
        public decimal FinancingAmount
        {
            get { return (FinancingYesNo) ? (ProjectTotal) : (0m); }
        }
        public decimal FinancingFee
        {
            get { return decimal.Round((FinancingYesNo) ? (FinancingAmount * 0.08m) : (0m), 2); }
        }
        public decimal ProcessingFee
        {
            get { return decimal.Round(AmountPaidByCreditCard * 0.03m, 2); }
        }
        public decimal InvoiceGrandTotal
        {
            get { return ProjectTotal + FinancingFee + ProcessingFee; }
        }
        //**********************************************************************************************
        [Display(Name = "Payments")]
        public decimal TotalPayment { get; set; }
        public decimal Balance
        {
            get
            {
                return decimal.Round((MaterialTotal + LabourTotal + ProcessingFee + FinancingFee) - TotalPayment, 2);
            }
        }

        [Display(Name = "Id")]
        public string NumberEstimate
        {
            get
            {
                return "Q" + (Id + 1000).ToString();
            }
        }

        [Display(Name = "Client")]
        public int ClientJobId { get; set; }
        public ClientJob ClientJob { get; set; } = new ClientJob();


        public List<EstimateLine> EstimateLines { get; set; } = new List<EstimateLine>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
