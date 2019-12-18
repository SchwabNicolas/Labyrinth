using System.Collections.Generic;
using System.Linq;

namespace Maze.Utils
{
    /// <summary>
    /// Extends the default <see cref="IList{T}">IList</see> interface.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Pick a random element from a list.
        /// </summary>
        /// <remarks>
        /// Code adapted from <see href="https://github.com/ericrrichards/mazes/blob/master/mazes/Core/ListExtensions.cs">ericrrichards/mazes</see>
        /// </remarks>
        /// <typeparam name="T">Element type of the list</typeparam>
        /// <param name="list">The list</param>
        /// <returns>A random element from the list</returns>
        public static T Sample<T>(this IEnumerable<T> list)
        {
            var tempList = list.ToList();
            int index = ThreadSafeRandom.Next(0, tempList.Count);
            return tempList[index];
        }

        /// <summary>
        /// Randomize the position of each element in a list.
        /// </summary>
        /// <remarks>
        /// Code adapted from <see href="https://github.com/ericrrichards/mazes/blob/master/mazes/Core/ListExtensions.cs">ericrrichards/mazes</see>
        /// </remarks>
        /// <typeparam name="T">Element type of the list.</typeparam>
        /// <param name="list">The list</param>
        /// <returns>The shuffled list</returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.RandomNumber(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
