namespace HackerNews.Domain.Entities
{
    #region Enums

    /// <summary>
    /// Logic reference to custom filter
    /// </summary>
    public enum HackerNewsFilterOperationEnum
    {
        /// <summary>
        /// More than five words
        /// </summary>
        MoreThanFiveWords,
        /// <summary>
        /// More than or equals to five words
        /// </summary>
        MoreThanOrEqualToFiveWords,
        /// <summary>
        /// Less than five words
        /// </summary>
        LessThanFiveWords,
        /// <summary>
        /// Less than or equals to five words
        /// </summary>
        LessThanOrEqualToFiveWords,
    }

    #endregion
}
