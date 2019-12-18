namespace Maze.Core
{
    /// <summary>
    /// A struct representing the four walls of a cell
    /// </summary>
    /// <remarks>
    /// Currently replaced with tuples
    /// </remarks>
    public struct Wall
    {
        /// <summary>
        /// The top wall
        /// </summary>
        public bool Top { get; set; }

        /// <summary>
        /// The right wall
        /// </summary>
        public bool Right { get; set; }

        /// <summary>
        /// The bottom wall
        /// </summary>
        public bool Bottom { get; set; }

        /// <summary>
        /// The left wall
        /// </summary>
        public bool Left { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="top">The top wall of the cell</param>
        /// <param name="right">The right wall of the cell</param>
        /// <param name="bottom">The bottom wall of the cell</param>
        /// <param name="left">The left wall of the cell</param>

        public Wall(bool top, bool right, bool bottom, bool left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
