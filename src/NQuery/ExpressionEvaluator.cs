using System.Diagnostics.CodeAnalysis;

namespace NQuery
{
    public sealed class ExpressionEvaluator
    {
        private readonly Func<object?> _evaluator;

        internal ExpressionEvaluator(
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type, 
            Func<object?> evaluator)
        {
            Type = type;
            _evaluator = evaluator;
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public Type Type { get; }

        public object? Evaluate()
        {
            return _evaluator();
        }
    }
}