using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velvet.Input
{
    public class Pointer : IPointer
    {
        public Pointer(PositionDependency dep, PointerClickedDelegate pointerClicked)
        {
            PositionDependency = dep;
            this.pointerClicked = pointerClicked;
        }

        public object TargetedObject { get; set; }

        private Vector2 position;
        public Vector2 Position {
            get
            {
                if (PositionDependency != null && PositionDependency.DependencyActive)
                {
                    PositionDependency.GetPositionOverride(ref position);
                }

                return position;

            }
            set
            {
                if (PositionDependency == null || !PositionDependency.DependencyActive)
                {
                    position = value;
                }

            }
        }
        public Vector2 TranslatedPosition
        {
            get; set;
        }



        public delegate bool PointerClickedDelegate();
        private PointerClickedDelegate pointerClicked { get; set; }


        /// <summary>
        /// Indicates whether this <see cref="Pointer"/>'s main button is clicked.
        /// </summary>
        public bool PointerClicked => pointerClicked.Invoke();

        private bool resetClickedPosition = false;
        /// <summary>
        /// <see cref="Vector2"/> Position on screen recorded when Pointer's button is clicked down and held.
        /// </summary>
        public Vector2 ClickedPosition { get; private set; }

        /// <summary>
        /// <see cref="BoundingRect"/> created by the <see cref="Vector2"/> differential between this <see cref="Pointer"/>'s <see cref="ClickedPosition"/> and the current <see cref="Position"/>.  Used to get distance between two points when the pointer is clicked.
        /// </summary>
        public BoundingRect CreatedRect
        {
            get
            {
                if (PointerClicked)
                {
                    if (!resetClickedPosition)
                    {
                        ClickedPosition = Position;
                        resetClickedPosition = true;
                    }

                    

                    Vector2 differential = (Position - ClickedPosition);

                    Dimensions2D dimensions = new Dimensions2D(Math.Abs(differential.X), Math.Abs(differential.Y));

                    return new BoundingRect(Position - differential / 2, dimensions);


                }

                else
                {
                    resetClickedPosition = false;
                    ClickedPosition = Vector2.Zero;
                    return BoundingRect.Empty;

                    
                }
            }
        }

        public BoundingRect CreatedRectMatrix(Matrix matrix)
        {
           if (PointerClicked)
           {
               if (!resetClickedPosition)
               {
                   ClickedPosition = Position;
                   resetClickedPosition = true;
               }



               Vector2 differential = (Position - ClickedPosition);

               Dimensions2D dimensions = new Dimensions2D(Math.Abs(differential.X), Math.Abs(differential.Y));

               return new BoundingRect(Position - differential / 2, dimensions);


           }

           else
           {
               resetClickedPosition = false;
               ClickedPosition = Vector2.Zero;
               return BoundingRect.Empty;


           }
            
        }



        public PositionDependency PositionDependency { get; set; }

        public IBoundingRect[] ActiveTargets { get; set; }

        public void GetTargetedObject()
        {
            bool reset = true;

            foreach (var t in ActiveTargets)
            {

                if (t.BoundingRect.Contains(Position))
                {
                    TargetedObject = t;
                    reset = false;
                }

                if (reset)
                {
                    TargetedObject = null;
                }
            }
        }

        public List<string> DebugInfo
        {
            get
            {
                var returnList = new List<string>();

                returnList.Add($"Targ Obj: {TargetedObject}");
                returnList.Add($"Pos: {Position}");
                returnList.Add($"ClickedPos: {ClickedPosition}");
                returnList.Add($"resetClicked: {resetClickedPosition}");


                return returnList;
                
            }
        }




    }
}
