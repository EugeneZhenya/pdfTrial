using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class IsoAgentAgreementForm
    {
        [Required]
        [Display(Name = "IN1")]
        [DisplayName("INITIALS")]
        public string Initials { get; set; }
        [Required]
        [Display(Name = "IN2")]
        [DisplayName("INITIALS_2")]
        public string Initials_2 { get; set; }
        [Required]
        [Display(Name = "IN3")]
        [DisplayName("INITIALS_3")]
        public string Initials_3 { get; set; }
        [Required]
        [Display(Name = "IN4")]
        [DisplayName("INITIALS_4")]
        public string Initials_4 { get; set; }
        [Required]
        [Display(Name = "IN5")]
        [DisplayName("INITIALS_5")]
        public string Initials_5 { get; set; }
        [Required]
        [Display(Name = "IN6")]
        [DisplayName("INITIALS_6")]
        public string Initials_6 { get; set; }
        [Required]
        [Display(Name = "IN7")]
        [DisplayName("INITIALS_7")]
        public string Initials_7 { get; set; }
        [Required]
        [Display(Name = "T10")]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "IN8")]
        [DisplayName("INITIALS_8")]
        public string Initials_8 { get; set; }
        [Required]
        [Display(Name = "IN9")]
        [DisplayName("INITIALS_9")]
        public string Initials_9 { get; set; }
        [Required]
        [Display(Name = "BN10")]
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [Required]
        [Display(Name = "DBA10")]
        [DisplayName("DBA")]
        public string DBA { get; set; }
        [Required]
        [Display(Name = "SSN10")]
        [DisplayName("FEIN or SS")]
        public string FeinOrSs { get; set; }
        [Required]
        [Display(Name = "BNK10")]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }
        [Required]
        [Display(Name = "RN")]
        [DisplayName("Bank ABA or Transit Routing Number")]
        public string BankABAorTRN { get; set; }
        [Required]
        [Display(Name = "AN")]
        [DisplayName("Bank Account Number")]
        public string BankAccountNumber { get; set; }
        [Required]
        [Display(Name = "IN10")]
        [DisplayName("INITIALS_10")]
        public string Initials_10 { get; set; }
        [Required]
        [Display(Name = "P1T")]
        [DisplayName("Party")]
        public string Party { get; set; }
        [Required]
        [Display(Name = "AD")]
        [DisplayName("adress")]
        public string Adress { get; set; }
        [Required]
        [Display(Name = "Ag10")]
        [DisplayName("Agent")]
        public string Agent { get; set; }
        [Required]
        [Display(Name = "PR10")]
        [DisplayName("Print Name")]
        public string PrintName { get; set; }
        [Required]
        [Display(Name = "Sig9")]
        [DisplayName("Signature")]
        public string Signature { get; set; }
        [Required]
        [Display(Name = "D8")]
        [DisplayName("Date")]
        public string Date { get; set; }
        [Required]
        [Display(Name = "D10")]
        [DisplayName("Date10")]
        public string Date10 { get; set; }
        [Required]
        [Display(Name = "TAcc")]
        [DisplayName("Savings Or Checking")]
        public bool SavingsOrChecking { get; set; }
        [Required]
        [Display(Name = "Sig10")]
        [DisplayName("signature10")]
        public string signature10 { get; set; }
    }
}