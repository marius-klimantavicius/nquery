using System.Diagnostics.CodeAnalysis;
using NQuery.Text;

namespace NQuery
{
    public struct SyntaxNodeOrToken
    {
        private readonly SyntaxNode? _syntaxNode;
        private readonly SyntaxToken? _syntaxToken;

        internal SyntaxNodeOrToken(SyntaxToken syntaxToken)
        {
            _syntaxToken = syntaxToken;
            _syntaxNode = null;
        }

        internal SyntaxNodeOrToken(SyntaxNode syntaxNode)
        {
            _syntaxToken = null;
            _syntaxNode = syntaxNode;
        }

        [MemberNotNullWhen(true, nameof(_syntaxToken), nameof(AsToken))]
        [MemberNotNullWhen(false, nameof(_syntaxNode), nameof(AsNode))]
        public bool IsToken => !IsNode;

        [MemberNotNullWhen(true, nameof(_syntaxNode), nameof(AsNode))]
        [MemberNotNullWhen(false, nameof(_syntaxToken), nameof(AsToken))]
        public bool IsNode => _syntaxNode is not null;

        public SyntaxToken? AsToken => _syntaxToken;

        public SyntaxNode? AsNode => _syntaxNode;

        public bool IsEquivalentTo(SyntaxNodeOrToken other)
        {
            return SyntaxTreeEquivalence.AreEquivalent(this, other);
        }

        public SyntaxNode? Parent => IsNode ? AsNode.Parent : AsToken.Parent;

        public SyntaxTree? SyntaxTree => Parent?.SyntaxTree;

        public SyntaxKind Kind => IsNode ? AsNode.Kind : AsToken.Kind;

        public TextSpan Span => IsNode ? AsNode.Span : AsToken.Span;

        public TextSpan FullSpan => IsNode ? AsNode.FullSpan : AsToken.FullSpan;

        public bool IsMissing => IsNode ? AsNode.IsMissing : AsToken.IsMissing;

        public static implicit operator SyntaxNodeOrToken(SyntaxToken syntaxToken)
        {
            return new SyntaxNodeOrToken(syntaxToken);
        }

        public static implicit operator SyntaxNodeOrToken(SyntaxNode syntaxNode)
        {
            return new SyntaxNodeOrToken(syntaxNode);
        }
    }
}