using System;

namespace Maze.Utils
{
    /// <summary>
    /// A thread-safe random class
    /// </summary>
    public static class ThreadSafeRandom
    {
        private static Random rnd = new Random(); // The random used for this class
        private static readonly object syncLock = new object(); // A lock object

        /// <summary>
        /// Get a random integer in a range
        /// </summary>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns>Random number</returns>
        public static int Next(int min, int max)
        {
            lock (syncLock)
            {
                return rnd.Next(min, max);
            }
        }

        /// <summary>
        /// Get a random integer
        /// </summary>
        /// <param name="max">Maximum</param>
        /// <returns>Random number</returns>
        public static int RandomNumber(int max)
        {
            return Next(0, max);
        }

        /// <summary>
        /// Get a random integer
        /// </summary>
        /// <returns>Random number</returns>
        public static int RandomNumber()
        {
            return Next(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// Reseed the random from this class
        /// </summary>
        /// <param name="seed">Seed</param>
        public static void ReseedRandom(int seed)
        {
            rnd = new Random(seed);
        }

        /// <summary>
        /// Replace this random by another one
        /// </summary>
        /// <param name="random">Random</param>
        public static void ReplaceRandom(Random random)
        {
            rnd = random;
        }

        /// <summary>
        /// Get a random double
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
        {
            lock (syncLock)
            {
                return rnd.NextDouble();
            }
        }
    }
}
