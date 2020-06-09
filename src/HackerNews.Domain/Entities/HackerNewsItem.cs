namespace HackerNews.Domain.Entities
{
    /// <summary>
    /// Defines the <see cref="HackerNewsItem" />.
    /// </summary>
    public class HackerNewsItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the AmountComments.
        /// </summary>
        public int AmountComments { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Points.
        /// </summary>
        public float Points { get; set; }

        /// <summary>
        /// Gets or sets the RankOrder.
        /// </summary>
        public int RankOrder { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}
