namespace NQuery.Binding
{
    internal sealed class QueryBinder : Binder
    {
        public QueryBinder(SharedBinderState sharedBinderState, Binder parent)
            : base(sharedBinderState, parent)
        {
            QueryState = new BoundQueryState(parent.QueryState);
        }

        public override BoundQueryState QueryState { get; }

        protected override bool InWhereClause => false;

        protected override bool InOnClause => false;

        protected override bool InGroupByClause => false;

        protected override bool InAggregateArgument => false;
    }
}