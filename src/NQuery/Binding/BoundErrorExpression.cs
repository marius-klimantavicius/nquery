namespace NQuery.Binding
{
    internal sealed class BoundErrorExpression : BoundExpression
    {
        public override BoundNodeKind Kind => BoundNodeKind.ErrorExpression;

        public override Type Type => TypeFacts.Unknown;

        public override string ToString()
        {
            return @"?";
        }
    }
}