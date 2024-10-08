using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;

namespace NQuery.Authoring.VSEditorWpf.Squiggles
{
    internal sealed class NQuerySyntaxErrorTagger : NQueryErrorTagger
    {
        private readonly Workspace _workspace;

        public NQuerySyntaxErrorTagger(Workspace workspace)
            : base(PredefinedErrorTypeNames.SyntaxError)
        {
            _workspace = workspace;
            _workspace.CurrentDocumentChanged += WorkspaceOnCurrentDocumentChanged;
            InvalidateTagsAsync();
        }

        private void WorkspaceOnCurrentDocumentChanged(object sender, EventArgs e)
        {
            InvalidateTagsAsync();
        }

        protected override async Task<(ITextSnapshot Snapshot, IEnumerable<Diagnostic> RawTags)> GetRawTagsAsync()
        {
            var document = _workspace.CurrentDocument;
            var syntaxTree = await document.GetSyntaxTreeAsync();
            var snapshot = document.GetTextSnapshot();
            return (snapshot, syntaxTree.GetDiagnostics());
        }
    }
}