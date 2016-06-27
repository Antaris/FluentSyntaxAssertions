namespace FluentSyntaxAssertions.CSharp
{
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;
    using Xunit;

    /// <summary>
    /// Provides tests for the <see cref="UsingDirectiveExtensions"/> type.
    /// </summary>
    public class UsingDirectiveExtensionTests : TestBase
    {
        public UsingDirectiveExtensionTests() : base("CSharp.UsingDirectiveExtensions", "cs") { }

        [Fact]
        public void GetMatchingUsingDirectives_ReturnsMatch()
        {
            // Arrange
            string code = GetEmbedContent("SingleUsingDirective");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act
            var matches = UsingDirectiveExtensions.GetMatchingUsingDirectives(compilationUnit.Usings, "System").ToList();

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
            string code = GetEmbedContent("MultipleUsingDirectives");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act
            var matches = UsingDirectiveExtensions.GetMatchingUsingDirectives(compilationUnit.Usings, "System").ToList();

            // Assert
            Assert.NotNull(matches);
            Assert.Equal(2, matches.Count);
            Assert.NotNull(matches[0]);
            Assert.Equal(0, matches[0].Index);
            Assert.NotNull(matches[1]);
            Assert.Equal(2, matches[1].Index);
        }

        [Fact]
        public void GetMatchingUsingDirectives_ChainsMatches()
        {
            // Arrange
            string code = GetEmbedContent("MultipleUsingDirectives");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act
            var matches = UsingDirectiveExtensions.GetMatchingUsingDirectives(compilationUnit.Usings, "System").ToList();

            // Assert
            Assert.NotNull(matches);
            Assert.Equal(2, matches.Count);
            Assert.NotNull(matches[0]);
            Assert.Equal(0, matches[0].Index);
            Assert.NotNull(matches[0].Next);
            Assert.Equal(matches[0].Next, matches[1]);
            Assert.NotNull(matches[1].Previous);
            Assert.Equal(matches[0], matches[1].Previous);
        }

        [Fact]
        public void HasUsingDirective_ForCompilationUnit_ThrowsIfUsingDirectiveNotFound()
        {
            // Arrange
            string code = GetEmbedContent("SingleUsingDirective");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act

            // Assert
            Assert.Throws<SyntaxAssertionException>(() => UsingDirectiveExtensions.HasUsingDirective(compilationUnit, "System.Collections"));
        }

        [Fact]
        public void HasUsingDirective_ForCompilationUnit_DoesNotThrowIfUsingDirectiveFound()
        {
            // Arrange
            string code = GetEmbedContent("SingleUsingDirective");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act

            // Assert
            UsingDirectiveExtensions.HasUsingDirective(compilationUnit, "System");
        }

        [Fact]
        public void HasUsingDirective_ForCompilationUnit_PassesFirstMatchToAssertDelegate()
        {
            // Arrange
            string code = GetEmbedContent("SingleUsingDirective");
            var compilationUnit = SyntaxFactory.ParseCompilationUnit(code);

            // Act

            // Assert
            UsingDirectiveExtensions.HasUsingDirective(compilationUnit, "System",
                assert: m =>
                {
                    Assert.NotNull(m);
                    Assert.Equal(0, m.Index);
                });
        }
    }
}
