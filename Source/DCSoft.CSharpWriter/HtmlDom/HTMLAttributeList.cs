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
	/// HTMLAttribute 集合对象,属性名称不区分大小写
	/// </summary>
	public class HTMLAttributeList : System.Collections.ICollection
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public HTMLAttributeList()
		{
		}

		private System.Collections.ArrayList myItems = new System.Collections.ArrayList();
		/// <summary>
		/// 返回指定序号的属性对象
		/// </summary>
		public HTMLAttribute this[ int index ]
		{
			get{ return ( HTMLAttribute ) myItems[ index ];}
		}

		/// <summary>
		/// 返回指定名称的属性对象,名称不区分大小写
		/// </summary>
		public HTMLAttribute this[ string name ]
		{
			get
			{
				foreach( HTMLAttribute a in myItems )
				{
					if( string.Compare( a.Name , name , true ) == 0 )
						return a;
				}
				return null;
			}
		}

		/// <summary>
		/// 返回属性在列表中的从0开始的序号
		/// </summary>
		/// <param name="attr">属性对象</param>
		/// <returns>序号,若不存在则返回-1</returns>
		public int IndexOf( HTMLAttribute attr )
		{
			return myItems.IndexOf( attr );
		}

		/// <summary>
		/// 判断列表是否包含指定的属性对象
		/// </summary>
		/// <param name="attr">属性对象</param>
		/// <returns>若包含指定的属性对象则返回true,否则返回false</returns>
		public bool Contains( HTMLAttribute attr )
		{
			return myItems.Contains( attr );
		}

		/// <summary>
		/// 删除指定的属性对象
		/// </summary>
		/// <param name="attr">属性对象</param>
		public void Remove( HTMLAttribute attr )
		{
			myItems.Remove( attr );
		}

		
		/// <summary>
		/// 判断是否存在指定名称的属性
		/// </summary>
		/// <param name="name">属性名称</param>
		/// <returns>若存在指定名称的属性则返回true,否则返回false</returns>
		public bool HasAttribute( string name )
		{
			return this[ name ] != null;
		}

		/// <summary>
		/// 删除指定名称的属性对象
		/// </summary>
		/// <param name="name">属性名称</param>
		public void RemoveAttribute( string name )
		{
			HTMLAttribute a = this[ name ];
			if( a != null)
				myItems.Remove( a );
		}

		/// <summary>
		/// 获得指定名称的属性值,若不存在指定名称的属性则返回空引用
		/// </summary>
		/// <param name="name">属性名称</param>
		/// <returns>属性值</returns>
		public string GetAttribute( string name )
		{
			HTMLAttribute a = this[ name ];
			if( a == null)
				return null;
			else
				return a.Value ;
		}

		/// <summary>
		/// 设置属性值
		/// </summary>
		/// <param name="name">属性名称</param>
		/// <param name="vValue">属性值</param>
		public void SetAttribute( string name , string vValue )
		{
			HTMLAttribute a = this[ name ];
			if( a == null)
			{
				a = new HTMLAttribute();
				a.Name = name ;
				myItems.Add( a );
			}
			a.Value = vValue ;
		}
		/// <summary>
		/// 删除所有的属性
		/// </summary>
		public void Clear()
		{
			myItems.Clear();
		}
		#region ICollection 成员

		/// <summary>
		/// 未实现
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return myItems.IsSynchronized ;
			}
		}

		/// <summary>
		/// 列表元素个数
		/// </summary>
		public int Count
		{
			get
			{
				return myItems.Count ;
			}
		}

		/// <summary>
		/// 未实现
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index)
		{
			// TODO:  添加 HTMLAttributeList.CopyTo 实现
		}

		/// <summary>
		/// 未实现
		/// </summary>
		public object SyncRoot
		{
			get
			{
				// TODO:  添加 HTMLAttributeList.SyncRoot getter 实现
				return null;
			}
		}

		#endregion

		#region IEnumerable 成员

		/// <summary>
		/// 获得属性对象枚举器
		/// </summary>
		/// <returns></returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return myItems.GetEnumerator();
		}

		#endregion
	}
}