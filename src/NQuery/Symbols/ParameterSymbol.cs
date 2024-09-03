using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols
{
    public class ParameterSymbol : Symbol
    {
        public ParameterSymbol(string name, Type type)
            : base(name)
        {
            Type = type;
        }

        public override SymbolKind Kind => SymbolKind.Parameter;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type { get; }
    }
}