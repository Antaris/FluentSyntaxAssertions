namespace FluentSyntaxAssertions
{
    using System;

    /// <summary>
    /// Provides argument validation
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Validates the given argument is not null.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="arg">The argument instance.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <returns>The argument instance.</returns>
        public static T ArgumentNotNull<T>(T arg, string paramName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return arg;
        }

        /// <summary>
        /// Validates the given argument is not null or empty.
        /// </summary>
        /// <param name="arg">The argument instance.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <returns>The argument instance.</returns>
        public static string ArgumentNotNullOrEmpty(string arg, string paramName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException($"The parameter '{paramName}' cannot be null.");
            }

            return arg;
        }
    }
}