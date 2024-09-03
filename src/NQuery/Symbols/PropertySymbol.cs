using System.Linq.Expressions;

namespace NQuery.Symbols
{
    public abstract class PropertySymbol : Symbol
    {
        protected PropertySymbol(string name, Type type)
            : base(name)
        {
            Type = type;
        }

        public abstract Expression CreateInvocation(Expression instance);

        public override SymbolKind Kind => SymbolKind.Property;

        public override Type Type { get; }
    }
}