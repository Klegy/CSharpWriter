using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Undo ;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    internal class XTextUndoTableColumnWidth : XTextUndoBase
    {
        public XTextUndoTableColumnWidth(
            DomTableColumnElement col,
            float oldWidth,
            float newWidth,
            bool setNextColumnWidth)
        {
            _ColumnElement = col;
            _OldWidth = oldWidth;
            _NewWidth = newWidth;
            _SetNextColumnWidth = setNextColumnWidth;
        }

        private DomTableColumnElement _ColumnElement = null;
        private float _OldWidth = 0f;
        private float _NewWidth = 0f;
        private bool _SetNextColumnWidth = true;

        public override void Undo( XUndoEventArgs args)
        {
            _ColumnElement.EditorSetWidth(_OldWidth, false, _SetNextColumnWidth);
        }

        public override void Redo( XUndoEventArgs args)
        {
            _ColumnElement.EditorSetWidth(_NewWidth, false, _SetNextColumnWidth);
        }
    }
}
