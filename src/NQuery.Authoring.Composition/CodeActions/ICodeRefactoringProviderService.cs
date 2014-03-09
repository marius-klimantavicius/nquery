﻿using System;
using System.Collections.Generic;

using NQuery.Authoring.CodeActions;

namespace NQuery.Authoring.Composition.CodeActions
{
    public interface ICodeRefactoringProviderService
    {
        IReadOnlyCollection<ICodeRefactoringProvider> Providers { get; }
    }
}