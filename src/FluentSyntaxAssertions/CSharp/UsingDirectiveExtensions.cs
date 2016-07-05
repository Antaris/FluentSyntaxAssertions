namespace FluentSyntaxAssertions.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides extensions for asserting over using directives.
    /// </summary>
    public static class UsingDirectiveExtensions
    {
        /// <summary>
        /// Asserts that the given compilation unit has the required using directive.
        /// </summary>
        /// <param name="syntax">The syntax</param>
        /// <param name="name">The using directive name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The compilation unit syntax.</returns>
        public static CompilationUnitSyntax HasUsingDirective(this CompilationUnitSyntax syntax, string name, Action<Match<UsingDirectiveSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingUsingDirectives(syntax.Usings, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The compilation unit should have a using directive '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Asserts that the given namespace declarataion has the required using directive.
        /// </summary>
        /// <param name="syntax">The syntax</param>
        /// <param name="name">The using directive name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The namespace declaration syntax.</returns>
        public static NamespaceDeclarationSyntax HasUsingDirective(this NamespaceDeclarationSyntax syntax, string name, Action<Match<UsingDirectiveSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingUsingDirectives(syntax.Usings, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The namespace '{syntax.Name}' should have a using directive '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Asserts that the given namespace declarataion has the required using directive.
        /// </summary>
        /// <param name="match">The syntax match</param>
        /// <param name="name">The using directive name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The namespace declaration syntax.</returns>
        public static Match<NamespaceDeclarationSyntax> HasUsingDirective(this Match<NamespaceDeclarationSyntax> match, string name, Action<Match<UsingDirectiveSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(match, nameof(match));

            HasUsingDirective(match.Node, name, assert, message);

            return match;
        }

        /// <summary>
        /// Gets the matching set of using directives.
        /// </summary>
        /// <param name="list">The syntax list.</param>
        /// <param name="name">The using directive name.</param>
        /// <returns>The set of using directive matches.</returns>
        internal static IEnumerable<Match<UsingDirectiveSyntax>> GetMatchingUsingDirectives(this SyntaxList<UsingDirectiveSyntax> list, string name)
        {
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            if (list.Count == 0)
            {
                yield break;
            }

            Match<UsingDirectiveSyntax> last = null;

            for (int i = 0; i < list.Count; i++)
            {
                var node = list[i];
                if (string.Equals(node.Name.ToString(), name, StringComparison.Ordinal))
                {
                    var match = new Match<UsingDirectiveSyntax>(node, i);
                    if (last != null)
                    {
                        last.Next = match;

                        yield return last;
                    }

                    last = match;
                }
            }

            if (last != null)
            {
                yield return last;
            }
        }
    }
}
