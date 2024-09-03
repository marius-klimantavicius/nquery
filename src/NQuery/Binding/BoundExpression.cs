using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal abstract class BoundExpression : BoundNode
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public abstract Type Type { get; }
    }
}