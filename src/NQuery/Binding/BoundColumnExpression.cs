using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Binding
{
    internal sealed class BoundColumnExpression : BoundExpression
    {
        public BoundColumnExpression(ColumnInstanceSymbol symbol)
        {
            Symbol = symbol;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ColumnExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => Symbol.Type;

        public ColumnInstanceSymbol Symbol { get; }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}