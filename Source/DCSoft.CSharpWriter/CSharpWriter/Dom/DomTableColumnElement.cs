using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom.Undo;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 表格列元素
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    [System.Xml.Serialization.XmlType("XTextTableColumn")]
    public class DomTableColumnElement : DomElement
    {
        public DomTableColumnElement()
        {
        }

        /// <summary>
        /// 属性可见可序列化
        /// </summary>
        [System.ComponentModel.Browsable( true )]
        [System.Xml.Serialization.XmlElement()]
        public override float Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        /// <summary>
        /// 在编辑器中设置表格列的宽度
        /// </summary>
        /// <param name="newWidth">新表格列宽度</param>
        /// <param name="logUndo">是否记录撤销操作</param>
        /// <param name="setNextColumnWidth">设置右边一个表格列的宽度</param>
        internal void EditorSetWidth(float newWidth , bool logUndo , bool setNextColumnWidth )
        {
            // 获得最小的表格列宽度
            float minTableColumnWidth = this.OwnerDocument.Options.ViewOptions.MinTableColumnWidth ;
            if (newWidth < minTableColumnWidth)
            {
                // 新表格列宽度小于最小值，在此进行修正
                newWidth = minTableColumnWidth;
            }
            DomTableElement table = this.OwnerTable;
            DomTableColumnElement nextCol = (DomTableColumnElement)
                table.Columns.GetNextElement(this);
            float totalWidth = this.Width;
            if (setNextColumnWidth)
            {
                if (nextCol != null)
                {
                    totalWidth = this.Width + nextCol.Width;
                    if (totalWidth - newWidth < minTableColumnWidth)
                    {
                        // 修改表格列的宽度导致右边的表格列宽度小于最小值，在此进行修正
                        newWidth = totalWidth - minTableColumnWidth;
                    }
                }
            }
            if (this.Width != newWidth)
            {
                float tableHeight = table.Height;
                table.InvalidateView();
                float oldWidth = this.Width;
                this.Width = newWidth;
                if (setNextColumnWidth)
                {
                    if (nextCol != null)
                    {
                        // 设置右边一列的宽度
                        nextCol.Width = totalWidth - newWidth;
                    }
                }
                if ( logUndo && this.OwnerDocument.BeginLogUndo())
                {
                    XTextUndoTableColumnWidth undo = new XTextUndoTableColumnWidth(
                        this, 
                        oldWidth, 
                        this.Width, 
                        setNextColumnWidth);
                    this.OwnerDocument.UndoList.Add(undo);
                    this.OwnerDocument.EndLogUndo();
                }
                this.OwnerDocument.Modified = true;
                table.ExecuteLayout();
                table.InvalidateView();
                if (table.Height != tableHeight)
                {
                    // 表格高度发生改变，需要重新设置行状态和重新分页
                    table.UpdatePagesForTable( false );
                }
                if (this.OwnerDocument.EditorControl != null)
                {
                    // 更新光标位置
                    this.OwnerDocument.EditorControl.UpdateTextCaretWithoutScroll();
                }
            }
        }

        new private int Height
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 列号
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public int Index
        {
            get
            {
                DomTableElement table = (DomTableElement)this.Parent;
                return table.Columns.IndexOf(this);
            }
        }
        /// <summary>
        /// 本表格列所属的表格对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public DomTableElement OwnerTable
        {
            get
            {
                return (DomTableElement)this.Parent;
            }
        }
    }
}
