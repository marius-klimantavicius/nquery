using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Binding
{
    internal sealed class BoundPropertyAccessExpression : BoundExpression
    {
        public BoundPropertyAccessExpression(BoundExpression target, PropertySymbol propertySymbol)
        {
            Target = target;
            Symbol = propertySymbol;
        }

        public override BoundNodeKind Kind => BoundNodeKind.PropertyAccessExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => Symbol.Type;

        public PropertySymbol Symbol { get; }

        public BoundExpression Target { get; }

        public PropertySymbol PropertySymbol => Symbol;

        public BoundPropertyAccessExpression Update(BoundExpression target, PropertySymbol propertySymbol)
        {
            if (target == Target && propertySymbol == Symbol)
                return this;

            return new BoundPropertyAccessExpression(target, propertySymbol);
        }

        public override string ToString()
        {
            return $"{Target}.{Symbol.Name}";
        }
    }
}