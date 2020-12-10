/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
 


namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 换行元素
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.Xml.Serialization.XmlType("XLineBreak")]
    [Serializable()]
	public class DomLineBreakElement : DomElement
	{
		private static System.Drawing.Bitmap myIcon = null;
		/// <summary>
		/// 静态构造函数,加载图标
		/// </summary>
		static DomLineBreakElement()
		{
            myIcon = WriterResources.linebreak;
            myIcon.MakeTransparent(System.Drawing.Color.White);
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomLineBreakElement()
		{
			
		}

		internal void RefreshSize2( DomElement PreElement )
		{
			float h =  this.OwnerDocument.DefaultStyle.DefaultLineHeight ;
			if( PreElement != null && PreElement.Height > 0 )
			{
				h = PreElement.Height ;
			}
			h = Math.Max( h , this.OwnerDocument.PixelToDocumentUnit( 10 ));
			this.Height = h ;
			this.Width = this.OwnerDocument.PixelToDocumentUnit( 10 );
			this.SizeInvalid = false;
		}

		/// <summary>
		/// 返回对象包含的字符串数据
		/// </summary>
		/// <returns>字符串数据</returns>
		public override string ToString()
		{
			return System.Environment.NewLine ;
		}
		 
        public override void WriteRTF(DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.WriteLineBreak();
        }
	}
}