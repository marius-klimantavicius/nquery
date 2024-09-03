using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NQuery.Binding
{
    internal sealed class BinaryOperatorSignature : Signature
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _returnType;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _leftParameterType;
        
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _rightParameterType;

        public BinaryOperatorSignature(BinaryOperatorKind kind,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type returnType,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type leftParameterType, 
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type rightParameterType, 
            MethodInfo? methodInfo = null)
        {
            Kind = kind;
            _returnType = returnType;
            _leftParameterType = leftParameterType;
            _rightParameterType = rightParameterType;
            MethodInfo = methodInfo;
        }

        [UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "TODO")] // TODO: is this AOT safe?
        public BinaryOperatorSignature(BinaryOperatorKind kind, MethodInfo methodInfo)
            : this(kind, methodInfo.ReturnType, methodInfo.GetParameters()[0].ParameterType, methodInfo.GetParameters()[1].ParameterType, methodInfo)
        {
        }

        public BinaryOperatorSignature(BinaryOperatorKind kind,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type type)
            : this(kind, type, type)
        {
        }

        public BinaryOperatorSignature(BinaryOperatorKind kind,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type returnType,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type parameterType)
            : this(kind, returnType, parameterType, parameterType)
        {
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type ReturnType => _returnType;

        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type GetParameterType(int index)
        {
            return index == 0 ? _leftParameterType : _rightParameterType;
        }

        public override int ParameterCount => 2;

        public BinaryOperatorKind Kind { get; }

        public MethodInfo? MethodInfo { get; }

        public override string ToString()
        {
            return $"{Kind}({_leftParameterType.ToDisplayName()}, {_rightParameterType.ToDisplayName()}) AS {_returnType.ToDisplayName()}";
        }
    }
}