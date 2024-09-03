using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace NQuery.Symbols
{
    public abstract class PropertySymbol : Symbol
    {
        protected PropertySymbol(string name, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type)
            : base(name)
        {
            Type = type;
        }

        public abstract Expression CreateInvocation(Expression instance);

        public override SymbolKind Kind => SymbolKind.Property;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type Type { get; }
    }
}