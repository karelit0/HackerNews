using HackerNews.Domain.Converters;
using HackerNews.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace HackerNews.Domain.HtmlCleaner
{
    /// <summary>
    /// Defines the <see cref="ManualHtmlTableParser" />.
    /// <para>This class uses: XmlDocument</para>
    /// <para>Manual clean up: Manual clean up to get a valid table html tag, fix encondig for reading</para>
    /// <para>XmlDocument: It is used to filter throw XPath</para>
    /// </summary>
    public class ManualHtmlTableParser
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualHtmlTableParser"/> class.
        /// </summary>
        public ManualHtmlTableParser()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Scrape Html content and map to business entity.
        /// </summary>
        /// <param name="hackerNewsHtmlContent">html content.</param>
        /// <param name="pageSize">number of business entities to map.</param>
        /// <returns>.</returns>
        public async Task<IList<HackerNewsItem>> ScrapeHackerNewsHtmlContent(Stream hackerNewsHtmlContent, int pageSize)
        {
            IList<HackerNewsItem> hackerNewsItem;

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(hackerNewsHtmlContent);
            }
            catch (XmlException xmlException)
            {
                // DEBUG: check message
                string xmlExceptionMessage = xmlException.Message;

                await LoadItemListTable(xmlDocument, hackerNewsHtmlContent);
            }

            hackerNewsItem = ReadMainFields(xmlDocument, pageSize);

            hackerNewsItem = ReadSecondaryFields(xmlDocument, pageSize, hackerNewsItem);

            return hackerNewsItem;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Read manually the html and clean table.
        /// </summary>
        /// <param name="xmlDocument">.</param>
        /// <param name="hackerNewsHtmlContent">.</param>
        /// <returns>.</returns>
        private async Task LoadItemListTable(XmlDocument xmlDocument, Stream hackerNewsHtmlContent)
        {
            hackerNewsHtmlContent.Position = 0;

            using (StreamReader reader = new StreamReader(hackerNewsHtmlContent))
            {
                string htmlContent = await reader.ReadToEndAsync();

                int itemListIndex = htmlContent.IndexOf("itemlist");

                int nearTableIndex = htmlContent.IndexOf("<table", itemListIndex - 67);

                htmlContent = htmlContent.Substring(htmlContent.IndexOf("<table", nearTableIndex));

                htmlContent = htmlContent.Remove(htmlContent.IndexOf("</table>") + 8);

                htmlContent = htmlContent.Replace("&nbsp;", " ");

                while (htmlContent.IndexOf("href='") >= 0)
                {
                    int hrefIndex = htmlContent.IndexOf("href='");
                    int endOfHrefIndex = htmlContent.IndexOf("'", hrefIndex + 6);

                    htmlContent = htmlContent.Remove(hrefIndex, endOfHrefIndex - hrefIndex + 1);
                }

                while (htmlContent.IndexOf("href=\"") >= 0)
                {
                    int hrefIndex = htmlContent.IndexOf("href=\"");
                    int endOfHrefIndex = htmlContent.IndexOf("\"", hrefIndex + 6);

                    htmlContent = htmlContent.Remove(hrefIndex, endOfHrefIndex - hrefIndex + 1);
                }

                xmlDocument.LoadXml(htmlContent);
            }
        }

        /// <summary>
        /// Iterate html main rows to get main fields.
        /// </summary>
        /// <param name="xmlDocument">.</param>
        /// <param name="pageSize">.</param>
        /// <returns>.</returns>
        private IList<HackerNewsItem> ReadMainFields(XmlDocument xmlDocument, int pageSize)
        {
            IList<HackerNewsItem> hackerNewsItem;

            // Use the full path for html well formed
            string xpath = $"//table/tr[@id][position()>0 and position()<={pageSize}]";

            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(xpath);

            hackerNewsItem = new List<HackerNewsItem>(xmlNodeList.Count);

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                HackerNewsItem newHackerNewsItem = new HackerNewsItem();

                // Id is inside element as "Id" over table row tag
                newHackerNewsItem.Id = StringConverter.GetInt32(xmlNode.Attributes["id"].Value);

                // Rank order ends with "." like "1."
                newHackerNewsItem.RankOrder = StringConverter.GetInt32(xmlNode.ChildNodes[0].ChildNodes[0].InnerText.Replace(".", ""));

                // Title is inside a link tag
                newHackerNewsItem.Title = xmlNode.ChildNodes[2].ChildNodes[0].InnerText;

                hackerNewsItem.Add(newHackerNewsItem);
            }

            return hackerNewsItem;
        }

        /// <summary>
        /// Iterate html secondary rows to get suplementary fields.
        /// </summary>
        /// <param name="xmlDocument">.</param>
        /// <param name="pageSize">.</param>
        /// <param name="hackerNewsItem">.</param>
        /// <returns>.</returns>
        private IList<HackerNewsItem> ReadSecondaryFields(XmlDocument xmlDocument, int pageSize, IList<HackerNewsItem> hackerNewsItem)
        {
            // Use the full path for html well formed
            string xpath = $"//table/tr/td[@class=\"subtext\"][position()>0 and position()<={pageSize}]/..";

            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(xpath);

            int hackerNewsItemIndex = 0;

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (hackerNewsItem.Count < hackerNewsItemIndex)
                {
                    break;
                }

                // Unexpected format
                if (xmlNode.ChildNodes.Count < 2)
                {
                    continue;
                }

                for (int i = 0; i < xmlNode.ChildNodes[1].ChildNodes.Count; i++)
                {
                    // Usually second child is points
                    // Points format is: "{number} points"
                    if (xmlNode.ChildNodes[1].ChildNodes[i].InnerText.Contains("points"))
                    {
                        hackerNewsItem[hackerNewsItemIndex].Points = StringConverter.GetSingle(xmlNode.ChildNodes[1].ChildNodes[i].InnerText.Split(new char[0])[0]);
                    }

                    // Usually last child to be taken for comments
                    // AmountComments format is: "{number}&nbsp;comments"
                    if (xmlNode.ChildNodes[1].ChildNodes[i].InnerText.Contains("comments"))
                    {
                        string htmlContent = xmlNode.ChildNodes[1].ChildNodes[i].InnerText.Replace("&nbsp;", " ");

                        htmlContent = htmlContent.Replace("&nbsp;", " ");

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
