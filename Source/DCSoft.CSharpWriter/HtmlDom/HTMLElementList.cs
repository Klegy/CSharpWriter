/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.HtmlDom
{
	/// <summary>
	/// HTML标签元素列表
	/// </summary>
	public class HTMLElementList : System.Collections.ICollection
	{
		private System.Collections.ArrayList myList = new System.Collections.ArrayList();
		/// <summary>
		/// 初始化对象
		/// </summary>
		public HTMLElementList()
		{
		}
		/// <summary>
		/// 列表中第一个元素
		/// </summary>
		public HTMLElement FirstElement
		{
			get
			{
				if( myList.Count > 0 )
					return ( HTMLElement ) myList[0];
				else
					return null;
			}
		}
		/// <summary>
		/// 列表中最后一个元素
		/// </summary>
		public HTMLElement LastElement
		{
			get
			{
				if( myList.Count > 0 )
					return ( HTMLElement) myList[ myList.Count -1 ];
				else
					return null;
			}
		}
		/// <summary>
		/// 获得列表中指定元素的前一个元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <returns>前一个元素,若不存在则返回空引用</returns>
		public HTMLElement GetPreElement( HTMLElement element )
		{
			int index = myList.IndexOf(element );
			if( index > 0 )
				return ( HTMLElement) myList[ index -1 ];
			else
				return null;
		}
		/// <summary>
		/// 获得列表中指定元素后面的一个元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <returns>后一个元素,若不存在则返回空引用</returns>
		public HTMLElement GetNextElement( HTMLElement element )
		{
			int index = myList.IndexOf( element );
			if( index >= 0 && index < myList.Count -1 )
				return ( HTMLElement) myList[ index +1 ];
			else
				return null;
		}
		/// <summary>
		/// 获得指定位置的元素对象
		/// </summary>
		public HTMLElement this[ int index ]
		{
			get{ return ( HTMLElement )myList[ index ] ;}
		}

		/// <summary>
		/// 列表是否包含指定的元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <returns>若列表包含该元素则返回true,否则返回false</returns>
		public bool Contains( HTMLElement element )
		{
			return myList.Contains( element );
		}
		/// <summary>
		/// 向列表中添加指定的元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		public void Add( HTMLElement element )
		{
			myList.Add( element );
		}
		/// <summary>
		/// 从列表中删除指定的元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		public void Remove( HTMLElement element )
		{
			myList.Remove( element );
		}
		/// <summary>
		/// 清空列表
		/// </summary>
		public void Clear()
		{
			myList.Clear();
		}
		/// <summary>
		/// 返回指定元素在列表中的从0开始的序号
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <returns>序号,若不包含则返回-1</returns>
		public int IndexOf( HTMLElement element )
		{
			return myList.IndexOf( element );
		}
		#region ICollection 成员

		/// <summary>
		/// 未实现
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return myList.IsSynchronized ;
			}
		}

		/// <summary>
		/// 列表元素个数
		/// </summary>
		public int Count
		{
			get
			{
				return myList.Count ;
			}
		}
		/// <summary>
		/// 未实现
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index)
		{
			myList.CopyTo( array , index );
		}

		/// <summary>
		/// 未实现
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return myList.SyncRoot ;
			}
		}

		#endregion

		#region IEnumerable 成员

		/// <summary>
		/// 返回列表元素的枚举器
		/// </summary>
		/// <returns>枚举器</returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return myList.GetEnumerator();
		}
		#endregion
	}
}//public class HTMLElementList : System.Collections.ICollection