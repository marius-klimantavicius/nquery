using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NQuery.Iterators
{
    internal sealed class SegmentIterator : Iterator
    {
        private readonly Iterator _input;
        private readonly ImmutableArray<IComparer> _comparers;
        private readonly ImmutableArray<RowBufferEntry> _partitionBy;
        private readonly ArrayRowBuffer _rowBuffer;
        private readonly CombinedRowBuffer _combinedRowBuffer;

        private object?[]? _previousPartitionBy;

        public SegmentIterator(Iterator input, IEnumerable<RowBufferEntry> partitionBy, ImmutableArray<IComparer> comparers)
        {
            _input = input;
            _comparers = comparers;
            _partitionBy = partitionBy.ToImmutableArray();
            _rowBuffer = new ArrayRowBuffer(1);
            _combinedRowBuffer = new CombinedRowBuffer(input.RowBuffer, _rowBuffer);
        }

        public override RowBuffer RowBuffer => _combinedRowBuffer;

        public override void Open()
        {
            _input.Open();
        }

        public override void Dispose()
        {
            _input.Dispose();
        }

        private void FillPartitionByExpressions(object?[] target)
        {
            for (var i = 0; i < _partitionBy.Length; i++)
                target[i] = _partitionBy[i].GetValue();
        }

        [MemberNotNull(nameof(_previousPartitionBy))]
        private bool CheckIfCurrentRowIsInSamePartition()
        {
            if (_previousPartitionBy == null)
            {
                _previousPartitionBy = new object?[_partitionBy.Length];
                return false;
            }

            for (var i = 0; i < _partitionBy.Length; i++)
            {
                var valueOfLastRow = _previousPartitionBy[i];
                var valueOfThisRow = _partitionBy[i].GetValue();
                var comparer = _comparers[i];
                var equalsPreviousRow = comparer.Compare(valueOfLastRow, valueOfThisRow) == 0;

                if (!equalsPreviousRow)
                    return false;
            }

            return true;
        }

        private int _rowNumber;

        public override bool Read()
        {
            if (!_input.Read())
                return false;

            if (!CheckIfCurrentRowIsInSamePartition())
                _rowNumber = 0;

            _rowBuffer.Array[0] = ++_rowNumber;

            FillPartitionByExpressions(_previousPartitionBy);

            return true;
        }
    }
}