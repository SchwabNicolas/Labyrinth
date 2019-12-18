using Maze.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Core.Algorithms
{
    /// <summary>
    /// A class implementing the Sidewinder algorithm.
    /// </summary>
    /// <remarks>
    /// Does not work properly at the moment
    /// </remarks>
    public class Sidewinder : IAlgorithm
    {
        /// <summary>
        /// Runs the algorithm with the grid
        /// </summary>
        /// <param name="grid">The maze grid</param>
        public void Run(ref IGrid grid)
        {
            foreach (var row in grid.GetAllRows())
            {
                var run = new List<Cell>();
                foreach (var cell in row)
                {
                    run.Add(cell);

                    bool atEasternBoundary = (cell.East == null);
                    bool atNorthernBoundary = (cell.North == null);

                    bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && (ThreadSafeRandom.Next(0, 2) == 0));

                    if (shouldCloseOut)
                    {
                        var index = ThreadSafeRandom.Next(0, run.Count());
                        var member = run[index];

                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }

            foreach (var row in grid.GetAllRows())
            {
                var run = new List<Cell>();
                foreach (var cell in row)
                {
                    cell.AvoidClosedCells();
                }
            }
        }
    }
}
