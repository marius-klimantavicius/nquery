﻿using System;
using System.Collections.Generic;

using NQuery.Text;

namespace NQuery.Authoring.Wpf
{
    public sealed partial class DiagnosticGrid
    {
        public DiagnosticGrid()
        {
            InitializeComponent();
        }

        public void UpdateGrid(IEnumerable<Diagnostic> diagnostics, TextBuffer textBuffer)
        {
            DataContext = diagnostics == null || textBuffer == null
                              ? null
                              : new DiagnosticsViewModel(diagnostics, textBuffer);
        }

        public Diagnostic SelectedDiagnostic
        {
            get
            {
                var viewModel = DiagnosticDataGrid.SelectedItem as DiagnosticViewModel;
                return viewModel == null ? null : viewModel.Diagnostic;
            }
        }
    }
}
