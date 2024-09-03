using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery
{
    public abstract class Symbol
    {
        private protected Symbol(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            Name = name;
        }

        public abstract SymbolKind Kind { get; }

        public string Name { get; }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public abstract Type Type { get; }

        public sealed override string ToString()
        {
            return SymbolMarkup.ForSymbol(this).ToString();
        }
    }
}