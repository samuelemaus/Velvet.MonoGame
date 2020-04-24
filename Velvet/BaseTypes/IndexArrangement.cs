using System;
using System.Collections.Generic;
using System.Text;

namespace Velvet
{
    public class IndexArrangement
    {
        public Orientation Orientation;
        public Order Order;
        /// <summary>
        /// If true, the starting-point of the Index changes from the top row to the bottom row for Horizontally-Oriented <see cref="IndexArrangement"/>s, and from the left-most to the right-most column for Vertically-Oriented <see cref="IndexArrangement"/>s respectively.
        /// </summary>
        public bool ReversedStartPoint;

        public Alignment CornerOfFirstIndex { get; private set; }

        private IndexArrangement(Orientation orientation, Order order, bool reversed = false)
        {
            Orientation = orientation;
            Order = order;
            CornerOfFirstIndex = GetReferencePoint(orientation, order, reversed);
        }
        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Bottom-Left corner, and first iterates Down vertically, then Right horizontally.
        /// </summary>
        public static IndexArrangement VerticalAscending = new IndexArrangement(Orientation.Vertical, Order.Ascending);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Top-Left corner, and first iterates Down Vertically, then Right horizontally.
        /// </summary>
        public static IndexArrangement VerticalDescending = new IndexArrangement(Orientation.Vertical, Order.Descending);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Top-Left corner, and first iterates Right horizontally, then Down vertically.
        /// </summary>
        public static IndexArrangement HorizontalAscending = new IndexArrangement(Orientation.Horizontal, Order.Ascending);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Top-Right corner, and first iterates Left horizontally, then Down vertically.
        /// </summary>
        public static IndexArrangement HorizontalDescending = new IndexArrangement(Orientation.Horizontal, Order.Descending);


        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Bottom-Right corner, and first iterates Up vertically, then Left horizontally.
        /// </summary>
        public static IndexArrangement VerticalAscendingReversed = new IndexArrangement(Orientation.Vertical, Order.Ascending, true);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Top-Right corner, and first iterates Down vertically, then Left horizontally.
        /// </summary>
        public static IndexArrangement VerticalDescendingReversed = new IndexArrangement(Orientation.Vertical, Order.Descending, true);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Bottom-Left corner, and first iterates Right horizontally, then Up vertically.
        /// </summary>
        public static IndexArrangement HorizontalAscendingReversed = new IndexArrangement(Orientation.Horizontal, Order.Ascending, true);

        /// <summary>
        /// An <see cref="IndexArrangement"/> organized by which the first index is located in the Bottom-Right corner, and first iterates Left horizontally, then Up vertically.
        /// </summary>
        public static IndexArrangement HorizontalDescendingReversed = new IndexArrangement(Orientation.Horizontal, Order.Descending, true);

        private Alignment GetReferencePoint(Orientation orientation, Order order, bool reversed)
        {
            HorizontalAlignment x = default;
            VerticalAlignment y = default;

            if(orientation == Orientation.Horizontal)
            {
                if (reversed)
                {
                    y = VerticalAlignment.Bottom;
                    
                }

                else
                {
                    y = VerticalAlignment.Top;
                }

                if (order == Order.Ascending)
                {
                    x = HorizontalAlignment.Left;
                }

                else
                {
                    x = HorizontalAlignment.Right;
                }

                return new Alignment(x, y);

            }

            else
            {
                if (reversed)
                {
                    x = HorizontalAlignment.Right;

                }

                else
                {
                    x = HorizontalAlignment.Left;
                }

                if (order == Order.Ascending)
                {
                    y = VerticalAlignment.Bottom;
                }

                else
                {
                    y = VerticalAlignment.Top;
                }

                return new Alignment(x, y);
            }

        }

    }
}
