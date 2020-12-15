using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common;
using DCSoft.WinForms;
using DCSoft.WinForms.Native;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Dom.Undo;
using DCSoft.CSharpWriter.Html;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 单元格元素
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    [System.Xml.Serialization.XmlType("XTextTableCell")]
    [System.Diagnostics.DebuggerDisplay("Cell {CellID}:{ PreviewString }")]
    public class DomTableCellElement : DomContentElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomTableCellElement()
        {
        }

        /// <summary>
        /// 不支持该方法
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public override bool EditorSetVisible(bool visible , bool fastMode )
        {
            throw new NotSupportedException("Cell.EditorSetVisible");
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Title
        {
            get 
            {
                return _Title; 
            }
            set
            {
                _Title = value; 
            }
        }

        /// <summary>
        /// 对象所属表格对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableElement OwnerTable
        {
            get
            {
                if (this.Parent == null)
                {
                    return null;
                }
                else
                {
                    return (DomTableElement)(this.Parent.Parent);
                }
            }
        }
        /// <summary>
        /// 对象所属表格行对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableRowElement OwnerRow
        {
            get
            {
                return (DomTableRowElement)this.Parent;
            }
        }

        [System.ComponentModel.Browsable( false )]
        public override float AbsLeft
        {
            get
            {
                return this.OwnerRow.AbsLeft + this.Left;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override float AbsTop
        {
            get
            {
                return this.OwnerRow.AbsTop + this.Top;
            }
        }

        /// <summary>
        /// 对象所属的最下面的表格行对象
        /// </summary>
        /// <remarks>当对象没有合并单元格时，该属性就返回单元格所在表格行对象，
        /// 当纵向合并了单元格时( RowSpan 属性大于1)则该属性返回该单元格所跨过的
        /// 表格行中最下面的一个表格行对象</remarks>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableRowElement LastOwnerRow
        {
            get
            {
                DomTableElement table = this.OwnerTable;
                return ( DomTableRowElement ) table.RuntimeRows.SafeGet(this.Parent.ElementIndex + this.RowSpan - 1);
                //return (XTextTableRowElement)this.OwnerTable.Elements.SafeGet(this.Parent.ElementIndex + this.intRowSpan - 1);
            }
        }


        /// <summary>
        /// 对象所属表格列对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableColumnElement OwnerColumn
        {
            get
            {
                return (DomTableColumnElement)(this.OwnerTable.Columns.SafeGet(this.ElementIndex));
            }
        }

        /// <summary>
        /// 对象所属的最右边的表格列对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableColumnElement LastOwnerColumn
        {
            get
            {
                return (DomTableColumnElement)this.OwnerTable.Columns.SafeGet(
                    this.ElementIndex + this.intColSpan - 1);
            }
        }

        internal DomTableCellElement myOverrideCell = null;
        /// <summary>
        /// 若单元格被其他单元格合并了则返回合并本单元格的单元格对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public DomTableCellElement OverrideCell
        {
            get
            {
                return myOverrideCell;
            }
        }

        /// <summary>
        /// 单元格是否处于选择状态
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool IsSelected
        {
            get
            {
                DomDocumentContentElement dce = this.DocumentContentElement ;
                if (dce.Selection.Cells != null )
                {
                    return dce.Selection.Cells.Contains(this);
                }
                return false;
            }
        }

        /// <summary>
        /// 单元格在表格中的编号
        /// </summary>
        [System.ComponentModel.Category("Design")]
        public string CellID
        {
            get
            {
                if (this.Parent != null && this.OwnerTable != null)
                {
                    return CellIndex.GetCellIndex(this.RowIndex + 1, this.ColIndex + 1);
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 内容垂直对齐方式
        /// </summary>
        public override Drawing.VerticalAlignStyle ContentVertialAlign
        {
            get
            {
                return this.RuntimeStyle.VerticalAlign;
            }
        }

        /// <summary>
        /// 获得或设置单元格是否可见,被合并的单元格是不可见的。
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        public override bool Visible
        {
            get
            {
                DomTableColumnElement col = this.OwnerColumn;
                if (col.Visible == false)
                {
                    return false;
                }
                if (this.myOverrideCell != null)
                {
                    return false;
                }
                return base.Visible;
            }
        }

        /// <summary>
        /// 判断本单元格是否被其他单元格合并了
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public bool IsOverrided
        {
            get
            {
                return myOverrideCell != null;
            }
        }

        /// <summary>
        /// 跨行数
        /// </summary>
        private int intRowSpan = 1;
        /// <summary>
        /// 跨行数
        /// </summary>
        /// <remarks>
        /// 单元格所占据的表格行数，本属性为1则占据一行，单元格纵向没有合并单元格，若该属性值大于1则纵向合并
        /// 单元格。本属性类似于 HTML 的 TD 元素的 ROWSPAN 属性。
        /// </remarks>
        [System.ComponentModel.DefaultValue(1)]
        [System.ComponentModel.Category("Layout")]
        public virtual int RowSpan
        {
            get
            {
                return intRowSpan;
            }
            set
            {
                if ( this.OwnerTable == null 
                    || this.OwnerDocument == null
                    || this.OwnerDocument.Initializing)
                {
                    this.intRowSpan = value;
                    return;
                }
                int iValue = FixRowSpan( value );
                if (intRowSpan != iValue)
                {
                    intRowSpan = iValue;
                    this.OwnerTable.ExecuteLayout();////////////
                }

            }
        }
        /// <summary>
        /// 修正单元格跨行数
        /// </summary>
        /// <param name="rowSpan">单元格跨行数</param>
        /// <returns>修正后的值</returns>
        public int FixRowSpan(int rowSpan)
        {
            if (rowSpan < 1 )
            {
                rowSpan = 1;
            }
            if (this.RowIndex + rowSpan - 1 >= this.OwnerTable.Rows.Count)
            {
                rowSpan = this.OwnerTable.Rows.Count - this.RowIndex;
            }
            return rowSpan;
        }

        /// <summary>
        /// 运行时的跨行数
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual int RuntimeRowSpan
        {
            get
            {
                return intRowSpan;
            }
        }
         
         
        /// <summary>
        /// 跨列数
        /// </summary>
        private int intColSpan = 1;
        /// <summary>
        /// 跨列数
        /// </summary>
        /// <remarks>
        /// 单元格所占据的表格列数，本属性值为1则单元格占据一列，单元格横向没有合并单元格，若该属性值大于1则横向合并
        /// 单元格。本属性类似 HTML 的 TD 元素的 COLSPAN 属性。
        /// </remarks>
        [System.ComponentModel.DefaultValue(1)]
        [System.ComponentModel.Category("Layout")]
        public virtual int ColSpan
        {
            get
            {
                return intColSpan;
            }
            set
            {
                if (this.OwnerDocument == null || this.OwnerDocument.Initializing)
                {
                    intColSpan = value;
                    return;
                }
                int iValue = FixColSpan( value );
                if (intColSpan != iValue)
                {
                    intColSpan = iValue;
                    this.OwnerTable.ExecuteLayout();//////////////////
                }
            }
        }

        /// <summary>
        /// 修正单元格跨列数
        /// </summary>
        /// <param name="colSpan">单元格跨列数</param>
        /// <returns>修正后的值</returns>
        public int FixColSpan(int colSpan)
        {
            if (this.ColIndex + colSpan - 1 >= this.OwnerTable.Columns.Count)
            {
                colSpan = this.OwnerTable.Columns.Count - this.ColIndex;
            }
            if (colSpan < 1)
            {
                colSpan = 1;
            }
            return colSpan;
        }

        public void InternalSetColSpan(int colSpan)
        {
            intColSpan = Math.Max( colSpan , 1 );
        }
        public void InternalSetRowSpan(int rowSpan)
        {
            intRowSpan = Math.Max( rowSpan , 1 );
        }

        /// <summary>
        /// 从0开始的行号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int RowIndex
        {
            get
            {
                if (this.Parent == null)
                    return -1;
                else
                    return this.Parent.ElementIndex;
            }
        }
        /// <summary>
        /// 从0开始的列号
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public int ColIndex
        {
            get
            {
                return this.ElementIndex ;
            }
        }

        private int intDesignRowIndex = 0;
        /// <summary>
        /// 设计时行号
        /// </summary>
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement()]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int DesignRowIndex
        {
            get
            {
                return intDesignRowIndex;
            }
            set
            {
                intDesignRowIndex = value;
            }
        }

        private int intDesignColIndex = 0;
        /// <summary>
        /// 设计时列号
        /// </summary>
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement()]
        [System.ComponentModel.DesignerSerializationVisibility(
            System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int DesignColIndex
        {
            get
            {
                return intDesignColIndex;
            }
            set
            {
                intDesignColIndex = value;
            }
        }

        public override void UpdateHeightByContentHeight()
        {
            // 无操作
            //base.UpdateHeightByContentHeight();
        }

        public override void ExecuteLayout()
        {
            //this.Width = this.OwnerDocument.PageSettings.ViewClientWidth;
            //this.UpdateContentElements();
            this.PrivateLines.Clear();
            foreach (DomElement element in this.PrivateContent)
            {
                element.OwnerLine = null;
            }
            if (this.ParticalRefreshLines(
                null,
                null,
                this.RuntimeStyle.VerticalAlign))
            {
                this.UpdateLinePosition(this.ContentVertialAlign, true, true);
            }
        }

        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseMove
                || args.Style == DocumentEventStyles.MouseDown)
            {
                if (args.StrictMatch == false)
                {
                    return;
                }
                // 处理鼠标移动或鼠标按键事件
                int size = (int)this.OwnerDocument.PixelToDocumentUnit(2);

                if (this.ColIndex == 0 && Math.Abs(args.X) <= size)
                {
                    // 鼠标靠近单元格左边框而且单元格是最左边的，
                    // 此时若鼠标点击，则选择整个表格行
                    args.Cursor = XCursors.Right;
                    if (args.Style == DocumentEventStyles.MouseDown && args.StrictMatch)
                    {
                        //XTextTableCellElement firstCell = this;
                        //if (this.IsOverrided)
                        //{
                        //    firstCell = this.OverrideCell;
                        //}
                        //XTextTableCellElement lastCell =( XTextTableCellElement ) firstCell.LastOwnerRow.Cells.LastElement;
                        //if (lastCell.IsOverrided)
                        //{
                        //    lastCell = lastCell.OverrideCell;
                        //}
                        DomElementList cells = this.OwnerTable.GetSelectionCells(
                            this.RowIndex,
                            0,
                            this.RowIndex,
                            this.OwnerRow.Cells.Count - 1);
                        int firstIndex = int.MaxValue ;
                        int lastIndex = 0;
                        foreach (DomTableCellElement cell in cells)
                        {
                            if (cell.IsOverrided == false)
                            {
                                firstIndex = Math.Min(firstIndex, cell.FirstContentElement.ViewIndex);
                                lastIndex = Math.Max(lastIndex, cell.Elements.LastElement.ViewIndex);
                            }
                            
                        }
                        //int firstIndex = cells.FirstContentElement.ViewIndex;
                        //int lastIndex = cells.LastContentElement.ViewIndex;
                        if (firstIndex < int.MaxValue)
                        {
                            this.DocumentContentElement.SetSelection(
                                firstIndex,
                                lastIndex - firstIndex);
                            args.CancelBubble = true;
                        }
                        //XTextTableColumnElement col = this.OwnerColumn;
                        //if (col.Index > 0)
                        //{
                        //    col = ( XTextTableColumnElement ) this.OwnerTable.Columns[col.Index - 1];
                        //    DragSetColumnWidth(col, args);
                        //}
                    }
                }
                else if (Math.Abs(args.X) <= size)
                {
                    DomTableColumnElement col = this.LastOwnerColumn;
                    col = (DomTableColumnElement)this.OwnerTable.Columns.GetPreElement(col);
                    if (col != null
                        && this.OwnerDocument.DocumentControler.CanModify(
                            this,
                            DomAccessFlags.Normal ))
                    {
                        args.Cursor = System.Windows.Forms.Cursors.VSplit;
                        if (args.Style == DocumentEventStyles.MouseDown
                            && args.StrictMatch)
                        {
                            // 对表格列执行鼠标拖拽左右移动修改表格列宽度操作
                            DragSetColumnWidth(col, args);
                        }
                    }
                }
                else if (Math.Abs(args.Y) <= size)
                {
                    DomTableRowElement row = this.LastOwnerRow;
                    row = (DomTableRowElement)row.PreviousElement;

                    if (row != null
                        && this.OwnerDocument.DocumentControler.CanModify(
                            this,
                            DomAccessFlags.Normal ))
                    {
                        args.Cursor = System.Windows.Forms.Cursors.HSplit;
                        if (args.Style == DocumentEventStyles.MouseDown
                            && args.StrictMatch)
                        {
                            // 对上一行的单元格执行鼠标拖拽上下移动修改表格行高操作
                            DragSetRowSpecifyHeight(row, args);
                        }
                    }
                }
                else if (Math.Abs(args.Y - this.Height) <= size)
                {
                    if (this.OwnerDocument.DocumentControler.CanModify(
                            this,
                            DomAccessFlags.Normal ))
                    {
                        args.Cursor = System.Windows.Forms.Cursors.HSplit;
                        if (args.Style == DocumentEventStyles.MouseDown
                            && args.StrictMatch)
                        {
                            // 对上一行的单元格执行鼠标拖拽上下移动修改表格行高操作
                            DragSetRowSpecifyHeight(this.LastOwnerRow, args);
                        }
                    }
                }
                else if (Math.Abs(args.X - this.Width) <= size)
                {
                    if (this.OwnerDocument.DocumentControler.CanModify(
                            this,
                            DomAccessFlags.Normal ))
                    {
                        args.Cursor = System.Windows.Forms.Cursors.VSplit;
                        if (args.Style == DocumentEventStyles.MouseDown
                            && args.StrictMatch)
                        {
                            // 对表格列执行鼠标拖拽左右移动修改表格列宽度操作
                            DragSetColumnWidth(this.LastOwnerColumn, args);
                        }
                    }
                }//if
            }//if
        }

        /// <summary>
        /// 拖拽方式设置表格行的高度
        /// </summary>
        /// <param name="row"></param>
        /// <param name="args"></param>
        private void DragSetRowSpecifyHeight(DomTableRowElement row, DocumentEventArgs args)
        {
            this.OwnerDocument.EditorControl.PagesTransform.UseAbsTransformPoint = true;
            System.Drawing.Point[] ps = this.OwnerDocument.EditorControl.CaptureMouseMove(
                            new CaptureMouseMoveEventHandler(mc_ReversibleDrawCallback),
                            System.Drawing.Rectangle.Empty,
                            null);
            if (ps != null)
            {
                float newHeight = row.Height + ps[1].Y - ps[0].Y;
                row.EditorSetRowSpecifyHeight(newHeight, true);
                args.CancelBubble = true;
            }
        }

        /// <summary>
        /// 拖拽方式设置表格列的宽度
        /// </summary>
        /// <param name="col"></param>
        /// <param name="args"></param>
        private void DragSetColumnWidth(DomTableColumnElement col, DocumentEventArgs args)
        {
            this.OwnerDocument.EditorControl.PagesTransform.UseAbsTransformPoint = true;
            System.Drawing.Point[] ps = this.OwnerDocument.EditorControl.CaptureMouseMove(
                        new CaptureMouseMoveEventHandler(mc2_ReversibleDrawCallback),
                        System.Drawing.Rectangle.Empty,
                        null);
            if (ps != null)
            {
                float newWidth = col.Width + ps[1].X - ps[0].X;
                if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control)
                    == System.Windows.Forms.Keys.Control)
                {
                    // 若按下Control键，则设置表格列的宽度而且不修改下一个表格列的宽度，这样会修改整个表格的宽度
                    col.EditorSetWidth(newWidth, true, false );
                }
                else
                {
                    // 若按下Control键，则设置表格列的宽度而且修改下一个表格列的宽度,这样能保持整个表格的宽度
                    col.EditorSetWidth(newWidth, true, true);
                }
                args.CancelBubble = true;
            }
        }

        private void mc_ReversibleDrawCallback(
            object sender,
            CaptureMouseMoveEventArgs e)
        {
            System.Drawing.RectangleF rect = this.OwnerTable.AbsBounds;
            this.OwnerDocument.EditorControl.ReversibleViewDrawLine(
                (int)rect.Left,
                (int)e.CurrentPosition.Y,
                (int)rect.Right,
                (int)e.CurrentPosition.Y);
        }
        private void mc2_ReversibleDrawCallback(
            object sender,
            CaptureMouseMoveEventArgs e)
        {
            System.Drawing.RectangleF rect = this.OwnerTable.AbsBounds;
            this.OwnerDocument.EditorControl.ReversibleViewDrawLine(
                (int)e.CurrentPosition.X,
                (int)rect.Top,
                e.CurrentPosition.X,
                (int)rect.Bottom);
        }

        /// <summary>
        /// 编辑器中设置单元格的合并信息
        /// </summary>
        /// <param name="newRowSpan">新的横向合并行数</param>
        /// <param name="newColSpan">新的纵向合并列数</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <returns>操作是否成功</returns>
        internal bool EditorSetCellSpan(
            int newRowSpan,
            int newColSpan ,
            bool logUndo,
            Dictionary<DomTableCellElement , DomElementList > cellContents )
        {
            newRowSpan = FixRowSpan(newRowSpan);
            newColSpan = FixColSpan(newColSpan);
            if (newRowSpan == this.RowSpan && newColSpan == this.ColSpan)
            {
                // 无需进行处理
                return false;
            }
            XTextUndoCellSpan undo = null;
            if (logUndo)
            {
                // 创建撤销操作记录信息对象
                undo = new XTextUndoCellSpan(this, newRowSpan, newColSpan);
            }
            DomTableElement table = this.OwnerTable;
            if (cellContents != null)
            {
                // 用户指定了单元格的内容
                foreach (DomTableCellElement cell in cellContents.Keys)
                {
                    DomElementList list = cellContents[cell];
                    cell.Elements.Clear();
                    cell.Elements.AddRange( list );
                    foreach (DomElement element in cell.Elements)
                    {
                        element.Parent = cell;
                        element.OwnerDocument = this.OwnerDocument;
                    }//foreach
                    cell.UpdateContentElements(false);
                    cell.Width = 0;
                }//foreach
            }
            else
            {
                // 将合并后的单元格的内容进行合并
                DomElementList oldCells = table.GetRange(
                    this.RowIndex,
                    this.ColIndex,
                    this.intRowSpan,
                    this.intColSpan,
                    true);
                    
                DomElementList cells = table.GetRange(
                    this.RowIndex,
                    this.ColIndex,
                    newRowSpan,
                    newColSpan,
                    true);
                foreach (DomTableCellElement cell in oldCells)
                {
                    if (cells.Contains(cell) == false )
                    {
                        // 原先已经本合并的单元格重现天日，即将要显示出来
                        // 修正内容
                        cell.UpdateContentElements(false);
                        cell.Width = 0;
                    }
                }//foreach

                DomElementList tempList = new DomElementList();
                foreach (DomTableCellElement cell in cells)
                {
                    if (cell != this)
                    {
                        if (cell.Elements.Count > 1)
                        {
                            // 单元格内容不为空，则移动到合并的大单元格中
                            tempList.AddRange(cell.Elements);
                            cell.Elements.Clear();
                            cell.Width = 0;
                            cell.UpdateContentElements(false);
                        }
                    }
                }
                this.Elements.AddRange( tempList );
                foreach (DomElement e in tempList)
                {
                    e.Parent = this;
                }
                if (undo != null)
                {
                    // 记录新单元格内容
                    undo.LogNewCellsContent();
                }
            }
            
            // 设置新的跨行跨列数
            this.intRowSpan = newRowSpan;
            this.intColSpan = newColSpan;
            this.UpdateContentVersion();
            // 更新单元格的合并状态
            table.UpdateCellOverrideState();
 
            this.UpdateContentElements(true);
            // 设置文档的选择状态
            DomContent c = this.DocumentContentElement.Content;
            
            this.DocumentContentElement.SetSelection(
                this.FirstContentElement.ViewIndex,
                this.LastContentElement.ViewIndex - this.FirstContentElement.ViewIndex);

            table.InvalidateView();
            table.ExecuteLayout();
            table.InvalidateView();
            table.UpdatePagesForTable( false );
            if (undo != null)
            {
                // 添加撤销操作信息
                if (this.OwnerDocument.BeginLogUndo())
                {
                    this.OwnerDocument.UndoList.Add(undo);
                    this.OwnerDocument.EndLogUndo();
                    this.OwnerDocument.OnSelectionChanged();
                    this.OwnerDocument.OnDocumentContentChanged();
                }
            }
            return true;
        }

        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.bolFirstParagraph = true;
            base.WriteRTF(writer);
        }

        /// <summary>
        /// 方法无效
        /// </summary>
        private new void EditorDelete(bool logUndo)
        {
            throw new NotSupportedException("EditorDelete");
        }

        //public override void WriteCotentDocument(RTFContentWriter writer)
        //{
        //    if (this.myDrawTextInfos.Count > 0)
        //    {
        //        writer.WriteImageElement(this);
        //    }
        //    else
        //    {
        //        ReportRTFWriter rtf = (ReportRTFWriter)writer;
        //        string txt = rtf.WritingHeaderFooter ? this.strText : this.DisplayText;
        //        if (txt != null)
        //        {
        //            DocumentFormatInfo info = new DocumentFormatInfo();
        //            info.Font = rs.Font.Value;
        //            info.TextColor = rs.Color;
        //            info.SetAlign(rs.SA_Align);
        //            info.Link = this.LinkReference;
        //            info.Multiline = rs.Multiline;
        //            //info.Nowrap = ! this.Multiline ;
        //            writer.WriteString(txt, info);
        //        }

        //        if (this.Items.Count > 0)
        //        {
        //            if (DomUtil.HasContent(this.DisplayText))
        //            {
        //                if (this.Align == System.Drawing.StringAlignment.Near)
        //                    writer.WriteKeyword("ql");
        //                else if (this.Align == System.Drawing.StringAlignment.Center)
        //                    writer.WriteKeyword("qc");
        //                else
        //                    writer.WriteKeyword("qr");
        //            }
        //            else
        //            {
        //                writer.WriteKeyword("ql");
        //            }

        //            writer.Writer.WriteStartGroup();
        //            writer.WriteItems(this.Items, null);
        //            writer.Writer.WriteEndGroup();
        //        }
        //    }
        //}

        /// <summary>
        /// 输出对象数据到HTML文档中
        /// </summary>
        /// <param name="myBuilder">HTML文档生成器</param>
        internal void WriteHTMLRowSpan(WriterHtmlDocumentWriter writer, int rowSpan)
        {
            writer.WriteStartElement("td");
            
              
            if ( string.IsNullOrEmpty( this.Title ) == false )
            {
                writer.WriteAttributeString("title", this.Title );
            }

            if (rowSpan > 1)
            {
                writer.WriteAttributeString("rowspan", rowSpan.ToString());
            }


            //			if( this.intRowSpan > 1 )
            //				rw.WriteAttributeString("rowspan" , intRowSpan.ToString());
            if (this.ColSpan > 1)
            {
                // 此处为了隐藏表格列来修正 colspan 属性值
                int fix = 0;
                int colindex = this.ColIndex;
                foreach (DomTableColumnElement col in this.OwnerTable.Columns)
                {
                    if (col.Index > colindex && col.Index < colindex + intColSpan - 1)
                    {
                        if (col.Visible == false)
                        {
                            fix++;
                        }
                    }
                }
                writer.WriteAttributeString("colspan", Convert.ToString(intColSpan - fix));
            }
             

            writer.WriteStartStyle();
            //if (tdWidthStyle == 1)
            //{
            //    rw.CssStyle.Width = rw.ToPixelValue(this.Width);
            //}
            //if (bolRowCollapse || this.SortStyle != CellSortStyle.None)
            //{
            //    writer.CssStyle.Cursor = "pointer";
            //}
            //rw.SetPaddingStyle( this );
            DocumentContentStyle rs = this.RuntimeStyle;
            writer.WriteDocumentContentStyle(rs , this );
            //设置字体行高，默认为字体的1.2倍
            //rw.CssStyle.Line_height = (this.Font.Value.Size * 1.4 ) + "px";
             
            writer.WriteEndStyle();

            base.WriteHTML(writer);
             
            writer.WriteFullEndElement();
        }
    }
}
