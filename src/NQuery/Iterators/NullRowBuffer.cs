namespace NQuery.Iterators
{
    internal sealed class NullRowBuffer : RowBuffer
    {
        private readonly int _count;

        public NullRowBuffer(int count)
        {
            _count = count;
        }

        public override int Count => _count;

        public override object? this[int index] => null;

        public override void CopyTo(object?[] array, int destinationIndex)
        {
            Array.Clear(array, destinationIndex, _count);
        }
    }
}