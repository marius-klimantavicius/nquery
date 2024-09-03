using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Binding
{
    internal sealed class MethodSymbolSignature : Signature
    {
        public MethodSymbolSignature(MethodSymbol symbol)
        {
            Symbol = symbol;
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type ReturnType => Symbol.Type;

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type GetParameterType(int index)
        {
            return Symbol.Parameters[index].Type;
        }

        public override int ParameterCount => Symbol.Parameters.Length;

        public MethodSymbol Symbol { get; }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}