namespace NQuery.Binding
{
    internal sealed class BoundEmptyRelation : BoundRelation
    {
        public override BoundNodeKind Kind => BoundNodeKind.EmptyRelation;

        public override IEnumerable<ValueSlot> GetDefinedValues()
        {
            return Enumerable.Empty<ValueSlot>();
        }

        public override IEnumerable<ValueSlot> GetOutputValues()
        {
            return Enumerable.Empty<ValueSlot>();
        }
    }
}