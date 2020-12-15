using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common;
using DCSoft.Printing ;
using System.Drawing;
using DCSoft.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.CSharpWriter.Dom.Undo;
using DCSoft.CSharpWriter.Html;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 表格元素
    /// </summary>
    /// <remarks>
    /// 本表格支持多行多列，支持横向和纵向合并单元格
    /// 编写  袁永福 2012-7-12</remarks>
    [Serializable]
    [System.Xml.Serialization.XmlType("XTextTable")]
    [System.Diagnostics.DebuggerDisplay("Table {RowsCount} rows , {ColumnsCount} columns")]
    [DomElementDescriptor(
        PropertiesType = typeof(DCSoft.CSharpWriter.Commands.XTextTableElementProperties))]
    public class DomTableElement : DomContainerElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomTableElement()
        {
        }

        //private string _ID = null;
        ///// <summary>
        ///// 对象编号
        ///// </summary>
        //[DefaultValue( null )]
        //public string ID
        //{
        //    get
        //    {
        //        return _ID; 
        //    }
        //    set
        //    {
        //        _ID = value; 
        //    }
        //}

        /// <summary>
        /// 表格行数
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlAttribute()]
        public int NumOfRows
        {
            get
            {
                if (this.Elements == null)
                {
                    return 0;
                }
                else
                {
                    return this.Elements.Count;
                }
            }
            set
            {
            }
        }

        /// <summary>
        /// 表格的列数
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlAttribute()]
        public int NumOfColumns
        {
            get
            {
                return this.Columns.Count;
            }
            set
            {
            }
        }



        /// <summary>
        /// 标题行集合
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElementList HeaderRows
        {
            get
            {
                DomElementList list = new DomElementList();
                foreach (DomTableRowElement row in this.Rows)
                {
                    if (row.HeaderStyle)
                    {
                        for (int iCount = this.Rows.IndexOf(row); iCount < this.Rows.Count; iCount++)
                        {
                            DomTableRowElement row2 = (DomTableRowElement)this.Rows[iCount];
                            if (row2.HeaderStyle)
                            {
                                list.Add(row2);
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 判断是否存在标题行
        /// </summary>
        /// <returns>是否存在标题行</returns>
        [Browsable( false )]
        public bool HasHeaderRow
        {
            get
            {
                foreach (DomTableRowElement row in this.Rows)
                {
                    if (row.HeaderStyle)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [NonSerialized]
        private DomElementList _RuntimeRows = new DomElementList();
        /// <summary>
        /// 运行时的表格行对象列表
        /// </summary>
        [XmlIgnore()]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomElementList RuntimeRows
        {
            get
            {
                if (_RuntimeRows == null || _RuntimeRows.Count < this.Rows.Count )
                {
                    _RuntimeRows = this.Rows.Clone();
                }
                return _RuntimeRows; 
            }
            set
            {
                _RuntimeRows = value; 
            }
        }

        /// <summary>
        /// 对象所属文档对象
        /// </summary>
        [XmlIgnore()]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DomDocument OwnerDocument
        {
            get
            {
                return base.OwnerDocument;
            }
            set
            {
                base.OwnerDocument = value;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    col.OwnerDocument = this.OwnerDocument;
                }
            }
        }

        private DomElementList _Columns = new DomElementList();
        /// <summary>
        /// 表格列对象
        /// </summary>
        public DomElementList Columns
        {
            get
            {
                return _Columns; 
            }
            set
            {
                _Columns = value; 
            }
        }

        /// <summary>
        /// 表格行对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElementList Rows
        {
            get
            {
                return this.Elements;
            }
        }

        /// <summary>
        /// 表格行数
        /// </summary>
        public int RowsCount
        {
            get
            {
                return this.Elements.Count;
            }
        }

        /// <summary>
        /// 表格列数
        /// </summary>
        public int ColumnsCount
        {
            get
            {
                return _Columns.Count ;
            }
        }

        ///// <summary>
        ///// 返回对象可接受的子对象的类型
        ///// </summary>
        //[Browsable( false )]
        //public override ElementType AcceptChildElementTypes
        //{
        //    get
        //    {
        //        ElementType type = base.AcceptChildElementTypes;
        //        type = type | ElementType.TableColumn | ElementType.TableRow;
        //        return type;
        //    }
        //}

        /// <summary>
        /// 获得所有的单元格对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElementList Cells
        {
            get
            {
                DomElementList myCells = new DomElementList();
                foreach (DomTableRowElement myRow in this.RuntimeRows)
                {
                    foreach (DomTableCellElement cell in myRow.Cells)
                    {
                        myCells.Add(cell);
                    }
                }
                return myCells;
            }
        }

        /// <summary>
        /// 第一个内容元素
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public override DomElement FirstContentElement
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// 最后一个内容元素
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public override DomElement LastContentElement
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// 表格中是否有内容被选择
        /// </summary>
        [Browsable( false )]
        public override bool HasSelection
        {
            get
            {
                DomSelection selection = this.DocumentContentElement.Selection;
                if (selection.Cells != null && selection.Cells.Count > 0)
                {
                    foreach (DomTableCellElement cell in selection.Cells)
                    {
                        if (cell.OwnerTable == this)
                        {
                            return true;
                        }
                    }
                }
                return base.HasSelection;
            }
        }

        /// <summary>
        /// 表格套嵌层次，文档中第一层表格的层次为1，子表格为2,再内部的子表格为3，以此类推.
        /// </summary>
        public int NeastLevel
        {
            get
            {
                int level = 0;
                DomElement element = this;
                while (element != null)
                {
                    if (element is DomTableElement)
                    {
                        level++;
                    }
                    element = element.Parent;
                }
                return level;
            }
        }

        /// <summary>
        /// 修正分页线位置
        /// </summary>
        /// <param name="pos">修正前的分页线位置</param>
        /// <returns>修正后的分页线位置</returns>
        internal void FixPageLine(PageLineInfo info)
        {
            float tableAbsTop = this.AbsTop;
            DomTableRowElement currentRow = null;
            foreach (DomTableRowElement row in this.RuntimeRows)
            {
                if (row.Visible)
                {
                    if (info.CurrentPoistion >= tableAbsTop + row.Top)
                    {
                        currentRow = row;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (currentRow == null)
            {
                // 没有命中任何表格行，返回原值
                return ;
            }
            float distance = info.CurrentPoistion - ( tableAbsTop + currentRow.Top);
            float minDistance = this.OwnerDocument.PixelToDocumentUnit(15);
            if (this.HasHeaderRow)
            {
                // 本表格有标题行，minDistance需要适当的放大
                minDistance = minDistance * 4;
            }
            if (distance > minDistance)
            {
                // 出现不可忽略的高度差，深入单元格内部来修正分页线位置
                List<DomTableCellElement> handledCells = new List<DomTableCellElement>();
                foreach (DomTableCellElement cell in currentRow.Cells)
                {
                    if (handledCells.Contains(cell))
                    {
                        continue;
                    }
                    handledCells.Add(cell);
                    if (cell.Visible)
                    {
                        cell.FixPageLine( info );
                    }
                    else
                    {
                        DomTableCellElement cell2 = cell.OverrideCell;
                        if (handledCells.Contains(cell2) == false)
                        {
                            handledCells.Add(cell2);
                            cell2.FixPageLine(info);
                        }
                    }
                }
            }
            else
            {
                if ( info.CurrentPoistion > tableAbsTop + currentRow.Top + currentRow.Height)
                {
                    info.CurrentPoistion = ( int ) ( tableAbsTop + currentRow.Top + currentRow.Height);
                    info.SourceElement = this.RuntimeRows.GetNextElement(currentRow);
                }
                else
                {
                    info.CurrentPoistion = (int)(tableAbsTop + currentRow.Top);// currentRow.AbsTop;
                    info.SourceElement = currentRow;
                }
            }
            if (this.HasHeaderRow
                && info.ForJumpPrint == false
                && info.PageViewMode == PageViewMode.Page )
            {
                if (this.RuntimeRows.Contains(info.SourceElement))
                {
                    // 插入临时的标题行
                    DomElement element = info.SourceElement;
                    while (element != null)
                    {
                        if (element.Parent == this)
                        {
                            break;
                        }
                        element = element.Parent;
                    }
                    if (element != null)
                    {
                        DomTableRowElement row = (DomTableRowElement)element;
                        if (this.RuntimeRows.IndexOf(row)
                            > this.RuntimeRows.IndexOf(this.HeaderRows.LastElement) + 1)
                        {
                            // 不可能标题行连着标题行
                            InsertHeaderRows(row, info);
                        }//if
                    }//if
                }
            }//if
        }

         
        /// <summary>
        /// 插入标题行
        /// </summary>
        /// <param name="row">导致分页的表格行对象</param>
        /// <param name="info">分页计算信息对象</param>
        /// <returns>是否插入标题行</returns>
        private bool InsertHeaderRows(DomTableRowElement row, PageLineInfo info)
        {
            if (info.ForJumpPrint)
                return false;

            DomElementList headerRows = this.HeaderRows;
            if (headerRows.Count == 0)
            {
                return false;
            }

            float heightCount = 0;
            int index = this.RuntimeRows.IndexOf(row);
            float pos = this.Top + row.Top ;

            for (int iCount = 0; iCount < headerRows.Count; iCount++)
            {
                DomTableRowElement oldRow = (DomTableRowElement)headerRows[iCount];
                DomTableRowElement newRow = (DomTableRowElement)oldRow.Clone(true);
                newRow._SourceHeaderRow = oldRow ;
                //foreach (XTextElement element in newRow.Elements)
                //{
                //    element.Guid = Guid.NewGuid();
                //}

                newRow.TemporaryHeaderRow = true;
                //newRow.Enabled = false;
                this.RuntimeRows.Insert(index + iCount, newRow);
                newRow.Top = row.Top + heightCount;
                heightCount += newRow.Height;
            }
            if (heightCount > 0)
            {
                // 更新表格行的位置和表格高度
                this.UpdateRowPosition();
                //float topCount = 0;
                //foreach (XTextTableRowElement row2 in this.RuntimeRows)
                //{
                //    row2.Top = topCount;
                //    topCount = topCount + row2.Height;
                //}
                // 更新文档行位置
                this.DocumentContentElement.UpdateLinePosition(
                    this.DocumentContentElement.ContentVertialAlign ,
                    false ,
                    false );
                this.DocumentContentElement.UpdateHeightByContentHeight();
                ////for (int iCount = this.Rows.IndexOf(row); iCount < this.Rows.Count; iCount++)
                ////{
                ////    DesignReportTableRow myRow = (DesignReportTableRow)this.Rows[iCount];
                ////    myRow.Top += heightCount;
                ////}
                //// 由于表格增长,移动本表格所在容器中的下面的其他文档元素
                //if (this.DesignMode == false)
                //{
                //    foreach (DesignElement element in this.Parent.Items)
                //    {
                //        if (element != this && element.Top > pos
                //            && (element.PrintDockStyle == PrintDockStyle.Top
                //                || element.PrintDockStyle == PrintDockStyle.None))
                //        {
                //            element.Top = element.Top + heightCount;
                //        }
                //    }
                //}
                //// 执行父容器内容排版
                //this.Parent.ExecuteLayout();
                //info.BodyHeightIncrease += heightCount;
                //heightCount = 0;
                //foreach (DesignReportTableRow row2 in this.Items)
                //{
                //    heightCount += row2.VisibleHeight;
                //}
                //this.myBounds.Height = heightCount;

                //this.OwnerDocument.EndUpdate();

                return true;
                //this.Parent.Height += heightCount;
            }
            return false;
        }

        /// <summary>
        /// 绘制对象内容
        /// </summary>
        /// <param name="args">参数</param>
        public override void Draw(DocumentPaintEventArgs args)
        {
            // 绘制单元格内容
            float leftFix = this.AbsLeft + this.RuntimeStyle.PaddingLeft;
            float topFix = this.AbsTop + this.RuntimeStyle.PaddingTop;
            foreach (DomTableRowElement row in this.RuntimeRows)
            {
                if( row.Visible == false )
                {
                    continue ;
                }

                RectangleF rowBounds = row.Bounds ;
                rowBounds.Offset(leftFix, topFix);
                if (args.ClipRectangle.IsEmpty == false)
                {
                    // 判断表格行是否在剪切矩形中
                    if (rowBounds.Top > args.ClipRectangle.Bottom)
                    {
                        // 表格行的顶端位置低于剪切矩形，则剩下的表格行必然不会
                        // 显示，因此退出循环
                        break;
                    }
                    // 获得表格行中所有的单元格的最大高度
                    float maxCellHeight = 0;
                    foreach (DomTableCellElement cell in row.Cells)
                    {
                        if (cell.IsOverrided == false)
                        {
                            maxCellHeight = Math.Max(maxCellHeight, cell.Height);
                        }
                    }
                    if (rowBounds.Top + maxCellHeight < args.ClipRectangle.Top - 2)
                    {
                        // 表格行的低端尚未达到剪切矩形，进行下一个表格行的判断
                        continue;
                    }
                }
                // 绘制单元格
                foreach (DomTableCellElement cell in row.Cells)
                {
                    if (cell.Visible == false)
                    {
                        continue;
                    }
                    args.Element = cell;
                    args.Style = cell.RuntimeStyle;
                    RectangleF cellBounds = cell.AbsBounds;
                    RectangleF backBounds = cellBounds;
                    if (args.ClipRectangleF.IsEmpty == false)
                    {
                        backBounds = RectangleF.Intersect(cellBounds, args.ClipRectangleF);
                    }
                    if (backBounds.IsEmpty)
                    {
                        continue;
                    }
                    // 绘制表格背景
                    if (args.PageClipRectangle.IsEmpty == false)
                    {
                        Rectangle prect = args.PageClipRectangle;
                        if (cellBounds.Y < prect.Y)
                        {
                            cellBounds.Height = cellBounds.Bottom - prect.Y;
                            cellBounds.Y = prect.Y;
                        }
                        if (cellBounds.Bottom > prect.Bottom - 1)
                        {
                            cellBounds.Height = prect.Bottom - 1 - cellBounds.Y;
                        }
                        //cellBounds = System.Drawing.RectangleF.Intersect(cellBounds, args.PageClipRectangle);
                    }
                    this.OwnerDocument.Render.DrawBackground(cell, args, cellBounds);
                    // 绘制内容
                    System.Drawing.Drawing2D.GraphicsState stateBack = args.Graphics.Save();
                    args.Graphics.SetClip(
                        new RectangleF(
                            cellBounds.Left - 4,
                            cellBounds.Top - 4,
                            cellBounds.Width + 8,
                            cellBounds.Height + 8),
                        System.Drawing.Drawing2D.CombineMode.Intersect );
                    if (args.RenderStyle == DocumentRenderStyle.Paint)
                    {
                        if (cellBounds.Height > 3)
                        {
                            // 当在用户界面上绘制单元格内容时，当绘制高度大于3才绘制单元格内容
                            cell.DrawContent(args);
                        }
                    }
                    else
                    {
                        cell.DrawContent(args);
                    }
                    args.Graphics.Restore(stateBack);
                    // 绘制单元格边框线
                    if (args.RenderStyle == DocumentRenderStyle.Paint)
                    {
                        if (this.OwnerDocument.Options.ViewOptions.ShowCellNoneBorder)
                        {
                            DocumentContentStyle cr = cell.RuntimeStyle;
                            if (cr.BorderLeft == false
                                || cr.BorderTop == false
                                || cr.BorderRight == false
                                || cr.BorderBottom == false)
                            {
                                Pen pen = GraphicsObjectBuffer.GetPen(
                                    this.OwnerDocument.Options.ViewOptions.NoneBorderColor);
                                args.Graphics.DrawRectangle(
                                    pen,
                                    cellBounds.Left,
                                    cellBounds.Top,
                                    cellBounds.Width,
                                    cellBounds.Height);
                            }
                        }
                    }
                    this.OwnerDocument.Render.RenderBorder(cell, args, cellBounds);
                    if (cell.IsSelected && this.OwnerDocument.EditorControl != null )
                    {
                        //若单元格是被选中，则逆转绘制突出显示
                        this.OwnerDocument.EditorControl.AddSelectionAreaRectangle(
                            Rectangle.Ceiling( cell.AbsBounds));
                    }
                }//foreach
            }//foreach
        }

        /// <summary>
        /// 获得指定区域内的单元格对象
        /// </summary>
        /// <param name="RowIndex">开始行号</param>
        /// <param name="ColIndex">开始列号</param>
        /// <param name="RowSpan">行数</param>
        /// <param name="ColSpan">列数</param>
        /// <param name="includeOverriedCell">是否包含被合并的单元格</param>
        /// <returns>单元格对象集合</returns>
        public DomElementList GetRange(
            int RowIndex,
            int ColIndex,
            int RowSpan,
            int ColSpan,
            bool includeOverriedCell )
        {
            // 检查参数
            if (RowSpan < 0 || ColSpan < 0)
            {
                return null;
            }
            int RowIndex2 = RowIndex + RowSpan - 1;
            int ColIndex2 = ColIndex + ColSpan - 1;

            if (RowIndex < 0)
            {
                RowIndex = 0;
            }
            if (ColIndex < 0)
            {
                ColIndex = 0;
            }

            if (RowIndex >= this.RuntimeRows.Count || ColIndex >= this.Columns.Count)
            {
                return null;
            }


            if (RowIndex2 >= this.RuntimeRows.Count)
            {
                RowIndex2 = this.RuntimeRows.Count - 1;
            }
            if (ColIndex2 >= this.Columns.Count)
            {
                ColIndex2 = this.Columns.Count - 1;
            }

            DomElementList myList = new DomElementList();
            for (int row = RowIndex; row <= RowIndex2; row++)
            {
                DomTableRowElement myRow = (DomTableRowElement)this.RuntimeRows[row];
                for (int col = ColIndex; col <= ColIndex2 && col < myRow.Cells.Count; col++)
                {
                    DomTableCellElement cell = (DomTableCellElement)myRow.Cells[col];
                    if (includeOverriedCell == false && cell.IsOverrided)
                    {
                        continue;
                    }
                    myList.Add( cell );
                }//for
            }//for
            return myList;
        }

        /// <summary>
        /// 添加子元素
        /// </summary>
        /// <param name="element">子元素</param>
        /// <returns>操作是否成功</returns>
        public override bool AppendChildElement(DomElement element)
        {
            if (element is DomTableColumnElement)
            {
                this.Columns.Add(element);
                element.Parent = this;
                element.OwnerDocument = this.OwnerDocument;
                return true;
            }
            else
            {
                return base.AppendChildElement(element);
            }
        }

        public override void FixDomState()
        {
            base.FixDomState();
            if (this._Columns != null)
            {
                foreach (DomTableColumnElement col in this._Columns)
                {
                    col.Parent = this;
                    col.OwnerDocument = this.OwnerDocument;
                }
            }
        }

        /// <summary>
		/// 从文件中加载对象后的操作
		/// </summary>
        public override void AfterLoad(FileFormat format)
        {
            foreach (DomTableColumnElement col in this.Columns)
            {
                col.Parent = this;
                col.OwnerDocument = this.OwnerDocument;
            }
            base.AfterLoad(format);
            this.FixCells();
            this.UpdateCellOverrideState();
        }

        private bool _LayoutInvalidate = true;
       
        /// <summary>
        /// 表格内容排版无效标志
        /// </summary>
        internal bool LayoutInvalidate
        {
            get
            {
                return _LayoutInvalidate; 
            }
            set
            {
                _LayoutInvalidate = value; 
            }
        }

        public override void ExecuteLayout()
        {
            this.RuntimeRows = null;
            this.LayoutInvalidate = false;
            this.FixCells();
            this.UpdateCellOverrideState();
            this.UpdateCellsState(true);
        }

        /// <summary>
        /// 更新单元格的合并状态
        /// </summary>
        internal void UpdateCellOverrideState()
        {
            this.RuntimeRows = null;
            DomElementList myCells = this.Cells;
            foreach (DomTableCellElement element in myCells)
            {
                element.myOverrideCell = null;
            }
            foreach (DomTableCellElement myCell in myCells)
            {
                if (myCell.myOverrideCell != null)
                {
                    // 本单元格已经被合并了,无需处理
                    continue;
                }
                if (myCell.RowSpan > 1)
                {
                    if (myCell.RowIndex + myCell.RowSpan > this.RuntimeRows.Count)
                    {
                        myCell.InternalSetRowSpan( this.Rows.Count - myCell.RowIndex );
                    }
                }
                if (myCell.ColSpan > 1)
                {
                    if (myCell.ColIndex + myCell.ColSpan > this.Columns.Count)
                    {
                        myCell.InternalSetColSpan(this.Columns.Count - myCell.ColIndex);
                    }
                }
                int runtimeRowSpan = myCell.RuntimeRowSpan;
                if (runtimeRowSpan > 1 || myCell.ColSpan > 1)
                {
                    // 获得所有被当前单元格合并的单元格
                    DomElementList myRange = this.GetRange(
                        myCell.RowIndex,
                        myCell.ColIndex,
                        runtimeRowSpan,
                        myCell.ColSpan,
                        true );

                    foreach (DomTableCellElement e2 in myRange)
                    {
                        DomTableRowElement row = myCell.OwnerRow;
                        if (row.HeaderStyle == false && row.TemporaryHeaderRow == false)
                        {
                            DomTableRowElement row2 = e2.OwnerRow;
                            if (row2.TemporaryHeaderRow)
                            {
                                // 临时生成的标题行不被隐藏
                                continue;
                            }
                        }
 
                        // 设置这些单元格的合并单元格
                        e2.myOverrideCell = myCell;
                    }
                    myCell.myOverrideCell = null;
                }//if
            }//foreach
        }
        
        internal void UpdateCellsState( bool CellExecuteLayout)
        {
            // 设置表格列的位置
            float colLeft = this.RuntimeStyle.PaddingLeft;
            foreach (DomTableColumnElement col in this.Columns)
            {
                col.Left = colLeft;
                if (col.Visible)
                {
                    colLeft += col.Width;
                }
            }
            DomElementList myCells = this.Cells;
            foreach (DomTableCellElement myCell in myCells)
            {
                myCell.FixElements();
                if (myCell.Visible)
                {
                    float WidthCount = 0;
                    if (myCell.ColSpan == 1)
                    {
                        WidthCount = ((DomTableColumnElement)this.Columns[myCell.ColIndex]).Width;
                    }
                    else
                    {
                        for (int iCount = 0; iCount < myCell.ColSpan; iCount++)
                        {
                            WidthCount += ((DomTableColumnElement)this.Columns[myCell.ColIndex + iCount]).Width;
                        }
                    }
                    if (myCell.Width != WidthCount)
                    {
                        // 重新设置单元格的宽度
                        myCell.Width = WidthCount;
                        if (CellExecuteLayout)
                        {
                            //重新进行内容排版
                            myCell.ExecuteLayout();
                        }
                    }
                    float HeightCount = 0;
                    int runtimeRowSpan = myCell.RuntimeRowSpan;
                    if (runtimeRowSpan == 1)
                    {
                        HeightCount = myCell.OwnerRow.Height;
                    }
                    else
                    {
                        for (int iCount = 0; iCount < runtimeRowSpan; iCount++)
                        {
                            HeightCount += this.Rows[myCell.RowIndex + iCount].Height;
                        }
                    }

                    System.Drawing.RectangleF CellBounds = new System.Drawing.RectangleF(
                        this.Columns[myCell.ColIndex].Left,
                        0,
                        WidthCount,
                        HeightCount);
                    if (CellExecuteLayout)
                    {
                        if (myCell.Height == 0 || myCell.Height != HeightCount)
                        {
                            //myCell.Height = HeightCount;
                            if (myCell.ContentVertialAlign != Drawing.VerticalAlignStyle.Top)
                            {
                                myCell.ExecuteLayout();
                            }
                        }
                    }
                }
                else
                {
                    myCell.Left = myCell.OwnerColumn.Left;
                    myCell.Top = 0;
                    myCell.Width = myCell.OwnerColumn.Width;
                    myCell.Height = myCell.OwnerRow.Height;
                }
            }
            // 计算表格行的高度

            // 根据单元格的内容高度来设置表格行的高度
            foreach (DomTableRowElement row in this.Rows)
            {
                if (row.SpecifyHeight < 0)
                {
                    row.Height = Math.Abs(row.SpecifyHeight);
                }
                else
                {
                    bool match = false;
                    DocumentContentStyle rs = row.RuntimeStyle;
                    row.Height = row.SpecifyHeight;
                    foreach (DomTableCellElement cell in row.Cells)
                    {
                        if (cell.IsOverrided == false && cell.RowSpan == 1)
                        {
                            row.Height = Math.Max(
                                row.Height,
                                cell.ContentHeight + rs.PaddingTop + rs.PaddingBottom );
                            match = true;
                        }
                    }
                    if (match == false)
                    {
                        // 设置表格行的高度为默认文本行的高度
                        float dh = this.OwnerDocument.DefaultStyle.DefaultLineHeight
                            + rs.PaddingTop
                            + rs.PaddingBottom ;
                        row.Height = Math.Max(row.SpecifyHeight, dh);
                    }
                }
            }//foreach

            // 根据纵向合并的单元格的高度来设置表格行的高度
            foreach (DomTableCellElement cell in myCells)
            {
                if (cell.IsOverrided == false && cell.RowSpan > 1)
                {
                    float heightCount = 0;
                    // 累计表格行的高度
                    for (int rowIndex = 0; rowIndex < cell.RowSpan; rowIndex++)
                    {
                        heightCount += ((DomTableRowElement)this.Rows[rowIndex + cell.RowIndex]).Height;
                    }
                    DocumentContentStyle rs = cell.OwnerRow.RuntimeStyle;
                    float ccHeight = cell.ContentHeight + rs.PaddingTop + rs.PaddingBottom;
                    if (ccHeight > heightCount)
                    {
                        //bool match = false;
                        for (int rowIndex = cell.RowSpan - 1; rowIndex >= 0; rowIndex--)
                        {
                            DomTableRowElement row = (DomTableRowElement)this.Rows[rowIndex + cell.RowIndex];
                            if (row.SpecifyHeight >= 0)
                            {
                                row.Height = row.Height + ccHeight - heightCount;
                                //match = true;
                                break;
                            }
                        }//for
                    }
                    else
                    {
                        cell.Height = heightCount;
                    }
                }//if
            }//foreach

            // 设置各个单元格的位置
            DocumentContentStyle rs2 = this.RuntimeStyle;
            float rowTop = rs2.PaddingTop ;
            foreach (DomTableRowElement row in this.RuntimeRows)
            {
                row.Top = rowTop;
                rowTop = rowTop + row.Height;
                row.Left = rs2.PaddingLeft ;
                foreach (DomTableCellElement cell in row.Cells)
                {
                    // 设置单元格的位置
                    cell.Left = cell.OwnerColumn.Left;
                    cell.Top = 0;
                    // 设置单元格的高度
                    if (cell.RowSpan == 1 || cell.IsOverrided)
                    {
                        cell.Height = row.Height;
                    }
                    else
                    {
                        float cellHeight = 0;
                        int rowIndex = this.Rows.IndexOf( row );
                        for (int iCount = 0; iCount < cell.RowSpan; iCount++)
                        {
                            cellHeight += this.Rows[rowIndex + iCount].Height;
                        }
                        cell.Height = cellHeight;
                    }
                    if (cell.RuntimeStyle.VerticalAlign != Drawing.VerticalAlignStyle.Top)
                    {
                        // 若单元格内容不是垂直顶端对齐，则重新设置文档行的位置
                        cell.UpdateLinePosition(cell.RuntimeStyle.VerticalAlign, false, false);
                    }
                }//foreach
            }//foreach
            this.Height = rowTop + rs2.PaddingTop + rs2.PaddingBottom ;
            float widthCount = 0;
            foreach (DomTableColumnElement col in this.Columns)
            {
                widthCount += col.Width;
            }
            this.Width = widthCount + rs2.PaddingLeft + rs2.PaddingRight;
            foreach (DomTableRowElement row in this.Rows)
            {
                // 设置表格行的宽度
                row.Width = widthCount;
            }
        }

        /// <summary>
        /// 更新表格行的位置
        /// </summary>
        /// <remarks>当应用程序</remarks>
        internal void UpdateRowPosition()
        {
            DocumentContentStyle rs2 = this.RuntimeStyle;
            float rowTop = rs2.PaddingTop;
            foreach (DomTableRowElement row in this.RuntimeRows )
            {
                row.Top = rowTop;
                rowTop = rowTop + row.Height;
                row.Left = rs2.PaddingLeft;
            }//foreach
            this.Height = rowTop + rs2.PaddingBottom;
            if (this.OwnerLine != null)
            {
                this.OwnerLine.RefreshState();
                //this.OwnerLine.Height = this.Height + XTextLine.LineHeighFix;
                //this.OwnerLine.ContentHeight = this.Height;
            }
        }

        /// <summary>
        /// 修正单元格对象的个数
        /// </summary>
        internal void FixCells()
        {
            int MaxCellCount = 0;
            foreach (DomTableRowElement myRow in this.Rows )
            {
                if (MaxCellCount < myRow.Cells.Count)
                {
                    MaxCellCount = myRow.Cells.Count;
                }
                if (myRow.Cells.Count > this.Columns.Count)
                {
                    for (int iCount = this.Columns.Count; iCount < myRow.Cells.Count; iCount++)
                    {
                        DomTableColumnElement NewCol = this.CreateColumnInstance();
                        NewCol.Width = myRow.Cells[iCount].Width;
                        if (NewCol.Width == 0)
                        {
                            if (this.Columns.Count == 0)
                            {
                                NewCol.Width = 100;
                            }
                            else
                            {
                                NewCol.Width = this.Columns.LastElement.Width;
                            }
                        }
                        NewCol.OwnerDocument = this.OwnerDocument;
                        NewCol.Parent = this;
                        this.Columns.Add(NewCol);
                    }
                }//if
            }//foreach
            if (this.Columns.Count > MaxCellCount)
            {
                this.Columns.RemoveToFixLenght(MaxCellCount);
            }
            CellIndex myCellIndex = new CellIndex();
            foreach (DomTableRowElement myRow in this.Elements )
            {
                //////////////////myRow.Items.Lock = false;
                if (myRow.Cells.Count != this.Columns.Count)
                {
                    myRow.Cells.RemoveToFixLenght(this.Columns.Count);
                    if (myRow.Cells.Count < this.Columns.Count)
                    {
                        DomTableCellElement LastCell = (DomTableCellElement)myRow.Cells.LastElement;
                        for (int iCount = myRow.Cells.Count; iCount < this.Columns.Count; iCount++)
                        {
                            DomTableCellElement cell = null;
                            if (LastCell == null)
                            {
                                cell = this.CreateCellInstance();
                                //cell.Style.BorderWidth = this.intDefaultCellBorderWidth;
                            }
                            else
                            {
                                cell = (DomTableCellElement)LastCell.Clone(false);
                                //cell.Text = null;
                            }
                            //cell.BackColor = System.Drawing.Color.Red ;
                            myRow.Cells.Add(cell);
                            cell.Parent = myRow;
                            cell.OwnerDocument = this.OwnerDocument;
                            myCellIndex.RowIndex = myRow.ElementIndex + 1;
                            myCellIndex.ColIndex = iCount + 1;
                        }
                    }//if( myRow.Items.Count < myColumns.Count )
                }
            }
        }

        /// <summary>
        ///  表格高度发生改变，需要重新设置行状态和重新分页
        /// </summary>
        /// <param name="table"></param>
        internal void UpdatePagesForTable( bool forceFixHeight )
        {
            // 表格高度发生改变，需要重新设置行状态和重新分页
            DomContentLine tableLine = this.OwnerLine;
            float lineHeight = this.OwnerLine.Height;
            // 刷新表格所在行的状态
            tableLine.RefreshState();
            if (tableLine.Height != lineHeight || forceFixHeight )
            {
                // 则向上调整父表格和更上级的表格的排版。
                DomElement parent = this;
                while (parent != null)
                {
                    if (parent is DomTableCellElement)
                    {
                        DomTableCellElement cell = (DomTableCellElement)parent;
                        DomTableElement table = cell.OwnerTable;
                        float heightBack = cell.Height;
                        cell.Height = 0;
                        table.ExecuteLayout();
                        if (cell.Height != heightBack)
                        {
                            table.OwnerLine.RefreshState();
                            table.OwnerLine.InvalidateState = true;
                        }
                    }
                    parent = parent.Parent;
                }

                // 更新文档行
                DomDocumentContentElement dce = this.DocumentContentElement;
                dce.UpdateLinePosition(dce.ContentVertialAlign, false, true);
                // 更新分页状态
                if (dce.ContentPartyStyle == PageContentPartyStyle.Body)
                {
                    this.OwnerDocument.RefreshPages();
                }
                if (this.OwnerDocument.EditorControl != null)
                {
                    this.OwnerDocument.EditorControl.UpdatePages();
                    this.OwnerDocument.EditorControl.Invalidate();
                }
            }
            if (this.OwnerDocument.EditorControl != null)
            {
                this.OwnerDocument.EditorControl.UpdateTextCaretWithoutScroll();
            }
        }

        /// <summary>
        /// 查找指定序号的单元格对象
        /// </summary>
        /// <param name="rowIndex">从0开始的行号</param>
        /// <param name="colIndex">从0开始的列号</param>
        /// <param name="throwException">若未找到单元格是否抛出异常</param>
        /// <returns>找的单元格对象，若未找到而且不抛出异常则返回空引用</returns>
        public DomTableCellElement GetCell(int rowIndex, int colIndex, bool throwException)
        {
            if (rowIndex >= 0 && rowIndex < this.RuntimeRows.Count)
            {
                DomTableRowElement row = (DomTableRowElement)this.RuntimeRows[rowIndex];
                if (colIndex >= 0 && colIndex < row.Cells.Count)
                {
                    return (DomTableCellElement)row.Cells[colIndex];
                }
            }
            if (throwException)
            {
                throw new Exception("Bad RowIndex=" + rowIndex + ", ColIndex=" + colIndex);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据两个单元格位置获得区域中被选择的单元格列表，包括被合并而隐藏的单元格
        /// </summary>
        /// <param name="rowIndex1">第一个单元格的行号</param>
        /// <param name="colIndex1">第一个单元格的列号</param>
        /// <param name="rowIndex2">第二个单元格的行号</param>
        /// <param name="colIndex2">第二个单元格的列号</param>
        /// <returns>单元格对象列表</returns>
        public DomElementList GetSelectionCells(int rowIndex1, int colIndex1, int rowIndex2, int colIndex2)
        {
            // 检查参数
            if (rowIndex1 < 0 || rowIndex1 >= this.RuntimeRows.Count)
            {
                throw new ArgumentOutOfRangeException("rowIndex1=" + rowIndex1 );
            }
            if (rowIndex2 < 0 || rowIndex2 >= this.RuntimeRows.Count)
            {
                throw new ArgumentOutOfRangeException("rowIndex2=" + rowIndex2 );
            }
            if( colIndex1 < 0 || colIndex1 >= this.Columns.Count )
            {
                throw new ArgumentOutOfRangeException("colIndex1=" + colIndex1 );
            }
            if( colIndex2 < 0 || colIndex2 >= this.Columns.Count )
            {
                throw new ArgumentOutOfRangeException("colIndex2=" + colIndex2 );
            }

            DomElementList cells = GetRange(
                Math.Min( rowIndex1 , rowIndex2 ) ,
                Math.Min( colIndex1 , colIndex2 ) ,
                Math.Abs( rowIndex1 - rowIndex2 ) +1 ,
                Math.Abs( colIndex1 - colIndex2 ) +1, 
                true );

            for( int iCount = 0 ; iCount < cells.Count ; iCount ++ )
            {
                DomTableCellElement cell = ( DomTableCellElement ) cells[iCount];    
                if( cell.IsOverrided )
                {
                    cell = cell.OverrideCell ;
                }
                rowIndex1 = Math.Min(rowIndex1, cell.RowIndex);
                rowIndex2 = Math.Max(rowIndex2, cell.RowIndex + cell.RowSpan - 1 );
                colIndex1 = Math.Min(colIndex1, cell.ColIndex);
                colIndex2 = Math.Max(colIndex2, cell.ColIndex + cell.ColSpan - 1);
            }//for

            cells = GetRange(
                rowIndex1,
                colIndex1,
                rowIndex2 - rowIndex1 + 1,
                colIndex2 - colIndex1 + 1,
                true );
            
            return cells;
        }

        #region 为编辑提供支持 ******************************************************************

        /// <summary>
        /// 表格的宽度
        /// </summary>
        [Browsable( false )]
        public float TableWidth
        {
            get
            {
                float oldWidth = 0;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    if (col.Visible)
                    {
                        oldWidth = oldWidth + col.Width;
                    }
                }
                return oldWidth;
            }
        }

        /// <summary>
        /// 设置表格的宽度
        /// </summary>
        /// <param name="newWidth">新宽度</param>
        public bool SetTableWidth(float newWidth)
        {
            if (newWidth <= 0)
            {
                throw new ArgumentOutOfRangeException("NewWidth=" + newWidth);
            }
            float oldWidth = 0;
            int visibleCount = 0;
            foreach (DomTableColumnElement col in this.Columns)
            {
                if (col.Visible)
                {
                    oldWidth = oldWidth + col.Width;
                    visibleCount++;
                }
            }
            bool result = false;
            if (oldWidth != newWidth && oldWidth > 0 )
            {
                float rate = newWidth / oldWidth;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    if (col.Visible)
                    {
                        col.Width = col.Width * rate;
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 在编辑器中插入表格行
        /// </summary>
        /// <param name="index">新表格行的序号</param>
        /// <param name="newRows">新表格行对象</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="specifyRowSpan">用户指定的单元格跨行数</param>
        internal void EditorInsertRows(
            int index,
            DomElementList  newRows ,
            bool logUndo ,
            Dictionary<DomTableCellElement , int >specifyRowSpan )
        {
            // 检查参数
            if (newRows == null || newRows.Count == 0 )
            {
                throw new ArgumentNullException("newRows");
            }
            
            if (index < 0 || index > this.Rows.Count)
            {
                throw new ArgumentOutOfRangeException("index=" + index);
            }
            foreach (DomTableRowElement row in newRows)
            {
                if (this.Rows.Contains(row))
                {
                    throw new ArgumentException("row existed in table");
                }
            }

            XTextUndoInsertRows undo = null;
            if (logUndo)
            {
                undo = new XTextUndoInsertRows(this, newRows, index);
            }

            for( int iCount = 0 ; iCount < newRows.Count ; iCount ++ )
            {
                DomTableRowElement row = (DomTableRowElement)newRows[iCount];
                // 设置新行的状态
                row.Parent = this;
                row.OwnerDocument = this.OwnerDocument;
                // 将新表格行插入的表格中
                this.Rows.Insert(index + iCount , row);
                // 更新单元格的内容列表
                foreach (DomTableCellElement cell in row.Cells)
                {
                    cell.UpdateContentElements(false);
                    cell.ExecuteLayout();
                }
            }
            // 更新表格内容版本号
            this.UpdateContentVersion();
            // 更新涉及到的合并单元格的跨行数
            if (specifyRowSpan != null && specifyRowSpan.Count > 0)
            {
                // 设置用户指定的单元格跨行数
                foreach (DomTableCellElement cell in specifyRowSpan.Keys)
                {
                    int newRowSpan = specifyRowSpan[cell];
                    if (cell.RowSpan != newRowSpan)
                    {
                        cell.InternalSetRowSpan( newRowSpan );
                        cell.UpdateContentVersion();
                    }
                }
            }
            else
            {
                // 自动计算
                for (int iCount = 0; iCount < index; iCount++)
                {
                    DomTableRowElement row = (DomTableRowElement)this.Rows[iCount];
                    foreach (DomTableCellElement cell in row.Cells)
                    {
                        if (cell.IsOverrided == false
                            && cell.RowSpan > 1
                            && cell.RowIndex + cell.RowSpan + 1 > index)
                        {
                            int newRowSpan = cell.RowSpan + newRows.Count;
                            if (undo != null)
                            {
                                // 记录撤销信息
                                undo.OldRowSpan[cell] = cell.RowSpan;
                                undo.NewRowSpan[cell] = newRowSpan;
                            }
                            // 增加单元格的跨行数
                            if (cell.RowSpan != newRowSpan)
                            {
                                cell.InternalSetRowSpan(newRowSpan);
                                cell.UpdateContentVersion();
                            }
                        }
                    }//foreach
                }
            }//foreach
            this._RuntimeRows = null;
            this.ExecuteLayout();
            this.ContentElement.UpdateContentElements(true);
            this.DocumentContentElement.RefreshGlobalLines();
            this.InvalidateView();
            this.OwnerDocument.Modified = true;
            this.UpdatePagesForTable( true );
            
            DomContentElement ce = this.ContentElement;
            //ce.UpdateContentElements(true);
            // 设置新的插入点的位置
            DomTableCellElement firstCell = ( DomTableCellElement ) newRows[0].Elements[0];
            if (firstCell.IsOverrided)
            {
                firstCell = firstCell.OverrideCell;
            }
            this.DocumentContentElement.SetSelection(
                firstCell.PrivateContent[0].ViewIndex, 0);
            if (logUndo && this.OwnerDocument.BeginLogUndo())
            {
                // 登记撤销操作信息
                this.OwnerDocument.UndoList.Add(undo);
                this.OwnerDocument.EndLogUndo();
                this.OwnerDocument.OnSelectionChanged();
                this.OwnerDocument.OnDocumentContentChanged();
            }
        }

        /// <summary>
        /// 在编辑器中删除多个连续的表格行
        /// </summary>
        /// <param name="startRowIndex">开始行号</param>
        /// <param name="rowsCount">要删除的行数</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="specifyRowSpan">用户指定的单元格跨行数修正值</param>
        internal bool EditorDeleteRows( 
            int startRowIndex ,
            int rowsCount ,
            bool logUndo ,
            Dictionary<DomTableCellElement , int > specifyRowSpan )
        {
            if (startRowIndex < 0 || startRowIndex > this.Rows.Count)
            {
                throw new ArgumentOutOfRangeException("startRowIndex=" + startRowIndex);
            }
            if (rowsCount < 0)
            {
                throw new ArgumentOutOfRangeException("count=" + rowsCount);
            }
            if (startRowIndex + rowsCount > this.Rows.Count)
            {
                throw new ArgumentOutOfRangeException("startRowIndex + count=" 
                    + Convert.ToString(startRowIndex + rowsCount));
            }

            XTextUndoDeleteRows undo = null;
            if (logUndo)
            {
                undo = new XTextUndoDeleteRows(this, startRowIndex, rowsCount);
            }
            this.InvalidateView();
            // 删除相关的高亮度显示区域
            for (int iCount = 0; iCount < rowsCount; iCount++)
            {
                this.OwnerDocument.HighlightManager.Remove(this.Rows[startRowIndex + iCount]);
            }
            // 删除多个表格行
            this.Rows.RemoveRange(startRowIndex, rowsCount);
            //更新内容版本号
            this.UpdateContentVersion();
            // 更新涉及到的合并单元格的跨行数
            if (specifyRowSpan != null && specifyRowSpan.Count > 0)
            {
                // 设置用户指定的跨行数
                foreach (DomTableCellElement cell in specifyRowSpan.Keys)
                {
                    cell.InternalSetRowSpan(specifyRowSpan[cell]);
                }
            }
            else
            {
                // 自动计算
                for (int iCount = 0; iCount < startRowIndex; iCount++)
                {
                    DomTableRowElement row = (DomTableRowElement)this.Rows[iCount];
                    foreach (DomTableCellElement cell in row.Cells)
                    {
                        if (cell.IsOverrided == false
                            && cell.RowSpan > 1
                            && cell.RowIndex + cell.RowSpan + 1 > startRowIndex)
                        {
                            // 计算新的跨行数
                            int newRowSpan = cell.RowSpan - rowsCount;
                            if (newRowSpan < startRowIndex - iCount)
                            {
                                newRowSpan = startRowIndex - iCount;
                            }
                            if (undo != null)
                            {
                                // 记录撤销操作信息
                                undo.OldRowSpan[cell] = cell.RowSpan;
                                undo.NewRowSpan[cell] = newRowSpan;
                            }
                            // 设置新的跨行数
                            cell.InternalSetRowSpan(newRowSpan);
                        }
                    }//for
                }//for
            }
            this._RuntimeRows = null;
            this.ExecuteLayout();
            this.InvalidateView();
            this.OwnerDocument.Modified = true;
            this.ContentElement.UpdateContentElements(true);
            this.DocumentContentElement.RefreshGlobalLines();
            this.UpdatePagesForTable( true );
            // 设置当前插入点的位置
            if (startRowIndex > this.Rows.Count)
            {
                startRowIndex = this.Rows.Count - 1;
            }
            //设置新的插入点的位置
            DomTableCellElement cell2 = GetCell(Math.Max( 0 , startRowIndex - 1 ), 0, false);
            if (cell2.IsOverrided)
            {
                cell2 = cell2.OverrideCell;
            }
            this.DocumentContentElement.Content.LineEndFlag = false;
            this.DocumentContentElement.SetSelection(cell2.PrivateContent[0].ViewIndex, 0);
            if (logUndo && this.OwnerDocument.BeginLogUndo())
            {
                // 登记撤销操作信息
                this.OwnerDocument.UndoList.Add(undo);
                this.OwnerDocument.EndLogUndo();
                this.OwnerDocument.OnSelectionChanged();
                this.OwnerDocument.OnDocumentContentChanged();
            }
            return true;
        }
 
        /// <summary>
        /// 在编辑器中插入多个表格列
        /// </summary>
        /// <param name="index">插入的表格列的开始序号</param>
        /// <param name="newColumns">新表格列对象</param>
        /// <param name="newCells">指定的新表格列对应的单元格对象</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <param name="specifyColSpan">用户指定的新跨列数</param>
        /// <param name="keepTableWidth"> 保持表格宽度</param>
        internal void EditorInsertColumns(
            int index,
            DomElementList newColumns,
            DomElementList newCells ,
            bool logUndo,
            Dictionary<DomTableCellElement , int > specifyColSpan ,
            bool keepTableWidth ,
            Dictionary<DomTableColumnElement , float > specifyColumWidths )
        {
            // 检查参数
            if (index < 0 || index > this.Columns.Count)
            {
                throw new ArgumentOutOfRangeException("index=" + index);
            }

            if (newColumns == null || newColumns.Count == 0 )
            {
                throw new ArgumentNullException("newColumns");
            }
            // 获得旧的表格宽度
            float oldTableWidth = 0;
            foreach (DomTableColumnElement col in this.Columns)
            {
                oldTableWidth = oldTableWidth + col.Width;
            }
            foreach (DomTableColumnElement col in newColumns)
            {
                if (this.Columns.Contains(col))
                {
                    throw new ArgumentException("col existed in table");
                }//if
                // 修正表格列宽度
                if (col.Width < this.OwnerDocument.Options.ViewOptions.MinTableColumnWidth)
                {
                    col.Width = this.OwnerDocument.Options.ViewOptions.MinTableColumnWidth;
                }
                col.Parent = this;
            }//foreach

            XTextUndoInsertColumns undo = null;
            if ( logUndo )
            {
                undo = new XTextUndoInsertColumns(this, newColumns, index , keepTableWidth );
                undo.OldColumnWidths = undo.GetColumnWidths(this);
            }

            this.FixCells();
            // 插入表格列对象
            this.Columns.InsertRange(index, newColumns);
            // 更新表格内容版本号
            this.UpdateContentVersion();
            if (newCells != null && newCells.Count == this.Rows.Count * newColumns.Count)
            {
                // 方法参数提供了现成的单元格对象
                for( int iCount = 0 ; iCount < this.Rows.Count ; iCount ++ )
                {
                    DomTableRowElement row = (DomTableRowElement)this.Rows[iCount];
                    for (int iCount2 = 0; iCount2 < newColumns.Count; iCount2++)
                    {
                        //将现成的单元格对象插入到各个表格行中
                        DomTableCellElement cell = (DomTableCellElement)newCells[
                            iCount * newColumns.Count + iCount2];
                        cell.Parent = row;
                        cell.FixElements();
                        row.Cells.Insert(index + iCount2, cell);
                    }//for
                }//for
            }
            else
            {
                //自动为新增表格列创建单元格
                using (System.Drawing.Graphics g = this.OwnerDocument.CreateGraphics())
                {
                    foreach (DomTableRowElement row in this.Rows)
                    {
                        DomTableCellElement preCell = null;
                        if (index == 0)
                        {
                            preCell = (DomTableCellElement)row.Cells[index];
                        }
                        else
                        {
                            preCell = (DomTableCellElement)row.Cells[index - 1];
                        }
                        if (preCell.IsOverrided)
                        {
                            preCell = preCell.OverrideCell;
                        }
                        foreach (DomTableColumnElement col in newColumns)
                        {
                            // 为新增的表格列添加相应的单元格对象
                            DomTableCellElement cell = this.CreateCellInstance();
                            cell.FixElements();
                            cell.Parent = row;
                            cell.StyleIndex = preCell.StyleIndex;
                            row.Cells.Insert(index, cell);
                            DocumentPaintEventArgs args = new DocumentPaintEventArgs(g, Rectangle.Empty);
                            args.Document = this.OwnerDocument;
                            args.Render = this.OwnerDocument.Render;
                            args.Element = cell;
                            cell.RefreshSize(args);
                            //this.OwnerDocument.Render.RefreshSize(cell , g );
                            //newCells.Add(cell);
                        }//foreach
                    }//foreach
                }//using
            }

            // 修正涉及到的横向合并单元格的跨列数
            if (specifyColSpan != null && specifyColSpan.Count > 0)
            {
                // 设置用户指定的跨列数
                foreach (DomTableCellElement cell in specifyColSpan.Keys)
                {
                    cell.InternalSetColSpan(specifyColSpan[cell]);
                }
            }
            else
            {
                // 自动计算
                foreach (DomTableCellElement cell in this.Cells)
                {
                    if (cell.IsOverrided == false
                        && cell.ColSpan > 1
                        && cell.ColIndex + cell.ColSpan - 1 >= index)
                    {
                        int newColSpan = cell.ColSpan + newColumns.Count;
                        if (undo != null)
                        {
                            undo.OldColSpan[cell] = cell.ColSpan;
                            undo.NewColSpan[cell] = newColSpan;
                        }
                        cell.InternalSetColSpan(newColSpan);
                    }
                }//for
            }
            // 重新排版
            if (specifyColumWidths != null && specifyColumWidths.Count > 0)
            {
                // 用户指定的表格列的宽度
                foreach (DomTableColumnElement col in this.Columns)
                {
                    if (specifyColumWidths.ContainsKey(col))
                    {
                        col.Width = specifyColumWidths[col];
                    }
                }
            }
            else if (keepTableWidth)
            {
                // 保持表格总体宽度，因此需要调整各个表格列的宽度
                float newTableWidth = 0;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    newTableWidth = newTableWidth + col.Width;
                }
                float rate = oldTableWidth / newTableWidth;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    col.Width = col.Width * rate;
                }
            }
            this.ExecuteLayout();
            this.ContentElement.UpdateContentElements(true);
            this.DocumentContentElement.RefreshGlobalLines();
            this.InvalidateView();
            this.OwnerDocument.Modified = true;
            this.UpdatePagesForTable( false );
            DomTableCellElement cell2 = GetCell(0, index, false );
            // 设置新的插入点位置
            if (cell2.IsOverrided)
            {
                cell2 = cell2.OverrideCell;
            }
            this.DocumentContentElement.SetSelection(cell2.PrivateContent[0].ViewIndex, 0);
            if (logUndo && this.OwnerDocument.BeginLogUndo())
            {
                undo.NewColumnWidths = undo.GetColumnWidths(this);
                // 记录撤销操作信息
                this.OwnerDocument.UndoList.Add(undo);
                this.OwnerDocument.EndLogUndo();
                this.OwnerDocument.OnSelectionChanged();
                this.OwnerDocument.OnDocumentContentChanged();
            }
        }

        /// <summary>
        /// 在编辑器中删除多个连续的表格列对象
        /// </summary>
        /// <param name="startColumnIndex">要删除的表格列的开始序号</param>
        /// <param name="colCount">表格列个数</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        internal bool EditorDeleteColumns(
            int startColumnIndex ,
            int colCount ,
            bool logUndo,
            Dictionary<DomTableCellElement , int > specifyColSpan,
            bool keepTableWidth ,
            Dictionary<DomTableColumnElement , float > specifyColumnWidths )
        {
            // 检查参数
            if (startColumnIndex < 0 || startColumnIndex >= this.Columns.Count)
            {
                throw new ArgumentOutOfRangeException("startColumnIndex=" + startColumnIndex);
            }
            if (colCount <= 0)
            {
                throw new ArgumentOutOfRangeException("colCount=" + colCount);
            }
            if (startColumnIndex + colCount > this.Columns.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex+Count="
                    + Convert.ToString(startColumnIndex + colCount));
            }
            
            // 获得旧的表格宽度
            float oldTableWidth = 0;
            foreach (DomTableColumnElement col in this.Columns)
            {
                oldTableWidth = oldTableWidth + col.Width;
            }

            XTextUndoDeleteColumns undo = null;
            if (logUndo)
            {
                undo = new XTextUndoDeleteColumns(this, startColumnIndex, colCount , keepTableWidth );
                undo.OldColumnWidths = undo.GetColumnWidths(this);
            }
            // 删除表格列
            this.Columns.RemoveRange(startColumnIndex, colCount);
            // 更新元素内容版本号
            this.UpdateContentVersion();
            // 删除表格列对应的单元格对象
            foreach (DomTableRowElement row in this.Rows)
            {
                if (row.Cells.Count > startColumnIndex)
                {
                    for (int iCount = 0; iCount < colCount; iCount++)
                    {
                        // 记录被删除的单元格对象
                        DomElement cell = row.Cells[startColumnIndex + iCount];
                        if (undo != null)
                        {
                            undo.RemovedCells.Add(row.Cells[startColumnIndex + iCount]);
                        }
                        this.OwnerDocument.HighlightManager.Remove(cell);
                    }
                    // 删除表格列下的单元格对象
                    row.Cells.RemoveRange(startColumnIndex, colCount);
                    // 修正涉及到本操作的合并单元格的跨列数
                    if (specifyColSpan != null && specifyColSpan.Count > 0)
                    {
                        // 用户指定了合并单元格的跨列数
                        foreach (DomTableCellElement cell in specifyColSpan.Keys)
                        {
                            cell.InternalSetColSpan(specifyColSpan[cell]);
                        }
                    }
                    else
                    {
                        // 自动修正跨列数
                        for (int iCount = 0; iCount < startColumnIndex; iCount++)
                        {
                            DomTableCellElement cell = (DomTableCellElement)row.Cells[iCount];
                            if (cell.IsOverrided == false
                                && cell.ColSpan > 1
                                && cell.ColIndex + cell.ColSpan - 1 > startColumnIndex)
                            {
                                int newColSpan = cell.ColSpan - colCount;
                                if (newColSpan < startColumnIndex - iCount)
                                {
                                    newColSpan = startColumnIndex - iCount;
                                }
                                if (undo != null)
                                {
                                    // 记录撤销操作信息
                                    undo.OldColSpan[cell] = cell.ColSpan;
                                    undo.NewColSpan[cell] = newColSpan;
                                }
                                cell.InternalSetColSpan(newColSpan);
                            }
                        }//for
                    }
                }//if
            }//foreach
            // 重新排版
            if (specifyColumnWidths != null && specifyColumnWidths.Count > 0)
            {
                foreach (DomTableColumnElement col in this.Columns)
                {
                    if (specifyColumnWidths.ContainsKey(col))
                    {
                        col.Width = specifyColumnWidths[col];
                    }
                }
            }
            else if (keepTableWidth)
            {
                // 保持表格总体宽度，因此需要调整各个表格列的宽度
                float newTableWidth = 0;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    newTableWidth = newTableWidth + col.Width;
                }
                float rate = oldTableWidth / newTableWidth;
                foreach (DomTableColumnElement col in this.Columns)
                {
                    col.Width = col.Width * rate;
                }
            }
            this.InvalidateView();
            this.ExecuteLayout();
            this.InvalidateView();
            this.ContentElement.UpdateContentElements(true);
            this.DocumentContentElement.RefreshGlobalLines(); 
            this.UpdatePagesForTable( false );
            this.OwnerDocument.Modified = true;
            // 设置新的插入点位置
            DomTableCellElement cell2 = GetCell(
                0, 
                startColumnIndex >= this.Columns.Count ? startColumnIndex - 1 : startColumnIndex , 
                false);
            if (cell2.IsOverrided )
            {
                cell2 = cell2.OverrideCell;
            }
            this.DocumentContentElement.SetSelection(cell2.PrivateContent[0].ViewIndex, 0);
            if (logUndo && this.OwnerDocument.BeginLogUndo())
            {
                // 记录撤销操作信息
                undo.NewColumnWidths = undo.GetColumnWidths(this);
                this.OwnerDocument.UndoList.Add(undo);
                this.OwnerDocument.EndLogUndo();
                this.OwnerDocument.OnSelectionChanged();
                this.OwnerDocument.OnDocumentContentChanged();
            }
            return true ;
        }

        /// <summary>
        /// 设置表格标题行样式
        /// </summary>
        /// <param name="headerStyles">新的标题行样式</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <returns>操作是否修改了文档内容</returns>
        internal bool EditorSetHeaderRow(Dictionary<DomTableRowElement, bool> headerStyles, bool logUndo)
        {
            if (headerStyles == null || headerStyles.Count == 0)
            {
                return false ;
            }
            bool modified = false;
            XTextUndoHeaderRow undo = new XTextUndoHeaderRow();
            undo.Table = this;
            foreach (DomTableRowElement row in headerStyles.Keys)
            {
                if (row.HeaderStyle == headerStyles[row])
                {
                    continue;
                }
                modified = true;
                undo.OldHeaderStyles[row] = row.HeaderStyle;
                undo.NewHeaderStyles[row] = headerStyles[row];
                row.HeaderStyle = headerStyles[row];
            }//foreach
            if (modified)
            {
                this.UpdateContentVersion();
                this.OwnerDocument.Modified = true;
                if (logUndo && this.OwnerDocument.CanLogUndo)
                {
                    this.OwnerDocument.UndoList.Add(undo);
                }
                if (this.OwnerDocument.EditorControl != null)
                {
                    if (this.OwnerDocument.EditorControl.ViewMode == PageViewMode.Normal)
                    {
                        // 当文档处于普通显示模式（非分页模式），则不刷新文档视图。
                        
                        // 触发文档内容发生改变事件
                        ContentChangedEventArgs args = new ContentChangedEventArgs();
                        args.Element = this;
                        this.RaiseBubbleOnContentChanged(args);

                        return modified;
                    }
                }
                this._RuntimeRows = null;
                this.UpdateRowPosition();
                this.UpdatePagesForTable(true);
                // 触发文档内容发生改变事件
                ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                args2.Element = this;
                this.RaiseBubbleOnContentChanged(args2);
            }
            return modified;
        }
        #endregion

        /// <summary>
        /// 创建一个新的单元格对象
        /// </summary>
        /// <remarks>
        /// 从XDesignerLib上派生的设计器中可以实现扩展表格模型,实现自己的
        /// 表格单元格元素类型,此时需要重载该函数来返回扩展的表格单元格
        /// 对象实例.
        /// </remarks>
        /// <returns>新的单元格对象</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public virtual DomTableCellElement CreateCellInstance()
        {
            return new DomTableCellElement();
        }

        /// <summary>
        /// 创建一个新的表格列对象
        /// </summary>
        /// <remarks>
        /// 从XDesignerLib上派生的设计器中可以实现扩展表格模型,实现自己的
        /// 表格单元格元素类型,此时需要重载该函数来返回扩展的表格列
        /// 对象实例.
        /// </remarks>
        /// <returns>新的表格列对象</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public virtual DomTableColumnElement CreateColumnInstance()
        {
            return new DomTableColumnElement();
        }
        /// <summary>
        /// 创建一个新的表格行对象
        /// </summary>
        /// <remarks>
        /// 从XDesignerLib上派生的设计器中可以实现扩展表格模型,实现自己的
        /// 表格单元格元素类型,此时需要重载该函数来返回扩展的表格行
        /// 对象实例.
        /// </remarks>
        /// <returns>新的表格行对象</returns>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public virtual DomTableRowElement CreateRowInstance()
        {
            return new DomTableRowElement();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="Deeply">是否复制子孙节点</param>
        /// <returns>复制品</returns>
        public override DomElement Clone(bool Deeply)
        {
            DomTableElement table = (DomTableElement)base.Clone(Deeply);
            if (Deeply)
            {
                table._Columns = new DomElementList();
                foreach (DomTableColumnElement col in this.Columns)
                {
                    DomTableColumnElement newCol = (DomTableColumnElement)col.Clone(true);
                    newCol.Parent = this;
                    table._Columns.Add(newCol);
                }
            }
            else
            {
                table._Columns = new DomElementList();
            }
            return table;
        }

        #region 输出表格内容到RTF文档 ******************************************************

        private bool CheckClipRectangle(DomTableRowElement row,
            System.Drawing.RectangleF ClipRectangle)
        {
            if (ClipRectangle.IsEmpty)
            {
                return true;
            }
            System.Drawing.RectangleF rect = row.AbsBounds;
            //rect.Height = row.Height ;
            System.Drawing.RectangleF vrect = RectangleF.Intersect(ClipRectangle, rect);
            if (!vrect.IsEmpty && vrect.Height > 4)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得被选中的表格行和单元格对象列表
        /// </summary>
        /// <param name="selectionRows">被选中的表格行列表</param>
        /// <param name="selectionCells">被选中的表格单元格列表</param>
        private void GetSelectionRowsCells(
            out DomElementList selectionRows,
            out DomElementList selectionCells)
        {
            // 只输出被选择区域
            // 需要输出的表格行列表
            selectionRows = new DomElementList();
            // 需要输出的单元格列表
            selectionCells = new DomElementList();
            DomDocumentContentElement dce = this.DocumentContentElement;
            if (dce.Selection.Cells != null)
            {
                foreach (DomTableCellElement cell in dce.Selection.Cells)
                {
                    if (cell.OwnerTable == this)
                    {
                        if (selectionRows.Contains(cell.OwnerRow) == false)
                        {
                            selectionRows.Add(cell.OwnerRow);
                        }

                        selectionCells.Add(cell);
                        if (cell.RowSpan > 1 || cell.ColSpan > 1)
                        {
                            
                        }
                    }
                }//foreach
            }//if
        }

        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            // 计算表格套嵌层数,若大于1则发生的表格套表格
            int NestTableLevel = 1;
            DomElement parent = this.Parent;
            while (parent != null)
            {
                if (parent is DomTableCellElement)
                {
                    NestTableLevel++;
                }
                parent = parent.Parent;
            }

            int OutputRowCount = 0;
            //bool RowGroup = false;

            //writer.WriteKeyword("pard");
            if (writer.LastOutputElement != null)
            {
                if (writer.LastOutputElement is DomTableElement)
                {
                }
                else
                {
                    writer.Writer.WriteKeyword("par");
                }
            }
            //writer.WriteKeyword("par");
            writer.Writer.WriteStartGroup();
            DomTableRowElement PreRow = null;
            DomElementList outputRows = null;
            DomElementList outputCells = null;
            DomDocumentContentElement dce = this.DocumentContentElement;
            if (writer.IncludeSelectionOnly)
            {
                GetSelectionRowsCells(out outputRows, out outputCells);
            }
            else
            {
                outputRows = this.Rows;
            }
            foreach (DomTableRowElement row in outputRows )
            {
                if (row.TemporaryHeaderRow)
                {
                    // 表格行是临时创建的标题表格行的复制品则不输出
                    continue;
                }

                if (CheckClipRectangle(row, writer.ClipRectangle) == false)
                {
                    continue;
                }

                bool isLastRow = ( outputRows.LastElement == row );
                DomElementList rowCells = row.Cells;
                if (writer.IncludeSelectionOnly)
                {
                    // 获得当前表格行要输出的单元格列表
                    rowCells = new DomElementList();
                    foreach (DomTableCellElement cell in outputCells)
                    {
                        if (cell.OwnerRow == row)
                        {
                            rowCells.Add(cell);
                        }
                    }
                }
                writer.Writer.WriteRaw(System.Environment.NewLine);
                //if (row.NewPage)
                //{
                //    if (PreRow != null)
                //    {
                //        writer.Writer.WriteEndGroup();
                //        writer.Writer.WriteStartGroup();
                //        writer.Writer.WriteKeyword("pard");
                //        writer.Writer.WriteKeyword("page");
                //        writer.Writer.WriteKeyword("par");
                //        writer.Writer.WriteEndGroup();
                //        writer.Writer.Writer.WriteRaw(System.Environment.NewLine);
                //        writer.Writer.WriteStartGroup();
                //    }
                //}
                OutputRowCount++;

                if (NestTableLevel > 1)
                {
                    // 输出套嵌表格
                    for (int iCount = 0; iCount < rowCells.Count; iCount++)
                    {
                        DomTableCellElement cell = (DomTableCellElement)rowCells[iCount];
                        if (cell.Visible)
                        {
                            //writer.WriteKeyword("par");
                            //writer.WriteKeyword("pard");
                            writer.WriteKeyword("intbl");
                            writer.WriteKeyword("itap" + NestTableLevel);
                            cell.WriteRTF(writer);
                            writer.WriteKeyword("nestcell");
                        }
                        else
                        {
                            if (cell.OverrideCell.OwnerRow != cell.OwnerRow)
                            {
                                writer.WriteKeyword("nestcell");
                                iCount = iCount + cell.OverrideCell.ColSpan - 1;
                            }
                        }
                    }
                    // 输出套嵌表格行的样式
                    writer.Writer.WriteStartGroup();
                    writer.WriteKeyword("nesttableprops");
                    writer.WriteKeyword("trowd");
                    WriteRowRTFControlWord(row, writer , rowCells , isLastRow );
                    writer.WriteKeyword("nestrow");
                    writer.Writer.WriteEndGroup();
                }
                else
                {
                    // 正常的输出表格行
                    writer.WriteKeyword("trowd");
                    WriteRowRTFControlWord(row, writer , rowCells , isLastRow );
                    // 输出表格内容
                    //writer.Writer.WriteStartGroup();
                    for (int iCount = 0; iCount < rowCells.Count; iCount++)
                    {
                        DomTableCellElement cell = (DomTableCellElement)rowCells[iCount];
                        
                        if (cell.Visible)
                        {
                            writer.WriteKeyword("intbl");
                            cell.WriteRTF(writer);
                            writer.WriteKeyword("cell");
                        }
                        else
                        {
                            if (cell.OverrideCell.OwnerRow != cell.OwnerRow)
                            {
                                writer.WriteKeyword("cell");
                                iCount = iCount + cell.OverrideCell.ColSpan - 1;
                            }
                        }
                    }
                    writer.WriteKeyword("row");
                }
                PreRow = row;
                //writer.Writer.WriteEndGroup();
            }//foreach

            writer.Writer.WriteEndGroup();
            writer.WriteKeyword("pard");
         
            writer.LastOutputElement = this;
        }

        /// <summary>
        /// 输出表格行设置
        /// </summary>
        /// <param name="row">表格行对象</param>
        /// <param name="writer">RTF文档书写器</param>
        private void WriteRowRTFControlWord(
            DomTableRowElement row,
            DCSoft.CSharpWriter.RTF.RTFContentWriter writer,
            DomElementList rowCells,
            bool isLastRow 
            )
        {
            writer.Writer.WriteKeyword("trgaph108");

            writer.Writer.WriteKeyword("trautofit1");

            //if (row.OutputHeightToHtml)
            {
                // 为Word正确的显示表格行的高度而进行额外的调整,Word好像对每个表格行多高了12.5个Document高度,在此进行修正.
                if (row.SpecifyHeight != 0)
                {
                    writer.Writer.WriteKeyword("trrh" + writer.ToTwips((int)(row.SpecifyHeight - 12.5)));
                }
            }
            //writer.Writer.WriteKeyword("trrh" + writer.ToTwips( - ( int ) ( row.Height )));
            //writer.Writer.WriteKeyword("trrh" + writer.ToTwips( ( int ) ( row.Height * 0.7625 ) ));

            // 累计计算行宽度
            float rowWidth = 0;
            foreach (DomTableCellElement cell in rowCells)
            {
                rowWidth = rowWidth + cell.Width;
            }
            // 计算单元格左端位置的修正量
            float cellLeftFix = 0;
            DomTableCellElement firstCell = (DomTableCellElement)rowCells[0];
            for (int iCount = 0; iCount < firstCell.ColIndex; iCount++)
            {
                cellLeftFix = cellLeftFix + this.Columns[iCount].Width;
            }

            writer.Writer.WriteKeyword("trwWidth" + writer.ToTwips(rowWidth));

            if (row.HeaderStyle)
            {
                // 标题行
                writer.WriteKeyword("trhdr");
            }
            // 判断本表格行是否是输出的最后一行
            //bool bolLastRow = true;
            //for (int iCount = this.Rows.IndexOf(row) + 1; iCount < this.Rows.Count; iCount++)
            //{
            //    if (writer.IsVisible(this.Rows[iCount]))
            //    {
            //        bolLastRow = false;
            //        break;
            //    }
            //}//if
            if (isLastRow )
            {
                // 输出最后一行标志
                writer.WriteKeyword("lastrow");
            }

            // 输出单元格设置
            float cellX = 0;
            for (int iCount = 0; iCount < rowCells.Count; iCount++)
            {
                DomTableCellElement cell = (DomTableCellElement)rowCells[iCount];

                writer.Writer.Writer.WriteRaw(System.Environment.NewLine);

                // 输出合并单元格信息
                if (cell.Visible)
                {
                    if (cell.RowSpan > 1)
                    {
                        writer.WriteKeyword("clvmgf");
                    }
                }
                else
                {
                    if (cell.OverrideCell.OwnerRow == cell.OwnerRow)
                    {
                        // 被同表格行的其他单元格合并则不输出本单元格的任何内容
                        continue;
                    }
                    else
                    {
                        writer.WriteKeyword("clvmrg");
                        WriteRTFCellBorder(cell, writer);
                        writer.WriteKeyword("cellx" + writer.ToTwips(cell.Left - cellLeftFix + cell.OverrideCell.Width));
                        iCount = iCount + cell.OverrideCell.ColSpan - 1;
                        continue;
                    }
                }

                WriteRTFCellBorder(cell, writer);

                // 输出文本对齐方式
                if (cell.Visible)
                {
                    DocumentContentStyle rs = cell.RuntimeStyle;

                    //string txt = cell.DisplayText;
                    //if (txt != null && txt.Length > 0)
                    {
                        // 无论什么时候都输出单元格文本对齐方式
                        //// 设置水平对齐方式
                        //if (rs.Align == DocumentContentAlignment.Center
                        //    || rs.Align == DocumentContentAlignment.Justify)
                        //{
                        //    writer.WriteCentered();
                        //}
                        //else if (rs.Align == DocumentContentAlignment.Right)
                        //{
                        //    writer.WriteRightAligned();
                        //}
                        //else
                        //{
                        //    writer.WriteLeftAligned();
                        //}

                        if (rs.VerticalAlign == VerticalAlignStyle.Top)
                        {
                            writer.Writer.WriteKeyword("clvertalt");
                        }
                        else if (rs.VerticalAlign == VerticalAlignStyle.Middle
                            || rs.VerticalAlign == VerticalAlignStyle.Justify)
                        {
                            writer.Writer.WriteKeyword("clvertalc");
                        }
                        else if (rs.VerticalAlign == VerticalAlignStyle.Bottom)
                        {
                            writer.Writer.WriteKeyword("clvertalb");
                        }
                    }

                    if (rs.Multiline == false)
                    {
                        writer.WriteKeyword("clNoWrap");
                    }
                    // 背景色
                    int index2 = writer.Writer.ColorTable.IndexOf(rs.BackgroundColor);
                    if (index2 >= 0)
                    {
                        writer.WriteKeyword("clcbpat" + Convert.ToString(index2 + 1));
                    }
                    
                    // 输出边距,（微软 MS Word 应当有一个BUG,导致 clpadl 和 clpadt 作用互换）,此处暂不处理这点

                    writer.WriteKeyword("clpadl" + writer.ToTwips(rs.PaddingLeft));
                    writer.WriteKeyword("clpadfl3");

                    writer.WriteKeyword("clpadt" + writer.ToTwips(rs.PaddingTop));
                    writer.WriteKeyword("clpadft3");

                    writer.WriteKeyword("clpadr" + writer.ToTwips(rs.PaddingRight));
                    writer.WriteKeyword("clpadfr3");

                    writer.WriteKeyword("clpadb" + writer.ToTwips(rs.PaddingBottom));
                    writer.WriteKeyword("clpadfb3");

                }

                // 单元格宽度
                //writer.Writer.WriteKeyword("clftsWidth3");
                //writer.Writer.WriteKeyword("clwWidth" + writer.ToTwips( cell.Width ));
                cellX = cellX + cell.Width ;
                writer.Writer.WriteKeyword("cellx" + writer.ToTwips(cell.Left - cellLeftFix + cell.Width));

            }//for
        }

        private void WriteRTFCellBorder(DomTableCellElement cell, DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            DocumentContentStyle rs = cell.RuntimeStyle;
            // 输出边框
            if (rs.BorderWidth > 0 && rs.BorderColor.A != 0)
            {
                int index = writer.Writer.ColorTable.IndexOf(rs.BorderColor);
                int w = writer.ToTwips(rs.BorderWidth);
                if (w < 1)
                    w = 1;
                if (w > 75)
                    w = 75;
                //w = 15 ;
                if (rs.BorderLeft )
                {
                    writer.WriteKeyword("clbrdrl");
                    if (w != 1)
                    {
                        writer.WriteKeyword("brdrw" + w);
                    }
                    writer.Writer.WriteBorderLineDashStyle(rs.BorderStyle);
                }
                if (rs.BorderTop )
                {
                    writer.WriteKeyword("clbrdrt");
                    if (w != 1)
                    {
                        writer.WriteKeyword("brdrw" + w);
                    }
                    writer.Writer.WriteBorderLineDashStyle(rs.BorderStyle);
                }
                if (rs.BorderRight )
                {
                    writer.WriteKeyword("clbrdrr");
                    if (w != 1)
                    {
                        writer.WriteKeyword("brdrw" + w);
                    }
                    writer.Writer.WriteBorderLineDashStyle(rs.BorderStyle);
                }
                if (rs.BorderBottom )
                {
                    writer.WriteKeyword("clbrdrb");
                    if (w != 1)
                    {
                        writer.WriteKeyword("brdrw" + w);
                    }
                    writer.Writer.WriteBorderLineDashStyle(rs.BorderStyle);
                }
            }
        }

        #endregion

        public override void WriteHTML( WriterHtmlDocumentWriter writer )
        {
            writer.WriteStartElement("table");
            writer.WriteAttributeString("id", this.ID);
            writer.WriteAttributeString("cellpadding", "0");
            writer.WriteAttributeString("cellspacing", "0");
            //writer.WriteAttributeString("width", writer.ToPixelValue(this.Width));
            //writer.WriteAttributeString("width", this.PixelWidth.ToString());
            writer.WriteStartStyle();
            writer.WriteDocumentContentStyle(this.RuntimeStyle, this);
            writer.WriteStyleItem("border-collapse", "collapse");
            writer.WriteStyleItem("table-layout", "fixed");

            if (writer.BrowserStyle != XWebBrowsersStyle.InternetExplorer)
            {
                if (writer.BoxModel == XHtmlBoxModelStyle.Standard)
                {
                    int tblWidth = 0;
                    foreach ( DomTableColumnElement col in this.Columns)
                    {
                        if (col.Visible)
                        {
                            if ( this.Columns.LastElement == col && this.Parent is DomTableCellElement )
                            {
                                // 套嵌表格
                                tblWidth = tblWidth + ( writer.ToPixel( col.Width ) - 2);
                            }
                            else
                            {
                                tblWidth = tblWidth + writer.ToPixel(col.Width);
                            }
                        }
                    }
                    writer.WriteStyleItem("width", tblWidth + "px");
                }
            }

            writer.WriteEndStyle();

            #region colgroup

            //if ((writer.RuntimeBrowser == InternetBrowsers.InternetExplorer8
            //    && writer.DocumentSchema != HtmlDocumentSchema.None) == false)
            {

                // 输出所有可见的表格列对象
                //writer.WriteStartElement("colgroup");
                foreach (DomTableColumnElement col in this.Columns)
                {
                    if (col.Visible)
                    {
                        writer.WriteStartElement("col");
                        if (this.Columns.LastElement == col && this.Parent is DomTableCellElement)
                        {
                            writer.WriteAttributeString("style", "width:" + Convert.ToString(writer.ToPixel(col.Width) - 2) + "px");
                        }
                        else
                        {
                            writer.WriteAttributeString("style", "width:" + writer.ToPixel(col.Width));
                        }
                        writer.WriteEndElement();
                    }
                }
                //writer.WriteEndElement();
            }

            //int tdWidthStyle = 0;
            ////if (writer.WritingExcel == false)
            //{
            //    //if (writer.WebControl != null)
            //    //{
            //    if ((writer.RuntimeBrowser == WebBrowsersStyle.AppleMAC_Safari
            //        || writer.RuntimeBrowser == WebBrowsersStyle.Chrome
            //        || writer.RuntimeBrowser == WebBrowsersStyle.InternetExplorer
            //        )
            //        ||
            //        (writer.RuntimeBrowser == WebBrowsersStyle.InternetExplorer7
            //        && writer.DocumentSchema != HtmlDocumentSchema.None
            //        )
            //        ||
            //        (writer.RuntimeBrowser == WebBrowsersStyle.InternetExplorer8
            //        && writer.DocumentSchema != HtmlDocumentSchema.None)
            //        )
            //    {
            //        tdWidthStyle = 1;

            //        //foreach (DesignReportTableColumn col in this.myColumns)
            //        //{
            //        //    if (col.Visible)
            //        //    {
            //        //        writer.WriteStartElement("td");
            //        //        if (myColumns.LastElement == col && this.myParent is DesignTableCellElement)
            //        //            writer.WriteAttributeString("style", "width:" + Convert.ToString(col.PixelWidth - 2) + "px");
            //        //        else
            //        //            writer.WriteAttributeString("style", "width:" + writer.ToPixelValue(col.Width));
            //        //        writer.Writer.WriteRaw("&nbsp;");
            //        //        writer.WriteEndElement();
            //        //    }
            //        //}

            //    }
            //    //}
            //}

            if (writer.WritingExcelHtml == false)
            {
                //if (writer.WebControl != null)
                //{
                if ((
                        writer.BrowserStyle == XWebBrowsersStyle.AppleMAC_Safari
                    //|| writer.RuntimeBrowser == WebBrowsersStyle.Chrome
                        || writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer
                    )
                    ||
                        (writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer7
                        && writer.DocumentSchema != XHtmlDocumentSchema.None
                    )
                    //||
                    //    (writer.RuntimeBrowser == WebBrowsersStyle.InternetExplorer8
                    //    && writer.DocumentSchema != HtmlDocumentSchema.None)
                    )
                {
                    //writer.WriteStartElement("tr");
                    //if (writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer
                    //    || writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer7
                    //    //|| writer.RuntimeBrowser == WebBrowsersStyle.InternetExplorer8
                    //    )
                    //{
                    //    writer.WriteAttributeString("style", "display:none;");
                    //}
                    //else
                    //{
                    //    writer.WriteAttributeString("style", "height:0px;");
                    //}
                    //foreach (XTextTableColumnElement col in this.Columns)
                    //{
                    //    if (col.Visible)
                    //    {
                    //        writer.WriteStartElement("td");
                    //        if (this.Columns.LastElement == col && this.Parent is XTextTableCellElement)
                    //        {
                    //            writer.WriteAttributeString("style", "width:" + Convert.ToString(writer.ToPixel(col.Width) - 2) + "px");
                    //        }
                    //        else
                    //        {
                    //            writer.WriteAttributeString("style", "width:" + writer.ToPixel(col.Width));
                    //        }
                    //        writer.WriteRaw("&nbsp;");
                    //        writer.WriteEndElement();
                    //    }
                    //}
                    //writer.WriteEndElement();
                }
                //}
            }

            #endregion

            System.Drawing.Rectangle rect = Rectangle.Ceiling( this.AbsBounds );
            System.Collections.ArrayList OutputCells = new System.Collections.ArrayList();
            DomTableRowElement firstVisibleRow = null;
            DomElementList outputRows = this.Rows;
            DomElementList outputCells = this.Cells ;
            if (writer.IncludeSelectionOndly)
            {
                // 获得被选中的表格行和单元格
                GetSelectionRowsCells(out outputRows, out outputCells);
            }
            // 输出所有可见的表格行
            foreach (DomTableRowElement row in outputRows )
            {
                if (writer.IsVisible(row) == false)
                {
                    continue;
                }
                //if (row.TemporaryHeaderRow && writer.SplitPageStyle == false)
                //{
                //    // 不处于分页模式显示则不输出临时标题行
                //    continue;
                //}
                rect.Height = (int)row.Height;
                System.Drawing.Rectangle vrect = rect;
                int fix = 0;
                if (writer.ClipRectangle.IsEmpty == false)
                {
                    vrect = System.Drawing.Rectangle.Intersect(writer.ClipRectangle, rect);
                    if (vrect.Bottom != rect.Bottom)
                    {
                        fix = 3;
                    }
                }
                if (!vrect.IsEmpty && vrect.Height > 4)// writer.ClipRectangle.IsEmpty || writer.ClipRectangle.IntersectsWith( rect ))
                {
                    if (firstVisibleRow == null)
                    {
                        firstVisibleRow = row;
                    }

                    writer.WriteStartElement("tr");
                    if ( row.SpecifyHeight < -0.05 )
                    {

                        //在IE6.0中，表格行高度比其他浏览器要高一些
                        if (writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer 
                            && writer.BoxModel == XHtmlBoxModelStyle.Standard)
                        {
                            writer.WriteAttributeString("style", "height:" + writer.ToPixel(vrect.Height + fix - 23));
                        }

                        else if (writer.BrowserStyle == XWebBrowsersStyle.InternetExplorer7 
                            && writer.DocumentSchema != XHtmlDocumentSchema.None)
                        {
                            writer.WriteAttributeString("style", "height:" + writer.ToPixel(vrect.Height + fix - 23));
                        }
                        else
                        {
                            writer.WriteAttributeString("style", "height:" + writer.ToPixel(vrect.Height + fix));// row.PixelHeight + "px" );
                        }
                    }

                    // 输出所有可见的表格列
                    foreach (DomTableCellElement cell in row.Cells )
                    {
                        if (outputCells.Contains(cell) == false)
                        {
                            // 当前单元格不在输出的单元格之列，忽略掉。
                            continue;
                        }
                        //int runtimeRowSpan = writer.SplitPageStyle ? cell.RuntimeRowSpan : cell.RowSpan ;
                        //int runtimeRowSpan = cell.RuntimeRowSpan ;
                        if (cell.Visible)
                        {
                            if (writer.ViewStyle == WriterHtmlViewStyle.Page)
                            {
                                cell.WriteHTMLRowSpan(writer, cell.RuntimeRowSpan);
                            }
                            else
                            {
                                cell.WriteHTMLRowSpan(writer, cell.RowSpan);
                            }
                            OutputCells.Add(cell);
                        }
                        else
                        {
                            DomTableCellElement cell2 = (DomTableCellElement)cell.OverrideCell;
                            if (cell2 != null && cell2.Visible && OutputCells.Contains(cell2) == false)
                            {
                                OutputCells.Add(cell2);
                                int rowspan = 0;
                                if (writer.ViewStyle == WriterHtmlViewStyle.Page)
                                {
                                    rowspan = cell2.RuntimeRowSpan - (row.Index - cell2.RowIndex);
                                }
                                else
                                {
                                    rowspan = cell2.RowSpan - (row.Index - cell2.RowIndex);
                                }
                                cell2.WriteHTMLRowSpan(writer, rowspan);
                            }
                            //cell.OverrideCell.WriteHTML( writer );
                        }
                    }
                    writer.WriteFullEndElement();
                }//if
                rect.Offset(0, rect.Height);
            }//foreach
             
            writer.WriteFullEndElement();
        }
    }//public override void WriteHTML( WriterHtmlDocumentWriter writer )
}//public class XTextTableElement : XTextContainerElement