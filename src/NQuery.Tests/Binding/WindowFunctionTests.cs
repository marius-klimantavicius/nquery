using System.Collections.Immutable;

namespace NQuery.Tests.Binding
{
    public class WindowFunctionTests
    {
        [Fact]
        public void WindowFunction_ParseOverClause()
        {
            var syntaxTree = SyntaxTree.ParseQuery("SELECT t.Id, t.Name, ROW_NUMBER() OVER (PARTITION BY t.Name ORDER BY t.Id) FROM Table t");
            var compilation = Compilation.Empty.WithSyntaxTree(syntaxTree).WithIdNameTable();
            var semanticModel = compilation.GetSemanticModel();
            var diagnostics = semanticModel.GetDiagnostics().ToImmutableArray();
            
            Assert.Empty(diagnostics);
        }
    }
}