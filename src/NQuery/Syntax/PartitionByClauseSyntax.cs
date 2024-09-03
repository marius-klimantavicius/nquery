namespace NQuery.Syntax;

public sealed class PartitionByClauseSyntax : SyntaxNode
{
    internal PartitionByClauseSyntax(SyntaxTree syntaxTree, SyntaxToken partitionKeyword, SyntaxToken byKeyword, SeparatedSyntaxList<OrderByColumnSyntax> columns)
        : base(syntaxTree)
    {
        PartitionKeyword = partitionKeyword;
        ByKeyword = byKeyword;
        Columns = columns;
    }

    public override SyntaxKind Kind => SyntaxKind.PartitionByClause;

    public override IEnumerable<SyntaxNodeOrToken> ChildNodesAndTokens()
    {
        yield return PartitionKeyword;
        yield return ByKeyword;

        foreach (var nodeOrToken in Columns.GetWithSeparators())
            yield return nodeOrToken;
    }

    public SyntaxToken PartitionKeyword { get; }

    public SyntaxToken ByKeyword { get; }

    public SeparatedSyntaxList<OrderByColumnSyntax> Columns { get; }
}