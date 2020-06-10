using AutoMapper;
using HackerNews.Domain.DataScraping;
using HackerNews.WebApi.Configuration;
using HackerNews.WebApi.Controllers.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.WebApi.Controllers
{
    /// <summary>
    /// Defines the <see cref="HackerNewsController" />.
    /// Endpoints to get information from "https://news.ycombinator.com/"
    /// </summary>
    public class HackerNewsController : HackerNewsBaseController, IHackerNewsAppService
    {
        #region Fields

        /// <summary>
        /// Defines the hackerNewsManager.
        /// </summary>
        private readonly IHackerNewsManager hackerNewsManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsController"/> class.
        /// </summary>
        /// <param name="hackerNewsManager">The hackerNewsManager<see cref="IHackerNewsManager"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public HackerNewsController(IHackerNewsManager hackerNewsManager, IMapper mapper) : base(mapper)
        {
            this.hackerNewsManager = hackerNewsManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get Hacker News from website. Use HtmlSanitizer and HtmlAgility to get information
        /// </summary>
        /// <param name="hackerNewsPagedResultRequestDto">The hackerNewsPagedResultRequestDto<see cref="HackerNewsPagedResultRequestDto"/>.</param>
        /// <returns>.</returns>
        [HttpGet("/api/services/app/HackerNews/HtmlSanitizer")]
        public async Task<IList<HackerNewsItemDto>> GetHackerNewsHtmlSanitizer([FromQuery] HackerNewsPagedResultRequestDto hackerNewsPagedResultRequestDto)
        {
            var hackerNews = await hackerNewsManager.GetHackerNewsHtmlSanitizer(hackerNewsPagedResultRequestDto.FilterField, hackerNewsPagedResultRequestDto.FilterOperation,
                hackerNewsPagedResultRequestDto.OrderByField, hackerNewsPagedResultRequestDto.OrderByFieldDesc,
                hackerNewsPagedResultRequestDto.MaxResultCount);

            var result = mapper.Map<IList<HackerNewsItemDto>>(hackerNews);

            return result;
        }

        /// <summary>
        /// Get Hacker News from website. Use manual cleaner functionallity
        /// </summary>
        /// <param name="hackerNewsPagedResultRequestDto">The hackerNewsPagedResultRequestDto<see cref="HackerNewsPagedResultRequestDto"/>.</param>
        /// <returns>.</returns>
        [HttpGet("/api/services/app/HackerNews/ManualCleaner")]
        public async Task<IList<HackerNewsItemDto>> GetHackerNewsManualCleaner([FromQuery] HackerNewsPagedResultRequestDto hackerNewsPagedResultRequestDto)
        {
            var hackerNews = await hackerNewsManager.GetHackerNewsManualCleaner(hackerNewsPagedResultRequestDto.FilterField, hackerNewsPagedResultRequestDto.FilterOperation,
                hackerNewsPagedResultRequestDto.OrderByField, hackerNewsPagedResultRequestDto.OrderByFieldDesc,
                hackerNewsPagedResultRequestDto.MaxResultCount);

            var result = mapper.Map<IList<HackerNewsItemDto>>(hackerNews);

            return result;
        }

        #endregion
    }
}
