namespace FluentSyntaxAssertions
{
    using System;

    /// <summary>
    /// Represents an exception thrown during assertion failures.
    /// </summary>
    public class SyntaxAssertionException : Exception
    {
        /// <summary>
        /// Initialises a new instance of <see cref="SyntaxAssertionException"/>
        /// </summary>
        /// <param name="message">The assertion failure message.</param>
        public SyntaxAssertionException(string message) : base(message) {  }
    }
}