/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.CSharpWriter.Dom.Undo
{
	/// <summary>
	/// 撤销设置对象属性操作
	/// </summary>
	public class XTextUndoNameProperty : XTextUndoBase
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public XTextUndoNameProperty()
		{
		}

		/// <summary>
		/// 属性信息
		/// </summary>
		protected System.Reflection.PropertyInfo myProperty = null;
		/// <summary>
		/// 属性信息
		/// </summary>
		public System.Reflection.PropertyInfo Property
		{
			get{ return myProperty ;}
			set{ myProperty = value;}
		}

		/// <summary>
		/// 对象实例
		/// </summary>
		protected object objObjectInstance = null;
		/// <summary>
		/// 对象实例
		/// </summary>
		public object ObjectInstance
		{
			get{ return objObjectInstance ;}
			set{ objObjectInstance = value;}
		}

		/// <summary>
		/// 旧数据
		/// </summary>
		protected object objOldValue = null;
		/// <summary>
		/// 旧数据
		/// </summary>
		public object OldValue
		{
			get{ return objOldValue ;}
			set{ objOldValue = value;}
		}

		/// <summary>
		/// 新数据
		/// </summary>
		protected object objNewValue = null;
		/// <summary>
		/// 新数据
		/// </summary>
		public object NewValue
		{
			get{ return objNewValue ;}
			set{ objNewValue = value;}
		}

		/// <summary>
		/// 重复操作
		/// </summary>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			if( myProperty != null && objObjectInstance != null )
			{
				myProperty.SetValue( objObjectInstance , objNewValue , null );
				if( objObjectInstance is DomElement )
				{
                    DomElement element = (DomElement)objObjectInstance;
                    element.SizeInvalid = true;
					this.OwnerList.RefreshElements.Add(element );
				}
                if (objObjectInstance is DomDocument)
                {
                    if (myProperty.Name == "PageSettings"
                        || myProperty.Name == "DefaultStyle")
                    {
                        this.OwnerList.NeedRefreshDocument = true;
                    }
                }
			}
		}

		/// <summary>
		/// 撤销操作
		/// </summary>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			if( myProperty != null && objObjectInstance != null )
			{
				myProperty.SetValue( objObjectInstance , objOldValue , null );
				if( objObjectInstance is DomElement )
				{
                    DomElement element = (DomElement)objObjectInstance;
                    element.SizeInvalid = true;
					this.OwnerList.RefreshElements.Add( element );
				}
                if (objObjectInstance is DomDocument)
                {
                    if (myProperty.Name == "PageSettings")
                    {
                        this.OwnerList.NeedRefreshDocument = true;
                    }
                }
			}
		}
	}
}