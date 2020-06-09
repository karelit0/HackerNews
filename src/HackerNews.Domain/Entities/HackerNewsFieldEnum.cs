using System.ComponentModel;

namespace HackerNews.Domain.Entities
{
    #region Enums

    /// <summary>
    /// Logic representation of fields <see cref="HackerNewsItem"/> for DTO's class
    /// </summary>
    public enum HackerNewsFieldEnum
    {
        /// <summary>
        /// Id
        /// </summary>
        [Description("Id")]
        Id,
        /// <summary>
        /// Rank Order
        /// </summary>
        [Description("RankOrder")]
        RankOrder,
        /// <summary>
        /// Title
        /// </summary>
        [Description("Title.Length")]
        Title,
        /// <summary>
        /// Title length
        /// </summary>
        [Description("Title.Length")]
        TitleLength,
        /// <summary>
        /// Amount of Comments
        /// </summary>
        [Description("AmountComments")]
        AmountComments,
        /// <summary>
        /// Points
        /// </summary>
        [Description("Points")]
        Points
    }

    #endregion
}
