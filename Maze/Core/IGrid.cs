using System.Collections.Generic;

namespace Maze.Core
{
    /// <summary>
    /// Interface for the grids.
    /// </summary>
    public interface IGrid
    {
        #region Indexer
        Cell this[int row, int column] { get; set; } // The indexer
        #endregion

        #region Properties
        /// <summary>
        /// The number of columns of the grid
        /// </summary>
        int Columns { get; set; }
        /// <summary>
        /// The number of rows of the grid
        /// </summary>
        int Rows { get; set; }
        /// <summary>
        /// The size of the grid
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Get a random cell
        /// </summary>
        Cell GetRandomCell { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Get all cells from the grid
        /// </summary>
        /// <returns>Alls cells from the grid</returns>
        IEnumerable<Cell> GetAllCells();
        /// <summary>
        /// Get all rows from the grid
        /// </summary>
        /// <returns>List of rows</returns>
        List<List<Cell>> GetAllRows();
        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator<Cell> GetEnumerator();
        /// <summary>
        /// Convert class to string
        /// </summary>
        /// <returns>A string</returns>
        string ToString();
        #endregion
    }
}
