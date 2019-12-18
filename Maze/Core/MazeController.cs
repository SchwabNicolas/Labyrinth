using Maze.Core.AI;
using Maze.Core.Algorithms;

namespace Maze.Core
{
    public class MazeController
    {
        #region Variables
        private static MazeController instance; // The singleton instance of this class
        private IGrid mazeGrid; // The maze grid of the maze
        #endregion

        #region Properties
        /// <summary>
        /// The singleton instance of the <see cref="MazeController">MazeController</see> class.
        /// </summary>
        /// <example>
        /// <code>
        /// mazeController = MazeController.Instance;
        /// </code>
        /// </example>
        public static MazeController Instance
        {
            get
            {
                if (instance == null) instance = new MazeController();
                return instance;
            }
        }

        /// <summary>
        /// The labyrinth grid
        /// </summary>
        public MazeGrid MazeGrid
        {
            get => (MazeGrid)mazeGrid;
            set => mazeGrid = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Build the maze
        /// </summary>
        /// <param name="algorithm">The algorithm to use</param>
        public void BuildMaze(Algorithm algorithm)
        {
            // Select the correct algorithm
            switch (algorithm)
            {
                case Algorithm.Sidewinder:
                    new Sidewinder().Run(ref mazeGrid);
                    break;
                case Algorithm.TruePrim:
                    new TruePrim().Run(ref mazeGrid);
                    break;

            }
        }

        /// <summary>
        /// Solve the maze
        /// </summary>
        public void SolveMaze()
        {
            RecursiveBacktracker solver = new RecursiveBacktracker(MazeGrid.GetStartCell, MazeGrid.GetEndCell);
            solver.Solve();
        }
        #endregion
    }
}
