using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 重复、撤销删除多个连续的表格列的操作
    /// </summary>
    public class XTextUndoDeleteColumns : XTextUndoBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="table">表格对象</param>
        /// <param name="startColumnIndex">开始删除的表格列的序号</param>
        /// <param name="colCount">删除的表格列个数</param>
        public XTextUndoDeleteColumns( DomTableElement table , int startColumnIndex , int colCount , bool keepTableWidth  )
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _TableElement = table;
            _StartColumnIndex = startColumnIndex;
            _ColCount = colCount;
            _Columns = _TableElement.Columns.GetRange(startColumnIndex, colCount);
            _KeepTableWidth = keepTableWidth;
        }
        private bool _KeepTableWidth = true;
        private DomTableElement _TableElement = null;
        private int _StartColumnIndex = 0;
        private int _ColCount = 0;
        private DomElementList _Columns = null;
        /// <summary>
        /// 操作而导致的被删除的单元格
        /// </summary>
        public DomElementList RemovedCells = new DomElementList();
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
        /// 执行撤销操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _TableElement.EditorInsertColumns(_StartColumnIndex, _Columns, RemovedCells , false , OldColSpan , _KeepTableWidth , OldColumnWidths  );
        }

        /// <summary>
        /// 执行重复操作
        /// </summary>
        /// <param name="args">参数</param>
        public override void Redo( DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            _TableElement.EditorDeleteColumns(_StartColumnIndex, _Columns.Count , false , NewColSpan , _KeepTableWidth , NewColumnWidths );
        }
    }
}
