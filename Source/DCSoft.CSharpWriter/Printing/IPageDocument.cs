/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
namespace DCSoft.Printing
{
	/// <summary>
	/// 支持分页显示的文档接口类型
	/// </summary>
	public interface IPageDocument
	{
        /// <summary>
        /// 页面设置信息对象
        /// </summary>
        XPageSettings PageSettings
        {
            get;
            set;
        }
		/// <summary>
		/// 文档使用的绘图单位
		/// </summary>
		System.Drawing.GraphicsUnit DocumentGraphicsUnit
		{
			get ;
			set ;
		}
		/// <summary>
		/// 页面对象集合
		/// </summary>
		PrintPageCollection Pages
		{
			get ;
			set ;
		}

        ///// <summary>
        ///// 当前打印的页面序号
        ///// </summary>
        //int PageIndex
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 绘制文档内容
        /// </summary>
        /// <param name="sender">事件发起者</param>
        /// <param name="args">事件参数</param>
        void DrawContent( PageDocumentPaintEventArgs args );
	}//public interface IPageDocument
}