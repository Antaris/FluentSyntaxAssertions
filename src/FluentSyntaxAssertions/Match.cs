namespace FluentSyntaxAssertions
{
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Represents a match from a fluent assertion.
    /// </summary>
    /// <typeparam name="T">The syntax node type.</typeparam>
    public class Match<T> where T : SyntaxNode
    {
        private Match<T> _next;
        private Match<T> _prev;

        /// <summary>
        /// Initialises a new instance of <see cref="Match{T}"/>
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="index">The index of that node.</param>
        public Match(T node, int index)
        {
            Node = node;
            Index = index;
        }

        /// <summary>
        /// Gets the index of the node within its syntax list.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the node.
        /// </summary>
        public T Node { get; private set; }

        /// <summary>
        /// Gets the next match.
        /// </summary>
        public Match<T> Next
        {
            get
            {
                return _next;
            }
            set
            {
                if (_next != null)
                {
                    _next._prev = null;
                }

                _next = value;

                if (_next != null)
                {
                    _next._prev = this;
                }
            }
        }

        /// <summary>
        /// Gets the previous match.
        /// </summary>
        public Match<T> Previous
        {
            get
            {
                return _prev;
            }
            set
            {
                if (_prev != null)
                {
                    _prev._next = null;
                }

                _prev = value;

                if (_prev != null)
                {
                    _prev._next = this;
                }
            }
        }
    }
}
