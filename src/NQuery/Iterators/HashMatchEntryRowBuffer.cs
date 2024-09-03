namespace NQuery.Iterators
{
    internal sealed class HashMatchEntryRowBuffer : RowBuffer
    {
        public HashMatchEntry? Entry { get; set; }

        private object?[] RowValues => Entry?.RowValues ?? Array.Empty<object?>();

        public override int Count => RowValues.Length;

        public override object? this[int index] => RowValues[index];

        public override void CopyTo(object?[] array, int destinationIndex)
        {
            var source = RowValues;
            Array.Copy(source, 0, array, destinationIndex, source.Length);
        }
    }
}