namespace FluentSyntaxAssertions.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides extensions for assering over namespace declarations.
    /// </summary>
    public static class NamespaceDeclarationExtensions
    {
        /// <summary>
        /// Asserts that the selected compilation unit contains a namespace declaration.
        /// </summary>
        /// <param name="syntax">The compilation unit syntax.</param>
        /// <param name="name">The namespace name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The compilation unit syntax.</returns>
        public static CompilationUnitSyntax HasNamespaceDeclaration(this CompilationUnitSyntax syntax, string name, Action<Match<NamespaceDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingNamespaceDeclarations(syntax.Members, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The compilation unit should have a namespace declaration '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Asserts that the selected namespace declaration contains a nested namespace declaration.
        /// </summary>
        /// <param name="syntax">The namespace declaration syntax.</param>
        /// <param name="name">The nested namespace name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The namespace declaration syntax.</returns>
        public static NamespaceDeclarationSyntax HasNamespaceDeclaration(this NamespaceDeclarationSyntax syntax, string name, Action<Match<NamespaceDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingNamespaceDeclarations(syntax.Members, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The namespace '{syntax.Name}' should have a namespace declaration '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Gets the matching set of namespace declarataions.
        /// </summary>
        /// <param name="list">The syntax list.</param>
        /// <param name="name">The namspace name.</param>
        /// <returns>The set of namespace declaration matches.</returns>
        internal static IEnumerable<Match<NamespaceDeclarationSyntax>> GetMatchingNamespaceDeclarations(this SyntaxList<MemberDeclarationSyntax> list, string name)
        {
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            if (list.Count == 0)
            {
                yield break;
            }

            Match<NamespaceDeclarationSyntax> last = null;

            for (int i = 0; i < list.Count; i++)
            {
                var node = list[i] as NamespaceDeclarationSyntax;
                if (node != null)
                {
                    if (string.Equals(node.Name.ToString(), name, StringComparison.Ordinal))
                    {
                        var match = new Match<NamespaceDeclarationSyntax>(node, i);
                        if (last != null)
                        {
                            last.Next = match;

                            yield return last;
                        }

                        last = match;
                    }
                }
            }

            if (last != null)
            {
                yield return last;
            }
        }
    }
}
