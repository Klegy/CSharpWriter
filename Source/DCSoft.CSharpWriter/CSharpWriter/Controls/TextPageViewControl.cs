/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.Printing ;
using DCSoft.Drawing;
using DCSoft.WinForms.Native;
using System.Windows.Forms;
using System.Drawing;

namespace DCSoft.CSharpWriter.Controls
{
	/// <summary>
	/// 支持文本编辑的分页视图控件
	/// </summary>
    [System.ComponentModel.ToolboxItem( false )]
	public class TextPageViewControl : PageViewControl
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public TextPageViewControl()
		{
			
		}

		#region 键盘操作代码群 ************************************************

		/// <summary>
		/// 获取或设置一个值，该值指示在控件中按 TAB 键时，是否在控件中键入一个 TAB 字符，而不是按选项卡的顺序将焦点移动到下一个控件。
		/// </summary>
		protected bool bolAcceptsTab = true;
		/// <summary>
		/// 获取或设置一个值，该值指示在控件中按 TAB 键时，是否在控件中键入一个 TAB 字符，而不是按选项卡的顺序将焦点移动到下一个控件。
		/// </summary>
        [System.ComponentModel.DefaultValue( true )]
        [System.ComponentModel.Category("Behavior")]
		public bool AcceptsTab
		{
			get
            { 
                return bolAcceptsTab;
            }
			set
            {
                bolAcceptsTab = value;
            }
		}

		private static System.Windows.Forms.Keys [] myInputKeys =
				{	
					System.Windows.Forms.Keys.Left ,
					System.Windows.Forms.Keys.Up ,
					System.Windows.Forms.Keys.Right ,
					System.Windows.Forms.Keys.Down ,
					System.Windows.Forms.Keys.Tab ,
					System.Windows.Forms.Keys.Enter ,
					System.Windows.Forms.Keys.ShiftKey  ,
					System.Windows.Forms.Keys.Control 
				};
		/// <summary>
		/// 重写键盘字符处理函数,保证控件能处理一些功能键
		/// </summary>
		/// <param name="keyData">按键数据</param>
		/// <returns>控件是否能处理按键数据</returns>
		protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
		{
            if (keyData == System.Windows.Forms.Keys.Tab
                && this.AcceptsTab == false)
            {
                return base.IsInputKey(keyData);
            }
			for(int iCount = 0 ; iCount < myInputKeys.Length ; iCount ++)
			{
				int iKey = (int)myInputKeys[iCount];
                if ((iKey & (int)keyData) == iKey)
                {
                    return true;
                }
			}
			return base.IsInputKey(keyData);
		}

		#endregion

		#region 光标操作函数群 ************************************************

		/// <summary>
		/// 是否强制显示光标而不管控件是否获得输入焦点
		/// </summary>
		private bool bolForceShowCaret = false;
		/// <summary>
		/// 是否强制显示光标而不管控件是否获得输入焦点
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility( System.ComponentModel.DesignerSerializationVisibility.Hidden )]
		public bool ForceShowCaret
		{
			get
            {
                return bolForceShowCaret ;
            }
			set
            {
                bolForceShowCaret = value;
            }
		}

		/// <summary>
		/// 移动光标时是否自动滚动到光标区域
		/// </summary>
		protected bool	bolMoveCaretWithScroll	= true;
		/// <summary>
		/// 移动光标时是否自动滚动到光标区域
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool MoveCaretWithScroll
		{
			get
            {
                return bolMoveCaretWithScroll ;
            }
			set
            {
                bolMoveCaretWithScroll = value;
            }
		}
		/// <summary>
		/// 当前是否处于插入模式,若处于插入模式,则光标比较细,否则处于改写模式,光标比较粗
		/// </summary>
		private bool	bolInsertMode = true;
		/// <summary>
		/// 当前是否处于插入模式,若处于插入模式,则光标比较细,否则处于改写模式,光标比较粗
		/// </summary>
		[System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden )]
		public virtual bool InsertMode
		{
			get
            {
                return bolInsertMode ;
            }
			set
            {
                bolInsertMode = value;
            }
		}

		private bool bolCaretCreated = false; // 光标已经创建标志
        /// <summary>
        /// 光标已经创建标志
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool CaretCreated
        {
            get
            {
                return bolCaretCreated;
            }
        }

		/// <summary>
		/// 默认光标宽度
		/// </summary>
		public static int DefaultCaretWidth = 2;
		/// <summary>
		/// 默认的宽光标的宽度,应用于修改模式中的文本编辑器
		/// </summary>
		public static int DefaultBroadCaretWidth = 6 ;

		/// <summary>
		/// 光标控制对象
		/// </summary>
		private Win32Caret myCaret = null;
        /// <summary>
        /// 光标控制对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public Win32Caret Caret
        {
            get
            {
                if (myCaret == null)
                {
                    myCaret = new Win32Caret(this);
                }
                return myCaret;
            }
        }

		/// <summary>
		/// 当前光标位置
		/// </summary>
		private System.Drawing.Rectangle myCaretBounds 
            = System.Drawing.Rectangle.Empty ;

        protected override void OnLoad(EventArgs e)
        {
            //if (this.DesignMode == false)
            //{
            //    myCaret = new Win32Caret(this);
            //}
            base.OnLoad(e);
        }
		/// <summary>
		/// 已重载:获得焦点,显示光标
		/// </summary>
		/// <param name="e"></param>
		protected override void OnGotFocus(EventArgs e)
		{
            //if( this.Focused )
            //{
            //    ShowCaret();
            //}
			base.OnGotFocus (e);
		}

        /// <summary>
        /// 显示插入点光标
        /// </summary>
        public void ShowCaret()
        {
            if (this.CaretCreated && myCaretBounds.IsEmpty == false)
            {
                this.Caret.Create(
                    1,
                    myCaretBounds.Width,
                    myCaretBounds.Height);
                this.Caret.SetPos(
                    myCaretBounds.X,
                    myCaretBounds.Y);
                this.Caret.Show();
            }
        }

		/// <summary>
		/// 已重载:失去焦点,隐藏光标
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLostFocus(EventArgs e)
		{
            if (this.CaretCreated )
            {
                myCaret.Hide();
            }
			base.OnLostFocus (e);
		}

		/// <summary>
		/// 移动光标到新的光标区域
		/// </summary>
		/// <param name="vLeft">在视图中光标区域的左端位置</param>
		/// <param name="vTop">在视图中光标区域的顶端位置</param>
		/// <param name="vWidth">光标区域宽度</param>
		/// <param name="vHeight">光标区域高度</param>
		/// <returns>移动光标是否造成滚动</returns>
		public bool MoveCaretTo(
            int vLeft , 
            int vTop , 
            int vWidth , 
            int vHeight)
		{
			//return false;

            if (this.IsUpdating)
            {
                return false;
            }
			if( this.ForceShowCaret == false 
                && this.Focused == false)
			{
                if (this.CaretCreated)
                {
                    this.Caret.Hide();
                }
				//this.ScrollToView( vLeft , vTop , vWidth , vHeight );
				return false;
			}

			int height = GraphicsUnitConvert.Convert(
                vHeight ,
                this.GraphicsUnit ,
                System.Drawing.GraphicsUnit.Pixel );

			if( vWidth > 0 && vHeight > 0 )
			{
				bolCaretCreated = this.Caret.Create( 0 , vWidth , height );
				if( this.CaretCreated )
				{
					if( bolMoveCaretWithScroll )
					{
						this.ScrollToView( vLeft , vTop  , vWidth , vHeight);
					}
					System.Drawing.Point p = this.ViewPointToClient( vLeft , vTop );
					myCaret.SetPos( p.X  , p.Y   );
					myCaret.Show();
					Win32Imm imm = new Win32Imm( this );
                    if (imm.IsImmOpen())
                    {
                        imm.SetImmPos(p);
                    }
					myCaretBounds = new System.Drawing.Rectangle( 
                        p.X ,
                        p.Y , 
                        vWidth , 
                        height );
					if( this.MoveCaretWithScroll )
					{
						return true ;
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// 针对文本编辑器的移动光标到指定位置
		/// </summary>
		/// <param name="vLeft">光标的左端区域</param>
		/// <param name="vBottom">光标的底端区域</param>
		/// <param name="vHeight">光标的高度</param>
		/// <returns>移动光标是否造成滚动</returns>
		public bool MoveTextCaretTo( int vLeft , int vBottom , int vHeight)
		{
			return MoveCaretTo(
				vLeft , 
				vBottom - vHeight , 
				( bolInsertMode ? DefaultCaretWidth : DefaultBroadCaretWidth ) , 
				vHeight);
		}

		/// <summary>
		/// 隐藏光标
		/// </summary>
		public void HideOwnerCaret()
		{
			this.Caret.Hide();
		}

		#endregion
	}//public class TextPageViewControl : PageScrollableControl
}