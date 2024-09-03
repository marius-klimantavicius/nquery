using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols
{
    public sealed class ErrorTableSymbol : TableSymbol
    {
        internal ErrorTableSymbol(string name)
            : base(name)
        {
        }

        public override SymbolKind Kind => SymbolKind.ErrorTable;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => TypeFacts.Unknown;

        public override ImmutableArray<ColumnSymbol> Columns => ImmutableArray<ColumnSymbol>.Empty;
    }
}