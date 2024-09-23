using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundWindowFunctionExpression : BoundExpression
    {
        public BoundWindowFunctionExpression(IEnumerable<BoundComparedValue> partitionBy, IEnumerable<BoundComparedValue> orderBy)
        {
            PartitionBy = partitionBy.ToImmutableArray();
            OrderBy = orderBy.ToImmutableArray();
        }

        public override BoundNodeKind Kind => BoundNodeKind.WindowFunctionExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => typeof(int);

        public ImmutableArray<BoundComparedValue> PartitionBy { get; }
        public ImmutableArray<BoundComparedValue> OrderBy { get; }

        public BoundWindowFunctionExpression Update(IEnumerable<BoundComparedValue> partitionBy, IEnumerable<BoundComparedValue> orderBy)
        {
            var newPartitionBy = partitionBy.ToImmutableArray();
            var newOrderBy = orderBy.ToImmutableArray();

            if (newPartitionBy == PartitionBy && newOrderBy == OrderBy)
                return this;

            return new BoundWindowFunctionExpression(newPartitionBy, newOrderBy);
        }

        public override string ToString()
        {
            var partitionByList = string.Join(", ", PartitionBy);
            var partitionBy = "";
            if (!string.IsNullOrEmpty(partitionByList))
                partitionBy = $"PARTITION BY {partitionByList}";

            var orderByList = string.Join(", ", OrderBy);
            var orderBy = "";
            if (!string.IsNullOrEmpty(orderByList))
                orderBy = $"ORDER BY {orderByList}";

            if (!string.IsNullOrEmpty(partitionBy) && !string.IsNullOrEmpty(orderBy))
                orderBy = " " + orderBy;

            return $"ROW_NUMBER() OVER({partitionBy}{orderBy})";
        }
    }
}