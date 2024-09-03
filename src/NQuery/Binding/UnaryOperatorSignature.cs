using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NQuery.Binding
{
    internal sealed class UnaryOperatorSignature : Signature
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _returnType;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _argumentType;

        private UnaryOperatorSignature(UnaryOperatorKind kind, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type returnType, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type argumentType, 
            MethodInfo? methodInfo)
        {
            Kind = kind;
            _returnType = returnType;
            _argumentType = argumentType;
            MethodInfo = methodInfo;
        }

        [UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "TODO")] // TODO: is this AOT safe?
        public UnaryOperatorSignature(UnaryOperatorKind kind, MethodInfo methodInfo)
            : this(kind, methodInfo.ReturnType, methodInfo.GetParameters()[0].ParameterType, methodInfo)
        {
        }

        public UnaryOperatorSignature(UnaryOperatorKind kind, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type returnType, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type argumentType)
            : this(kind, returnType, argumentType, null)
        {
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type ReturnType => _returnType;

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type GetParameterType(int index)
        {
            return _argumentType;
        }

        public override int ParameterCount => 1;

        public UnaryOperatorKind Kind { get; }

        public MethodInfo? MethodInfo { get; }

        public override string ToString()
        {
            return $"{Kind}({_argumentType.ToDisplayName()}) AS {_returnType.ToDisplayName()}";
        }
    }
}