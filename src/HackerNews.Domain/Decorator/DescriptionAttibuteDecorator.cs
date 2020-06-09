using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HackerNews.Domain.Decorator
{
    /// <summary>
    /// Defines the <see cref="DescriptionAttibuteDecorator" />.
    /// </summary>
    public static class DescriptionAttibuteDecorator
    {
        #region Public Methods

        /// <summary>
        /// The GetDescription.
        /// </summary>
        /// <param name="e">The e<see cref="Enum"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetDescription(this Enum e)
        {
            DescriptionAttribute attribute = (DescriptionAttribute)e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault();

            return attribute?.Description ?? e.ToString();
        }

        #endregion
    }
}
