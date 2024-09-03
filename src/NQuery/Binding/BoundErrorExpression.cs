using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundErrorExpression : BoundExpression
    {
        public override BoundNodeKind Kind => BoundNodeKind.ErrorExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => TypeFacts.Unknown;

        public override string ToString()
        {
            return @"?";
        }
    }
}