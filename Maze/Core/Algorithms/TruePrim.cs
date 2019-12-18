using Maze.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Core.Algorithms
{
    /// <summary>
    /// A class implementing True Prim's algorithm
    /// </summary>
    class TruePrim
    {
        private Stack<Cell> active; // The active cells
        private Dictionary<Cell, int> costs; // Cost of the cells

        /// <summary>
        /// Run the true Prim's algorithm
        /// </summary>
        /// <param name="grid">The maze grid</param>
        public void Run(ref IGrid grid)
        {
            var startAt = grid.GetRandomCell;
            active = new Stack<Cell>();
            active.Push(startAt);

            costs = new Dictionary<Cell, int>();
            foreach (var cell in grid)
            {
                costs[cell] = ThreadSafeRandom.Next(0, 100);
            }

            int iterations = 1;
            while (active.Any())
            {
                iterations++;
                var cell = Min(active);  // Get the minimum value in the *active* set
                var availableNeighbors = cell.Neighbors.Where(x => x.Links.Count == 0);

                if (availableNeighbors.Any())
                {
                    var neighbor = Min(availableNeighbors); // Get the minimum value in the *availableNeighbors* set
                    cell.Link(neighbor);
                    active.Push(neighbor);
                }
                else
                {
                    var list = active.ToList();
                    list.Remove(cell);
                    active = new Stack<Cell>(list);
                }
            }
            Console.WriteLine($"Iterations: {iterations}");
        }

        /// <summary>
        /// Return the lowest value between multiple cells
        /// </summary>
        /// <param name="cells">The cells to compare</param>
        /// <returns>The lowest cell</returns>
        private Cell Min(IEnumerable<Cell> cells)
        {
            Cell lowest = null;
            int lowestValue = int.MaxValue;
            foreach (var cell in cells)
            {
                int cost = costs[cell];
                if (cost < lowestValue)
                {
                    lowestValue = cost;
                    lowest = cell;
                }
            }

            return lowest;
        }
    }
}
