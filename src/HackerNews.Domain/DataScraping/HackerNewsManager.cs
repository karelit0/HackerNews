using HackerNews.Domain.Decorator;
using HackerNews.Domain.Entities;
using HackerNews.Domain.HtmlCleaner;
using HackerNews.Domain.HttpClients;
using LambdaExpressionBuilder;
using LambdaExpressionBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HackerNews.Domain.DataScraping
{
    /// <summary>
    /// Defines the <see cref="HackerNewsManager" />.
    /// </summary>
    public class HackerNewsManager : IHackerNewsManager
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsManager"/> class.
        /// </summary>
        public HackerNewsManager()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the hacker news on the website.
        /// </summary>
        /// <param name="filterField">.</param>
        /// <param name="filterOperation">.</param>
        /// <param name="orderByField">.</param>
        /// <param name="orderByFieldDesc">.</param>
        /// <param name="maxResultCount">.</param>
        /// <returns>.</returns>
        public async Task<IList<HackerNewsItem>> GetHackerNewsHtmlSanitizer(HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation, HackerNewsFieldEnum orderByField, bool orderByFieldDesc, int maxResultCount)
        {
            IList<HackerNewsItem> hackerNewsItemList;

            // Initialize http client
            using (HackerNewsClient hackerNewsClient = new HackerNewsClient())
            {
                // Get response from web site
                string hackerNewsHtmlContent = await hackerNewsClient.GetNewsAsString();
                HtmlDocumentSanitizer dataManualScraping = new HtmlDocumentSanitizer();
                hackerNewsItemList = dataManualScraping.ScrapeHackerNewsHtmlContent(hackerNewsHtmlContent, maxResultCount);
            }

            hackerNewsItemList = FilterHackerNews(hackerNewsItemList, filterField, filterOperation, orderByField, orderByFieldDesc);

            return hackerNewsItemList;
        }

        /// <summary>
        /// Returns the hacker news on the website.
        /// </summary>
        /// <param name="filterField">.</param>
        /// <param name="filterOperation">.</param>
        /// <param name="orderByField">.</param>
        /// <param name="orderByFieldDesc">.</param>
        /// <param name="maxResultCount">.</param>
        /// <returns>.</returns>
        public async Task<IList<HackerNewsItem>> GetHackerNewsManualCleaner(HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation, HackerNewsFieldEnum orderByField, bool orderByFieldDesc, int maxResultCount)
        {
            IList<HackerNewsItem> hackerNewsItemList;

            // Initialize http client
            using (HackerNewsClient hackerNewsClient = new HackerNewsClient())
            {
                // Get response from web site
                using (Stream hackerNewsHtmlContent = await hackerNewsClient.GetNewsAsStream())
                {
                    ManualHtmlTableParser dataManualScraping = new ManualHtmlTableParser();
                    hackerNewsItemList = await dataManualScraping.ScrapeHackerNewsHtmlContent(hackerNewsHtmlContent, maxResultCount);
                }
            }

            LambdaExpressionBuilder<HackerNewsItem> expressionBuilder = new LambdaExpressionBuilder<HackerNewsItem>();

            Expression<Func<HackerNewsItem, bool>> filteringQuery = GetHackerNewsItemExpressionFilter(expressionBuilder, filterField, filterOperation);
            Expression<Func<HackerNewsItem, object>> sortingQuery = GetHackerNewsItemExpressionSort(expressionBuilder, orderByField, orderByFieldDesc);

            // DEBUG: To check the final value
            if (filteringQuery != null)
            {
                hackerNewsItemList = hackerNewsItemList.Where(filteringQuery.Compile()).ToList();
            }

            if (sortingQuery != null)
            {
                if (orderByFieldDesc)
                {
                    hackerNewsItemList = hackerNewsItemList.OrderByDescending(sortingQuery.Compile()).ToList();
                }
                else
                {
                    hackerNewsItemList = hackerNewsItemList.OrderBy(sortingQuery.Compile()).ToList();
                }
            }

            return hackerNewsItemList;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The FilterHackerNews.
        /// </summary>
        /// <param name="hackerNewsItemList">The hackerNewsItemList<see cref="IList{HackerNewsItem}"/>.</param>
        /// <param name="filterField">The filterField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="filterOperation">The filterOperation<see cref="HackerNewsFilterOperationEnum"/>.</param>
        /// <param name="orderByField">The orderByField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="orderByFieldDesc">The orderByFieldDesc<see cref="bool"/>.</param>
        /// <returns>The <see cref="IList{HackerNewsItem}"/>.</returns>
        private IList<HackerNewsItem> FilterHackerNews(IList<HackerNewsItem> hackerNewsItemList, HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation, HackerNewsFieldEnum orderByField, bool orderByFieldDesc)
        {
            LambdaExpressionBuilder<HackerNewsItem> expressionBuilder = new LambdaExpressionBuilder<HackerNewsItem>();

            Expression<Func<HackerNewsItem, bool>> filteringQuery = GetHackerNewsItemExpressionFilter(expressionBuilder, filterField, filterOperation);
            Expression<Func<HackerNewsItem, object>> sortingQuery = GetHackerNewsItemExpressionSort(expressionBuilder, orderByField, orderByFieldDesc);

            // DEBUG: To check the final value
            if (filteringQuery != null)
            {
                hackerNewsItemList = hackerNewsItemList.Where(filteringQuery.Compile()).ToList();
            }

            if (sortingQuery != null)
            {
                if (orderByFieldDesc)
                {
                    hackerNewsItemList = hackerNewsItemList.OrderByDescending(sortingQuery.Compile()).ToList();
                }
                else
                {
                    hackerNewsItemList = hackerNewsItemList.OrderBy(sortingQuery.Compile()).ToList();
                }
            }

            return hackerNewsItemList;
        }

        /// <summary>
        /// The GetHackerNewsItemExpressionFilter.
        /// </summary>
        /// <param name="expressionBuilder">The expressionBuilder<see cref="LambdaExpressionBuilder{HackerNewsItem}"/>.</param>
        /// <param name="filterField">The filterField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="filterOperation">The filterOperation<see cref="HackerNewsFilterOperationEnum"/>.</param>
        /// <returns>The <see cref="Expression{Func{HackerNewsItem, bool}}"/>.</returns>
        private Expression<Func<HackerNewsItem, bool>> GetHackerNewsItemExpressionFilter(LambdaExpressionBuilder<HackerNewsItem> expressionBuilder, HackerNewsFieldEnum filterField, HackerNewsFilterOperationEnum filterOperation)
        {
            IList<QueryFilter> queryFilterList = new List<QueryFilter>();

            var queryFilter = new QueryFilter
            {
                PropertyPath = filterField.GetDescription(),
            };

            if (filterOperation == HackerNewsFilterOperationEnum.MoreThanFiveWords)
            {
                queryFilter.Operator = LambdaExpressionBuilder.Enums.OperatorEnum.GreaterThan;
                queryFilter.PropertyValue = "5";
            }

            if (filterOperation == HackerNewsFilterOperationEnum.MoreThanOrEqualToFiveWords)
            {
                queryFilter.Operator = LambdaExpressionBuilder.Enums.OperatorEnum.GreaterThanOrEqualTo;
                queryFilter.PropertyValue = "5";
            }

            if (filterOperation == HackerNewsFilterOperationEnum.LessThanFiveWords)
            {
                queryFilter.Operator = LambdaExpressionBuilder.Enums.OperatorEnum.LessThan;
                queryFilter.PropertyValue = "5";
            }

            if (filterOperation == HackerNewsFilterOperationEnum.LessThanOrEqualToFiveWords)
            {
                queryFilter.Operator = LambdaExpressionBuilder.Enums.OperatorEnum.LessThanOrEqualTo;
                queryFilter.PropertyValue = "5";
            }

            queryFilterList.Add(queryFilter);

            return expressionBuilder.BuildQueryFilteringExpression(queryFilterList);
        }

        /// <summary>
        /// The GetHackerNewsItemExpressionSort.
        /// </summary>
        /// <param name="expressionBuilder">The expressionBuilder<see cref="LambdaExpressionBuilder{HackerNewsItem}"/>.</param>
        /// <param name="orderByField">The orderByField<see cref="HackerNewsFieldEnum"/>.</param>
        /// <param name="orderByFieldDesc">The orderByFieldDesc<see cref="bool"/>.</param>
        /// <returns>The <see cref="Expression{Func{HackerNewsItem, object}}"/>.</returns>
        private Expression<Func<HackerNewsItem, object>> GetHackerNewsItemExpressionSort(LambdaExpressionBuilder<HackerNewsItem> expressionBuilder, HackerNewsFieldEnum orderByField, bool orderByFieldDesc)
        {
            QuerySort querySort = new QuerySort
            {
                PropertyPath = orderByField.GetDescription(),
                Desc = orderByFieldDesc
            };

            return expressionBuilder.BuildQuerySortingExpression(querySort);
        }

        #endregion
    }
}
