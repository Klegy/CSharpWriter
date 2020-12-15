using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 重复、撤销删除表格行操作
    /// </summary>
    public class XTextUndoDeleteRows : XTextUndoBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="row">表格行对象</param>
        /// <param name="rowIndex">行号</param>
        public XTextUndoDeleteRows(DomTableElement table, int startRowIndex , int rowsCount )
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _Table = table;
            _StartRowIndex = startRowIndex;
            _Rows = _Table.Rows.GetRange(startRowIndex, rowsCount);
        }

        private DomTableElement _Table = null;

        private int _StartRowIndex = 0;

        private DomElementList _Rows = new DomElementList();

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
        /// 撤销执行操作,将表格行插入到表格中
        /// </summary>
        /// <param name="args">参数</param>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Table.EditorInsertRows( _StartRowIndex , _Rows , false , OldRowSpan );
        }

        /// <summary>
        /// 重复执行操作，删除表格行
        /// </summary>
        /// <param name="args">参数</param>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _Table.EditorDeleteRows( _StartRowIndex , _Rows.Count , false , NewRowSpan );
        }
    }
}
