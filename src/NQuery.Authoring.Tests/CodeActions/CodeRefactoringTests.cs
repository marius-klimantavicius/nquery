using System.Collections.Immutable;

using NQuery.Authoring.CodeActions;

namespace NQuery.Authoring.Tests.CodeActions
{
    public abstract class CodeRefactoringTests : CodeActionTest
    {
        protected override ImmutableArray<ICodeAction> GetActions(string query)
        {
            var compilation = CompilationFactory.CreateQuery(query, out int position);
            var semanticModel = compilation.GetSemanticModel();

            var provider = CreateProvider();
            var providers = new[] { provider };
            return semanticModel.GetRefactorings(position, providers).ToImmutableArray();
        }

        protected abstract ICodeRefactoringProvider CreateProvider();
    }
}