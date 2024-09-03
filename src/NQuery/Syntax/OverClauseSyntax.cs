namespace NQuery.Syntax;

public sealed class OverClauseSyntax : SyntaxNode
{
    internal OverClauseSyntax(SyntaxTree syntaxTree, SyntaxToken overKeyword, SyntaxToken leftParenthesis, PartitionByClauseSyntax? partitionBy, OrderByClauseSyntax? orderBy, SyntaxToken rightParenthesis) : base(syntaxTree)
    {
        OverKeyword = overKeyword;
        LeftParenthesis = leftParenthesis;
        PartitionBy = partitionBy;
        OrderBy = orderBy;
        RightParenthesis = rightParenthesis;
    }

    public override SyntaxKind Kind => SyntaxKind.OverClause;

    public SyntaxToken OverKeyword { get; }
    public SyntaxToken LeftParenthesis { get; }
    public PartitionByClauseSyntax? PartitionBy { get; }
    public OrderByClauseSyntax? OrderBy { get; }
    public SyntaxToken RightParenthesis { get; }

    public override IEnumerable<SyntaxNodeOrToken> ChildNodesAndTokens()
    {
        yield return OverKeyword;
        yield return LeftParenthesis;

        if (PartitionBy != null)
            yield return PartitionBy;
        if (OrderBy != null)
            yield return OrderBy;

        yield return RightParenthesis;
    }
}