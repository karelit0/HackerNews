using HackerNews.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.WebApi.Controllers.Dto
{
    /// <summary>
    /// Defines the <see cref="HackerNewsPagedResultRequestDto" />.
    /// </summary>
    public class HackerNewsPagedResultRequestDto
    {
        #region Properties

        /// <summary>
        /// Logic reference to field to use on filter
        /// </summary>
        public HackerNewsFieldEnum FilterField { get; set; }

        /// <summary>
        /// Logic reference to operation to use on filter
        /// </summary>
        public HackerNewsFilterOperationEnum FilterOperation { get; set; }

        /// <summary>
        /// Default max setted on 30 rows
        /// </summary>
        [Range(1, int.MaxValue)]
        public int MaxResultCount { get; set; } = 30;

        /// <summary>
        /// Logic reference to filed to use on orderby
        /// </summary>
        public HackerNewsFieldEnum OrderByField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OrderByFieldDesc
        /// </summary>
        public bool OrderByFieldDesc { get; set; }

        #endregion
    }
}
