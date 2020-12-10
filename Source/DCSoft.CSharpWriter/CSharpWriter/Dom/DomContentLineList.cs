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
	/// 文本行集合
	/// </summary>
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class DomContentLineList : System.Collections.CollectionBase
	{
		/// <summary>
		/// 获得指定序号处的文本行对象
		/// </summary>
		public DomContentLine this[ int index ]
		{
			get{ return( DomContentLine ) this.List[ index ] ;}
		}
		/// <summary>
		/// 获得指定的文本行对象在列表中的序号
		/// </summary>
		/// <param name="line">文本行对象</param>
		/// <returns>文本行对象在列表中的序号,若未找到则返回-1</returns>
		public int IndexOf( DomContentLine line )
		{
			return this.List.IndexOf( line );
		}
		/// <summary>
		/// 判断列表是否存在指定的文本行对象
		/// </summary>
		/// <param name="line">文本行对象</param>
		/// <returns>列表中是否存在指定的文本行对象</returns>
		public bool Contains( DomContentLine line )
		{
			return this.List.Contains( line );
		}
		/// <summary>
		/// 所有文本行的高度和
		/// </summary>
		public float Height
		{
			get
			{
				float h = 0 ;
				DomContentLine LastLine = null;
				foreach( DomContentLine line in this )
				{
					LastLine = line ;
					h = h + line.Height + line.Spacing ;
				}
				if( LastLine != null )
					h -= LastLine.Spacing ;
				return h ;
			}
		}
		/// <summary>
		/// 添加文本行
		/// </summary>
		/// <param name="line">要添加的文本行对象</param>
		/// <returns>新文本行在列表中的序号</returns>
		public int Add( DomContentLine line )
		{
			if( line != null )
			{
				//line.OwnerList = this ;
				return this.List.Add( line );
			}
			return -1 ;
		}

		/// <summary>
		/// 将旧行对象替换成新的行对象
		/// </summary>
		/// <param name="index">序号</param>
		/// <param name="NewLine">新的行对象</param>
		public void Replace( int index , DomContentLine NewLine )
		{
			//NewLine.OwnerList = this ;
			this.List[ index ] = NewLine ;
		}

//		public void Replace( XTextLine OldLine , XTextLine NewLine )
//		{
//
//		}
		/// <summary>
		/// 插入文本行对象
		/// </summary>
		/// <param name="index">插入的位置序号</param>
		/// <param name="line">要插入的文本行对象</param>
		public void Insert( int index , DomContentLine line )
		{
			//line.OwnerList = this ;
			this.List.Insert( index , line );
		}
		/// <summary>
		/// 删除文本行对象
		/// </summary>
		/// <param name="line">要删除的文本行对象</param>
		public void Remove( DomContentLine line )
		{
			this.List.Remove( line );
		}

		/// <summary>
		/// 列表中最后 一行文本行
		/// </summary>
		internal DomContentLine LastLine
		{
			get
			{
				if( this.Count > 0 )
					return ( DomContentLine ) this.List[ this.Count - 1 ];
				else
					return null;
			}
		}
	}//public class XTextLineList : System.Collections.CollectionBase
}