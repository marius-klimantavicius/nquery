using System.Diagnostics.CodeAnalysis;

namespace NQuery.Symbols.Aggregation
{
    public sealed class CountAggregateDefinition : AggregateDefinition
    {
        public override string Name => @"COUNT";

        public override IAggregatable CreateAggregatable([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type argumentType)
        {
            return new CountAggregatable();
        }

        private sealed class CountAggregatable : IAggregatable
        {
            public IAggregator CreateAggregator()
            {
                return new CountAggregator();
            }

            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            public Type ReturnType => typeof(int);
        }

        private sealed class CountAggregator : IAggregator
        {
            private int _count;

            public void Initialize()
            {
                _count = 0;
            }

            public void Accumulate(object? value)
            {
                if (value is not null)
                    _count++;
            }

            public object GetResult()
            {
                return _count;
            }
        }
    }
}