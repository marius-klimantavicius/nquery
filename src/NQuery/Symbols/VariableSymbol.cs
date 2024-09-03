namespace NQuery.Symbols
{
    public sealed class VariableSymbol : Symbol
    {
        private readonly Type _type;

        private object? _value;

        public VariableSymbol(string name, Type type, object? value = null)
            : base(name)
        {
            _type = type;
            Value = value;
        }

        public override SymbolKind Kind => SymbolKind.Variable;

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