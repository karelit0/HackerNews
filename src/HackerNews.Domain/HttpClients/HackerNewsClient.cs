using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Domain.HttpClients
{
    /// <summary>
    /// Defines the <see cref="HackerNewsClient" />.
    /// </summary>
    public class HackerNewsClient : HttpClient
    {
        #region Fields

        /// <summary>
        /// Defines the hackerNewsUrl.
        /// </summary>
        private readonly string hackerNewsUrl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsClient"/> class.
        /// </summary>
        public HackerNewsClient()
        {
            hackerNewsUrl = "https://news.ycombinator.com/";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send Http.Get to read the content of the web page.
        /// <para>If error: return Exception</para>
        /// <para>If success: return stream</para>.
        /// </summary>
        /// <returns>.</returns>
        public async Task<Stream> GetNewsAsStream()
        {
            var result = await GetAsync(hackerNewsUrl);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Server response with: StatusCode => {result.StatusCode}. ReasonPhrase => {result.ReasonPhrase}");
            }

            return await result.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Send Http.Get to read the content of the web page.
        /// <para>If error: return Exception</para>
        /// <para>If success: return stream</para>.
        /// </summary>
        /// <returns>.</returns>
        public async Task<string> GetNewsAsString()
        {
            var result = await GetAsync(hackerNewsUrl);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Server response with: StatusCode => {result.StatusCode}. ReasonPhrase => {result.ReasonPhrase}");
            }

            return await result.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
