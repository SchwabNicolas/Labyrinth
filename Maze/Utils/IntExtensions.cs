namespace Maze.Utils
{
    /// <summary>
    /// Extends default <see cref="int">Integer</see> class.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Encode integer to base36 
        /// </summary>
        /// <remarks>
        /// Code from <see href="https://github.com/ericrrichards/mazes/blob/master/mazes/Core/IntExtensions.cs">erircrrichards/mazes</see>
        /// </remarks>
        /// <param name="i">An integer</param>
        /// <returns>The base36 encoded integer</returns>
        public static string ToBase36(this int i)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            return chars[i % 36].ToString();
        }

        /// <summary>
        /// Encode integer to base64 
        /// </summary>
        /// /// <remarks>
        /// Code from <see href="https://github.com/ericrrichards/mazes/blob/master/mazes/Core/IntExtensions.cs">erircrrichards/mazes</see>
        /// </remarks>
        /// <param name="i">An integer</param>
        /// <returns>The base64 encoded integer</returns>
        public static string ToBase64(this int i)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ%=";
            return chars[i % 64].ToString();
        }
    }
}
