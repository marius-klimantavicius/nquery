namespace NQuery.Iterators
{
    internal sealed class ConstantIterator : Iterator
    {
        private readonly EmptyRowBuffer _rowBuffer = new EmptyRowBuffer();
        private bool _isEof;

        public override RowBuffer RowBuffer => _rowBuffer;

        public override void Open()
        {
            _isEof = false;
        }

        public override void Dispose()
        {
        }

        public override bool Read()
        {
            if (_isEof)
                return false;

            _isEof = true;
            return true;
        }
    }
}