using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleSample
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using FluentSyntaxAssertions;
    using FluentSyntaxAssertions.CSharp;

    public class Program
    {
        public static void Main(string[] args)
        {
            const string FilePath = @".\Program.cs";

            var compilationUnit = SyntaxFactory.ParseCompilationUnit(
                File.ReadAllText(FilePath));

            // Assert
            compilationUnit
                .HasUsingDirective("System")
                .HasUsingDirective("System.Collections.Generic")
                .HasUsingDirective("System.IO")
                .HasUsingDirective("System.Linq")
                .HasUsingDirective("System.Threading.Tasks")
                .HasNamespaceDeclaration("ConsoleSample", ns => ns
                    .HasUsingDirective("Microsoft.CodeAnalysis")
                    .HasUsingDirective("Microsoft.CodeAnalysis.CSharp")
                    .HasUsingDirective("FluentSyntaxAssertions")
                    .HasUsingDirective("FluentSyntaxAssertions.CSharp")
                    .HasClassDeclaration("Program", c => c
                        .IsPublic()
                    )
                );

            Console.ReadKey();
        }
    }
}
