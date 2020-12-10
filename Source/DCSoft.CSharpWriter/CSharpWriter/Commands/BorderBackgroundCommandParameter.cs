/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DCSoft.CSharpWriter.Dom;
using System.Drawing.Drawing2D ;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 边框命令参数对象
    /// </summary>
    [Serializable]
    public class BorderBackgroundCommandParameter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public BorderBackgroundCommandParameter()
        {
        }

        private bool _TopBorder = true;
        /// <summary>
        /// 是否显示顶端边框线
        /// </summary>
        public bool TopBorder
        {
            get { return _TopBorder; }
            set { _TopBorder = value; }
        }

        private bool _MiddleHorizontalBorder = true;
        /// <summary>
        /// 是否显示水平中间的边框线
        /// </summary>
        public bool MiddleHorizontalBorder
        {
            get { return _MiddleHorizontalBorder; }
            set { _MiddleHorizontalBorder = value; }
        }

        private bool _BottomBorder = true;
        /// <summary>
        /// 是否显示低端边框线
        /// </summary>
        public bool BottomBorder
        {
            get { return _BottomBorder; }
            set { _BottomBorder = value; }
        }

        private bool _LeftBorder = true;
        /// <summary>
        /// 是否显示左端边框线
        /// </summary>
        public bool LeftBorder
        {
            get { return _LeftBorder; }
            set { _LeftBorder = value; }
        }

        private bool _CenterVerticalBorder = true;
        /// <summary>
        /// 是否显示垂直居中的边框线
        /// </summary>
        public bool CenterVerticalBorder
        {
            get { return _CenterVerticalBorder; }
            set { _CenterVerticalBorder = value; }
        }

        private bool _RightBorder = true;
        /// <summary>
        /// 是否显示右边的边框线
        /// </summary>
        public bool RightBorder
        {
            get { return _RightBorder; }
            set { _RightBorder = value; }
        }

        private Color _BorderColor = Color.Black;
        /// <summary>
        /// 边框线颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        private DashStyle _BorderStyle = DashStyle.Solid;
        /// <summary>
        /// 边框线样式
        /// </summary>
        public DashStyle BorderStyle
        {
            get { return _BorderStyle; }
            set { _BorderStyle = value; }
        }

        private int _BorderWidth = 1;
        /// <summary>
        /// 边框线宽度
        /// </summary>
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }

        private Color _BackgroundColor = Color.Transparent;
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }

        private BorderCommandApplyRange _ApplyRange = BorderCommandApplyRange.Cell;
        /// <summary>
        /// 设置应用范围
        /// </summary>
        public BorderCommandApplyRange ApplyRange
        {
            get
            {
                return _ApplyRange; 
            }
            set
            {
                _ApplyRange = value; 
            }
        }

         
        private BorderSettingsStyle _BorderSettingsStyle = BorderSettingsStyle.None;

        public BorderSettingsStyle BorderSettingsStyle
        {
            get { return _BorderSettingsStyle; }
            set { _BorderSettingsStyle = value; }
        }

        public void SetBorderSettingsStyle()
        {
            if (this.LeftBorder
                && this.TopBorder
                && this.RightBorder
                && this.BottomBorder)
            {
                if (this.CenterVerticalBorder && this.MiddleHorizontalBorder)
                {
                    this.BorderSettingsStyle = BorderSettingsStyle.Both;
                }
                else if (this.CenterVerticalBorder == false
                    && this.MiddleHorizontalBorder == false)
                {
                    this.BorderSettingsStyle = BorderSettingsStyle.Rectangle;
                }
                else
                {
                    this.BorderSettingsStyle = BorderSettingsStyle.Custom;
                }
            }
            else if (this.LeftBorder == false
                && this.TopBorder == false
                && this.RightBorder == false
                && this.BottomBorder == false
                && this.CenterVerticalBorder == false
                && this.MiddleHorizontalBorder == false)
            {
                this.BorderSettingsStyle = BorderSettingsStyle.None;
            }
            else
            {
                this.BorderSettingsStyle = BorderSettingsStyle.Custom;
            }
        }

        //public void Read(XTextSelection selection )
        //{
        //    if (selection == null)
        //    {
        //        throw new ArgumentNullException("selection");
        //    }
        //    this.Clear();
        //    this.ForTable = false;
        //    XTextElementList cells = null ;
        //    if (selection.Mode == ContentRangeMode.Cell)
        //    {
        //        cells = selection.Cells;
        //    }
        //    else
        //    {
        //        XTextTableCellElement cell = selection.ContentElements[0].OwnerCell;
        //        if (cell != null)
        //        {
        //            cells.Add(cell);
        //        }
        //    }
        //    if ( cells != null && cells.Count > 0 )
        //    {
        //        // 纯粹的选择了表格单元格
        //        this.ForTable = true;
        //        // 获得左上角的单元格
        //        XTextTableCellElement firstCell = ( XTextTableCellElement ) selection.Cells[0];
        //        this.TopBorder = firstCell.RuntimeStyle.BorderTop;
        //        this.LeftBorder = firstCell.RuntimeStyle.BorderLeft;
        //        this.BorderColor = firstCell.RuntimeStyle.BorderColor;
        //        this.BorderStyle = firstCell.RuntimeStyle.BorderStyle;
        //        // 获得占据右下角的单元格
        //        XTextTableCellElement lastCell = firstCell;
        //        bool hasBothBorder = true ;
        //        foreach (XTextTableCellElement cell in selection.Cells)
        //        {
        //            if (cell.IsOverrided)
        //            {
        //                continue;
        //            }
        //            DocumentContentStyle rs = cell.RuntimeStyle;
        //            if (hasBothBorder)
        //            {
        //                if (rs.BorderLeft == false
        //                    || rs.BorderTop == false
        //                    || rs.BorderRight == false
        //                    || rs.BorderBottom == false)
        //                {
        //                    hasBothBorder = false;
        //                }
        //            }
        //            if (this.BackgroundColor.A != 0)
        //            {
        //                if (rs.BackgroundColor.A != 0)
        //                {
        //                    this.BackgroundColor = rs.BackgroundColor;
        //                }
        //            }
        //            if (cell.RowIndex + cell.RowSpan > lastCell.RowIndex + lastCell.RowSpan)
        //            {
        //                lastCell = cell;
        //            }
        //            else if (cell.ColIndex + cell.ColSpan > lastCell.ColIndex + lastCell.ColSpan)
        //            {
        //                lastCell = cell;
        //            }
        //        }//foreach
        //        if (hasBothBorder)
        //        {
        //            this.BorderSettingsStyle = Commands.BorderSettingsStyle.Both;
        //        }
        //        this.RightBorder = lastCell.RuntimeStyle.BorderRight;
        //        this.BottomBorder = lastCell.RuntimeStyle.BorderBottom;

        //        this.MiddleHorizontalBorder = false;
        //        this.CenterVerticalBorder = false;
        //        foreach (XTextTableCellElement cell in selection.Cells)
        //        {
        //            if (cell != firstCell && cell != lastCell)
        //            {
        //                if (this.CenterVerticalBorder == false)
        //                {
        //                    if (cell.ColIndex > firstCell.ColIndex
        //                        && cell.RuntimeStyle.BorderLeft)
        //                    {
        //                        this.CenterVerticalBorder = true;
        //                    }
        //                    else if (cell.ColIndex + cell.ColSpan < lastCell.ColIndex + lastCell.ColSpan
        //                        && cell.RuntimeStyle.BorderRight )
        //                    {
        //                        this.CenterVerticalBorder = true;
        //                    }
        //                }
        //                if (this.MiddleHorizontalBorder == false)
        //                {
        //                    if (cell.RowIndex > firstCell.RowIndex && cell.RuntimeStyle.BorderTop)
        //                    {
        //                        this.MiddleHorizontalBorder = true;
        //                    }
        //                    else if (cell.RowIndex + cell.RowSpan < lastCell.RowIndex + lastCell.RowSpan
        //                        && cell.RuntimeStyle.BorderBottom )
        //                    {
        //                        this.MiddleHorizontalBorder = true;
        //                    }
        //                }
        //                if (this.CenterVerticalBorder && this.MiddleHorizontalBorder)
        //                {
        //                    // 判断完成，提前退出循环
        //                    break;
        //                }
        //            }//if
        //        }//foreach
        //        this.ApplyRange = BorderCommandApplyRange.Cell;
        //    }
        //    else
        //    {
               
        //    }
        //}

        public void Clear()
        {
            this.TopBorder = true;
            this.MiddleHorizontalBorder = true;
            this.BottomBorder = true;
            this.LeftBorder = true;
            this.CenterVerticalBorder = true;
            this.RightBorder = true;
            this.BorderColor = Color.Black;
            this.BorderWidth = 1;
            this.BorderStyle = DashStyle.Solid;
            this.BackgroundColor = Color.Transparent;
           
            this.BorderSettingsStyle = BorderSettingsStyle.None;
        }
    }

    /// <summary>
    /// 边框命令应用范围
    /// </summary>
    public enum BorderCommandApplyRange
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text ,
        /// <summary>
        /// 段落
        /// </summary>
        Paragraph ,
        /// <summary>
        /// 单元格
        /// </summary>
        Cell ,
        /// <summary>
        /// 整个表格
        /// </summary>
        Table
    }

    public enum BorderSettingsStyle
    {
        /// <summary>
        /// 无边框
        /// </summary>
        None = 0 ,
        /// <summary>
        /// 方框
        /// </summary>
        Rectangle ,
        /// <summary>
        /// 全部
        /// </summary>
        Both,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom
    }

}
