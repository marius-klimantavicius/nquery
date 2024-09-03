using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundSingleRowSubselect : BoundExpression
    {
        public BoundSingleRowSubselect(ValueSlot value, BoundRelation relation)
        {
            Value = value;
            Relation = relation;
        }

        public override BoundNodeKind Kind => BoundNodeKind.SingleRowSubselect;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => Value.Type;

        public ValueSlot Value { get; }

        public BoundRelation Relation { get; }

        public BoundSingleRowSubselect Update(ValueSlot value, BoundRelation relation)
        {
            if (value == Value && relation == Relation)
                return this;

            return new BoundSingleRowSubselect(value, relation);
        }

        public override string ToString()
        {
            return $"({Relation})";
        }
    }
}