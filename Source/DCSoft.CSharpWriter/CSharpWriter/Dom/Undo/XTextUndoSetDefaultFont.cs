/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Controls ;
namespace DCSoft.CSharpWriter.Dom.Undo
{
    public class XTextUndoSetDefaultFont : XTextUndoBase
    {
        public XTextUndoSetDefaultFont( CSWriterControl ctl , XFontValue of, Color oc, XFontValue nf, Color nc)
        {
            _Control = ctl;
            _OldFont = of;
            _OldColor = oc;
            _NewFont = nf;
            _NewColor = nc;
        }
        private CSWriterControl _Control = null;
        private XFontValue _OldFont = null;
        private Color _OldColor = Color.Empty;
        private XFontValue _NewFont = null;
        private Color _NewColor = Color.Empty;

        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Control.EditorSetDefaultFont(_OldFont, _OldColor, true);
        }

        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Control.EditorSetDefaultFont(_NewFont, _NewColor, true);
        }
    }
}
