/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.Printing ;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文本文档书写器
	/// </summary>
	public class DocumentContentWriter
	{
		/// <summary>
		/// 只包含选择的部分
		/// </summary>
		protected bool bolIncludeSelectionOnly = false;
		/// <summary>
		/// 只包含选择的部分
		/// </summary>
		public virtual bool IncludeSelectionOnly
		{
			get
            {
                return bolIncludeSelectionOnly ;
            }
			set
            {
                bolIncludeSelectionOnly = value;
            }
		}


        public DomDocument Document
        {
            get
            {
                if (_Documents == null)
                    return null;
                else
                    return _Documents.FirstDocument;
            }
            set
            {
                _Documents = new DomDocumentList(value); 
            }
        }


        private DomDocumentList _Documents = null;
        /// <summary>
        /// 要输出的文档对象列表
        /// </summary>
        public DomDocumentList Documents
        {
            get { return _Documents; }
            set { _Documents = value; }
        }

        /// <summary>
        /// 获得主文档对象
        /// </summary>
        public DomDocument MainDocument
        {
            get
            {
                if (_Documents == null)
                {
                    return null;
                }
                else
                {
                    return _Documents.FirstDocument;
                }
            }
        }


        private System.Drawing.RectangleF myClipRectangle = System.Drawing.RectangleF.Empty;
        /// <summary>
        /// 剪切矩形
        /// </summary>
        public System.Drawing.RectangleF ClipRectangle
        {
            get
            {
                return myClipRectangle;
            }
            set
            {
                myClipRectangle = value;
            }
        }


        private System.Drawing.PointF myOffset = System.Drawing.PointF.Empty;
        /// <summary>
        /// 坐标位置偏移量
        /// </summary>
        public System.Drawing.PointF Offset
        {
            get
            {
                return myOffset;
            }
            set
            {
                myOffset = value;
            }
        }


        public virtual bool IsVisible(DomElement element)
        {
            return true;
        }



        /// <summary>
        /// 输出报表元素集合
        /// </summary>
        /// <param name="items">报表元素集合</param>
        public void WriteItems(DomElementList items )
        {
            foreach (DomElement element in items)
            {
                if (IsVisible(element) == false)
                {
                    continue;
                }
                if (this.ClipRectangle.IsEmpty == false)
                {
                    RectangleF rect = RectangleF.Intersect(
                        this.ClipRectangle,
                        element.AbsBounds);
                    if (rect.Height >= 2)
                    {
                        WriteElement(element);

                        //element.WriteDocument(this);
                    }
                }
                else
                {
                    WriteElement(element);
                }
            }//foreach
        }

        /// <summary>
        /// 输出一个元素
        /// </summary>
        /// <param name="element">元素对象</param>
        public virtual void WriteElement(DomElement element)
        {
        }

        ///// <summary>
        ///// 开始输出文档
        ///// </summary>
        //public virtual void WriteStartDocument()
        //{
        //}
        ///// <summary>
        ///// 结束输出文档
        ///// </summary>
        //public virtual void WriteEndDocument()
        //{
        //}

        ///// <summary>
        ///// 开始输出页眉
        ///// </summary>
        //public virtual void WriteStartHeader()
        //{
        //}
        ///// <summary>
        ///// 结束输出页眉
        ///// </summary>
        //public virtual void WriteEndHeader()
        //{
        //}
        ///// <summary>
        ///// 开始输出页脚
        ///// </summary>
        //public virtual void WriteStartFooter()
        //{
        //}
        ///// <summary>
        ///// 结束输出页脚
        ///// </summary>
        //public virtual void WriteEndFooter()
        //{
        //}
		 

        ///// <summary>
        ///// 开始输出段落
        ///// </summary>
        ///// <param name="info">文档格式信息</param>
        //public virtual void WriteStartParagraph( DocumentContentStyle style )
        //{
        //}

        ///// <summary>
        ///// 结束输出段落
        ///// </summary>
        //public virtual void WriteEndParagraph()
        //{
        //}

        ///// <summary>
        ///// 开始输出纯文本
        ///// </summary>
        ///// <param name="strText">文本内容</param>
        ///// <param name="info">文档格式信息</param>
        //public virtual void WriteStartString( string strText , DocumentContentStyle style )
        //{
        //}

        ///// <summary>
        ///// 结束输出纯文本内容
        ///// </summary>
        //public virtual void WriteEndString()
        //{}

        ///// <summary>
        ///// 开始输出书签
        ///// </summary>
        ///// <param name="strName">书签名称</param>
        //public virtual void WriteStartBookmark( string strName )
        //{}

        ///// <summary>
        ///// 结束输出书签
        ///// </summary>
        ///// <param name="strName">书签名称</param>
        //public virtual void WriteEndBookmark( string strName )
        //{}

        ///// <summary>
        ///// 输出一个换行符
        ///// </summary>
        //public virtual void WriteLineBreak()
        //{
        //}
        ///// <summary>
        ///// 输出图片内容
        ///// </summary>
        ///// <param name="img">图片对象</param>
        ///// <param name="width">显示象素宽度</param>
        ///// <param name="height">显示象素高度</param>
        ///// <param name="ImageData">图片数据</param>
        //public virtual void WriteImage(
        //    System.Drawing.Image img , 
        //    int width ,
        //    int height ,
        //    byte[] ImageData ,
        //    DocumentContentStyle style )
        //{}
	}
}