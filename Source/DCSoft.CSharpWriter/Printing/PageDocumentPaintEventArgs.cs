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
using DCSoft.Drawing ;

namespace DCSoft.Printing
{
    /// <summary>
    /// 分页文档绘制内容事件委托类型
    /// </summary>
    /// <param name="sender">事件发起者</param>
    /// <param name="args">事件参数</param>
    public delegate void PageDocumentPaintEventHandler(
        object sender ,
        PageDocumentPaintEventArgs args );

    /// <summary>
    /// 分页文档绘制内容事件参数
    /// </summary>
    public class PageDocumentPaintEventArgs : EventArgs 
    {
        public PageDocumentPaintEventArgs(
            Graphics graphics,
            Rectangle clipRectangle,
            IPageDocument document,
            PrintPage page,
            PageContentPartyStyle contentStyle)
        {
            _Graphics = graphics;
            _ClipRectangle = clipRectangle;
            _Document = document;
            _Page = page;
            _ContentStyle = contentStyle;
            if (page != null)
            {
                _PageIndex = page.GlobalIndex + 1;
            }
        }

        private int _PageIndex = 0;
        /// <summary>
        /// 从1开始计算的页码号
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex; 
            }
            set
            {
                _PageIndex = value; 
            }
        }

        private int _NumberOfPages = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        [System.ComponentModel.DefaultValue( 0 )]
        public int NumberOfPages
        {
            get
            {
                return _NumberOfPages; 
            }
            set
            {
                _NumberOfPages = value; 
            }
        }

        private Graphics _Graphics = null;
        /// <summary>
        /// 图形绘制对象
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return _Graphics; 
            }
        }

        private Rectangle _ClipRectangle = Rectangle.Empty;
        /// <summary>
        /// 剪切矩形
        /// </summary>
        public Rectangle ClipRectangle
        {
            get
            {
                return _ClipRectangle; 
            }
        }

        private Rectangle _ContentBounds = Rectangle.Empty;
        /// <summary>
        /// 当绘制页眉页脚时，页眉页脚内容边界
        /// </summary>
        public Rectangle ContentBounds
        {
            get 
            {
                return _ContentBounds; 
            }
            set
            {
                _ContentBounds = value; 
            }
        }
        private IPageDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public IPageDocument Document
        {
            get
            {
                return _Document; 
            }
        }

        private PrintPage _Page = null;
        /// <summary>
        /// 页面对象
        /// </summary>
        public PrintPage Page
        {
            get
            {
                return _Page; 
            }
        }

        private PageContentPartyStyle _ContentStyle = PageContentPartyStyle.Body;
        /// <summary>
        /// 内容样式
        /// </summary>
        public PageContentPartyStyle ContentStyle
        {
            get
            {
                return _ContentStyle; 
            }
            //set
            //{
            //    _ContentStyle = value; 
            //}
        }

        private ContentRenderMode _RenderMode = ContentRenderMode.Paint;
        /// <summary>
        /// 内容呈现模式
        /// </summary>
        public ContentRenderMode RenderMode
        {
            get
            { 
                return _RenderMode;
            }
            set
            {
                _RenderMode = value;
            }
        }

        //private bool _EditMode = false;
        ///// <summary>
        ///// 处于编辑模式
        ///// </summary>
        //public bool EditMode
        //{
        //    get
        //    {
        //        return _EditMode;
        //    }
        //    set
        //    {
        //        _EditMode = value;
        //    }
        //}

        private bool _Cancel = false;
        /// <summary>
        /// 取消操作
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _Cancel; 
            }
            set
            {
                _Cancel = value; 
            }
        }
    }

    public enum ContentRenderMode
    {
        /// <summary>
        /// 绘制图形
        /// </summary>
        Paint ,
        /// <summary>
        /// 打印
        /// </summary>
        Print,
        /// <summary>
        /// 生成元素图片
        /// </summary>
        GenerateElementImage

    }
}
