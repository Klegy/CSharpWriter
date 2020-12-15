using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using DCSoft.CSharpWriter.Dom ;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 创建表格使用的信息对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [System.Xml.Serialization.XmlType()]
    public class XTextTableElementProperties : XTextElementProperties , System.ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextTableElementProperties()
        {
        }

        /// <summary>
        /// 不支持
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public override bool ReadProperties(DomElement element)
        {
            return false;
        }

        private int _RowsCount = 3;
        /// <summary>
        /// 表格行数
        /// </summary>
        [DefaultValue( 3 )]        
        public int RowsCount
        {
            get { return _RowsCount; }
            set { _RowsCount = value; }
        }

        private int _ColumnsCount = 3;
        /// <summary>
        /// 表格列数
        /// </summary>
        [DefaultValue( 3 )]
        public int ColumnsCount
        {
            get { return _ColumnsCount; }
            set { _ColumnsCount = value; }
        }

        private float _ColumnWidth = 0f;
        /// <summary>
        /// 用户指定的表格列宽度,为0则自动设置,单位Document
        /// </summary>
        [DefaultValue( 0f )]
        public float ColumnWidth
        {
            get { return _ColumnWidth; }
            set { _ColumnWidth = value; }
        }

        private float _RowHeight = 0;
        /// <summary>
        /// 用户指定的表格行高度,为0则自动设置,单位Document
        /// </summary>
        [DefaultValue( 0f)]
        public float RowHeight
        {
            get { return _RowHeight; }
            set { _RowHeight = value; }
        }

        private int _BorderWidth = 1;
        /// <summary>
        /// 边框宽度
        /// </summary>
        [DefaultValue( 1 )]
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }

        private Color _BorderColor = Color.Black;
        /// <summary>
        /// 边框色
        /// </summary>
        [DefaultValue( typeof( Color) , "Black")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        private System.Drawing.Drawing2D.DashStyle _BorderStyle
            = System.Drawing.Drawing2D.DashStyle.Solid;
        /// <summary>
        /// 边框线样式
        /// </summary>
        [DefaultValue( System.Drawing.Drawing2D.DashStyle.Solid )]
        public System.Drawing.Drawing2D.DashStyle BorderStyle
        {
            get { return _BorderStyle; }
            set { _BorderStyle = value; }
        }

        private float _CellPaddingLeft = 15f;
        /// <summary>
        /// 单元格左内边距
        /// </summary>
        [DefaultValue( 15f )]
        public float CellPaddingLeft
        {
            get { return _CellPaddingLeft; }
            set { _CellPaddingLeft = value; }
        }

        private float _CellPaddingTop = 0f;
        /// <summary>
        /// 单元格上内边距
        /// </summary>
        [DefaultValue(0f)]
        public float CellPaddingTop
        {
            get { return _CellPaddingTop; }
            set { _CellPaddingTop = value; }
        }

        private float _CellPaddingRight = 0f;
        /// <summary>
        /// 单元格右内边距
        /// </summary>
        [DefaultValue(0f)]
        public float CellPaddingRight
        {
            get { return _CellPaddingRight; }
            set { _CellPaddingRight = value; }
        }

        private float _CellPaddingBottom = 15f;
        /// <summary>
        /// 单元格下内边距
        /// </summary>
        [DefaultValue(15f)]
        public float CellPaddingBottom
        {
            get { return _CellPaddingBottom; }
            set { _CellPaddingBottom = value; }
        }
        /// <summary>
        /// 绘制预览图形
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="bounds">边界</param>
        public virtual void DrawPreview(Graphics g, Rectangle bounds)
        {
            if (this.RowsCount <= 0 || this.ColumnsCount <= 0)
            {
                // 设置了无效的行数和列数
                return;
            }
            Color color = this.BorderColor;
            if (color.A == 0)
            {
                // 无边框，则设置灰色边框
                color = Color.Gray;
            }
            using (Pen p = new Pen(color, this.BorderWidth))
            {
                p.DashStyle = this.BorderStyle;
                float xStep = bounds.Width / this.ColumnsCount;
                float yStep = bounds.Height / this.RowsCount;
                for (int rowIndex = 0; rowIndex < this.RowsCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < this.ColumnsCount; colIndex++)
                    {
                        // 绘制各个单元格的边框
                        g.DrawRectangle(
                            p, 
                            bounds.Left + xStep * colIndex,
                            bounds.Top + yStep * rowIndex, 
                            xStep ,
                            yStep);
                    }//for
                }//for
            }//using 
        }

        /// <summary>
        /// 根据对象设置创建文档表格对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>创建的表格对象</returns>
        public override DomElement CreateElement(DomDocument document)
        {
            if (this.RowsCount <= 0 || this.ColumnsCount <= 0)
            {
                return null;
            }
            DomTableElement table = new DomTableElement();
            ApplyToElement(document, table , false );
            return table;
        }

        /// <summary>
        /// 根据对象参数来设置文档表格对象
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="table">文档表格对象</param>
        /// <returns>创建的表格对象</returns>
        public override bool ApplyToElement(DomDocument document, DomElement element , bool logUndo)
        {
            DomTableElement table = (DomTableElement)element;
            table.OwnerDocument = document;
            // 创建单元格对象使用的样式
            DocumentContentStyle style = new DocumentContentStyle();
            style.BorderColor = this.BorderColor;
            style.BorderWidth = this.BorderWidth;
            style.BorderStyle = this.BorderStyle;
            style.BorderLeft = true;
            style.BorderTop = true;
            style.BorderRight = true;
            style.BorderBottom = true;
            style.PaddingBottom = this.CellPaddingBottom;
            style.PaddingLeft = this.CellPaddingLeft;
            style.PaddingRight = this.CellPaddingRight;
            style.PaddingTop = this.CellPaddingTop;
            style.VerticalAlign = Drawing.VerticalAlignStyle.Middle;
            int styleIndex = document.ContentStyles.GetStyleIndex(style);
            float cw = this.ColumnWidth;
            if (cw <= 0)
            {
                cw = ( document.PageSettings.ViewClientWidth - 30)/ this.ColumnsCount;
            }
            float rh = this.RowHeight;
            float rh2 =( ( DocumentContentStyle ) document.ContentStyles.Default).DefaultLineHeight;
            // 创建表格列对象
            for (int colIndex = 0; colIndex < this.ColumnsCount; colIndex++)
            {
                DomTableColumnElement col = table.CreateColumnInstance();
                col.Width = cw;
                table.AppendChildElement(col);
            }
            for (int rowIndex = 0; rowIndex < this.RowsCount; rowIndex++)
            {
                // 创建表格行对象
                DomTableRowElement row = table.CreateRowInstance();
                row.SpecifyHeight = rh;
                row.Height = rh2;
                row.Top = rowIndex * rh2;
                table.AppendChildElement( row );
                for (int colIndex = 0; colIndex < this.ColumnsCount; colIndex++)
                {
                    // 创建单元格对象
                    DomTableCellElement cell = table.CreateCellInstance();
                    cell.StyleIndex = styleIndex;
                    //cell.Width = cw;
                    //cell.Height = rh2;
                    cell.Left = cw * colIndex;
                    cell.Top = 0;
                    row.AppendChildElement( cell );
                    cell.AppendChildElement(new DomParagraphFlagElement());
                }//for
            }//for
            return true;
        }

        /// <summary>
        /// 显示用户界面来让用户设置对象数据
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <returns>用户是否确认操作</returns>
        public override bool PromptNewElement(WriterCommandEventArgs args)
        {
            using (dlgInsertTable dlg = new dlgInsertTable())
            {
                dlg.TableCreationInfo = this;
                if (dlg.ShowDialog( args == null ? null : args.EditorControl)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PromptEditProperties(WriterCommandEventArgs args)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return (XTextTableElementProperties)this.MemberwiseClone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XTextTableElementProperties Clone()
        {
            return (XTextTableElementProperties)this.MemberwiseClone();
        }
    }
}
