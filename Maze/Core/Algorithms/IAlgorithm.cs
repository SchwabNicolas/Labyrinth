namespace Maze.Core.Algorithms
{
    /// <summary>
    /// Interface for the algorithms.
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Run the algorithm
        /// </summary>
        /// <param name="grid">A reference to a grid</param>
        void Run(ref IGrid grid);
    }
}
