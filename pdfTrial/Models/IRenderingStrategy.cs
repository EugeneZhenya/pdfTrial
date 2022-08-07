using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public interface IRenderingStrategy : IEventListener
    {
        IList<PDFTextFullInfo> GetResultantRender();
    }
}