namespace NQuery.Syntax
{
    public sealed class TopClauseSyntax : SyntaxNode
    {
        internal TopClauseSyntax(SyntaxTree syntaxTree, SyntaxToken topKeyword, SyntaxToken? leftParenthesis, SyntaxToken value, SyntaxToken? rightParenthesis, SyntaxToken? withKeyword, SyntaxToken? tiesKeyword)
            : base(syntaxTree)
        {
            TopKeyword = topKeyword;
            LeftParenthesis = leftParenthesis;
            Value = value;
            RightParenthesis = rightParenthesis;
            WithKeyword = withKeyword;
            TiesKeyword = tiesKeyword;
        }

        public override SyntaxKind Kind => SyntaxKind.TopClause;

        public override IEnumerable<SyntaxNodeOrToken> ChildNodesAndTokens()
        {
            yield return TopKeyword;
            if (LeftParenthesis is not null)
                yield return LeftParenthesis;
            yield return Value;
            if (RightParenthesis is not null)
                yield return RightParenthesis;
            if (WithKeyword is not null)
                yield return WithKeyword;
            if (TiesKeyword is not null)
                yield return TiesKeyword;
        }

        public SyntaxToken TopKeyword { get; }
        
        public SyntaxToken? LeftParenthesis { get; }

        public SyntaxToken Value { get; }
        
        public SyntaxToken? RightParenthesis { get; }

        public SyntaxToken? WithKeyword { get; }

        public SyntaxToken? TiesKeyword { get; }
    }
}