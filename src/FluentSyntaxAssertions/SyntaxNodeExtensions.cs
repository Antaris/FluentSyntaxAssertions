namespace FluentSyntaxAssertions
{
    using System;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Provides extensions for the <see cref="SyntaxNode"/> type.
    /// </summary>
    public static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Performs a number of asserts and throws the first exception if they all fail.
        /// </summary>
        /// <typeparam name="T">The syntax node type.</typeparam>
        /// <param name="node">The syntax node instance.</param>
        /// <param name="asserts">The set of assert functions.</param>
        /// <returns>The syntax node.</returns>
        public static T Any<T>(this T node, params Action<T>[] asserts) where T : SyntaxNode
        {
            Ensure.ArgumentNotNull(node, nameof(node));
            Ensure.ArgumentNotNull(asserts, nameof(asserts));

            if (asserts.Length > 0)
            {
                SyntaxAssertionException exception = null;

                for (int i = 0; i < asserts.Length; i++)
                {
                    try
                    {
                        asserts[i](node);
                    }
                    catch (SyntaxAssertionException ex)
                    {
                        if (exception == null)
                        {
                            exception = ex;
                        }
                    }
                }

                if (exception != null)
                {
                    throw exception;
                }
            }

            return node;
        }

        /// <summary>
        /// Provides a conditional branch for a given node.
        /// </summary>
        /// <typeparam name="T">The syntax node type.</typeparam>
        /// <param name="node">The syntax node instance.</param>
        /// <param name="result">The boolean result.</param>
        /// <param name="trueAssert">The assert delegate to execute if the result is true.</param>
        /// <param name="falseAssert">[Optional] The assert delegate to execute if the result is false.</param>
        /// <returns>The syntax node.</returns>
        public static T If<T>(this T node, bool result, Action<T> trueAssert, Action<T> falseAssert = null)
        {
            Ensure.ArgumentNotNull(node, nameof(node));
            Ensure.ArgumentNotNull(trueAssert, nameof(trueAssert));

            if (result)
            {
                trueAssert(node);
            }
            else
            {
                falseAssert?.Invoke(node);
            }

            return node;
        }

        /// <summary>
        /// Attempts an assertion and sets the a flag. This method catches any instance of <see cref="SyntaxAssertionException"/>.
        /// </summary>
        /// <typeparam name="T">The syntax node type.</typeparam>
        /// <param name="node">The syntax node instance.</param>
        /// <param name="assert">The assert delegate to try.</param>
        /// <param name="success">[Output] The success state.</param>
        /// <returns>The syntax node.</returns>
        public static T Try<T>(this T node, Action<T> assert, out bool success)
        {
            Ensure.ArgumentNotNull(node, nameof(node));
            Ensure.ArgumentNotNull(assert, nameof(assert));

            try
            {
                assert(node);

                success = true;
            }
            catch (SyntaxAssertionException ex)
            {
                success = false;
            }

            return node;
        }
    }
}