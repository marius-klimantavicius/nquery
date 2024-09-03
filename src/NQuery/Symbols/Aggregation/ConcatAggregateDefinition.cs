using System.Globalization;
using System.Text;

namespace NQuery.Symbols.Aggregation
{
    public sealed class ConcatAggregateDefinition : AggregateDefinition
    {
        public override string Name => @"CONCAT";

        public override IAggregatable CreateAggregatable(Type argumentType)
        {
            return new ConcatAggregatable();
        }

        private sealed class ConcatAggregatable : IAggregatable
        {
            public IAggregator CreateAggregator()
            {
                return new ConcatAggregator();
            }

            public Type ReturnType => typeof(string);
        }

        private sealed class ConcatAggregator : IAggregator
        {
            private readonly SortedSet<string> _valueList = new SortedSet<string>();

            public void Initialize()
            {
                _valueList.Clear();
            }

            public void Accumulate(object? value)
            {
                if (value is null)
                    return;

                var strValue = Convert.ToString(value, CultureInfo.InvariantCulture);

                if (strValue is null)
                    return;

                strValue = strValue.Trim();

                _valueList.Add(strValue);
            }

            public object GetResult()
            {
                var sb = new StringBuilder(_valueList.Count * 8);

                foreach (var value in _valueList)
                {
                    if (sb.Length > 0)
                        sb.Append(@", ");

                    sb.Append(value);
                }

                return sb.ToString();
            }
        }
    }
}