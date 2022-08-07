using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class PDFTextFullInfo
    {
        public float CharSpacing { get; set; }
        public Color FillColor { get; set; }
        public string Font { get; set; }
        public string Weight { get; set; }
        public float FontSize { get; set; }
        public string Text { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }
}