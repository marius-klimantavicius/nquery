using System;
using System.Windows.Media;

using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

using NQuery.Language.Services;
using NQuery.Language.Wpf;

namespace NQuery.Language.ActiproWpf.SymbolContent
{
    internal sealed class GlyphImageProvider : IImageSourceProvider
    {
        private readonly INQueryGlyphService _glyphService;
        private readonly NQueryGlyph _glyph;

        public GlyphImageProvider(INQueryGlyphService glyphService, NQueryGlyph glyph)
        {
            _glyphService = glyphService;
            _glyph = glyph;
        }

        public ImageSource GetImageSource()
        {
            return _glyphService.GetGlyph(_glyph);
        }
    }
}