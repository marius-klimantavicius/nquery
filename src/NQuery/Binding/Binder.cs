using NQuery.Syntax;

namespace NQuery.Binding
{
    internal abstract partial class Binder
    {
        private readonly SharedBinderState _sharedBinderState;

        protected Binder(SharedBinderState sharedBinderState, Binder? parent)
        {
            Parent = parent;
            _sharedBinderState = sharedBinderState;
        }

        public Binder? Parent { get; }

        private List<Diagnostic> Diagnostics => _sharedBinderState.Diagnostics;

        private ValueSlotFactory ValueSlotFactory => _sharedBinderState.ValueSlotFactory;

        protected virtual bool InWhereClause => Parent is not null && Parent.InWhereClause;

        protected virtual bool InOnClause => Parent is not null && Parent.InOnClause;

        protected virtual bool InGroupByClause => Parent is not null && Parent.InGroupByClause;

        protected virtual bool InAggregateArgument => Parent is not null && Parent.InAggregateArgument;

        private Binder CreateLocalBinder(IEnumerable<Symbol> symbols)
        {
            return new LocalBinder(_sharedBinderState, this, symbols);
        }

        private Binder CreateLocalBinder(params Symbol[] symbols)
        {
            return CreateLocalBinder(symbols.AsEnumerable());
        }

        private Binder CreateJoinConditionBinder(BoundRelation left, BoundRelation right)
        {
            var leftTables = left.GetDeclaredTableInstances();
            var rightTables = right.GetDeclaredTableInstances();
            var tables = leftTables.Concat(rightTables);
            return new JoinConditionBinder(_sharedBinderState, this, tables);
        }

        private Binder CreateQueryBinder()
        {
            return new QueryBinder(_sharedBinderState, this);
        }

        private Binder CreateGroupByClauseBinder()
        {
            return new GroupByClauseBinder(_sharedBinderState, this);
        }

        private Binder CreateWhereClauseBinder()
        {
            return new WhereClauseBinder(_sharedBinderState, this);
        }

        private Binder CreateAggregateArgumentBinder()
        {
            return new AggregateArgumentBinder(_sharedBinderState, this);
        }

        public static BindingResult Bind(CompilationUnitSyntax compilationUnit, DataContext dataContext)
        {
            var sharedBinderState = new SharedBinderState();
            var binder = new GlobalBinder(sharedBinderState, dataContext);
            var boundRoot = binder.BindRoot(compilationUnit.Root);
            return new BindingResult(compilationUnit, boundRoot, sharedBinderState.BoundNodeFromSyntaxNode, sharedBinderState.BinderFromBoundNode, sharedBinderState.Diagnostics);
        }

        private BoundNode BindRoot(SyntaxNode? root)
        {
            return root switch
            {
                null => BindEmptyQuery(),
                QuerySyntax query => BindQuery(query),
                ExpressionSyntax expression => BindExpression(expression),
                _ => throw ExceptionBuilder.UnexpectedValue(root),
            };
        }

        private TResult Bind<TInput, TResult>(TInput node, Func<TInput, TResult> bindMethod)
            where TInput : SyntaxNode
            where TResult : BoundNode
        {
            var boundNode = bindMethod(node);

            Bind(node, boundNode);

            return boundNode;
        }

        private void Bind<TInput, TResult>(TInput node, TResult boundNode)
            where TInput : SyntaxNode
            where TResult : BoundNode
        {
            _sharedBinderState.BoundNodeFromSyntaxNode.Add(node, boundNode);
            if (!_sharedBinderState.BinderFromBoundNode.ContainsKey(boundNode))
                _sharedBinderState.BinderFromBoundNode.Add(boundNode, this);
        }

        private T? GetBoundNode<T>(SyntaxNode node)
            where T : BoundNode
        {
            _sharedBinderState.BoundNodeFromSyntaxNode.TryGetValue(node, out var result);
            return result as T;
        }
    }
}