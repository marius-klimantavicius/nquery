namespace NQuery.Syntax;

public sealed class OrderByClauseSyntax : SyntaxNode
{
    internal OrderByClauseSyntax(SyntaxTree syntaxTree, SyntaxToken orderKeyword, SyntaxToken byKeyword, SeparatedSyntaxList<OrderByColumnSyntax> columns)
        : base(syntaxTree)
    {
        OrderKeyword = orderKeyword;
        ByKeyword = byKeyword;
        Columns = columns;
    }

    public override SyntaxKind Kind => SyntaxKind.OrderByClause;

    public override IEnumerable<SyntaxNodeOrToken> ChildNodesAndTokens()
    {
        yield return OrderKeyword;
        yield return ByKeyword;

        foreach (var nodeOrToken in Columns.GetWithSeparators())
            yield return nodeOrToken;
    }

    public SyntaxToken OrderKeyword { get; }

    public SyntaxToken ByKeyword { get; }

    public SeparatedSyntaxList<OrderByColumnSyntax> Columns { get; }
}