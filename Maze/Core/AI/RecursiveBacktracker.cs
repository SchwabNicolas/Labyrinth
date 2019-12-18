namespace Maze.Core.AI
{
    /// <summary>
    /// A class solving maze with recursive backtracking
    /// </summary>
    class RecursiveBacktracker
    {
        MazeController mazeController = MazeController.Instance; // The singleton instance of the maze controller
        bool[,] visitedCells; // A grid with all visited cells
        bool[,] correctPath; // A grid with the correct path
        int startingX; // The x coordinate of the first cell of the maze
        int startingY; // The y coordinate of the first cell of the maze
        int endingX; // The x coordinate of the end cell of the maze
        int endingY; // The x coordinate of the end cell of the maze

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingCell">The first cell of the maze</param>
        /// <param name="endingCell">The last cell of the maze</param>
        public RecursiveBacktracker(Cell startingCell, Cell endingCell)
        {
            startingX = startingCell.Column;
            startingY = startingCell.Row;

            endingX = endingCell.Column;
            endingY = endingCell.Row;

            visitedCells = new bool[mazeController.MazeGrid.Rows, mazeController.MazeGrid.Columns];
            correctPath = new bool[mazeController.MazeGrid.Rows, mazeController.MazeGrid.Columns];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingX">The x coordinate of the first cell of the maze</param>
        /// <param name="startingY">The y coordinate of the first cell of the maze</param>
        /// <param name="endingX">The x coordinate of the last cell of the maze</param>
        /// <param name="endingY">The y coordinate of the last cell of the maze</param>
        public RecursiveBacktracker(int startingX, int startingY, int endingX, int endingY)
        {
            this.startingX = startingX;
            this.startingY = startingY;

            this.endingX = endingX;
            this.endingY = endingY;

            visitedCells = new bool[mazeController.MazeGrid.Rows, mazeController.MazeGrid.Columns];
            correctPath = new bool[mazeController.MazeGrid.Rows, mazeController.MazeGrid.Columns];
        }

        /// <summary>
        /// Solve the maze
        /// </summary>
        public void Solve()
        {
            for (int row = 0; row < mazeController.MazeGrid.Rows; row++)
            {
                for (int col = 0; col < mazeController.MazeGrid.Columns; col++)
                {
                    visitedCells[row, col] = false;
                    correctPath[row, col] = false;
                }
            }
            bool solved = RecursiveSolve(startingX, startingY, -1, -1); // If the maze is solved => true

            for (int row = 0; row < mazeController.MazeGrid.Rows; row++)
            {
                for (int col = 0; col < mazeController.MazeGrid.Columns; col++)
                {
                    if(correctPath[row, col])
                    {
                        mazeController.MazeGrid[row, col].IsRightPath = true;
                    }
                }
            }
        }

        /// <summary>
        /// Solve the maze using recursivity.
        /// </summary>
        /// <param name="x">The current cell x coordinate</param>
        /// <param name="y">The current cell y coordinate</param>
        /// <param name="lastX">The last cell X</param>
        /// <param name="lastY">The last cell Y</param>
        /// <returns>True if it has solved the path, false if not</returns>
        /// 
        private bool RecursiveSolve(int x, int y, int lastX, int lastY)
        {
            if ((!(lastX == -1 || lastY == -1) && !mazeController.MazeGrid[x, y].IsLinked(mazeController.MazeGrid[lastX, lastY])) || visitedCells[x, y]) return false;

            visitedCells[x, y] = true;

            if (x != 0)
            {
                if (RecursiveSolve(x - 1, y, x, y))
                {
                    correctPath[x, y] = true;
                    return true;
                }
            }

            if (x != mazeController.MazeGrid.Columns - 1)
            {
                if (RecursiveSolve(x + 1, y, x, y))
                {
                    correctPath[x, y] = true;
                    return true;
                }
            }

            if (y != 0)
            {
                if (RecursiveSolve(x, y - 1, x, y))
                {
                    correctPath[x, y] = true;
                    return true;
                }
            }

            if (y != mazeController.MazeGrid.Rows - 1)
            {
                if (RecursiveSolve(x, y + 1, x, y))
                {
                    correctPath[x, y] = true;
                    return true;
                }
            }

            if (x == endingX && y == endingY) return true;

            return false;
        }

    }
}
