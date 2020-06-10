using Ganss.XSS;
using HackerNews.Domain.Converters;
using HackerNews.Domain.Entities;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Domain.HtmlCleaner
{
    /// <summary>
    /// Defines the <see cref="HtmlDocumentSanitizer" />.
    /// <para>This uses: HtmlSanitizer and HtmlAgilityPack</para>
    /// <para>HtmlSanitizer: It is used to clean and close all the html tags, to get a valid Html</para>
    /// <para>HtmlAgilityPack> It is used to filter throw XPath</para>
    /// </summary>
    public class HtmlDocumentSanitizer
    {
        #region Public Methods

        /// <summary>
        /// Scrape Html content and map to business entity.
        /// </summary>
        /// <param name="hackerNewsHtmlContent">html content.</param>
        /// <param name="pageSize">number of business entities to map.</param>
        /// <returns>.</returns>
        public IList<HackerNewsItem> ScrapeHackerNewsHtmlContent(string hackerNewsHtmlContent, int pageSize)
        {
            IList<HackerNewsItem> hackerNewsItem;

            HtmlDocument htmlDocument = new HtmlDocument();

            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedAttributes.Add("id");
            sanitizer.AllowedAttributes.Add("class");

            sanitizer.KeepChildNodes = false;

            var sanitized = sanitizer.SanitizeDocument(hackerNewsHtmlContent, "https://news.ycombinator.com/");

            htmlDocument.LoadHtml(sanitized);

            hackerNewsItem = ReadMainFields(htmlDocument, pageSize);

            hackerNewsItem = ReadSecondaryFields(htmlDocument, pageSize, hackerNewsItem);

            return hackerNewsItem;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Iterate html main rows to get main fields.
        /// </summary>
        /// <param name="htmlDocument">.</param>
        /// <param name="pageSize">.</param>
        /// <returns>.</returns>
        private IList<HackerNewsItem> ReadMainFields(HtmlDocument htmlDocument, int pageSize)
        {
            IList<HackerNewsItem> hackerNewsItem;

            // Use the full path for html well formed
            string xpath = $"/html/body/center/table[@id=\"hnmain\"]/tbody/tr/td/table/tbody/tr[@id][position()>0 and position()<={pageSize}]";

            HtmlNodeCollection htmlNodeList = htmlDocument.DocumentNode.SelectNodes(xpath);

            hackerNewsItem = new List<HackerNewsItem>(htmlNodeList.Count);

            foreach (HtmlNode htmlNode in htmlNodeList)
            {
                // Remove extra children
                while (htmlNode.ChildNodes.FirstOrDefault(x => x.Name.Equals("#text")) != null)
                {
                    htmlNode.ChildNodes.Remove(htmlNode.ChildNodes.FirstOrDefault(x => x.Name.Equals("#text")));
                }

                HackerNewsItem newHackerNewsItem = new HackerNewsItem();

                // Id is inside element as "Id" over table row tag
                newHackerNewsItem.Id = StringConverter.GetInt32(htmlNode.Attributes["id"].Value);

                // Rank order ends with "." like "1."
                newHackerNewsItem.RankOrder = StringConverter.GetInt32(htmlNode.ChildNodes.FirstOrDefault().InnerText.Replace(".", ""));

                // Title is inside a link tag
                newHackerNewsItem.Title = htmlNode.ChildNodes.LastOrDefault().InnerText;

                hackerNewsItem.Add(newHackerNewsItem);
            }

            return hackerNewsItem;
        }

        /// <summary>
        /// Iterate html secondary rows to get suplementary fields.
        /// </summary>
        /// <param name="htmlDocument">.</param>
        /// <param name="pageSize">.</param>
        /// <param name="hackerNewsItem">.</param>
        /// <returns>.</returns>
        private IList<HackerNewsItem> ReadSecondaryFields(HtmlDocument htmlDocument, int pageSize, IList<HackerNewsItem> hackerNewsItem)
        {
            // Use the full path for html well formed
            string xpath = $"/html/body/center/table[@id=\"hnmain\"]/tbody/tr/td/table/tbody/tr/td[@class=\"subtext\"][position()>0 and position()<={pageSize}]/..";

            HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(xpath);

            int hackerNewsItemIndex = 0;

            foreach (HtmlNode htmlNode in htmlNodeCollection)
            {
                if (hackerNewsItem.Count <= hackerNewsItemIndex)
                {
                    break;
                }

                // Unexpected format
                if (htmlNode.ChildNodes.Count < 2)
                {
                    continue;
                }

                // Remove extra children
                while (htmlNode.ChildNodes[1].ChildNodes.FirstOrDefault(x => x.Name.Equals("#text")) != null)
                {
                    htmlNode.ChildNodes[1].ChildNodes.Remove(htmlNode.ChildNodes[1].ChildNodes.FirstOrDefault(x => x.Name.Equals("#text")));
                }

                for (int i = 0; i < htmlNode.ChildNodes[1].ChildNodes.Count; i++)
                {
                    // Usually second child is points
                    // Points format is: "{number} points"
                    if (htmlNode.ChildNodes[1].ChildNodes[i].InnerText.Contains("points"))
                    {
                        hackerNewsItem[hackerNewsItemIndex].Points = StringConverter.GetSingle(htmlNode.ChildNodes[1].ChildNodes[i].InnerText.Split(new char[0])[0]);
                    }

                    // Usually last child to be taken for comments
                    // AmountComments format is: "{number}&nbsp;comments"
                    if (htmlNode.ChildNodes[1].ChildNodes[i].InnerText.Contains("comments"))
                    {
                        string htmlContent = htmlNode.ChildNodes[1].ChildNodes[i].InnerText.Replace("&nbsp;", " ");

                        hackerNewsItem[hackerNewsItemIndex].AmountComments = StringConverter.GetInt32(htmlContent);
                    }
                }

                hackerNewsItemIndex++;
            }

            return hackerNewsItem;
        }

        #endregion
    }
}
