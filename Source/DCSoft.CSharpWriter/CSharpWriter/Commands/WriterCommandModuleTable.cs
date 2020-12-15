using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Controls;
using System.Drawing;
using DCSoft.Drawing;
using DCSoft.Printing;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 操作表格的命令功能模块对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [WriterCommandDescription(StandardCommandNames.ModuleTable)]
    internal class WriterCommandModuleTable : WriterCommandModule
    {
        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_InsertTable,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandInsertTable.bmp")]
        protected void Table_InsertTable(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.DocumentControler != null)
                {
                    args.Enabled = args.DocumentControler.CanInsertElementAtCurrentPosition(
                        typeof(DomTableElement) );
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomTableElement table = null;
                if (args.Parameter is DomTableElement)
                {
                    table = (DomTableElement)args.Parameter;
                }
                else
                {
                    XTextTableElementProperties info = args.Parameter as XTextTableElementProperties;
                    if (info == null)
                    {
                        info = (XTextTableElementProperties)args.Host.CreateProperties(typeof(DomTableElement));
                    }
                    if (args.ShowUI)
                    {
                        if (info.PromptNewElement(args) == false)
                        {
                            // 用户取消操作
                            return;
                        }
                    }
                    if (info.ColumnsCount <= 0 || info.RowsCount <= 0)
                    {
                        // 新增的表格的行数和列数不能为0
                        return;
                    }
                    if (info.ColumnWidth == 0)
                    {
                        // 根据当前位置设置表格列的宽度
                        if (args.Document.CurrentElement != null)
                        {
                            DomContentElement ce = args.Document.CurrentElement.ContentElement;
                            info.ColumnWidth = (ce.ClientWidth - args.Document.PixelToDocumentUnit(2)) / info.ColumnsCount;
                        }
                    }
                    table = (DomTableElement)info.CreateElement(
                        args.Document);
                }
                if (table != null)
                {
                    using (System.Drawing.Graphics g = args.Document.CreateGraphics())
                    {
                        DocumentPaintEventArgs args2 = new DocumentPaintEventArgs(g, Rectangle.Empty);
                        args2.Document = args.Document;
                        args2.Element = table;
                        args2.Render = args.Document.Render;
                        table.RefreshSize(args2);

                        //args.Document.Render.RefreshSize(table, g);
                    }
                    args.Document.BeginLogUndo();
                    args.Document.InsertElement(table);
                    args.Document.EndLogUndo();
                    args.RefreshLevel = UIStateRefreshLevel.All;
                    args.Result = table;
                }
            }
        }

        /// <summary>
        /// 删除整个表格
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.Table_DeleteTable )]
        protected void Table_DeleteTable(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                DomTableCellElement cell = GetCurrentCell(args.Document);
                if (cell != null)
                {
                    args.Enabled = args.DocumentControler.CanDelete(cell);
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomTableCellElement cell = GetCurrentCell(args.Document);
                DomTableElement table = cell.OwnerTable;
                table.EditorDelete(true);
                args.RefreshLevel = UIStateRefreshLevel.All;
                args.Result = true;
            }
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.Table_MergeCell ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandMergeCell.bmp")]
        protected void Table_MegeCell(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    DomDocumentContentElement dce = args.Document.CurrentContentElement;
                    if (dce.Selection.Mode == ContentRangeMode.Cell )
                    {
                        // 仅仅处于纯粹的选择单元格的模式下本动作才有效
                        DomTableCellElement cell = (DomTableCellElement)dce.Selection.Cells.FirstElement ;
                        DomTableElement table = cell.OwnerTable;
                        if (cell.RowIndex < table.Rows.Count - 1
                            || cell.ColIndex < table.Columns.Count - 1)
                        {
                            args.Enabled = args.DocumentControler.CanModify( cell );
                        }
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomDocumentContentElement dce = args.Document.CurrentContentElement;
                if (dce.Selection.Mode == ContentRangeMode.Cell )
                {
                    // 获得选择区域的第一个单元格作为要处理的单元格
                    DomTableCellElement firstCell = (DomTableCellElement)dce.Selection.Cells.FirstElement ;
                    // 获得选择区域中最后一个单元格作为合并的截止单元格
                    DomTableCellElement lastCell = (DomTableCellElement)dce.Selection.Cells.LastElement;
                    // 计算新跨行数和跨列数
                    int rowSpan = lastCell.RowIndex + lastCell.RowSpan - firstCell.RowIndex;
                    int colSpan = lastCell.ColIndex + lastCell.ColSpan - firstCell.ColIndex;
                    // 执行合并单元格的操作
                    firstCell.EditorSetCellSpan(rowSpan, colSpan, true , null );
                    args.RefreshLevel = UIStateRefreshLevel.All;
                    args.Result = firstCell;
                }
            }
        }

        /// <summary>
        /// 拆分单元格
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_SplitCell,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandSplitCell.bmp")]
        protected void Table_SplitCell(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    DomTableCellElement cell = GetCurrentCell(args.Document);
                    if (cell != null)
                    {
                        // 只有当前表格的跨行数或者跨列数大于1则动作才有效
                        if (cell.RowSpan > 1 || cell.ColSpan > 1)
                        {
                            args.Enabled = args.DocumentControler.CanModify(cell);
                        }
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomTableCellElement cell = GetCurrentCell(args.Document);
                if (cell != null)
                {
                    if (cell.RowSpan > 1 || cell.ColSpan > 1)
                    {
                        cell.EditorSetCellSpan(1, 1, true , null );
                        args.Result = cell;
                        args.RefreshLevel = UIStateRefreshLevel.All;
                    }
                }
            }
        }

        /// <summary>
        /// 在当前行上面插入表格行
        /// </summary>
        /// <param name="sender">事件参数</param>
        /// <param name="args">事件参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_InsertRowUp,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandInsertRowUp.bmp")]
        protected void Table_InsertRowUp(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                DomTableRowElement row = GetCurrentRow(args.Document);
                if (row != null)
                {
                    args.Enabled = args.DocumentControler.CanInsert(
                        row.OwnerTable,
                        0, 
                        typeof(DomTableRowElement));
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                InsertRow(args , true );
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 在当前行上面插入表格行
        /// </summary>
        /// <param name="sender">事件参数</param>
        /// <param name="args">事件参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_InsertRowDown,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandInsertRowDown.bmp")]
        protected void Table_InsertRowDown(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                DomTableRowElement row = GetCurrentRow(args.Document);
                if (row != null)
                {
                    args.Enabled = args.DocumentControler.CanInsert(row.OwnerTable, 0, typeof(DomTableRowElement));
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                InsertRow(args,  false );
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }
        /// <summary>
        /// 删除当前表格行
        /// </summary>
        /// <param name="sender">事件参数</param>
        /// <param name="args">事件参数</param>
        [WriterCommandDescription ( StandardCommandNames.Table_DeleteRow ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDeleteRow.bmp")]
        protected void Table_DeleteRow(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    DomElementList list = GetRowsOrColumns(args.Document, true);
                    if (list != null && list.Count > 0)
                    {
                        args.Enabled = args.DocumentControler.CanDelete(list[0]);
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomElementList rows = GetRowsOrColumns(args.Document, true);
                if (rows != null && rows.Count > 0)
                {
                    // 删除表格行
                    DomTableRowElement row = (DomTableRowElement)rows[0];
                    DomTableElement table = row.OwnerTable;
                    if (rows.Count == table.Rows.Count)
                    {
                        // 删除整个表格
                        args.Result = table.EditorDelete( true );
                    }
                    else
                    {
                        // 删除部分表格行
                        args.Result = table.EditorDeleteRows(row.Index, rows.Count, true, null);
                    }
                    args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }
        }

        /// <summary>
        /// 在左边插入表格列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription( StandardCommandNames.Table_InsertColumnLeft ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandInsertColumnLeft.bmp")]
        protected void Table_InsertColumnLeft(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                DomTableElement table = GetCurrentTable(args.Document);
                if (table != null)
                {
                    args.Enabled = args.DocumentControler.CanInsert(table, 0, typeof(DomTableColumnElement));
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = InsertCol(args.Document , true );
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 在右边插入表格列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [WriterCommandDescription(StandardCommandNames.Table_InsertColumnRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandInsertColumnRight.bmp")]
        protected void Table_InsertColumnRight(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                DomTableElement table = GetCurrentTable(args.Document);
                if (table != null)
                {
                    args.Enabled = args.DocumentControler.CanInsert(table, 0, typeof(DomTableColumnElement));
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = InsertCol(args.Document, false );
                args.RefreshLevel = UIStateRefreshLevel.All;
            }
        }

        /// <summary>
        /// 删除当前表格行
        /// </summary>
        /// <param name="sender">事件参数</param>
        /// <param name="args">事件参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_DeleteColumn,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandDeleteColumn.bmp")]
        protected void Table_DeleteColumn(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                if (args.Document != null)
                {
                    DomElementList list = GetRowsOrColumns(args.Document, false);
                    if (list != null && list.Count > 0)
                    {
                        args.Enabled = args.DocumentControler.CanDelete(list[0]);
                    }
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                DomElementList list = GetRowsOrColumns(args.Document, false);
                if (list != null && list.Count > 0)
                {
                    // 删除多个表格列
                    DomTableColumnElement col = (DomTableColumnElement)list[0];
                    DomTableElement table = col.OwnerTable;
                    if (list.Count == table.Columns.Count)
                    {
                        // 删除整个表格
                        args.Result = table.EditorDelete(true);
                    }
                    else
                    {
                        // 删除部分表格列
                        args.Result = table.EditorDeleteColumns(
                            col.Index, 
                            list.Count,
                            true,
                            null ,
                            args.Document.Options.EditOptions.KeepTableWidthWhenInsertDeleteColumn ,
                            null );
                    }
                    args.RefreshLevel = UIStateRefreshLevel.All;
                }
            }
        }


        public static DomTableRowElement InsertRow(
            DomTableElement table,
            DomTableRowElement currentRow ,
            bool insertUp )
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (currentRow == null)
            {
                throw new ArgumentNullException("currentRow");
            }
            if (currentRow.OwnerTable != table)
            {
                throw new ArgumentOutOfRangeException("currentRow");
            }
            DomTableRowElement newRow = table.CreateRowInstance();
            newRow.SpecifyHeight = currentRow.SpecifyHeight;
            newRow.Height = currentRow.Height;
            newRow.StyleIndex = currentRow.StyleIndex;
            newRow.Parent = table;
            newRow.OwnerDocument = table.OwnerDocument;
            int iCount = 0;
            foreach (DomTableColumnElement col in table.Columns)
            {
                DomTableCellElement newCell = table.CreateCellInstance();
                newCell.Width = col.Width;
                newCell.Height = newRow.Height;
                newCell.AppendChildElement(new DomParagraphFlagElement());
                newRow.AppendChildElement(newCell);
                iCount++;
            }//foreach
            CloneCellsStyle(currentRow, newRow);
            DomDocument document = table.OwnerDocument;
            using (System.Drawing.Graphics g = document.CreateGraphics())
            {
                DocumentPaintEventArgs args = new DocumentPaintEventArgs(g, Rectangle.Empty);
                args.Document = document;
                args.Element = newRow;
                args.Render = args.Document.Render;
                newRow.RefreshSize(args);
            }
            DomElementList rows = new DomElementList();
            rows.Add(newRow);
            if (insertUp)
            {
                // 在原地插入表格行，将原先的表格行向下挤压
                table.EditorInsertRows( table.Rows.IndexOf( currentRow ), rows, true, null);
            }
            else
            {
                // 在下面插入表格行
                table.EditorInsertRows( table.Rows.IndexOf( currentRow ) + 1, rows, true, null);
            }
            return newRow;
        }

        /// <summary>
        /// 在当前单元格下面插入表格行
        /// </summary>
        private void InsertRow(WriterCommandEventArgs args, bool insertUp )
        {
            DomElementList list = GetRowsOrColumns(args.Document, true);
            if (list == null || list.Count == 0)
            {
                // 无法执行操作
                return;
            }
            DomTableRowElement myRow = null;
            if (insertUp )
            {
                myRow = (DomTableRowElement)list.FirstElement;
            }
            else
            {
                myRow = (DomTableRowElement)list.LastElement ;
            }
            DomTableElement myTable = myRow.OwnerTable;
            // 插入表格行
            args.Result = InsertRow(myTable, myRow, insertUp);
        }

        /// <summary>
        /// 将一行表格行的单元格的样式复制到另外一个表格行的单元格
        /// </summary>
        /// <param name="SourceRow">原始表格行</param>
        /// <param name="DescRow">目标表格行</param>
        public static void CloneCellsStyle(DomTableRowElement SourceRow, DomTableRowElement DescRow)
        {
            if (SourceRow == DescRow)
                return;
            if (SourceRow == null)
            {
                throw new ArgumentNullException("SourceRow");
            }
            if (DescRow == null)
            {
                throw new ArgumentNullException("DescRow");
            }
            if (SourceRow.OwnerTable != DescRow.OwnerTable)
            {
                //throw new ArgumentException("不属于同一个表格");
            }
            DescRow.StyleIndex = SourceRow.StyleIndex;
            for (int iCount = 0; iCount < SourceRow.Elements.Count; iCount++)
            {
                DomTableCellElement cell1 = (DomTableCellElement)SourceRow.Cells[iCount];
                DomTableCellElement cell2 = (DomTableCellElement)DescRow.Cells[iCount];
                CloneCellStyle(cell1, cell2);
            }
        }

        /// <summary>
        /// 将同一个表格中的一列单元格的样式复制到另外一列的所有的单元格。
        /// </summary>
        /// <param name="SourceCol">原始表格列</param>
        /// <param name="DescCol">目标表格列</param>
        public static void CloneCellsStyle(DomTableColumnElement SourceCol, DomTableColumnElement DescCol)
        {
            if (SourceCol == DescCol)
                return;
            if (SourceCol == null)
            {
                throw new System.ArgumentNullException("SourceCol");
            }
            if (DescCol == null)
            {
                throw new System.ArgumentNullException("DescCol");
            }
            if (SourceCol.Parent != DescCol.Parent)
            {
                throw new ArgumentException("不属于同一个表格");
            }
            int ColIndex = DescCol.Index;
            foreach (DomTableRowElement row in SourceCol.Parent.Elements)
            {
                DomTableCellElement cell1 = (DomTableCellElement)row.Elements[SourceCol.Index];
                DomTableCellElement cell2 = (DomTableCellElement)row.Elements[DescCol.Index];
                CloneCellStyle(cell1, cell2);
            }
        }

        /// <summary>
        /// 将一个表格单元格的样式复制到另外一个单元中
        /// </summary>
        /// <param name="SourceCell">原始单元格</param>
        /// <param name="DescCell">目标单元格</param>
        public static void CloneCellStyle(DomTableCellElement SourceCell, DomTableCellElement DescCell)
        {
            if (SourceCell == DescCell)
                return;
            if (SourceCell == null)
            {
                throw new ArgumentNullException("SourceCell");
            }
            if (DescCell == null)
            {
                throw new ArgumentNullException("DescCell");
            }
            DescCell.StyleIndex = SourceCell.StyleIndex;
            DescCell.ColSpan = SourceCell.ColSpan;
            if (SourceCell.FirstContentElement != null)
            {
                DomParagraphFlagElement flag = SourceCell.FirstContentElement.OwnerParagraphEOF;
                if (flag != null && DescCell.FirstContentElement != null)
                {
                    DomParagraphFlagElement flag2 = DescCell.FirstContentElement.OwnerParagraphEOF;
                    if (flag2 != null)
                    {
                        flag2.StyleIndex = flag.StyleIndex;
                    }
                }
            }
        }

        /// <summary>
        /// 判断能否进行删除表格行操作
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>能否进行操作</returns>
        public static bool CanDeleteRow(DomDocument doc)
        {
            DomTableRowElement row = GetCurrentRow(doc);
            if (row == null)
            {
                return false;
            }
            if (row.OwnerTable.Rows.Count == 1)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 判断能否进行插入表格列操作
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>能否执行操作</returns>
        public static bool CanInsertCol(DomDocument doc)
        {
            return GetCurrentCell(doc) != null;
        }

        /// <summary>
        /// 在当前单元格旁边插入表格列
        /// </summary>
        private DomTableColumnElement InsertCol(DomDocument doc, bool insertLeft)
        {
            DomElementList list = GetRowsOrColumns(doc, false);
            if (list == null || list.Count == 0)
            {
                // 没有选择任何表格列，无法进行操作
                return null ;
            }
            DomTableColumnElement col = null;
            if (insertLeft)
            {
                col = (DomTableColumnElement)list.FirstElement;
            }
            else
            {
                col = (DomTableColumnElement)list.LastElement;
            }
            DomTableElement myTable = col.OwnerTable;
            DomTableColumnElement newCol = myTable.CreateColumnInstance();
            newCol.Width = col.Width ;
            DomElementList newCols = new DomElementList();
            newCols.Add(newCol);

            if (insertLeft)
            {
                // 在左侧插入表格列
                myTable.EditorInsertColumns(
                    col.Index,
                    newCols,
                    null,
                    true,
                    null,
                    doc.Options.EditOptions.KeepTableWidthWhenInsertDeleteColumn,
                    null );
            }
            else
            {
                // 在右侧插入表格列
                myTable.EditorInsertColumns(
                    col.Index + 1,
                    newCols,
                    null, 
                    true, 
                    null, 
                    doc.Options.EditOptions.KeepTableWidthWhenInsertDeleteColumn,
                    null);
            }
            return newCol;
        }

        /// <summary>
        /// 判断能否进行删除表格列操作
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>能否进行操作</returns>
        public static bool CanDeleteCol(DomDocument doc)
        {
            DomTableCellElement cell = GetCurrentCell(doc);
            if (cell == null)
            {
                return false;
            }
            if (cell.OwnerTable.Columns.Count == 1)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断能否进行合并单元格操作
        /// </summary>
        /// <param name="doc">文档对象</param>
        /// <returns>能否进行操作</returns>
        public static bool CanMergeCell(DomDocument doc)
        {
            DomTableCellElement cell = GetCurrentCell(doc);
            if (cell == null)
            {
                return false;
            }
            if (cell.OwnerTable.Rows.Count == 1
                && cell.OwnerTable.Columns.Count == 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获得当前表格行对象
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>当前单元格对象,若没有则返回空引用</returns>
        public static DomTableRowElement GetCurrentRow(DomDocument doc)
        {
            if (doc == null)
            {
                return null;
            }
            DomElement element = doc.CurrentElement;
            while (element != null)
            {
                if (element is DomTableRowElement)
                {
                    return (DomTableRowElement)element;
                }
                element = element.Parent;
            }
            return null;
        }

        /// <summary>
        /// 获得当前表格对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>当前表格对象</returns>
        public static DomTableElement GetCurrentTable(DomDocument document)
        {
            if (document == null)
            {
                return null;
            }
            DomElement element = document.CurrentElement;
            while (element != null)
            {
                if (element is DomTableElement)
                {
                    return (DomTableElement)element;
                }
                element = element.Parent;
            }
            return null;
        }

        /// <summary>
        /// 获得当前位置所在的表格行或者所有在同一个表格中的表格行或者表格列的列表
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>获得的要操作的对象列表</returns>
        private static DomElementList GetRowsOrColumns(DomDocument document , bool getTableRow )
        {
            DomElementList result = new DomElementList();
            DomSelection selection = document.Selection;
            if (selection.Cells != null && selection.Cells.Count > 0)
            {
                DomTableElement table = ((DomTableCellElement)selection.Cells[0]).OwnerTable;
                foreach (DomTableCellElement cell in selection.Cells)
                {
                    if (getTableRow)
                    {
                        DomTableRowElement row = cell.OwnerRow;
                        if (result.Contains(row) == false && row.OwnerTable == table)
                        {
                            result.Add(row);
                        }
                    }
                    else
                    {
                        DomTableColumnElement col = cell.OwnerColumn;
                        if (result.Contains(col) == false && col.OwnerTable == table)
                        {
                            result.Add(col);
                        }
                    }
                }
            }
            else
            {
                DomTableCellElement cell = GetCurrentCell(document);
                if (cell != null)
                {
                    DomTableElement table = cell.OwnerTable;
                    if (getTableRow)
                    {
                        for (int iCount = 0; iCount < cell.RowSpan; iCount++)
                        {
                            result.Add(table.RuntimeRows[cell.RowIndex + iCount]);
                        }
                    }
                    else
                    {
                        for (int iCount = 0; iCount < cell.ColSpan; iCount++)
                        {
                            result.Add(table.Columns[cell.ColIndex + iCount]);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获得当前单元格对象
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>当前单元格对象,若没有则返回空引用</returns>
        public static DomTableCellElement GetCurrentCell(DomDocument doc)
        {
            if (doc == null)
            {
                return null;
            }
            DomContent content = doc.Content;
            if (content.Selection.Cells != null
                && content.Selection.Cells.Count > 0)
            {
                return (DomTableCellElement)content.Selection.Cells[0];
            }
            else
            {
                DomElement element = doc.CurrentElement;
                while (element != null)
                {
                    if (element is DomTableCellElement)
                    {
                        return (DomTableCellElement)element;
                    }
                    element = element.Parent;
                }
            }
            return null;
        }


       

        /// <summary>
        /// 判断能否进行插入表格行操作
        /// </summary>
        /// <param name="doc">设计文档对象</param>
        /// <returns>能否进行插入表格行操作</returns>
        public static bool CanInsertRow(DomDocument doc)
        {
            return GetCurrentRow(doc) != null;
        }

        /// <summary>
        /// 根据插入点所在的容器来修正表格的总宽度
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="element">图片元素</param>
        public static void CheckTableWidthWhenInsertTable(DomDocument document, DomTableElement table )
        {
            if (document.Options.EditOptions.FixWidthWhenInsertImage)
            {
                DomContainerElement container = null;
                int elementIndex = 0;
                document.Content.GetCurrentPositionInfo(out container, out elementIndex);
                container = container.ContentElement;

                float newWidth = Math.Min(container.ClientWidth, table.TableWidth);
                if (newWidth != table.TableWidth)
                {
                    table.SetTableWidth(newWidth);
                }
            }
        }

        public static DomElementList GetHandledRows(DomDocument document)
        {
            DomElementList cells = GetHandledCells(document);
            DomElementList result = new DomElementList();
            if (cells != null && cells.Count > 0)
            {
                foreach (DomTableCellElement cell in cells)
                {
                    if (result.Contains(cell.OwnerRow) == false)
                    {
                        result.Add(cell.OwnerRow);
                    }
                }
            }
            return result;
        }

        public static DomElementList GetHandledCells(DomDocument document)
        {
            DomElementList result = new DomElementList();
            if (document.Selection.Length == 0)
            {
                DomTableCellElement cell = document.CurrentElement.OwnerCell;
                if (cell != null)
                {
                    result.Add(cell);
                }
            }
            else
            {
                if (document.Selection.Mode == ContentRangeMode.Cell
                    || document.Selection.Mode == ContentRangeMode.Mixed)
                {
                    result = document.Selection.Cells.Clone();
                }
                else
                {
                    DomTableCellElement cell = document.Selection.ContentElements[0].OwnerCell;
                    if (cell != null)
                    {
                        result.Add(cell);
                    }
                }
            }
            return result;
        }

       
        /// <summary>
        /// 单元格内容底端居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( StandardCommandNames.AlignBottomCenter ,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignBottomCenter.bmp")]
        protected void AlignBottomCenter(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.BottomCenter, args);
        }
        /// <summary>
        /// 单元格内容底端左对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignBottomLeft,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignBottomLeft.bmp")]
        protected void AlignBottomLeft(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.BottomLeft, args);
        }
        
        /// <summary>
        /// 单元格内容底端居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignBottomRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignBottomRight.bmp")]
        protected void AlignBottomRight(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.BottomRight, args);
        }

        /// <summary>
        /// 单元格内容垂直居中水平居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignMiddleCenter,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignMiddleCenter.bmp")]
        protected void AlignMiddleCenter(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.MiddleCenter, args);
        }

        /// <summary>
        /// 单元格内容垂直居中水平左对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignMiddleLeft,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignMiddleLeft.bmp")]
        protected void AlignMiddleLeft(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.MiddleLeft, args);
        }
        /// <summary>
        /// 单元格内容垂直居中右对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignMiddleRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignMiddleRight.bmp")]
        protected void AlignMiddleRight(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.MiddleRight, args);
        }
        /// <summary>
        /// 单元格内容靠上居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignTopCenter,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignTopCenter.bmp")]
        protected void AlignTopCenter(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.TopCenter, args);
        }
        /// <summary>
        /// 单元格内容靠上居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignTopLeft,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignTopLeft.bmp")]
        protected void AlignTopLeft(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.TopLeft, args);
        }
        /// <summary>
        /// 单元格内容靠上居中对齐
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.AlignTopRight,
            ImageResource = "DCSoft.CSharpWriter.Commands.Images.CommandAlignTopRight.bmp")]
        protected void AlignTopRight(object sender, WriterCommandEventArgs args)
        {
            SetCellContentAlign(ContentAlignment.TopRight, args);
        }

        /// <summary>
        /// 设置单元格的内容对齐方式
        /// </summary>
        /// <param name="align">新对齐方式</param>
        /// <param name="args">参数</param>
        private static void SetCellContentAlign(
            System.Drawing.ContentAlignment align ,
            WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled = false;
                args.Checked = false;
                DomElementList cells = GetHandledCells(args.Document);
                if (cells != null && cells.Count > 0)
                {
                    foreach (DomTableCellElement cell in cells)
                    {
                        if (args.DocumentControler.CanModify(cell))
                        {
                            foreach (DomParagraphFlagElement flag in cell.ParagraphsEOFs)
                            {
                                if (args.DocumentControler.CanModify(flag))
                                {
                                    args.Enabled = true;
                                    switch (cell.ContentVertialAlign)
                                    {
                                        case VerticalAlignStyle.Top :
                                            switch (flag.Style.Align)
                                            {
                                                case DocumentContentAlignment.Left :
                                                    args.Checked = align == ContentAlignment.TopLeft;
                                                    break;
                                                case DocumentContentAlignment.Center :
                                                    args.Checked = align == ContentAlignment.TopCenter;
                                                    break;
                                                case DocumentContentAlignment.Right :
                                                    args.Checked = align == ContentAlignment.TopRight;
                                                    break;
                                                case DocumentContentAlignment.Justify :
                                                    args.Checked = align == ContentAlignment.TopCenter;
                                                    break;
                                            }
                                            break;
                                        case VerticalAlignStyle.Justify:
                                        case VerticalAlignStyle.Middle:
                                            switch (flag.Style.Align)
                                            {
                                                case DocumentContentAlignment.Left:
                                                    args.Checked = align == ContentAlignment.MiddleLeft ;
                                                    break;
                                                case DocumentContentAlignment.Center:
                                                    args.Checked = align == ContentAlignment.MiddleCenter;
                                                    break;
                                                case DocumentContentAlignment.Right:
                                                    args.Checked = align == ContentAlignment.MiddleRight;
                                                    break;
                                                case DocumentContentAlignment.Justify:
                                                    args.Checked = align == ContentAlignment.MiddleCenter;
                                                    break;
                                            }
                                            break;
                                        case VerticalAlignStyle.Bottom :
                                            switch (flag.Style.Align)
                                            {
                                                case DocumentContentAlignment.Left:
                                                    args.Checked = align == ContentAlignment.BottomLeft;
                                                    break;
                                                case DocumentContentAlignment.Center:
                                                    args.Checked = align == ContentAlignment.BottomCenter;
                                                    break;
                                                case DocumentContentAlignment.Right:
                                                    args.Checked = align == ContentAlignment.BottomRight;
                                                    break;
                                                case DocumentContentAlignment.Justify:
                                                    args.Checked = align == ContentAlignment.BottomCenter;
                                                    break;
                                            }
                                            break;
                                    }//switch
                                    return;
                                }//if
                            }//foreach
                        }//if
                    }//foreach
                }
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                args.Result = false;
                args.RefreshLevel = UIStateRefreshLevel.All;
                DomElementList cells = GetHandledCells(args.Document);
                if (cells != null && cells.Count > 0)
                {
                    Dictionary<DomElement, int> newStyleIndexs = new Dictionary<DomElement, int>();
                    VerticalAlignStyle newVAlign = VerticalAlignStyle.Justify;
                    DocumentContentAlignment newHAlign = DocumentContentAlignment.Left;
                    switch (align)
                    {
                        case ContentAlignment.BottomCenter :
                            newVAlign = VerticalAlignStyle.Bottom;
                            newHAlign = DocumentContentAlignment.Center;
                            break;
                        case ContentAlignment.BottomLeft :
                            newVAlign = VerticalAlignStyle.Bottom;
                            newHAlign = DocumentContentAlignment.Left;
                            break;
                        case ContentAlignment.BottomRight :
                            newVAlign = VerticalAlignStyle.Bottom;
                            newHAlign = DocumentContentAlignment.Right;
                            break;
                        case ContentAlignment.MiddleCenter :
                            newVAlign = VerticalAlignStyle.Middle;
                            newHAlign = DocumentContentAlignment.Center;
                            break;
                        case ContentAlignment.MiddleLeft :
                            newVAlign = VerticalAlignStyle.Middle;
                            newHAlign = DocumentContentAlignment.Left;
                            break;
                        case ContentAlignment.MiddleRight :
                            newVAlign = VerticalAlignStyle.Middle;
                            newHAlign = DocumentContentAlignment.Right;
                            break;
                        case  ContentAlignment.TopCenter :
                            newVAlign = VerticalAlignStyle.Top;
                            newHAlign = DocumentContentAlignment.Center;
                            break;
                        case ContentAlignment.TopLeft :
                            newVAlign = VerticalAlignStyle.Top;
                            newHAlign = DocumentContentAlignment.Left;
                            break;
                        case ContentAlignment.TopRight :
                            newVAlign = VerticalAlignStyle.Top;
                            newHAlign = DocumentContentAlignment.Right;
                            break;
                    }
                    
                    foreach (DomTableCellElement cell in cells)
                    {
                        if (args.DocumentControler.CanModify(cell))
                        {
                            if (cell.ContentVertialAlign != newVAlign )
                            {
                                DocumentContentStyle rs = ( DocumentContentStyle ) cell.Style.Clone();
                                rs.DisableDefaultValue = true;
                                rs.VerticalAlign = newVAlign;
                                newStyleIndexs[cell] = args.Document.ContentStyles.GetStyleIndex(rs);
                            }
                            foreach (DomParagraphFlagElement flag in cell.ParagraphsEOFs)
                            {
                                if (args.DocumentControler.CanModify(flag))
                                {
                                    if (flag.Style.Align != newHAlign)
                                    {
                                        DocumentContentStyle rs = (DocumentContentStyle)flag.Style.Clone();
                                        rs.DisableDefaultValue = true;
                                        rs.Align = newHAlign;
                                        newStyleIndexs[flag] = args.Document.ContentStyles.GetStyleIndex(rs);
                                    }
                                }//if
                            }//foreach
                        }//if
                    }//foreach

                    if (newStyleIndexs.Count > 0)
                    {
                        args.Result = true;
                        args.Document.EditorSetElementStyle(newStyleIndexs, true);
                    }
                }
            }
        }

        /// <summary>
        /// 标题行
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription(StandardCommandNames.Table_HeaderRow )]
        protected void Table_HeaderRow(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Checked = false;
                DomElementList rows = GetHandledRows(args.Document);
                if (rows != null && rows.Count > 0)
                {
                    args.Enabled = true;
                    foreach (DomTableRowElement row in rows )
                    {
                        if (row.HeaderStyle)
                        {
                            args.Checked = true;
                            break;
                        }
                    }
                }
                else
                {
                    args.Enabled = false;
                }
            }
            else if( args.Mode == WriterCommandEventMode.Invoke )
            {
                DomElementList rows = GetHandledRows(args.Document);
                if (rows != null && rows.Count > 0)
                {
                    bool newHeaderStyle = false;
                    if (args.Parameter is bool)
                    {
                        newHeaderStyle = (bool)args.Parameter;
                    }
                    else
                    {
                        newHeaderStyle = true;
                        foreach (DomTableRowElement row in rows)
                        {
                            if (row.HeaderStyle)
                            {
                                newHeaderStyle = false;
                                break;
                            }
                        }//foreach
                    }
                    //bool modified = false;
                    Dictionary<DomTableRowElement, bool> newHeaderStyles = new Dictionary<DomTableRowElement, bool>();
                    foreach (DomTableRowElement row in rows)
                    {
                        if (row.HeaderStyle != newHeaderStyle)
                        {
                            newHeaderStyles[row] = newHeaderStyle;

                            //if (args.Document.CanLogUndo)
                            //{
                            //    args.Document.UndoList.AddProperty(
                            //        "HeaderStyle",
                            //        row.HeaderStyle, 
                            //        newHeaderStyle,
                            //        row);
                            //}
                            //row.HeaderStyle = newHeaderStyle;
                            //modified = true;
                            //row.OwnerTable.RuntimeRows = null;
                        }
                    }
                    args.Document.BeginLogUndo();
                    if (newHeaderStyles.Count > 0)
                    {
                        DomTableElement table = ((DomTableRowElement)rows[0]).OwnerTable;
                        table.EditorSetHeaderRow(newHeaderStyles, true);
                    }
                    args.Document.EndLogUndo();
                    args.RefreshLevel = UIStateRefreshLevel.All;
                    //if (modified)
                    //{
                    //    args.Document.Modified = true;
                    //    args.Document.OnDocumentContentChanged();
                    //    if (args.EditorControl != null
                    //        && args.EditorControl.ViewMode == PageViewMode.Page )
                    //    {
                    //        args.EditorControl.RefreshDocument();
                    //    }
                    //}
                }
            }
        }
    }
}