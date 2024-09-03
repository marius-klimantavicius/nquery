namespace NQuery.Symbols.Aggregation;

public sealed class RowNumberAggregateDefinition : AggregateDefinition
{
    public override string Name => "ROW_NUMBER";

    public override IAggregatable CreateAggregatable(Type argumentType)
    {
        return new RowNumberAggregatable();
    }

    private sealed class RowNumberAggregatable : IAggregatable
    {
        public IAggregator CreateAggregator()
        {
            return new RowNumberAggregator();
        }

        public Type ReturnType => typeof(int);
    }

    private sealed class RowNumberAggregator : IAggregator
    {
        private int _count;

        public void Initialize()
        {
            _count = 0;
        }

        public void Accumulate(object? value)
        {
            _count++;
        }

        public object GetResult()
        {
            return _count;
        }
    }
}