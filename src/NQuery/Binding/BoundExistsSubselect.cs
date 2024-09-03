using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundExistsSubselect : BoundExpression
    {
        public BoundExistsSubselect(BoundRelation relation)
        {
            Relation = relation;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ExistsSubselect;

        public BoundRelation Relation { get; }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => typeof(bool);

        public BoundExistsSubselect Update(BoundRelation relation)
        {
            if (relation == Relation)
                return this;

            return new BoundExistsSubselect(relation);
        }

        public override string ToString()
        {
            return $"EXISTS ({Relation})";
        }
    }
}