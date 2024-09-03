using System.Collections;
using System.Diagnostics.CodeAnalysis;
using NQuery.Hosting;

namespace NQuery.Symbols
{
    internal sealed class EnumerableTableDefinition : TableDefinition
    {
        private readonly IEnumerable _source;

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        private readonly Type _rowType;
        
        private readonly IPropertyProvider _propertyProvider;

        public EnumerableTableDefinition(string name, IEnumerable source,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
            Type rowType, IPropertyProvider propertyProvider)
        {
            Name = name;
            _source = source;
            _rowType = rowType;
            _propertyProvider = propertyProvider;
        }

        protected override IEnumerable<ColumnDefinition> GetColumns()
        {
            return _propertyProvider.GetProperties(_rowType)
                                    .Select(p => new PropertyColumnDefinition(_rowType, p));
        }

        public override IEnumerable GetRows()
        {
            return _source;
        }

        public override string Name { get; }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
        public override Type RowType => _rowType;
    }
}