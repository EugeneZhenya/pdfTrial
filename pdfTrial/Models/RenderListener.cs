using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    public class RenderListener : IRenderingStrategy
    {
        private TextRenderInfo lastTextRenderInfo;
        private readonly IList<TextChunk> locationalResult = new List<TextChunk>();
        private readonly LocationTextExtractionStrategy.ITextChunkLocationStrategy tclStrat;
        public IList<PDFTextFullInfo> locationalfullResult = new List<PDFTextFullInfo>();
        private Rectangle textRectangle = null;

        public RenderListener() : this(new RenderListener.ITextChunkLocationStrategyImpl())
        {
        }

        public RenderListener(LocationTextExtractionStrategy.ITextChunkLocationStrategy strat) {
            tclStrat = strat;
        }

        public void EventOccurred(IEventData data, EventType type)
        {
            if (type.Equals(EventType.RENDER_TEXT))
            {
                TextRenderInfo renderInfo = (TextRenderInfo)data;
                LineSegment segment = renderInfo.GetBaseline();
                if (renderInfo.GetRise() != 0)
                {
                    // remove the rise from the baseline - we do this because the text from a super/subscript render operations should probably be considered as part of the baseline of the text the super/sub is relative to
                    Matrix riseOffsetTransform = new Matrix(0, -renderInfo.GetRise());
                    segment = segment.TransformBy(riseOffsetTransform);
                }

                TextChunk tc = new TextChunk(renderInfo.GetText(), tclStrat.CreateLocation(renderInfo, segment));
                locationalResult.Add(tc);

                var offset = tc.GetLocation().GetStartLocation();

                var font = renderInfo.GetFont();
                string fontString = font.GetFontProgram().ToString();
                string Bold = "";
                if (fontString.Substring(0, 1) == "*")
                {
                    if (fontString.Contains("Bold"))
                    {
                        if (fontString.Contains("BoldItalic"))
                        {
                            Bold = "italic bold";
                        }
                        else
                        {
                            Bold = "bold";
                        }
                    }

                    if (fontString.Contains("Arial"))
                    {
                        fontString = "Arial";
                    }
                    if (fontString.Contains("Times New Roman"))
                    {
                        fontString = "Times New Roman";
                    }
                    if (fontString.Contains("Verdana"))
                    {
                        fontString = "Verdana";
                    }
                    if (fontString.Contains("Courier"))
                    {
                        fontString = "Courier";
                    }
                }

                var lastInfo = new PDFTextFullInfo();
                lastInfo.Text = renderInfo.GetText();
                lastInfo.CharSpacing = renderInfo.GetCharSpacing();
                lastInfo.FillColor = renderInfo.GetFillColor();
                lastInfo.Font = fontString;
                lastInfo.FontSize = renderInfo.GetFontSize();
                lastInfo.Weight = Bold;
                lastInfo.OffsetX = (int)Math.Round(offset.Get(0), 0);
                lastInfo.OffsetY = (int)Math.Round(offset.Get(1), 0);

                locationalfullResult.Add(lastInfo);
            }
        }

        public ICollection<EventType> GetSupportedEvents()
        {
            return null;
        }

        private CanvasTag FindLastTagWithActualText(IList<CanvasTag> canvasTagHierarchy)
        {
            CanvasTag lastActualText = null;
            foreach (CanvasTag tag in canvasTagHierarchy)
            {
                if (tag.GetActualText() != null)
                {
                    lastActualText = tag;
                    break;
                }
            }
            return lastActualText;
        }

        public IList<PDFTextFullInfo> GetResultantRender()
        {
            return locationalfullResult;
        }

        public interface ITextChunkLocationStrategy
        {
            ITextChunkLocation CreateLocation(TextRenderInfo renderInfo, LineSegment baseline);
        }

        private class TextChunkMarks
        {
            internal IList<TextChunk> preceding = new List<TextChunk>();

            internal IList<TextChunk> succeeding = new List<TextChunk>();
        }

        private sealed class ITextChunkLocationStrategyImpl : LocationTextExtractionStrategy.ITextChunkLocationStrategy
        {
            public ITextChunkLocation CreateLocation(TextRenderInfo renderInfo, LineSegment baseline)
            {
                return new TextChunkLocationDefaultImp(baseline.GetStartPoint(), baseline.GetEndPoint(), renderInfo.GetSingleSpaceWidth());
            }
        }
    }
}