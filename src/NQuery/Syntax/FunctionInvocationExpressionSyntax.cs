namespace NQuery.Syntax
{
    public sealed class FunctionInvocationExpressionSyntax : ExpressionSyntax
    {
        internal FunctionInvocationExpressionSyntax(SyntaxTree syntaxTree, SyntaxToken name, ArgumentListSyntax argumentList, OverClauseSyntax? overClause)
            : base(syntaxTree)
        {
            Name = name;
            ArgumentList = argumentList;
            OverClause = overClause;
        }

        public override SyntaxKind Kind => SyntaxKind.FunctionInvocationExpression;

        public override IEnumerable<SyntaxNodeOrToken> ChildNodesAndTokens()
        {
            yield return Name;
            yield return ArgumentList;

            if (OverClause != null)
                yield return OverClause;
        }

        public SyntaxToken Name { get; }

        public ArgumentListSyntax ArgumentList { get; }
        public OverClauseSyntax? OverClause { get; }
    }
}