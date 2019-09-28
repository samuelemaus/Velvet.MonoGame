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

        public ReferencePoint CornerOfFirstIndex { get; private set; }

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

        private ReferencePoint GetReferencePoint(Orientation orientation, Order order, bool reversed)
        {
            XReference x = default;
            YReference y = default;

            if(orientation == Orientation.Horizontal)
            {
                if (reversed)
                {
                    y = YReference.Bottom;
                    
                }

                else
                {
                    y = YReference.Top;
                }

                if (order == Order.Ascending)
                {
                    x = XReference.Left;
                }

                else
                {
                    x = XReference.Right;
                }

                return new ReferencePoint(x, y);

            }

            else
            {
                if (reversed)
                {
                    x = XReference.Right;

                }

                else
                {
                    x = XReference.Left;
                }

                if (order == Order.Ascending)
                {
                    y = YReference.Bottom;
                }

                else
                {
                    y = YReference.Top;
                }

                return new ReferencePoint(x, y);
            }

        }

    }
}
