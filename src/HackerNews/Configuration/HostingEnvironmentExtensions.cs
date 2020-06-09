using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HackerNews.WebApi.Configuration
{
    /// <summary>
    /// Defines the <see cref="HostingEnvironmentExtensions" />.
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        #region Public Methods

        /// <summary>
        /// The GetAppConfiguration.
        /// </summary>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        /// <returns>The <see cref="IConfigurationRoot"/>.</returns>
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }

        #endregion
    }
}
