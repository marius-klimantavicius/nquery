using System.Diagnostics.CodeAnalysis;
using NQuery.Binding;

namespace NQuery.Symbols
{
    public abstract class ColumnInstanceSymbol : Symbol
    {
        private protected ColumnInstanceSymbol(string name)
            : base(name)
        {
        }

        internal abstract ValueSlot ValueSlot { get; }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public sealed override Type Type => ValueSlot.Type;
    }
}