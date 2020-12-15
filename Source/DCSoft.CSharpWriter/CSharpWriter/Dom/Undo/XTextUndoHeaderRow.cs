using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    public class XTextUndoHeaderRow : XTextUndoBase
    {
        private DomTableElement _Table = null;

        public DomTableElement Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        private Dictionary<DomTableRowElement, bool> _OldHeaderStyles = new Dictionary<DomTableRowElement, bool>();

        public Dictionary<DomTableRowElement, bool> OldHeaderStyles
        {
            get { return _OldHeaderStyles; }
            set { _OldHeaderStyles = value; }
        }

        private Dictionary<DomTableRowElement, bool> _NewHeaderStyles = new Dictionary<DomTableRowElement, bool>();
        /// <summary>
        /// 新标题行样式
        /// </summary>
        public Dictionary<DomTableRowElement, bool> NewHeaderStyles
        {
            get { return _NewHeaderStyles; }
            set { _NewHeaderStyles = value; }
        }

        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            this.Table.EditorSetHeaderRow(this.OldHeaderStyles, false);
        }

        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            this.Table.EditorSetHeaderRow(this.NewHeaderStyles, false);
        }

    }
}
