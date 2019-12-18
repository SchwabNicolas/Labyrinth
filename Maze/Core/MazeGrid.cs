using Maze.Utils;
using System;
using System.Collections.Generic;

namespace Maze.Core
{
    /// <summary>
    /// A class representing the labyrinth grid.
    /// </summary>
    public class MazeGrid : IGrid
    {
        #region Variables
        protected Cell[,] grid; // The grid
        #endregion

        #region Properties
        /// <summary>
        /// The number of rows of the grid
        /// </summary>
        public int Rows { get; set; } = 0;

        /// <summary>
        /// The number of columns of the grid
        /// </summary>
        public int Columns { get; set; } = 0;

        /// <summary>
        /// The size of the grid
        /// </summary>
        public virtual int Size { get { return Rows * Columns; } }

        /// <summary>
        /// The random of the grid
        /// </summary>
        /// <remarks>
        /// Used to have a fixed seed for the grid
        /// </remarks>
        public Random Random { get; set; }

        /// <summary>
        /// Get the first cell of the maze
        /// </summary>
        public Cell GetStartCell
        {
            get
            {
                for (var row = 0; row < Rows; row++)
                {
                    for (var col = 0; col < Columns; col++)
                    {
                        if (grid[row, col].IsStart) return grid[col, row];
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Get the last cell of the maze
        /// </summary>
        public Cell GetEndCell
        {
            get
            {
                for (var row = 0; row < Rows; row++)
                {
                    for (var col = 0; col < Columns; col++)
                    {
                        if (grid[row, col].IsEnd) return grid[col, row];
                    }
                }
                return null;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="rows">The number of rows of the grid</param>
        /// <param name="columns">The number of columns of the grid</param>
        /// <param name="seed">The seed of the grid</param>
        public MazeGrid(int rows, int columns, int? seed)
        {
            Rows = rows;
            Columns = columns;
            Random = new Random(seed ?? ThreadSafeRandom.Next(int.MinValue, int.MaxValue));
            ThreadSafeRandom.ReplaceRandom(Random);

            PrepareGrid();
            ConfigureCells();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the grid
        /// </summary>
        protected virtual void PrepareGrid()
        {
            grid = new Cell[Rows, Columns];
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    grid[row, col] = new Cell(row, col);
                }
            }
        }

        /// <summary>
        /// Configure cells of the grid
        /// </summary>
        /// <remarks>
        /// Needs to be called after <see cref="PrepareGrid">PrepareGrid()</see> method
        /// </remarks>
        protected virtual void ConfigureCells()
        {
            foreach (var cell in grid)
            {
                cell.North = this[cell.Row - 1, cell.Column];
                cell.South = this[cell.Row + 1, cell.Column];
                cell.West = this[cell.Row, cell.Column - 1];
                cell.East = this[cell.Row, cell.Column + 1];
            }
        }

        /// <summary>
        /// Get a specific row
        /// </summary>
        /// <param name="row">The row index</param>
        /// <returns>The specific row</returns>
        protected IEnumerable<Cell> GetRow(int row)
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    if (i == row)
                        yield return this[i, j];
                }
            }
        }

        /// <summary>
        /// Get all the rows
        /// </summary>
        /// <returns>A <see cref="List{List{Cell}}">List</see> with all the rows</returns>
        public List<List<Cell>> GetAllRows()
        {
            var results = new List<List<Cell>>();
            for (var row = 0; row < Rows; row++)
            {
                var innerList = new List<Cell>();
                for (var col = 0; col < Columns; col++)
                {
                    innerList.Add(this[row, col]);
                }
                results.Add(innerList);
            }
            return results;
        }

        /// <summary>
        /// Get all cells
        /// </summary>
        /// <returns>An IEnumerable with all cells</returns>
        public IEnumerable<Cell> GetAllCells()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    yield return grid[i, j];
                }
            }
        }

        /// <summary>
        /// Get a random cell
        /// </summary>
        public virtual Cell GetRandomCell
        {
            get
            {
                var i = ThreadSafeRandom.Next(0, Rows - 1);
                var j = ThreadSafeRandom.Next(0, Columns - 1);
                return this[i, j];
            }
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Indexer for the <see cref="MazeGrid"/>MazeGrid</summary> class.
        /// </summary>
        /// <param name="row">The row</param>
        /// <param name="column">The column</param>
        /// <returns>The cell placed at column:row</returns>
        public Cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows) return null;
                if (column < 0 || column >= Columns) return null;
                return grid[row, column];
            }
            set
            {
                grid[row, column] = value;
            }
        }
        #endregion
    }
}
