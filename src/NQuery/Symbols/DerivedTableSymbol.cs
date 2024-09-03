using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols
{
    public sealed class DerivedTableSymbol : TableSymbol
    {
        internal DerivedTableSymbol(IEnumerable<ColumnSymbol> columns)
            : base(string.Empty)
        {
            Columns = columns.ToImmutableArray();
        }

        public override SymbolKind Kind => SymbolKind.DerivedTable;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => TypeFacts.Missing;

        public override ImmutableArray<ColumnSymbol> Columns { get; }
    }
}