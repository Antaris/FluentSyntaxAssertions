namespace FluentSyntaxAssertions
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.FileProviders;
    
    /// <summary>
    /// Provides a base implementation of a test.
    /// </summary>
    public abstract class TestBase
    {
        private readonly string _prefix;
        private readonly string _extension;
        private readonly IFileProvider _provider;
        
        /// <summary>
        /// Initialises a new instance of <see cref="TestBase"/>
        /// </summary>
        /// <param name="prefix">The file prefix.</param>
        /// <param name="extension">The file extension.</param>
        public TestBase(string prefix, string extension)
        {
            _prefix = Ensure.ArgumentNotNullOrEmpty(prefix, nameof(prefix));
            _extension = Ensure.ArgumentNotNullOrEmpty(extension, nameof(extension));

            _provider = new EmbeddedFileProvider(typeof(TestBase).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Gets the embedded content from the given resource.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <returns>The embedded</returns>
        public string GetEmbedContent(string resource)
        {
            Ensure.ArgumentNotNullOrEmpty(resource, nameof(resource));

            string path = $"FluentSyntaxAssertions.Tests._Content.{_prefix}.{resource}.{_extension}";

            var file = _provider.GetFileInfo(path);
            if (file.Exists)
            {
                using (var stream = file.CreateReadStream())
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }

            throw new ArgumentException($"The path '{path}' could not be found.");
        }
    }
}