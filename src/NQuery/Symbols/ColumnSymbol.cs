using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols
{
    public class ColumnSymbol : Symbol
    {
        internal ColumnSymbol(string name, Type type)
            : base(name)
        {
            ArgumentNullException.ThrowIfNull(type);

            Type = type;
        }

        public override SymbolKind Kind => SymbolKind.Column;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type { get; }
    }
}