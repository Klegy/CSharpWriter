using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 重复、撤销插入多个表格行操作的对象
    /// </summary>
    public class XTextUndoInsertRows : XTextUndoBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">表格行对象</param>
        /// <param name="rowIndex">行号</param>
        public XTextUndoInsertRows(DomTableElement table, DomElementList newRows , int rowIndex)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _Table = table;
            if (newRows == null || newRows.Count == 0 )
            {
                throw new ArgumentNullException("newRows");
            }
            _NewRows = newRows;
            _RowIndex = rowIndex;
        }

        private DomTableElement _Table = null;
        private DomElementList _NewRows = null;
        
        private int _RowIndex = 0;

        /// <summary>
        /// 操作前的单元格跨行数
        /// </summary>
        public Dictionary<DomTableCellElement, int> OldRowSpan 
            = new Dictionary<DomTableCellElement, int>();
        /// <summary>
        /// 操作后的单元格跨行数
        /// </summary>
        public Dictionary<DomTableCellElement, int> NewRowSpan
            = new Dictionary<DomTableCellElement, int>();
        

        /// <summary>
        /// 撤销执行操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Table.EditorDeleteRows(_RowIndex , _NewRows.Count , false , OldRowSpan );
        }

        /// <summary>
        /// 重复执行操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Table.EditorInsertRows(_RowIndex, _NewRows, false , NewRowSpan);
        }
    }
}