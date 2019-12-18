using Maze.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Core
{
    /// <summary>
    /// Represents a maze cell.
    /// </summary>
    public class Cell : IEquatable<Cell>
    {
        #region Variables
        protected readonly MazeController mazeController = MazeController.Instance; // The singleton instance of the MazeController class
        protected Dictionary<Cell, bool> links; // The links between the cells
        #endregion

        #region Properties
        /// <summary>
        /// The northern cell
        /// </summary>
        public Cell North { get; set; }
        /// <summary>
        /// The southern cell
        /// </summary>
        public Cell South { get; set; }
        /// <summary>
        /// The eastern cell
        /// </summary>
        public Cell East { get; set; }
        /// <summary>
        /// The western cell
        /// </summary>
        public Cell West { get; set; }
        /// <summary>
        /// The y coordinate of the cell
        /// </summary>
        public int Row { get; set; } = -1;
        /// <summary>
        /// The x coordinate of the cell
        /// </summary>
        public int Column { get; set; } = -1;
        /// <summary>
        /// The cell neigbours.
        /// </summary>
        public List<Cell> Neighbors
        {
            get
            {
                var list = new List<Cell>();

                if (North != null)
                    list.Add(North);
                if (South != null)
                    list.Add(South);
                if (East != null)
                    list.Add(East);
                if (West != null)
                    list.Add(West);

                return list;
            }
        }

        /// <summary>
        /// The links between this cells and its neighbours
        /// </summary>
        public List<Cell> Links
        {
            get
            {
                return links.Keys.ToList();
            }
        }

        /// <summary>
        /// The borders of the cell.
        /// </summary>
        public (bool Top, bool Right, bool Bottom, bool Left) Borders
        {
            get
            {
                var borders = (Top: false, Right: false, Bottom: false, Left: false);
                if (Neighbors.Count <= 0) return borders; // If it doesn't have neighbor => return borders (no border)

                if (North == null) borders.Top = true;
                else if (!North.IsLinked(this)) borders.Top = true;

                if (East == null) borders.Right = true;
                else if (!East.IsLinked(this)) borders.Right = true;

                if (South == null) borders.Bottom = true;
                else if (!South.IsLinked(this)) borders.Bottom = true;

                if (West == null) borders.Left = true;
                else if (!West.IsLinked(this)) borders.Left = true;

                return borders;
            }
        }

        /// <summary>
        /// If the cell is completely closed by walls => true
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return North.IsLinked(this) && East.IsLinked(this) && South.IsLinked(this) && West.IsLinked(this);
            }
        }

        /// <summary>
        /// If the cell is the first of the maze => true
        /// </summary>
        public bool IsStart
        {
            get
            {
                return Row == 0 && Column == 0;
            }
        }

        /// <summary>
        /// If the cell is the last of the maze => true
        /// </summary>
        public bool IsEnd
        {
            get
            {
                return Row == mazeController.MazeGrid.Rows - 1 && Column == mazeController.MazeGrid.Columns - 1;
            }
        }

        /// <summary>
        /// If the maze is in the correct path => true
        /// </summary>
        /// <remarks>
        /// Needs an external algorithm to be changed
        /// </remarks>
        public bool IsRightPath { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The constructor of the cell
        /// </summary>
        /// <param name="row">The y coord of the cell</param>
        /// <param name="column">The x coord of the cell</param>
        public Cell(int row, int column)
        {
            Initialize(row, column);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the object
        /// </summary>
        /// <param name="row">The y coord of the cell</param>
        /// <param name="column">The x coord of the cell</param>
        protected void Initialize(int row, int column)
        {
            Row = row;
            Column = column;
            links = new Dictionary<Cell, bool>();
        }

        /// <summary>
        /// Link this cell with another
        /// </summary>
        /// <param name="cell">Cell to link with</param>
        /// <param name="bidirectional">If the other cell should be linked too</param>
        /// <returns>This cell</returns>
        public Cell Link(Cell cell, bool bidirectional = true)
        {
            if (!links.ContainsKey(cell))
                links.Add(cell, true);

            if (bidirectional)
                cell.Link(this, false);

            return this;
        }

        /// <summary>
        /// Unlink this cell from another
        /// </summary>
        /// <param name="cell">Cell to unlink from</param>
        /// <param name="bidirectional">If the other cell should be unlinked too</param>
        /// <returns>This cell</returns>
        public Cell Unlink(Cell cell, bool bidirectional = true)
        {
            if (links.ContainsKey(cell))
                links.Remove(cell);

            if (bidirectional)
                cell.Unlink(this, false);

            return this;
        }

        /// <summary>
        /// Check if the cell is linked to another
        /// </summary>
        /// <param name="cell">The other cell</param>
        /// <returns>Is the cell is linked to the other one ?</returns>
        public bool IsLinked(Cell cell)
        {
            if (cell == null) return false;
            return links.ContainsKey(cell);
        }

        /// <summary>
        /// Avoids the completely closed by walls cells in the maze
        /// </summary>
        public void AvoidClosedCells()
        {
            if (North == null || East == null || South == null || West == null) { }
            else
            {
                if (IsClosed)
                {
                    int rnd = ThreadSafeRandom.RandomNumber(4);
                    switch (rnd)
                    {
                        case 0:
                            if (North != null || North.IsClosed)
                            {
                                Unlink(North);
                                North.Unlink(this);
                                break;
                            }
                            goto case 1;
                        case 1:
                            if (East != null || East.IsClosed)
                            {
                                Unlink(East);
                                East.Unlink(this);
                                break;
                            }
                            goto case 2;
                        case 2:
                            if (South != null || South.IsClosed)
                            {
                                Unlink(South);
                                South.Unlink(this);
                                break;
                            }
                            goto case 3;
                        case 3:
                            if (West != null || West.IsClosed)
                            {
                                Unlink(West);
                                West.Unlink(this);
                                break;
                            }
                            goto case 1;
                        default: goto case 1;
                    }
                }
            }
        }

        /// <summary>
        /// Check if this cell is equal to another
        /// </summary>
        /// <param name="other">Other cell</param>
        /// <returns>Is the cell equal to the other one ?</returns>
        public bool Equals(Cell other)
        {
            if (other == null) return false;
            return (Row == other.Row && Column == other.Column);
        }

        /// <summary>
        /// Check if this cell is equal to another object
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns>Is the cell equal to the other object ?</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        /// <summary>
        /// Gets a hash code from the class.
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return $"{Row}_{Column}".GetHashCode();
        }
        #endregion
    }
}
