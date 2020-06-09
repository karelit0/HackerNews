namespace HackerNews.WebApi.Controllers.Dto
{
    /// <summary>
    /// Defines the <see cref="HackerNewsItemDto" />.
    /// </summary>
    public class HackerNewsItemDto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the AmountComments
        /// Amount of Comments...
        /// </summary>
        public int AmountComments { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// Id...
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Points
        /// Points...
        /// </summary>
        public float Points { get; set; }

        /// <summary>
        /// Gets or sets the RankOrder
        /// Rank Order...
        /// </summary>
        public int RankOrder { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// Title...
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the TitleLength
        /// Title length...
        /// </summary>
        public int TitleLength
        {
            get { return Title.Length; }
        }

        #endregion
    }
}
