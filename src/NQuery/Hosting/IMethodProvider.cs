using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Hosting
{
    public interface IMethodProvider
    {
        IEnumerable<MethodSymbol> GetMethods([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] Type type);
    }
}