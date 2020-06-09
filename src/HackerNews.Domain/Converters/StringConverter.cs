using System;

namespace HackerNews.Domain.Converters
{
    /// <summary>
    /// Defines the <see cref="StringConverter" />.
    /// </summary>
    public static class StringConverter
    {
        #region Public Methods

        /// <summary>
        /// Simple validation of empty string for default value.
        /// </summary>
        /// <param name="number">.</param>
        /// <returns>.</returns>
        public static int GetInt32(string number)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(number))
                {
                    return 0;
                }

                return Convert.ToInt32(number);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Simple validation of empty string for default value.
        /// </summary>
        /// <param name="number">.</param>
        /// <returns>.</returns>
        public static float GetSingle(string number)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(number))
                {
                    return 0;
                }

                return Convert.ToSingle(number);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
