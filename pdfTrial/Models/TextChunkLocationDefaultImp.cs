using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pdfTrial.Models
{
    internal class TextChunkLocationDefaultImp : ITextChunkLocation
    {
        private const float DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION = 2;
        private readonly Vector startLocation;
        private readonly Vector endLocation;
        private readonly Vector orientationVector;
        private readonly int orientationMagnitude;
        private readonly int distPerpendicular;
        private readonly float distParallelStart;
        private readonly float distParallelEnd;
        private readonly float charSpaceWidth;

        public TextChunkLocationDefaultImp(Vector startLocation, Vector endLocation, float charSpaceWidth)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.charSpaceWidth = charSpaceWidth;
            Vector oVector = endLocation.Subtract(startLocation);
            if (oVector.Length() == 0)
            {
                oVector = new Vector(1, 0, 0);
            }
            orientationVector = oVector.Normalize();
            orientationMagnitude = (int)(Math.Atan2(orientationVector.Get(Vector.I2), orientationVector.Get(Vector.I1)
                ) * 1000);
            // see http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
            // the two vectors we are crossing are in the same plane, so the result will be purely
            // in the z-axis (out of plane) direction, so we just take the I3 component of the result
            Vector origin = new Vector(0, 0, 1);
            distPerpendicular = (int)(startLocation.Subtract(origin)).Cross(orientationVector).Get(Vector.I3);
            distParallelStart = orientationVector.Dot(startLocation);
            distParallelEnd = orientationVector.Dot(endLocation);
        }

        public virtual float DistanceFromEndOf(ITextChunkLocation other)
        {
            return DistParallelStart() - other.DistParallelEnd();
        }

        public virtual float DistParallelEnd()
        {
            return distParallelEnd;
        }

        public virtual float DistParallelStart()
        {
            return distParallelStart;
        }

        public virtual int DistPerpendicular()
        {
            return distPerpendicular;
        }

        public virtual float GetCharSpaceWidth()
        {
            return charSpaceWidth;
        }

        public virtual Vector GetEndLocation()
        {
            return endLocation;
        }

        public virtual Vector GetStartLocation()
        {
            return startLocation;
        }

        public virtual bool IsAtWordBoundary(ITextChunkLocation previous)
        {
            if (startLocation.Equals(endLocation) || previous.GetEndLocation().Equals(previous.GetStartLocation()))
            {
                return false;
            }
            float dist = DistanceFromEndOf(previous);
            if (dist < 0)
            {
                dist = previous.DistanceFromEndOf(this);
                //The situation when the chunks intersect. We don't need to add space in this case
                if (dist < 0)
                {
                    return false;
                }
            }
            return dist > GetCharSpaceWidth() / 2.0f;
        }

        public virtual int OrientationMagnitude()
        {
            return orientationMagnitude;
        }

        public virtual bool SameLine(ITextChunkLocation @as)
        {
            if (OrientationMagnitude() != @as.OrientationMagnitude())
            {
                return false;
            }
            float distPerpendicularDiff = DistPerpendicular() - @as.DistPerpendicular();
            if (distPerpendicularDiff == 0)
            {
                return true;
            }
            LineSegment mySegment = new LineSegment(startLocation, endLocation);
            LineSegment otherSegment = new LineSegment(@as.GetStartLocation(), @as.GetEndLocation());
            return Math.Abs(distPerpendicularDiff) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION && (mySegment.GetLength
                () == 0 || otherSegment.GetLength() == 0);
        }

        internal static bool ContainsMark(ITextChunkLocation baseLocation, ITextChunkLocation markLocation)
        {
            return baseLocation.GetStartLocation().Get(Vector.I1) <= markLocation.GetStartLocation().Get(Vector.I1) &&
                 baseLocation.GetEndLocation().Get(Vector.I1) >= markLocation.GetEndLocation().Get(Vector.I1) && Math.
                Abs(baseLocation.DistPerpendicular() - markLocation.DistPerpendicular()) <= DIACRITICAL_MARKS_ALLOWED_VERTICAL_DEVIATION;
        }
    }
}