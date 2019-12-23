namespace Huffman_Encoding
{
    /// <summary>
    /// Count amount of ways to go from top left cell of field (1,1) coordinates
    /// To (n,m) cell using DP approach with the ability to move only to BOTTOM & RIGHT
    /// </summary>
    public class CountPathsProblem
    {
        /// <summary>
        /// Returns the amount of bottom-right paths from top-left corner
        /// </summary>
        /// <param name="n">row of target element</param>
        /// <param name="m">column of target element</param>
        /// <returns></returns>
        public static long GetAmountOfWaysTo(int n, int m, long[,] matrix)
        {
            for (int i = 0; i < n; i++)
            {
                matrix[i, 0] = 1;
            }

            for (int j = 0; j < m; j++)
            {
                matrix[0, j] = 1;
            }

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    matrix[i, j] = matrix[i - 1, j] + matrix[i, j - 1];
                }
            }

            return matrix[n - 1, m - 1];
        }
    
    }
}