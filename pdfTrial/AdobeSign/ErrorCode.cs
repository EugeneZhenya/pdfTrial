using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.AdobeSign
{
    public class ErrorCode
    {
        public string code { get; set; }
        public string message { get; set; }

        public string error_description { get; set; }

        public string error { get; set; }
    }
}