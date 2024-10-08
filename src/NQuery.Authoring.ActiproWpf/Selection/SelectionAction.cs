﻿using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

using NQuery.Authoring.Selection;
using NQuery.Text;

namespace NQuery.Authoring.ActiproWpf.Selection
{
    public abstract class SelectionAction : EditActionBase
    {
        protected SelectionAction(string text)
            : base(text)
        {
        }

        private sealed class SelectionHandler
        {
            private readonly IEditorView _editorView;
            private readonly Stack<TextSpan> _selectionStack = new();

            public SelectionHandler(IEditorView editorView)
            {
                _editorView = editorView;
                SubscribeToSelectionChanged();
            }

            private void SubscribeToSelectionChanged()
            {
                _editorView.SyntaxEditor.ViewSelectionChanged += SyntaxEditorOnViewSelectionChanged;
            }

            private void UnsubscribeToSelectionChanged()
            {
                _editorView.SyntaxEditor.ViewSelectionChanged -= SyntaxEditorOnViewSelectionChanged;
            }

            private void SyntaxEditorOnViewSelectionChanged(object sender, EditorViewSelectionEventArgs e)
            {
                if (e.View == _editorView)
                    _selectionStack.Clear();
            }

            public async Task ExtendSelectionAsync()
            {
                var snapshot = _editorView.SyntaxEditor.GetDocumentView();
                var syntaxTree = await snapshot.Document.GetSyntaxTreeAsync();
                var currentSelection = snapshot.Selection;
                var extendedSelection = syntaxTree.ExtendSelection(currentSelection);

                if (currentSelection == extendedSelection)
                    return;

                _selectionStack.Push(currentSelection);
                Select(extendedSelection, snapshot.Text);
            }

            public void ShrinkSelection()
            {
                var snapshot = _editorView.SyntaxEditor.GetDocumentView();

                if (_selectionStack.Count == 0)
                    return;

                var newSelection = _selectionStack.Pop();
                Select(newSelection, snapshot.Text);
            }

            private void Select(TextSpan selection, SourceText text)
            {
                var snapshotRange = text.ToSnapshotRange(selection);

                UnsubscribeToSelectionChanged();
                _editorView.Selection.SelectRange(snapshotRange);
                SubscribeToSelectionChanged();
            }
        }

        private static SelectionHandler GetSelectionHandler(ITextView textView)
        {
            if (textView is not IEditorView editorView)
                return null;

            var key = typeof(SelectionHandler);
            if (!textView.Properties.TryGetValue(key, out SelectionHandler value))
            {
                value = new SelectionHandler(editorView);
                textView.Properties.Add(key, value);
            }

            return value;
        }

        protected static void ExtendSelectionAsync(ITextView textView)
        {
            var selectionHandler = GetSelectionHandler(textView);
            selectionHandler?.ExtendSelectionAsync();
        }

        protected static void ShrinkSelection(ITextView textView)
        {
            var selectionHandler = GetSelectionHandler(textView);
            selectionHandler?.ShrinkSelection();
        }
    }
}