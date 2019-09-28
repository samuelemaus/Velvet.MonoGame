using System;
using System.Collections.Generic;
using System.Text;

using Velvet;

namespace Velvet.UI
{
    public class ViewGrid
    {
        public ViewGrid(int rows, int columns, BoundingRect targetRect)
        {

        }


        public float CellWidth { get; set; }
        public float CellHeight { get; set; }

        public Dimensions2D CellDimensions => new Dimensions2D(CellWidth, CellWidth);

        public int NumRows { get; protected set; }
        public int NumColumns { get; protected set; }

        public float GridWidth => CellWidth * NumColumns;
        public float GridHeight => CellHeight * NumRows;



        public List<float> YAxesSecondary { get; protected set; }
        public List<float> XAxesSecondary { get; protected set; }



        public void AddSecondaryAxisY(float axis)
        {
            if (!YAxesSecondary.Contains(axis))
            {
                YAxesSecondary.Add(axis);
            }
        }

        public void AddSecondaryAxisX(float axis)
        {
            if (!XAxesSecondary.Contains(axis))
            {
                XAxesSecondary.Add(axis);
            }
        }

        /// <summary>
        /// Primary 2-Dimensional <see cref="Array"/> of <see cref="GridCell"/>s.  Contains all <see cref="IViewObject"/>s in this <see cref="ViewGrid"/>.
        /// </summary>
        public GridCell[,] Cells { get; protected set; }

        /// <summary>
        /// Multiplies total number of <see cref="GridCell"/>s by given <see cref="byte"/> to increase precision of movement in this <see cref="ViewGrid"/>
        /// </summary>
        /// <param name="factor"></param>
        public void SubDivide(byte factor)
        {
            
        }

        /// <summary>
        /// Reduces number of <see cref="GridCell"/>s to minimum necessary for active <see cref="IViewObject"/>s for this <see cref="ViewGrid"/>.
        /// </summary>
        public void Optimize()
        {

        }


        

    }
}
