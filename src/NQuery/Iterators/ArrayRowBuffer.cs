namespace NQuery.Iterators
{
    internal sealed class ArrayRowBuffer : RowBuffer
    {
        public ArrayRowBuffer(int size)
        {
            Array = new object[size];
        }

        public object?[] Array { get; }

        public override int Count => Array.Length;

        public override object? this[int index] => Array[index];

        public override void CopyTo(object?[] array, int destinationIndex)
        {
            System.Array.Copy(Array, 0, array, destinationIndex, Array.Length);
        }
    }
}