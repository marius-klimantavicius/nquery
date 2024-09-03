namespace NQuery.Iterators
{
    internal sealed class EmptyRowBuffer : RowBuffer
    {
        public override int Count => 0;

        public override object this[int index] => throw new ArgumentOutOfRangeException();

        public override void CopyTo(object?[] array, int destinationIndex)
        {
        }
    }
}