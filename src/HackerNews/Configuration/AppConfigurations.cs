using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Reflection;

namespace HackerNews.WebApi.Configuration
{
    /// <summary>
    /// Defines the <see cref="AppConfigurations" />.
    /// </summary>
    public static class AppConfigurations
    {
        #region Fields

        /// <summary>
        /// Defines the _configurationCache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="AppConfigurations"/> class.
        /// </summary>
        static AppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="environmentName">The environmentName<see cref="string"/>.</param>
        /// <param name="addUserSecrets">The addUserSecrets<see cref="bool"/>.</param>
        /// <returns>The <see cref="IConfigurationRoot"/>.</returns>
        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The BuildConfiguration.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="environmentName">The environmentName<see cref="string"/>.</param>
        /// <param name="addUserSecrets">The addUserSecrets<see cref="bool"/>.</param>
        /// <returns>The <see cref="IConfigurationRoot"/>.</returns>
        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!string.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(Assembly.GetAssembly(typeof(AppConfigurations)));
            }

            return builder.Build();
        }

        #endregion
    }
}
