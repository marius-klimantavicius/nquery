namespace NQuery.Syntax
{
    public abstract class StructuredTriviaSyntax : SyntaxNode
    {
        private protected StructuredTriviaSyntax(SyntaxTree syntaxTree)
            : base(syntaxTree)
        {
        }

        public SyntaxTrivia? ParentTrivia => SyntaxTree.GetParentTrivia(this);
    }
}