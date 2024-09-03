using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols.Aggregation
{
    public sealed class AggregateSymbol : Symbol
    {
        public AggregateSymbol(AggregateDefinition definition)
            : base(definition.Name)
        {
            Definition = definition;
        }

        public AggregateDefinition Definition { get; }

        public override SymbolKind Kind => SymbolKind.Aggregate;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => TypeFacts.Missing;
    }
}