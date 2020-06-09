using HackerNews.WebApi.Controllers.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.WebApi.Controllers
{
    /// <summary>
    /// Scrap data from Hacker News WebSite.
    /// </summary>
    public interface IHackerNewsAppService
    {
        /// <summary>
        /// The GetHackerNewsHtmlSanitizer.
        /// </summary>
        /// <param name="hackerNewsPagedResultRequestDto">The hackerNewsPagedResultRequestDto<see cref="HackerNewsPagedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{IList{HackerNewsItemDto}}"/>.</returns>
        Task<IList<HackerNewsItemDto>> GetHackerNewsHtmlSanitizer(HackerNewsPagedResultRequestDto hackerNewsPagedResultRequestDto);

        /// <summary>
        /// The GetHackerNewsManualCleaner.
        /// </summary>
        /// <param name="hackerNewsPagedResultRequestDto">The hackerNewsPagedResultRequestDto<see cref="HackerNewsPagedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{IList{HackerNewsItemDto}}"/>.</returns>
        Task<IList<HackerNewsItemDto>> GetHackerNewsManualCleaner(HackerNewsPagedResultRequestDto hackerNewsPagedResultRequestDto);
    }
}
