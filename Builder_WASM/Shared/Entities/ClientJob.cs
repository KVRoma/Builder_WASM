using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class ClientJob
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Date registration")]
        public DateTime DateRegistration { get; set; } = DateTime.Today;
        [Display(Name = "Client Id")]
        public string NumberClient
        {
            get { return "C" + (Id + 1000).ToString(); }
        }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = "";

        [Required(ErrorMessage = "The Name field cannot be null !!! Please, write the name. ")]
        [MinLength(2), MaxLength(50)]
        [Display(Name = "Primary first name")]
        public string PrimaryFirstName { get; set; } = "";
        [Display(Name = "Primary last name")]
        public string PrimaryLastName { get; set; } = "";
        [Display(Name = "Primary phone number")]
        public string PrimaryPhoneNumber { get; set; } = "";
        [Display(Name = "Primary email")]
        public string PrimaryEmail { get; set; } = "";
        [Display(Name = "Primary full name")]
        public string PrimaryFullName
        {
            get { return PrimaryFirstName + " " + PrimaryLastName; }
        }

        public string NameSearch
        {
            get { return CompanyName + " " + PrimaryFirstName + " " + PrimaryLastName + " " + PrimaryPhoneNumber + " " + PrimaryEmail; }
        }

        [Display(Name = "Secondary first name")]
        public string SecondaryFirstName { get; set; } = "";
        [Display(Name = "Secondary last name")]
        public string SecondaryLastName { get; set; } = "";
        [Display(Name = "Secondary phone number")]
        public string SecondaryPhoneNumber { get; set; } = "";
        [Display(Name = "Secondary email")]
        public string SecondaryEmail { get; set; } = "";
        [Display(Name = "Secondary full name")]
        public string SecondaryFullName
        {
            get { return SecondaryFirstName + " " + SecondaryLastName; }
        }

        [Display(Name = "Address bill street")]
        public string AddressBillStreet { get; set; } = "";
        [Display(Name = "Address bill city")]
        public string AddressBillCity { get; set; } = "";
        [Display(Name = "Address bill province")]
        public string AddressBillProvince { get; set; } = "";
        [Display(Name = "Address bill postal code")]
        public string AddressBillPostalCode { get; set; } = "";
        [Display(Name = "Address bill country")]
        public string AddressBillCountry { get; set; } = "";
        [Display(Name = "Address bill full")]
        public string AddressBillFull
        {
            get
            {
                return (string.IsNullOrWhiteSpace(AddressBillStreet) ? "" : (AddressBillStreet + ", ")) +
                       (string.IsNullOrWhiteSpace(AddressBillCity) ? "" : (AddressBillCity + ", ")) +
                       (string.IsNullOrWhiteSpace(AddressBillProvince) ? "" : (AddressBillProvince + ", ")) +
                       (string.IsNullOrWhiteSpace(AddressBillPostalCode) ? "" : (AddressBillPostalCode + ", ")) +
                       (string.IsNullOrWhiteSpace(AddressBillCountry) ? "" : (AddressBillCountry));
            }
        }

        [Display(Name = "Address site street")]
        public string AddressSiteStreet { get; set; } = "";
        [Display(Name = "Address site city")]
        public string AddressSiteCity { get; set; } = "";
        [Display(Name = "Address site province")]
        public string AddressSiteProvince { get; set; } = "";
        [Display(Name = "Address site postal code")]
        public string AddressSitePostalCode { get; set; } = "";
        [Display(Name = "Address site country")]
        public string AddressSiteCountry { get; set; } = "";
        [Display(Name = "Address site full")]
        public string AddressSiteFull
        {
            get
            {
                return (string.IsNullOrWhiteSpace(AddressSiteStreet) ? "" : (AddressSiteStreet + ", ")) +
                        (string.IsNullOrWhiteSpace(AddressSiteCity) ? "" : (AddressSiteCity + ", ")) +
                        (string.IsNullOrWhiteSpace(AddressSiteProvince) ? "" : (AddressSiteProvince + ", ")) +
                        (string.IsNullOrWhiteSpace(AddressSitePostalCode) ? "" : (AddressSitePostalCode + ", ")) +
                        (string.IsNullOrWhiteSpace(AddressSiteCountry) ? "" : (AddressSiteCountry));
            }
        }

        [Display(Name = "Specify")]
        public string Specify { get; set; } = "";
        [Display(Name = "Notes")]
        public string Notes { get; set; } = "";



        public List<Estimate> Estimates { get; set; } = new List<Estimate>();

        public int CompanyId { get; set; }
        public Company Company { get; set; } = new Company();
    }
}
