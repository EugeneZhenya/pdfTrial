using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class W9Form
    {
        [Required]
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public bool Individual { get; set; }
        public bool CCorporation { get; set; }
        public bool SCorporation { get; set; }
        public bool Partnership { get; set; }
        public bool TrustOrEstate { get; set; }
        public bool Limited { get; set; }
        public bool Other { get; set; }
        public string PayeeCode { get; set; }
        public string ReportingCode { get; set; }
        [Required]
        public string Address { get; set; }
        public string RequesterInfo { get; set; }
        [Required]
        public string CityAndState { get; set; }
        public string AccountNumber { get; set; }
        public string SocialNumberStart { get; set; }
        public string SocialNumberMiddle { get; set; }
        public string SocialNumberEnd { get; set; }
        public string EmployerIdStart { get; set; }
        public string EmployerIdEnd { get; set; }
        public DateTime SignDate { get; set; }
    }
}