using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Binding
{
    internal sealed class BoundRowNumberExpression : BoundExpression
    {
        public BoundRowNumberExpression(IEnumerable<BoundExpression> partitionBy, IEnumerable<BoundExpression> orderBy)
        {
            PartitionBy = partitionBy.ToImmutableArray();
            OrderBy = orderBy.ToImmutableArray();
        }

        public override BoundNodeKind Kind => BoundNodeKind.RowNumberExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => typeof(int);

        public ImmutableArray<BoundExpression> PartitionBy { get; }
        public ImmutableArray<BoundExpression> OrderBy { get; }

        public BoundRowNumberExpression Update(IEnumerable<BoundExpression> partitionBy, IEnumerable<BoundExpression> orderBy)
        {
            var newPartitionBy = partitionBy.ToImmutableArray();
            var newOrderBy = orderBy.ToImmutableArray();

            if (newPartitionBy == PartitionBy && newOrderBy == OrderBy)
                return this;

            return new BoundRowNumberExpression(newPartitionBy, newOrderBy);
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

    internal sealed class BoundFunctionInvocationExpression : BoundExpression
    {
        public BoundFunctionInvocationExpression(IEnumerable<BoundExpression> arguments, OverloadResolutionResult<FunctionSymbolSignature> result)
        {
            Arguments = arguments.ToImmutableArray();
            Result = result;
        }

        public override BoundNodeKind Kind => BoundNodeKind.FunctionInvocationExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => Symbol is null ? TypeFacts.Unknown : Symbol.Type;

        public FunctionSymbol? Symbol => Result.Selected?.Signature.Symbol;

        public ImmutableArray<BoundExpression> Arguments { get; }

        public OverloadResolutionResult<FunctionSymbolSignature> Result { get; }

        public BoundFunctionInvocationExpression Update(IEnumerable<BoundExpression> arguments, OverloadResolutionResult<FunctionSymbolSignature> result)
        {
            var newArguments = arguments.ToImmutableArray();

            if (newArguments == Arguments && result == Result)
                return this;

            return new BoundFunctionInvocationExpression(newArguments, result);
        }

        public override string ToString()
        {
            return $"{Symbol?.Name}({string.Join(@",", Arguments)})";
        }
    }
}