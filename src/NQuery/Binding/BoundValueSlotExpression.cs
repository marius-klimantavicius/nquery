using System.Diagnostics.CodeAnalysis;

namespace NQuery.Binding
{
    internal sealed class BoundValueSlotExpression : BoundExpression
    {
        public BoundValueSlotExpression(ValueSlot valueSlot)
        {
            ValueSlot = valueSlot;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ValueSlotExpression;


        public override Type Type
        {
            [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] get => ValueSlot.Type;
        }

        public ValueSlot ValueSlot { get; }

        public BoundValueSlotExpression Update(ValueSlot valueSlot)
        {
            if (valueSlot == ValueSlot)
                return this;

            return new BoundValueSlotExpression(valueSlot);
        }

        public override string? ToString()
        {
            return ValueSlot.Name;
        }
    }
}