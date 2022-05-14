using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_WASM.Shared.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Display(Name = "Сompany logo file")]
        public string LogoPath { get; set; } = string.Empty;
        [Display(Name = "Agreement file")]
        public string ContractPath { get; set; } = string.Empty;

        [Display(Name = "Supervisor's name")]
        public string HeaderName { get; set; } = string.Empty;
        [Display(Name = "The name of the company")]
        public string HeaderCompanyName { get; set; } = string.Empty;
        [Display(Name = "Position held in the company")]
        public string HeaderPost { get; set; } = string.Empty;
        [Display(Name = "Company address")]
        public string HeaderAddress { get; set; } = string.Empty;
        [Display(Name = "Additional field")]
        public string HeaderAdditional { get; set; } = string.Empty;
        [Display(Name = "Phone number")]
        public string HeaderPhone { get; set; } = string.Empty;
        [Display(Name = "E-mail company")]
        public string HeaderEmail { get; set; } = string.Empty;
        [Display(Name = "Website company")]
        public string HeaderWebSite { get; set; } = string.Empty;

        [Display(Name = "GST #")]
        public string GST { get; set; } = string.Empty;
        [Display(Name = "PST #")]
        public string PST { get; set; } = string.Empty;
        [Display(Name = "WCB #")]
        public string WCB { get; set; } = string.Empty;
        [Display(Name = "Liability #")]
        public string Liability { get; set; } = string.Empty;
        [Display(Name = "Licence #")]
        public string Licence { get; set; } = string.Empty;
        [Display(Name = "Incorporation #")]
        public string Incorporation { get; set; } = string.Empty;

        [Display(Name = "Percent TAX")]
        public int TAXpercent { get; set; } = 12;
        [Display(Name = "Percent GST")]
        public int GSTpercent { get; set; } = 5;

        public List<UserRegistered> UserRegistered { get; set; } = new List<UserRegistered>();
        public List<ClientJob> ClientJobs { get; set; } = new List<ClientJob>();

    }
}
