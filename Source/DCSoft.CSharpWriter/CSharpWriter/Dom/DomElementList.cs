/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 元素对象列表
	/// </summary>
	/// <remarks>
	/// 本列表专门用于放置若干个文本文档元素对象
	/// 编制 袁永福 2006-11-13
	/// </remarks>
    [Serializable()]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
    public class DomElementList : System.Collections.CollectionBase , ICloneable
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomElementList()
		{
    	}

    	/// <summary>
		/// 能否删除EOF元素
		/// </summary>
		private bool bolCanDeleteEOF = false;
		/// <summary>
		/// 能否删除EOF元素
		/// </summary>
		public bool CanDeleteEOF
		{
			get
            {
                return bolCanDeleteEOF ;
            }
			set
            {
                bolCanDeleteEOF = value;
            }
		}
		/// <summary>
		/// 结束标记元素
		/// </summary>
		private DomElement myEOFElement = null;
		/// <summary>
		/// 结束标记元素
		/// </summary>
		internal DomElement EOFElement
		{
			get
            {
                return myEOFElement ;
            }
			set
			{
				int index = this.List.IndexOf( myEOFElement );
                if (index >= 0)
                {
                    this.List.RemoveAt(index);
                }
				myEOFElement = value; 
				CheckEOF( -1 );
			}
		}


        /// <summary>
        /// 修正元素序号以保证需要在元素列表的范围内
        /// </summary>
        /// <param name="index">原始的序号</param>
        /// <returns>修正后的序号</returns>
        public int FixElementIndex(int index)
        {
            if (index <= 0)
            {
                return 0;
            }
            if (index >= this.Count)
            {
                return this.Count - 1;
            }
            return index;
        }

        /// <summary>
        /// 检查元素序号是否合法
        /// </summary>
        /// <param name="index">元素序号</param>
        /// <returns>是否合法</returns>
        public bool CheckElementIndex(int index)
        {
            return index >= 0 && index < this.Count - 1;
        }


		protected void CheckEOF( int index )
		{
			if( myEOFElement != null )
			{
                if (this.LastElement != myEOFElement)
                {
                    this.List.Add(myEOFElement);
                }
				if( index >= 0 )
				{
                    if (index >= this.Count)
                    {
                        throw new System.Exception("EOF是只读的");
                    }
				}
			}
		}
		/// <summary>
		/// 列表所属容器元素
		/// </summary>
		protected DomContainerElement myOwnerElement = null;
		/// <summary>
		/// 列表所属容器元素
		/// </summary>
		public DomContainerElement OwnerElement
		{
			get
            {
                return myOwnerElement ;
            }
			set
            {
                myOwnerElement = value;
            }
		}
		/// <summary>
		/// 列表中的第一个元素
		/// </summary>
		public DomElement FirstElement
		{
			get
			{
                if (this.Count > 0)
                {
                    return (DomElement)this.List[0];
                }
                else
                {
                    return null;
                }
			}
		}
		/// <summary>
		/// 列表中的最后一个元素
		/// </summary>
		public DomElement LastElement
		{
			get
			{
				if( this.Count > 0 )
					return ( DomElement ) this.List[ this.Count - 1];
				else
					return null;
			}
		}

		/// <summary>
		/// 元素列表中第一个显示在文档视图中的元素
		/// </summary>
		public DomElement FirstContentElement
		{
			get
			{
				foreach( DomElement element in this )
				{
					DomElement e = element.FirstContentElement ;
					if( e != null )
						return e ;
				}
				return null;
			}
		}
		/// <summary>
		/// 元素列表中最后一个显示在文档视图中的元素
		/// </summary>
		public DomElement LastContentElement
		{
			get
			{
				for( int iCount = this.Count - 1 ; iCount >= 0 ; iCount -- )
				{
					DomElement e = this[ iCount ].LastContentElement ;
					if( e != null )
						return e ;
				}
				return null;
			}
		}
		/// <summary>
		/// 安全的获得指定序号的元素,若序号超出范围则返回空引用
		/// </summary>
		/// <param name="index">序号</param>
		/// <returns>获得的元素对象</returns>
		public DomElement SafeGet( int index )
		{
            if (index >= 0 && index < this.Count)
            {
                return (DomElement)this.List[index];
            }
            else
            {
                return null;
            }
		}
		/// <summary>
		/// 返回指定序号的对象
		/// </summary>
		public DomElement this[ int index ]
		{
			get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(index + " (0->" + this.Count + ")");
                }
                return ( DomElement ) this.List[ index ] ;
            }
		}

		
		/// <summary>
		/// 设置元素
		/// </summary>
		/// <param name="index"></param>
		/// <param name="element"></param>
		public void SetElement( int index , DomElement element )
		{
			this.List[ index ] = element ;
		}
		/// <summary>
		/// 获得对象在列表中的序号
		/// </summary>
		/// <param name="element">对象</param>
		/// <returns>序号</returns>
		public int IndexOf( DomElement element )
		{
			this.CheckEOF( - 1 );
			return this.List.IndexOf( element );
		}

        /// <summary>
        /// 快速的获得对象在列表中的序号
        /// </summary>
        /// <param name="element">对象</param>
        /// <returns>序号</returns>
		public int FastIndexOf( DomElement element )
		{
            return this.List.IndexOf(element);

            //System.Collections.ArrayList list = this.InnerList ;
            //int end = list.Count ;
            //for( int iCount = 0 ; iCount < end ; iCount ++ )
            //{
            //    if( list[ iCount ] == element )
            //        return iCount ;
            //}
            //return -1 ;
		}

		/// <summary>
		/// 判断对象是否在列表中
		/// </summary>
		/// <param name="element">对象</param>
		/// <returns>对象是否在列表中</returns>
		public bool Contains( DomElement element)
		{
			this.CheckEOF( - 1 );
			return this.List.Contains( element );
		}

        /// <summary>
        /// 获得子列表
        /// </summary>
        /// <param name="startIndex">开始区域位置</param>
        /// <param name="length">长度</param>
        /// <returns>子列表</returns>
        public DomElementList GetRange(int startIndex, int length)
        {
            DomElementList result = new DomElementList();
            for (int iCount = 0; iCount < length; iCount++)
            {
                result.List.Add( this.List[startIndex + iCount] );
            }
            return result;
        }


        //internal int AddRaw(XTextElement element)
        //{
        //    return this.List.Add(element);
        //}

        /// <summary>
        /// 向列表添加对象
        /// </summary>
        /// <param name="element">对象</param>
        /// <returns>对象在列表中的序号</returns>
        public int Add(DomElement element)
		{
			this.CheckEOF( - 1 );
            if (myEOFElement != null)
            {
                if (myEOFElement.GetType().IsInstanceOfType(element) == false)
                {
                    this.List.Insert(this.Count - 1, element);
                    if (this.myOwnerElement != null)
                    {
                        element.Parent = myOwnerElement;
                        element.OwnerDocument = myOwnerElement.OwnerDocument;
                    }
                    return this.Count - 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (this.myOwnerElement != null)
                {
                    element.Parent = myOwnerElement;
                }
                return this.List.Add(element);
            }
		}

        internal int AddRaw(DomElement element)
        {
            return this.List.Add(element);
        }

		/// <summary>
		/// 向类别添加多个对象
		/// </summary>
		/// <param name="list">多个元素所在的列表</param>
		public void AddRange( DomElementList list )
		{
			this.CheckEOF( - 1 );
            if (list == null || list.Count == 0)
            {
                return;
            }
			if( myEOFElement != null )
			{
				this.InnerList.InsertRange( this.Count - 1 , list );
			}
			else
			{
				this.InnerList.AddRange( list );
			}
		}
		private bool bolFastRemoveFlag = false;
        /// <summary>
        /// 开始快速删除元素
        /// </summary>
		internal void BeginFastRemove()
		{
			bolFastRemoveFlag = true ;
		}
        /// <summary>
        /// 结束快速删除元素
        /// </summary>
		internal void EndFastRemove()
		{
			bolFastRemoveFlag = false;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			int end = this.Count  ;
            for (int iCount = 0; iCount < end; iCount++)
            {
                object obj = this.List[iCount];
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
			this.InnerList.Clear();
			this.InnerList.AddRange( list );
		}

        /// <summary>
        /// 若元素个数超过指定值,则删除元素直到元素个数等于指定值
        /// </summary>
        /// <param name="length">指定的元素个数</param>
        internal virtual void RemoveToFixLenght(int length)
        {
            bool flag = false;
            for (int iCount = this.List.Count - 1; iCount >= length; iCount--)
            {
                this.List.RemoveAt(iCount);
                flag = true;
            }
            if (flag)
            {
                //OnItemChange();
            }
        }

        /// <summary>
        /// 删除若干个元素
        /// </summary>
        /// <param name="startIndex">要删除区域的起始坐标</param>
        /// <param name="length">删除区域的长度</param>
        /// <returns>删除的元素个数</returns>
        public int RemoveRange(int startIndex, int length)
        {
            this.CheckEOF(-1);
            if (startIndex < 0 || startIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex=" + startIndex );
            }
            if (length < 0)
            {
                throw new ArgumentException("length=" + length);
            }
            if (startIndex + length > this.Count)
            {
                throw new ArgumentOutOfRangeException("start+length=" + Convert.ToString(startIndex + length));
            }
            if (length == 0)
            {
                return 0;
            }
            this.InnerList.RemoveRange(startIndex, length);
            return length;
        }

		/// <summary>
		/// 删除对象
		/// </summary>
		/// <param name="element">对象</param>
		/// <returns>操作是否成功</returns>
        public bool Remove(DomElement element)
        {
            this.CheckEOF(-1);
            if (element == null)
            {
                throw new System.ArgumentNullException("未指定元素");
            }
            if (element != myEOFElement || this.bolCanDeleteEOF)
            {
                if (bolFastRemoveFlag)
                {
                    int index = this.FastIndexOf(element);
                    if (index >= 0)
                    {
                        this.InnerList[index] = null;
                    }
                }
                else
                {
                    this.List.Remove(element);
                }
                return true;
            }
            else
            {
                throw new Exception("EOF不能删除");
            }
        }

        internal void RemoveRaw(DomElement element)
        {
            this.List.Remove(element);
        }

        internal void RemoveAtRaw(int index)
        {
            this.List.RemoveAt(index); 
        }


		/// <summary>
		/// 在指定的元素前面插入新的元素
		/// </summary>
		/// <param name="element">指定的元素</param>
		/// <param name="NewElement">要插入的新的元素</param>
		/// <returns>操作是否成功</returns>
		public bool InsertBefore( DomElement OldElement , DomElement NewElement )
		{
			this.CheckEOF( -1 );
			if( OldElement == null )
				throw new System.ArgumentNullException("未指定元素");
			if( NewElement == null )
				throw new System.ArgumentNullException("未指定新元素");
			int index = this.List.IndexOf( OldElement );
			if( index >= 0 )
			{
				this.List.Insert( index , NewElement );
				return true ;
			}
			return false;
		}

		/// <summary>
		/// 在指定的元素后面插入新的元素
		/// </summary>
		/// <param name="OldElement">指定的元素</param>
		/// <param name="NewElement">要插入的新的元素</param>
		/// <returns>操作是否成功</returns>
		public bool InsertAfter( DomElement OldElement , DomElement NewElement )
		{
			this.CheckEOF( -1 );
			if( OldElement == null )
				throw new System.ArgumentNullException("未指定元素");
			if( NewElement == null )
				throw new System.ArgumentNullException("未指定新元素");
			if( OldElement == myEOFElement )
				throw new Exception("不能在EOF后面插入元素");
			int index = this.List.IndexOf( OldElement );
			if( index >= 0 )
			{
				this.List.Insert( index + 1 , NewElement );
				return true ;
			}
			return false;
		}

		/// <summary>
		/// 插入元素
		/// </summary>
		/// <param name="index">序号</param></param>
		/// <param name="element">要插入的元素</param>
		public void Insert( int index , DomElement element )
		{
			CheckEOF( index );
			this.List.Insert( index , element );
		}
		/// <summary>
		/// 在指定元素前面插入多个元素
		/// </summary>
		/// <param name="OldElement">指定的元素</param>
		/// <param name="list">要插入的新元素列表</param>
		public void InsertRangeBefore( DomElement OldElement , DomElementList list )
		{
			this.CheckEOF( -1 );
			int index = this.IndexOf( OldElement );
			if( index >= 0 )
			{
				this.InnerList.InsertRange( index , list );
			}
		}

		/// <summary>
		/// 插入多个元素
		/// </summary>
		/// <param name="index">需要插入的位置</param>
		/// <param name="list">要插入的元素列表</param>
		public void InsertRange( int index , DomElementList list )
		{
			this.CheckEOF( -1 );
			if( list != null && list.Count > 0 )
			{
				for( int iCount = 0 ; iCount < list.Count ; iCount ++ )
				{
					this.List.Insert( index + iCount , list[ iCount ] );
				}
			}
		}

//		/// <summary>
//		/// 获得指定元素的前一个元素
//		/// </summary>
//		/// <param name="refElement">指定的元素</param>
//		/// <returns>该元素的前一个元素若没找到则返回空</returns>
//		public XTextElement GetPreElement( XTextElement refElement)
//		{
//			int index = this.IndexOf( refElement );
//			return SafeGet( index - 1 );
//		}
//
//		/// <summary>
//		/// 获得指定元素的后一个元素
//		/// </summary>
//		/// <param name="refElement">指定的元素</param>
//		/// <returns>该元素的前一个元素，若没有找到则返回空</returns>
//		public XTextElement GetNextElement( XTextElement refElement )
//		{
//			int index = this.IndexOf( refElement );
//			if( index >= 0 )
//				return SafeGet( index + 1 );
//			else
//				return null;
//		}


        /// <summary>
        /// 获得子列表
        /// </summary>
        /// <param name="startIndex">开始元素的序号</param>
        /// <param name="length">元素的个数</param>
        /// <returns>获得的子列表</returns>
        public DomElementList GetElements(int startIndex, int length)
        {
            if (startIndex <= 0)
                throw new ArgumentException("startIndex");
            DomElementList list = new DomElementList();
            int endIndex = Math.Min(this.Count - 1, startIndex + length - 1);
            for (int iCount = startIndex; iCount <= endIndex; iCount++)
            {
                list.Add(this[iCount]);
            }
            return list;
        }

		/// <summary>
		/// 获得指定元素前面的一个元素
		/// </summary>
		/// <param name="element">元素对象</param>
		/// <returns>该元素的前一个元素对象</returns>
		public DomElement GetPreElement( DomElement element )
		{
			int index = this.IndexOf( element );
            if (index > 0)
            {
                return this[index - 1];
            }
            else
            {
                return null;
            }
		}
		/// <summary>
		/// 获得指定元素后面的一个元素
		/// </summary>
		/// <param name="element">元素对象</param>
		/// <returns>该元素后面的一个元素</returns>
		public DomElement GetNextElement( DomElement element )
		{
			int index = this.IndexOf( element );
            if (index >= 0 && index < this.Count - 1)
            {
                return this[index + 1];
            }
            else
            {
                return null;
            }
		}

		/// <summary>
		/// 删除元素列表中指定序号后面的元素,并将删除的元素放置到一个新的元素列表中 
		/// </summary>
		/// <param name="index">指定删除开始的序号</param>
		/// <returns>放置删除的元素的元素列表</returns>
		public DomElementList Split( int index )
		{
			this.CheckEOF( -1 );
			DomElementList list = new DomElementList();
			int end = this.Count - 1 ;
			if( myEOFElement != null )
				end -- ;
			for( int iCount = end ;iCount >= index ; iCount -- )
			{
				list.List.Insert( 0 , this.List[ iCount ] );
				this.RemoveAt( iCount );
			}
			return list ;
		}

        public override string ToString()
        {
            this.CheckEOF(-1);
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            foreach (DomElement element in this)
            {
                myStr.Append(element.ToString());
            }
            return myStr.ToString();
        }

        public string GetInnerText()
        {
            this.CheckEOF(-1);
            System.Text.StringBuilder myStr = new System.Text.StringBuilder();
            foreach (DomElement element in this)
            {
                myStr.Append(element.Text);
            }
            return myStr.ToString();
        }

		public DomElementList DeeplyClone( )
		{
			this.CheckEOF( -1 );
			DomElementList list = new DomElementList();
			foreach( DomElement element in this )
			{
				list.Add( element.Clone( true ));
			}
			return list ;
		}
		/// <summary>
		/// 比较两个列表的元素清单是否一致
		/// </summary>
		/// <param name="list">元素列表</param>
		/// <returns>两个对象的元素清单是否一致</returns>
		public bool EqualsElements( DomElementList list )
		{
			this.CheckEOF( -1 );
			if( list != null && list.Count == this.Count )
			{
                for (int iCount = 0; iCount < this.Count; iCount++)
                {
                    DomElement element = this[iCount];
                    if (list[iCount] != element)
                    {
                        return false;
                    }
                }
				return true ;
			}
			return false;
		}

        public void Reverse()
        {
            base.InnerList.Reverse();
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DomElementList list = (DomElementList)System.Activator.CreateInstance(this.GetType());
            list.Clear();
            foreach (DomElement element in this)
            {
                list.List.Add(element);
            }
            return list;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DomElementList Clone()
        {
            DomElementList list = (DomElementList)System.Activator.CreateInstance(this.GetType());
            list.bolCanDeleteEOF = this.bolCanDeleteEOF;
            list.bolFastRemoveFlag = this.bolFastRemoveFlag;
            list.myEOFElement = this.myEOFElement;
            list.myOwnerElement = this.myOwnerElement;
            foreach (DomElement e in this)
            {
                list.Add(e);
            }
            return list;
        }
         
    }//public class XTextElementList : System.Collections.CollectionBase
}