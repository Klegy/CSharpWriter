using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 表格行元素
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable ]
    [System.Xml.Serialization.XmlType("XTextTableRow")]
    [System.Diagnostics.DebuggerDisplay("Row {Index}")]
    public class DomTableRowElement : DomContainerElement 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomTableRowElement()
        {
        }
         
        //private bool _HeaderRow = false;
        ///// <summary>
        ///// 标题行
        ///// </summary>
        //[System.ComponentModel.DefaultValue( false )]
        //public bool HeaderRow
        //{
        //    get
        //    {
        //        return _HeaderRow; 
        //    }
        //    set
        //    {
        //        _HeaderRow = value; 
        //    }
        //}

        ///// <summary>
        ///// 返回对象可接受的元素类型
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public override ElementType AcceptChildElementTypes
        //{
        //    get
        //    {
        //        return base.AcceptChildElementTypes | ElementType.TableCell ;
        //    }
        //}

        /// <summary>
        /// 对象所属的表格对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public DomTableElement OwnerTable
        {
            get
            {
                return (DomTableElement)this.Parent;
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public int Index
        {
            get
            {
                if (this.OwnerTable == null)
                {
                    return 0;
                }
                else
                {
                    return this.OwnerTable.RuntimeRows.IndexOf(this);
                }
            }
        }

        [System.ComponentModel.Browsable( false )]
        public override float AbsLeft
        {
            get
            {
                return this.OwnerTable.AbsLeft + this.Left;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override float AbsTop
        {
            get
            {
                return this.OwnerTable.AbsTop + this.Top;
            }
        }

        /// <summary>
        /// 本表格行包含的单元格对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public DomElementList Cells
        {
            get
            {
                return this.Elements;
            }
        }

        private float _SpecifyHeight = 0f;
        /// <summary>
        /// 用户指定的高度
        /// </summary>
        /// <remarks>
        /// 若等于0则表格行自动设置高度，
        /// 若大于0则表格行高度自动设置高度而且高度不小于用户指定的高度，
        /// 若小于0则固定设置表格行的高度为用户指定的高度。
        /// </remarks>
        [System.ComponentModel.DefaultValue( 0f)]
        public float SpecifyHeight
        {
            get
            {
                return _SpecifyHeight; 
            }
            set
            {
                _SpecifyHeight = value; 
            }
        }

        private bool bolHeaderStyle = false;
        /// <summary>
        /// 标题行样式
        /// </summary>
        /// <remarks>
        /// 在分页时，若导致分页的表格行的DataRow属性为false则不自动插入临时标题行
        /// </remarks>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Appearance")]
        public bool HeaderStyle
        {
            get
            {
                return bolHeaderStyle;
            }
            set
            {
                bolHeaderStyle = value;
            }
        }

        internal DomTableRowElement _SourceHeaderRow = null;

        /// <summary>
        /// 本表格行是临时生成的标题行
        /// </summary>
        private bool bolTemporaryHeaderRow = false;
        /// <summary>
        /// 本表格行是临时生成的标题行
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement()]
        public bool TemporaryHeaderRow
        {
            get
            {
                return bolTemporaryHeaderRow;
            }
            set
            {
                bolTemporaryHeaderRow = value;
            }
        }

        /// <summary>
        /// 获得同一层次中的上一个元素
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public override DomElement PreviousElement
        {
            get
            {
                DomTableElement table = (DomTableElement)this.Parent;
                if (table != null)
                {
                    return table.RuntimeRows.GetPreElement(this);
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获得元素的同一层次的下一个元素
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public override DomElement NextElement
        {
            get
            {
                DomTableElement table = (DomTableElement)this.Parent;
                if (table != null)
                {
                    return table.RuntimeRows.GetNextElement(this);
                }
                else
                {
                    return null;
                }
            }
        }

        [System.ComponentModel.Browsable( false )]
        public override int ElementIndex
        {
            get
            {
                if (this.Parent != null)
                {
                    DomTableElement table = (DomTableElement)this.Parent;
                    return table.RuntimeRows.IndexOf(this);
                }
                return -1;
                //return base.ElementIndex;
            }
        }

        /// <summary>
        /// 处理文档事件
        /// </summary>
        /// <param name="args"></param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (this.bolTemporaryHeaderRow)
            {
                // 对于临时生成的标题表格行不处理任何事件。
                args.CancelBubble = true;
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }

        public override void OnContentChanged(ContentChangedEventArgs args)
        {
            if (this.HeaderStyle && args.LoadingDocument == false )
            {
                // 更新所有临时插入的表格行对象
                foreach (DomTableRowElement row in this.OwnerTable.RuntimeRows)
                {
                    if (row.bolTemporaryHeaderRow && row._SourceHeaderRow == this )
                    {
                        row.StyleIndex = this.StyleIndex;
                        row.Elements.Clear();
                        foreach (DomTableCellElement cell in this.Cells)
                        {
                            DomTableCellElement newCell = (DomTableCellElement)cell.Clone(true);
                            row.Elements.Add(newCell);
                            newCell.Parent = row;
                        }//foreach
                        row.InvalidateView();
                    }
                }
            }
            base.OnContentChanged(args);
        }

        /// <summary>
        /// 在编辑器中设置表格行的用户指定高度
        /// </summary>
        /// <param name="newHeight">新高度</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        internal void EditorSetRowSpecifyHeight(float newHeight, bool logUndo)
        {
            DomTableElement table = this.OwnerTable;
            table.InvalidateView();
            float tabelHeight = table.Height;
            float oldHeight = this.SpecifyHeight;
            this.SpecifyHeight = newHeight;
            if ( logUndo && this.OwnerDocument.BeginLogUndo())
            {
                this.OwnerDocument.UndoList.AddRowSpecifyHeight( this , oldHeight );
                this.OwnerDocument.EndLogUndo();
            }
            this.OwnerDocument.Modified = true;
            table.ExecuteLayout();
            table.InvalidateView();
            if (table.Height != tabelHeight)
            {
                // 表格高度发生改变，需要重新设置行状态和重新分页
                table.UpdatePagesForTable( false );
            }
        }

    }
}