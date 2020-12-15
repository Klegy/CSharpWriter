/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 文档结束标记
	/// </summary>
    [Serializable()]
	public class DomEOFElement : DomElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomEOFElement()
		{
		}
		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="doc">对象所属文本</param>
		public DomEOFElement( DomDocument doc )
		{
			this.OwnerDocument = doc ;
			//RefreshSize( null );
		}

        ///// <summary>
        ///// 重新计算对象大小
        ///// </summary>
        ///// <param name="g">图形绘制对象</param>
        //public override void RefreshSize(System.Drawing.Graphics g)
        //{
        //    this.SizeInvalid = false;
        //    System.Drawing.Size size = GraphicsUnitConvert.Convert( 
        //        new System.Drawing.Size( 4 , 10 ) , 
        //        System.Drawing.GraphicsUnit.Pixel , 
        //        this.OwnerDocument.DocumentGraphicsUnit );
        //    this.Width = size.Width ;
        //    if( g == null )
        //        this.Height = size.Height ;
        //    else
        //        this.Height = ( int ) Math.Ceiling(
        //            this.OwnerDocument.DefaultStyle.Font.GetHeight( g ));
        //    this.SizeInvalid = false;
        //}

        ///// <summary>
        ///// 绘制对象图形
        ///// </summary>
        ///// <param name="g">图形绘制对象</param>
        ///// <param name="ClipRectangle">剪切区域</param>
        //public override void DrawContent(DocumentPaintEventArgs args )
        //{
        //    if( args.RenderStyle == DocumentRenderStyle.Paint )
        //    {
        //        if( this.OwnerLine != null )
        //        {
        //            args.Graphics.FillRectangle(
        //                System.Drawing.Brushes.Blue  ,
        //                this.AbsBounds );
        //        }
        //    }
        //}
	}
}