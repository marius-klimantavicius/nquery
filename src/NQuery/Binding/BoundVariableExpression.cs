using NQuery.Symbols;

namespace NQuery.Binding
{
    internal sealed class BoundVariableExpression : BoundExpression
    {
        public BoundVariableExpression(VariableSymbol variableSymbol)
        {
            Symbol = variableSymbol;
        }

        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;

        public override Type Type => Symbol.Type;

        public VariableSymbol Symbol { get; }

        public override string ToString()
        {
            return $"@{Symbol.Name}";
        }
    }
}