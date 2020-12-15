using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 重复、撤销插入表格列的操作
    /// </summary>
    public class XTextUndoInsertColumns : XTextUndoBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="tableElement">表格对象</param>
        /// <param name="columnElements">插入的表格列对象列表</param>
        /// <param name="columnIndex">表格列序号</param>
        public XTextUndoInsertColumns(
            DomTableElement tableElement,
            DomElementList columnElements ,
            int columnIndex ,
            bool keepTableWidth )
        {
            _TableElement = tableElement;
            _ColumnElements = columnElements;
            _ColumnIndex = columnIndex;
            _KeepTableWidth = keepTableWidth;
        }

        private bool _KeepTableWidth = true;
        private DomElementList _ColumnElements = null;

        private DomTableElement _TableElement = null;
        private int _ColumnIndex = 0;

        /// <summary>
        /// 操作前的单元格跨列数
        /// </summary>
        public Dictionary<DomTableCellElement, int> OldColSpan
            = new Dictionary<DomTableCellElement, int>();
        /// <summary>
        /// 操作后的单元格跨列数
        /// </summary>
        public Dictionary<DomTableCellElement, int> NewColSpan
            = new Dictionary<DomTableCellElement, int>();

        /// <summary>
        /// 旧的表格列宽度
        /// </summary>
        public Dictionary<DomTableColumnElement, float> OldColumnWidths
            = new Dictionary<DomTableColumnElement, float>();
        /// <summary>
        /// 新的表格列宽度
        /// </summary>
        public Dictionary<DomTableColumnElement, float> NewColumnWidths
            = new Dictionary<DomTableColumnElement, float>();

        internal Dictionary<DomTableColumnElement, float> GetColumnWidths(DomTableElement table)
        {
            Dictionary<DomTableColumnElement, float> result = new Dictionary<DomTableColumnElement, float>();
            foreach (DomTableColumnElement col in table.Columns)
            {
                result[col] = col.Width;
            }
            return result;
        }

        /// <summary>
        /// 撤销操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _TableElement.EditorDeleteColumns( _ColumnIndex , _ColumnElements.Count , false , OldColSpan , _KeepTableWidth , OldColumnWidths );
        }

        /// <summary>
        /// 重复操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _TableElement.EditorInsertColumns(_ColumnIndex, _ColumnElements, null, false , NewColSpan , _KeepTableWidth , NewColumnWidths );
        }
    }
}
