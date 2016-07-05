namespace FluentSyntaxAssertions.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides extensions for asserting over class declarations.
    /// </summary>
    public static class ClassDeclarationExtensions
    {
        /// <summary>
        /// Asserts that the selected namespace declaration contains a class declaration.
        /// </summary>
        /// <param name="syntax">The namespace declaration syntax.</param>
        /// <param name="name">The class name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The namespace declaration syntax.</returns>
        public static NamespaceDeclarationSyntax HasClassDeclaration(this NamespaceDeclarationSyntax syntax, string name, Action<Match<ClassDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingClassDeclarations(syntax.Members, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The compilation unit should have a namespace declaration '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Asserts that the selected class declaration contains a class declaration.
        /// </summary>
        /// <param name="syntax">The class declaration syntax.</param>
        /// <param name="name">The class name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The class declaration syntax.</returns>
        public static ClassDeclarationSyntax HasClassDeclaration(this ClassDeclarationSyntax syntax, string name, Action<Match<ClassDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            var matches = GetMatchingClassDeclarations(syntax.Members, name).ToList();

            if (matches.Count == 0)
            {
                throw new SyntaxAssertionException(
                    message ?? $"The compilation unit should have a namespace declaration '{name}'.");
            }

            assert?.Invoke(matches[0]);

            return syntax;
        }

        /// <summary>
        /// Asserts that the selected namespace declaration contains a class declaration.
        /// </summary>
        /// <param name="syntax">The namespace declaration syntax.</param>
        /// <param name="name">The class name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The namespace declaration syntax.</returns>
        public static Match<NamespaceDeclarationSyntax> HasClassDeclaration(this Match<NamespaceDeclarationSyntax> match, string name, Action<Match<ClassDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(match, nameof(match));

            HasClassDeclaration(match.Node, name, assert, null);

            return match;
        }

        /// <summary>
        /// Asserts that the selected class declaration contains a class declaration.
        /// </summary>
        /// <param name="syntax">The class declaration syntax.</param>
        /// <param name="name">The class name.</param>
        /// <param name="assert">[Optional] The assert delegate.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The class declaration syntax.</returns>
        public static Match<ClassDeclarationSyntax> HasClassDeclaration(this Match<ClassDeclarationSyntax> match, string name, Action<Match<ClassDeclarationSyntax>> assert = null, string message = null)
        {
            Ensure.ArgumentNotNull(match, nameof(match));

            HasClassDeclaration(match.Node, name, assert, null);

            return match;
        }

        /// <summary>
        /// Asserts that the selected class declaration has the public modifier.
        /// </summary>
        /// <param name="syntax">The class declaration syntax.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The class declaration syntax.</returns>
        public static ClassDeclarationSyntax IsPublic(this ClassDeclarationSyntax syntax, string message = null)
        {
            Ensure.ArgumentNotNull(syntax, nameof(syntax));

            if (!syntax.Modifiers.Any(SyntaxKind.PublicKeyword))
            {
                throw new SyntaxAssertionException(
                    message ?? $"The class declaration '{syntax.Identifier}' must have the 'public' modifier.");
            }

            return syntax;
        }

        /// <summary>
        /// Asserts that the selected class declaration has the public modifier.
        /// </summary>
        /// <param name="match">The class declaration match.</param>
        /// <param name="message">[Optional] The failure message.</param>
        /// <returns>The class declaration match.</returns>
        public static Match<ClassDeclarationSyntax> IsPublic(this Match<ClassDeclarationSyntax> match, string message = null)
        {
            Ensure.ArgumentNotNull(match, nameof(match));

            IsPublic(match.Node, message);

            return match;
        }

        /// <summary>
        /// Gets the matching set of namespace declarataions.
        /// </summary>
        /// <param name="list">The syntax list.</param>
        /// <param name="name">The namspace name.</param>
        /// <returns>The set of namespace declaration matches.</returns>
        internal static IEnumerable<Match<ClassDeclarationSyntax>> GetMatchingClassDeclarations(this SyntaxList<MemberDeclarationSyntax> list, string name)
        {
            Ensure.ArgumentNotNullOrEmpty(name, nameof(name));

            if (list.Count == 0)
            {
                yield break;
            }

            Match<ClassDeclarationSyntax> last = null;

            for (int i = 0; i < list.Count; i++)
            {
                var node = list[i] as ClassDeclarationSyntax;
                if (node != null)
                {
                    if (string.Equals(node.Identifier.ToString(), name, StringComparison.Ordinal))
                    {
                        var match = new Match<ClassDeclarationSyntax>(node, i);
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
