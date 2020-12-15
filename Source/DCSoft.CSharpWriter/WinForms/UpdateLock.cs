/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.WinForms
{
	/// <summary>
	/// 更新锁定对象
	/// </summary>
	public class UpdateLock
	{
		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public UpdateLock()
		{
		}

		private System.Windows.Forms.Control myBindControl = null;
		/// <summary>
		/// 对象绑定的控件对象
		/// </summary>
		public System.Windows.Forms.Control BindControl
		{
			get
            {
                return myBindControl ;
            }
			set
            {
                myBindControl = value;
            }
		}

		/// <summary>
		/// 更新层次 
		/// </summary>
		private int	intUpdateLevel			= 0;
		/// <summary>
		/// 禁止绘制用户界面，一般表示开始更新文档内容
		/// </summary>
		public void BeginUpdate()
		{
			intUpdateLevel ++ ;
		}
		/// <summary>
		/// 允许绘制用户界面，一般表示文档更新完毕，可以绘制文档内容
		/// </summary>
		public void EndUpdate()
		{
			intUpdateLevel -- ;
			if( intUpdateLevel < 0)
				intUpdateLevel = 0 ;
			if( intUpdateLevel <= 0 )
			{
				if( myBindControl != null )
					myBindControl.Update();
				if( Update != null )
					Update( this , null );
			}
		}
		/// <summary>
		/// 能否更新
		/// </summary>
		public bool CanUpdate()
		{
			return intUpdateLevel <= 0 ;
		}
		/// <summary>
		/// 正在更新界面
		/// </summary>
		public bool Updating 
		{
			get
            {
                return intUpdateLevel > 0 ;
            }
		}

        /// <summary>
        /// 清除更新状态
        /// </summary>
        public void ClearUpdateState()
        {
            intUpdateLevel = 0;
        }

		private object objTag = null;
		/// <summary>
		/// 对象额外数据
		/// </summary>
		public object Tag
		{
			get{ return objTag ;}
			set{ objTag = value;}
		}
		/// <summary>
		/// 更新界面事件
		/// </summary>
		public event System.EventHandler Update = null;

	}//public class UpdateLock
}