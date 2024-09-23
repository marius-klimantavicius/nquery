using System.Collections.Immutable;

namespace NQuery.Binding
{
    internal sealed class BoundWindowFunctionRelation : BoundRelation
    {
        public BoundWindowFunctionRelation(BoundRelation input, ValueSlot output, IEnumerable<BoundComparedValue> orderBy, IEnumerable<BoundComparedValue> partitionBy)
        {
            Input = input;
            Output = output;
            OrderBy = orderBy.ToImmutableArray();
            PartitionBy = partitionBy.ToImmutableArray();
        }

        public override BoundNodeKind Kind => BoundNodeKind.WindowFunctionRelation;

        public BoundRelation Input { get; }
        public ValueSlot Output { get; }
        public ImmutableArray<BoundComparedValue> OrderBy { get; }
        public ImmutableArray<BoundComparedValue> PartitionBy { get; }

        public override IEnumerable<ValueSlot> GetDefinedValues()
        {
            return Input.GetDefinedValues().Concat(new[] { Output });
        }

        public override IEnumerable<ValueSlot> GetOutputValues()
        {
            return Input.GetOutputValues().Concat(new[] { Output });
        }

        public BoundWindowFunctionRelation Update(BoundRelation input, ValueSlot output, IEnumerable<BoundComparedValue> orderBy, IEnumerable<BoundComparedValue> partitionBy)
        {
            var newOrderBy = orderBy.ToImmutableArray();
            var newPartitionBy = partitionBy.ToImmutableArray();
            if (input == Input && output == Output && newOrderBy == OrderBy && newPartitionBy == PartitionBy)
                return this;

            return new BoundWindowFunctionRelation(input, output, newOrderBy, newPartitionBy);
        }

        public static BoundRelation Build(BoundRelation input, ImmutableArray<BoundWindowFunctionValue> windowFunctions)
        {
            foreach (var item in windowFunctions)
                input = new BoundWindowFunctionRelation(input, item.Output, item.OrderBy, item.PartitionBy);

            return input;
        }
    }
}