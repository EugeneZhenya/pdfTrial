using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class PDFFieldFullInfo
    {
        public string Name { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public int BorderWidth { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public PdfObject Value { get; set; }
    }
}