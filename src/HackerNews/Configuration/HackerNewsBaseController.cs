using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.WebApi.Configuration
{
    /// <summary>
    /// Defines the <see cref="HackerNewsBaseController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public abstract class HackerNewsBaseController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// Defines the mapper.
        /// </summary>
        protected readonly IMapper mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsBaseController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        public HackerNewsBaseController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        #endregion
    }
}
