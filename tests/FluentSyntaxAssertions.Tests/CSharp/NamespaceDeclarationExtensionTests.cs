namespace FluentSyntaxAssertions.CSharp
{
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Xunit;

    /// <summary>
    /// Provides tests for the <see cref="NamespaceDeclarationExtensions"/> type.
    /// </summary>
    public class NamespaceDeclarationExtensionTests : TestBase
    {
        public NamespaceDeclarationExtensionTests() : base("CSharp.NamespaceDeclarationExtensions", "cs") { }

        [Fact]
        public void GetMatchingUsingDirectives_ReturnsMatch()
        {
            // Arrange
            string code = GetEmbedContent("SingleNamespaceDeclaration");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act
            var matches = NamespaceDeclarationExtensions.GetMatchingNamespaceDeclarations(compilationUnit.Members, "ModuleA").ToList();

            // Assert
            Assert.NotNull(matches);
            Assert.Equal(1, matches.Count);
            Assert.NotNull(matches[0]);
            Assert.Equal(0, matches[0].Index);
        }

        [Fact]
        public void GetMatchingUsingDirectives_ReturnsMatches()
        {
            // Arrange
            string code = GetEmbedContent("MultipleNamespaceDeclarations");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act
            var matches = NamespaceDeclarationExtensions.GetMatchingNamespaceDeclarations(compilationUnit.Members, "ModuleA").ToList();

            // Assert
            Assert.NotNull(matches);
            Assert.Equal(2, matches.Count);
            Assert.NotNull(matches[0]);
            Assert.Equal(0, matches[0].Index);
            Assert.NotNull(matches[0]);
            Assert.Equal(0, matches[0].Index);
        }
    }
}
