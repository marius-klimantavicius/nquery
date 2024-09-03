using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols.Aggregation
{
    public abstract class AggregateDefinition
    {
        public abstract string Name { get; }
        public abstract IAggregatable? CreateAggregatable([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type argumentType);
    }
}