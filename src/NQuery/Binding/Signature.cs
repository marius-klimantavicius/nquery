using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal abstract class Signature
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public abstract Type ReturnType { get; }
        
        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public abstract Type GetParameterType(int index);

        public abstract int ParameterCount { get; }

        public IEnumerable<Type> GetParameterTypes()
        {
            for (var i = 0; i < ParameterCount; i++)
                yield return GetParameterType(i);
        }
    }
}