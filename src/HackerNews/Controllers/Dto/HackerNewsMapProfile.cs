using HackerNews.Domain.Entities;
using HackerNews.WebApi.Configuration;

namespace HackerNews.WebApi.Controllers.Dto
{
    /// <summary>
    /// Defines the <see cref="HackerNewsMapProfile" />.
    /// </summary>
    public class HackerNewsMapProfile : HackerNewsBaseMapProfile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsMapProfile"/> class.
        /// </summary>
        public HackerNewsMapProfile()
        {
            CreateMap<HackerNewsItemDto, HackerNewsItem>();

            CreateMap<HackerNewsItem, HackerNewsItemDto>();
        }

        #endregion
    }
}
