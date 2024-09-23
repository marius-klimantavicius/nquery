using System.Collections.Immutable;

namespace NQuery.Binding
{
    internal sealed class BoundWindowFunctionValue
    {
        public BoundWindowFunctionValue(ValueSlot output, IEnumerable<BoundComparedValue> partitionBy, IEnumerable<BoundComparedValue> orderBy)
        {
            Output = output;
            PartitionBy = partitionBy.ToImmutableArray();
            OrderBy = orderBy.ToImmutableArray();
        }

        public ValueSlot Output { get; }
        public ImmutableArray<BoundComparedValue> PartitionBy { get; set; }
        public ImmutableArray<BoundComparedValue> OrderBy { get; set; }

        public BoundWindowFunctionValue Update(ValueSlot output, IEnumerable<BoundComparedValue> partitionBy, IEnumerable<BoundComparedValue> orderBy)
        {
            var newPartitionBy = partitionBy.ToImmutableArray();
            var newOrderBy = orderBy.ToImmutableArray();

            if (output == Output && newPartitionBy == PartitionBy && newOrderBy == OrderBy)
                return this;

            return new BoundWindowFunctionValue(output, newPartitionBy, newOrderBy);
        }
    }
}