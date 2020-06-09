using HackerNews.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Domain.DataScraping
{
    /// <summary>
    /// Defines the <see cref="IHackerNewsManager" />.
    /// </summary>
    public interface IHackerNewsManager
    {
        /// <summary>
        /// The GetHackerNewsHtmlSanitizer.
        /// </summary>
        /// <param name="filterField">The filterField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="filterOperation">The filterOperation<see cref="HackerNewsFilterOperationEnum"/>.</param>
        /// <param name="orderByField">The orderByField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="orderByFieldDesc">The orderByFieldDesc<see cref="bool"/>.</param>
        /// <param name="maxResultCount">The maxResultCount<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IList{HackerNewsItem}}"/>.</returns>
        Task<IList<HackerNewsItem>> GetHackerNewsHtmlSanitizer(HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation, HackerNewsFieldEnum orderByField, bool orderByFieldDesc, int maxResultCount);

        /// <summary>
        /// The GetHackerNewsManualCleaner.
        /// </summary>
        /// <param name="filterField">The filterField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="filterOperation">The filterOperation<see cref="HackerNewsFilterOperationEnum"/>.</param>
        /// <param name="orderByField">The orderByField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="orderByFieldDesc">The orderByFieldDesc<see cref="bool"/>.</param>
        /// <param name="maxResultCount">The maxResultCount<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{IList{HackerNewsItem}}"/>.</returns>
        Task<IList<HackerNewsItem>> GetHackerNewsManualCleaner(HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation, HackerNewsFieldEnum orderByField, bool orderByFieldDesc, int maxResultCount);
    }
}
