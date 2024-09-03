using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundConversionExpression : BoundExpression
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _type;

        public BoundConversionExpression(BoundExpression expression, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type, 
            Conversion conversion)
        {
            Expression = expression;
            _type = type;
            Conversion = conversion;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ConversionExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => _type;

        public BoundExpression Expression { get; }

        public Conversion Conversion { get; }

        public BoundConversionExpression Update(BoundExpression expression, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type, 
            Conversion conversion)
        {
            if (expression == Expression && type == _type && conversion == Conversion)
                return this;

            return new BoundConversionExpression(expression, type, conversion);
        }

        public override string ToString()
        {
            return $"CAST({Expression} AS {_type.ToDisplayName()})";
        }
    }
}