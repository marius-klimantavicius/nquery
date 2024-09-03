using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols
{
    public sealed class VariableSymbol : Symbol
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _type;

        private object? _value;

        public VariableSymbol(string name, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type, 
            object? value = null)
            : base(name)
        {
            _type = type;
            Value = value;
        }

        public override SymbolKind Kind => SymbolKind.Variable;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type => _type;

        public object? Value
        {
            get => _value;
            set
            {
                if (value is not null && !_type.IsInstanceOfType(value))
                    throw new ArgumentException(string.Format(Resources.VariableValueTypeMismatch, value, _type), nameof(value));

                _value = value;
            }
        }
    }
}