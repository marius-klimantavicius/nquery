using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundLiteralExpression : BoundExpression
    {
        public BoundLiteralExpression(object? value)
        {
            Value = value;
        }

        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type =>
            Value is null
                ? TypeFacts.Null
                : Value.GetType();

        public object? Value { get; }

        public override string? ToString()
        {
            return Value switch
            {
                null => @"NULL",
                string => $"'{Value}'",
                DateTime => $"#{Value}#",
                _ => Value.ToString()
            };
        }
    }
}