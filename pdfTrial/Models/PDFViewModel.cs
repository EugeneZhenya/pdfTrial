using iText.Forms.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class PDFViewModel
    {
        public string DefaultFont { get; set; }
        public IList<PDFTextFullInfo> Texts { get; set; }
        public IList<PDFFieldFullInfo> Fields { get; set; }
        public int PageHeight { get; set; }
    }
}