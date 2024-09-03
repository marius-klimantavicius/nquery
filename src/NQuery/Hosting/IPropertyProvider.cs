using System.Diagnostics.CodeAnalysis;
using NQuery.Symbols;

namespace NQuery.Hosting
{
    public interface IPropertyProvider
    {
        IEnumerable<PropertySymbol> GetProperties(
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
            Type type);
    }
}