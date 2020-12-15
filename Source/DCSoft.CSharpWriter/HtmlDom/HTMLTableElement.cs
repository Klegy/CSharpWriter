/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System ;
namespace DCSoft.HtmlDom
{
	/// <summary>
	/// 表格对象
	/// </summary>
	public class HTMLTableElement : HTMLContainer
	{
		/// <summary>
		/// 对象标签名称,返回"table"
		/// </summary>
		public override string TagName
		{
			get{ return StringConstTagName.Table ;}
		}
		/// <summary>
		/// 表格边框
		/// </summary>
		public string Border
		{
			get{ return GetAttribute( StringConstAttributeName.Border );}
			set{ SetAttribute( StringConstAttributeName.Border , value);}
		}
		/// <summary>
		/// 边框宽度
		/// </summary>
		public string BorderColor
		{
			get{ return GetAttribute( StringConstAttributeName.BorderColor );}
			set{ SetAttribute( StringConstAttributeName.BorderColor , value);}
		}
		/// <summary>
		/// 背景色
		/// </summary>
		public string BgColor
		{
			get{ return GetAttribute( StringConstAttributeName.BgColor );}
			set{ SetAttribute( StringConstAttributeName.BgColor , value);}
		}
		/// <summary>
		/// Sets or retrieves the amount of space between the border of the cell and the content of the cell. 
		/// </summary>
		public string CellPadding
		{
			get{ return GetAttribute( StringConstAttributeName.CellPadding );}
			set{ SetAttribute( StringConstAttributeName.CellPadding , value);}
		}
		/// <summary>
		/// Sets or retrieves the amount of space between cells in a table. 
		/// </summary>
		public string CellSpacing 
		{
			get{ return GetAttribute( StringConstAttributeName.CellSpacing );}
			set{ SetAttribute( StringConstAttributeName.CellSpacing ,value );}
		}
		/// <summary>
		/// Sets or retrieves which dividing lines (inner borders) are displayed. 
		/// </summary>
		public string Rules
		{
			get{ return GetAttribute( StringConstAttributeName.Rules );}
			set{ SetAttribute( StringConstAttributeName.Rules , value);}
		}
		/// <summary>
		/// 对齐方式,可以为 left,center,right
		/// </summary>
		public string Align
		{
			get{ return GetAttribute( StringConstAttributeName.Align );}
			set{ SetAttribute( StringConstAttributeName.Align , value);}
		}
		/// <summary>
		/// Sets or retrieves the background picture tiled behind the text and graphics in the object. 
		/// </summary>
		public string BackGround
		{
			get{ return GetAttribute( StringConstAttributeName.BackGround );}
			set{ SetAttribute( StringConstAttributeName.BackGround ,value);}
		}
		/// <summary>
		/// 宽度
		/// </summary>
		public string Width
		{
			get{ return GetAttribute( StringConstAttributeName.Width );}
			set{ SetAttribute( StringConstAttributeName.Width , value);}
		}
		/// <summary>
		/// 高度
		/// </summary>
		public string Height
		{
			get{ return GetAttribute( StringConstAttributeName.Height );}
			set{ SetAttribute( StringConstAttributeName.Height , value);}
		}
		/// <summary>
		/// 行数
		/// </summary>
		public int RowCount
		{
			get{ return myChildNodes.Count ;}
		}
		/// <summary>
		/// 列数
		/// </summary>
		public int ColCount
		{
			get{ return myCols.Count ;}
		}

		/// <summary>
		/// 行对象集合
		/// </summary>
		public HTMLElementList Rows
		{
			get{ return myChildNodes;}
		}
		/// <summary>
		/// 列对象集合
		/// </summary>
		public HTMLElementList Cols
		{
			get{ return myCols ;}
		}

		
		private HTMLElementList myCols = new HTMLElementList();
		/// <summary>
		/// 添加子标签对象
		/// </summary>
		/// <param name="e">对象</param>
		/// <returns>操作是否成功</returns>
		public override bool AppendChild(HTMLElement e)
		{
			if( e.TagName == StringConstTagName.Tr 
				|| e.TagName == StringConstTagName.TBody )
				myChildNodes.Add( e );
			else
				myCols.Add( e );
			e.Parent = this ;
			e.OwnerDocument = myOwnerDocument ;
			return true;
		}
		/// <summary>
		/// 将对象数据输出到XML书写其中
		/// </summary>
		/// <param name="myWriter">XML书写器</param>
		/// <returns>操作是否成功</returns>
		protected override bool InnerWrite(System.Xml.XmlWriter myWriter)
		{
			foreach( HTMLColElement col in myCols)
				col.Write( myWriter );
			foreach( HTMLElement tr in myChildNodes )
				tr.Write( myWriter );
			return true;
		}
		/// <summary>
		/// 表格对象下属对象只能是 tr,col或tbody
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
		internal override bool CheckChildTagName(string strName)
		{
			return strName == StringConstTagName.Tr 
				|| strName == StringConstTagName.Col 
				|| strName == StringConstTagName.TBody ;
		}
	}//public class HTMLTableElement : HTMLContainer
}