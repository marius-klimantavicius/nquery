using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols.Aggregation
{
    public interface IAggregatable
    {
        IAggregator CreateAggregator();

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        Type ReturnType { get; }
    }
}