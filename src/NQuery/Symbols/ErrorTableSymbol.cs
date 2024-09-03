using System.Collections.Immutable;

namespace NQuery.Symbols
{
    public sealed class ErrorTableSymbol : TableSymbol
    {
        internal ErrorTableSymbol(string name)
            : base(name)
        {
        }

        public override SymbolKind Kind => SymbolKind.ErrorTable;

        public override Type Type => TypeFacts.Unknown;

        public override ImmutableArray<ColumnSymbol> Columns => ImmutableArray<ColumnSymbol>.Empty;
    }
}