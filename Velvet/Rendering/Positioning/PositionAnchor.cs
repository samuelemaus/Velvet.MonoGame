using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Velvet.Rendering
{
    public class PositionAnchor
    {
        #region//Fields & Properties
        public RectRelativity XRelativity { get; set; }
        public RectRelativity YRelativity { get; set; }

        private ReferencePoint _RefPoint;
        public ReferencePoint ReferencePoint { get; set; }

        public ReferenceRect SourceRect;

        public object XTargetValue { get; private set; }
        public object YTargetValue { get; private set; }

        public virtual ReferenceRect XTargetRect { get; set; }
        public virtual ReferenceRect YTargetRect { get; set; }

        private bool IsMultiRect()
        {
            return (XTargetRect != null && YTargetRect != null);
        }

        public virtual float XRatio { get; set; }
        public virtual float YRatio { get; set; }

        public int XOffset;
        public int YOffset;


        public virtual Vector2 PositionDifferential
        {
            get
            {
                return RenderingExtensions.GetRectDifferential(SourceRect.Content, XTargetRect.Content, YTargetRect.Content, XRelativity, YRelativity, ReferencePoint, XOffset);
            }
        }

        #endregion

        #region//Constructors
        public PositionAnchor()
        {

        }


        /// <summary>
        /// hello
        /// </summary>
        /// <param name="source"> is this where I type </param>
        /// <param name="target"></param>
        /// <param name="rel"></param>
        /// <param name="refPoint"></param>
        /// <param name="offset">Uniform offset used for both XOffset and YOffset Properties</param>
        public PositionAnchor(ReferenceRect source, ReferenceRect target, RectRelativity rel, ReferencePoint refPoint, int offset)
        {
            SourceRect = source;
            XTargetRect = target;
            YTargetRect = target;

            XRelativity = rel;
            YRelativity = rel;
            ReferencePoint = refPoint;
            XOffset = offset;
            YOffset = offset;

            if (ReferencePoint == ReferencePoint.Centered)
            {
                XRelativity = RectRelativity.Inside;
            }

            //PositionDifferential = Extensions.GetRectDifferential(SourceRect.Content, TargetRect.Content, Relativity, XPosition,YPosition, Offset);

        }

        public PositionAnchor(ReferenceRect source, ReferenceRect xTarget, ReferenceRect yTarget, RectRelativity xRel, RectRelativity yRel, ReferencePoint refPoint, int offset)
        {
            SourceRect = source;
            XTargetRect = xTarget;
            YTargetRect = yTarget;

            XRelativity = xRel;
            YRelativity = yRel;
            ReferencePoint = refPoint;
            XOffset = offset;
            YOffset = offset;

            if (ReferencePoint == ReferencePoint.Centered)
            {
                XRelativity = RectRelativity.Inside;
            }

            //PositionDifferential = Extensions.GetRectDifferential(SourceRect.Content, TargetRect.Content, Relativity, XPosition,YPosition, Offset);

        }

        public PositionAnchor(RectRelativity rel, ReferencePoint refPoint, int offset)
        {

            XRelativity = rel;
            ReferencePoint = refPoint;
            XOffset = offset;

            if (ReferencePoint == ReferencePoint.Centered)
            {
                XRelativity = RectRelativity.Inside;
            }

            //PositionDifferential = Extensions.GetRectDifferential(SourceRect.Content, TargetRect.Content, Relativity, XPosition,YPosition, Offset);

        }

        public PositionAnchor(ReferenceRect source, ReferenceRect target, RectRelativity rel)
        {
            SourceRect = source;
            XTargetRect = target;
            XRelativity = rel;
        }

        public PositionAnchor(ReferenceRect source, ReferenceRect target, RectRelativity rel, XReference xRef, float YRatio)
        {
            SourceRect = source;
            XTargetRect = target;
            XRelativity = rel;
        }

        public PositionAnchor(ReferenceRect source, ReferenceRect target, RectRelativity rel, float XOffset, YReference yRef)
        {
            SourceRect = source;
            XTargetRect = target;
            XRelativity = rel;
        }

        public PositionAnchor(ReferenceRect source, ReferenceRect target, RectRelativity rel, float XRatio, float YRatio)
        {
            SourceRect = source;
            XTargetRect = target;
            XRelativity = rel;
        }

        #endregion

       

    }
}
