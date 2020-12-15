/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
//
//      ！！！！！！！！！ 注意 ！！！！！！！！！！！！！！！！
//
//.NET1.1的窗体关闭事件 Form.Closed 在 .NET2.0中是过时的。在.NET2.0环境中
//
// 应当使用 Form.FormClosed 事件,这两个事件名称和使用的委托类型都不一样,为此
//
// 定义一个名为 DOTNET20 的条件编译标志,若编译环境使用.NET1.1则不需要定义这个
//
// 条件编译变量,若编译环境使用 .NET2.0则建议定义这个条件编译标记，即在代码文件
//
// 的第一行添加    #define DOTNET20
//
// 由于.NET2.0保持兼容性还支持Form.Closed事件，因此当不定义这个条件编译标记而
//
// 使用 Form.Closed 事件仍然可以编译通过,但仍建议进行上述的处理。
//
// 袁永福( http://www.xdesigner.cn ) 2006-12-12
//

//#define DOTNET20

using System;
using System.Text;
//using DCSoft.Common;

namespace DCSoft.WinForms.Native
{
	public enum AnimatedDrawStyle
	{
		/// <summary>
		/// 无样式
		/// </summary>
		None ,
		/// <summary>
		/// 系统样式
		/// </summary>
		System ,
		/// <summary>
		/// 矩形
		/// </summary>
		Rectangle ,
		/// <summary>
		/// 旋转矩形样式
		/// </summary>
		RotateRectangle 
	}
    /// <summary>
    /// 绘制矩形动画的模块,本模块为Win32API函数 DrawAnimatedRects 的托管包装,并提供了新的动画功能.
    /// </summary>
    /// <remarks>
    /// 编写 袁永福( http://www.xdesigner.cn ) 2006-12-12
    /// </remarks>
    /// <example>
    /// 测试 AnimatedRectDrawer 的功能的一段代码
	/// 
	/// public class AnimatedRectDrawerTestForm : System.Windows.Forms.Form
	/// {
	///    public AnimatedRectDrawerTestForm()
	///    {
	///        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	///        btn = new System.Windows.Forms.Button();
	///        this.Controls.Add(btn);
	///        btn.Text = "打开窗体";
	///        btn.Location = new System.Drawing.Point(30, 30);
	///        btn.Click += new EventHandler(btn_Click);
	///    }
	///    
	///    private AnimatedRectDrawer drawer = new AnimatedRectDrawer();
	///    System.Windows.Forms.Button btn = null;
	///    
	///    void btn_Click(object sender, EventArgs e)
	///    {
	///        System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
	///        drawer.Add(btn, frm);
	///        frm.Owner = this;
	///        frm.Show();
	///    }
	///    
	///    [STAThread]
	///    static void Main()
	///    {
	///        System.Windows.Forms.Application.Run(new AnimatedRectDrawerTestForm());
	///    }
	/// }//public class AnimatedRectDrawerTestForm : System.Windows.Forms.Form
	/// </example>
    public class AnimatedRectDrawer : System.IDisposable
    {
		private static int intDrawCount = 0 ;
		/// <summary>
		/// 绘制次数累计
		/// </summary>
		public static int DrawCount
		{
			get
			{
				return intDrawCount ;
			}
			set
			{
				intDrawCount = value;
			}
		}

		/// <summary>
		/// 默认动画样式
		/// </summary>
		public static AnimatedDrawStyle DefaultStyle = AnimatedDrawStyle.System ;

		private AnimatedDrawStyle intStyle = DefaultStyle ;
		/// <summary>
		/// 动画样式
		/// </summary>
		public AnimatedDrawStyle Style
		{
			get
			{
				return intStyle ;
			}
			set
			{
				intStyle = value;
			}
		}
        /// <summary>
        /// 注册项目类型
        /// </summary>
        public class DrawInfoItem
        {
            /// <summary>
            /// 动画起源控件
            /// </summary>
            public System.Windows.Forms.Control SourceControl = null;
            /// <summary>
            /// 动画起源区域
            /// </summary>
            public System.Drawing.Rectangle SourceRect = System.Drawing.Rectangle.Empty;
			/// <summary>
			/// 动画目标控件
			/// </summary>
			public System.Windows.Forms.Control DescControl = null;
			/// <summary>
			/// 动画目标区域
			/// </summary>
			public System.Drawing.Rectangle DescRect = System.Drawing.Rectangle.Empty ;
            /// <summary>
            /// 对象额外数据
            /// </summary>
            public object Tag = null;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public AnimatedRectDrawer()
        {

 
			_FormClosed = new System.EventHandler(Form_Closed);
 
            _FormLoad = new EventHandler(Form_Load);
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="ctl">原始控件</param>
        /// <param name="rect">原始矩形</param>
        /// <param name="frm">目标窗体</param>
        /// <returns>新增的项目对象</returns>
        public DrawInfoItem Add(
            System.Windows.Forms.Control ctl,
            System.Drawing.Rectangle rect, 
            System.Windows.Forms.Form frm)
        {
            DrawInfoItem item = GetItem(frm);
            if (item == null)
            {
                item = new DrawInfoItem();
                myItems.Add(item);
                frm.Load += _FormLoad;
 
				frm.Closed += _FormClosed;
 
            }
            item.SourceControl = ctl;
            item.SourceRect = rect;
			item.DescControl = frm ;
            return item;
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="ctl">原始控件</param>
        /// <param name="frm">目标窗体</param>
        /// <returns>新增的项目对象</returns>
        public DrawInfoItem Add(
            System.Windows.Forms.Control ctl,
            System.Windows.Forms.Form frm)
        {
            return Add(ctl, System.Drawing.Rectangle.Empty, frm);
        }
        /// <summary>
        /// 删除所有项目
        /// </summary>
        public void Clear()
        {
            myItems.Clear();
        }
        /// <summary>
        /// 为窗体查找项目对象
        /// </summary>
        /// <param name="frm">窗体对象</param>
        /// <returns>找到的项目对象,若未找到则返回空引用</returns>
        public DrawInfoItem GetItem(System.Windows.Forms.Form frm)
        {
            foreach (DrawInfoItem item in myItems)
            {
                if ( item.DescControl == frm )
                    return item;
            }
            return null;
        }

		public static void DrawOpenAnimatedRect(
			System.Windows.Forms.Control ctl ,
			System.Drawing.Rectangle rect , 
			System.Windows.Forms.Control frm ,
			AnimatedDrawStyle style)
		{
			DrawInfoItem item = new DrawInfoItem();
			item.SourceControl = ctl;
			item.SourceRect = rect;
			item.DescControl = frm ;
			InnerDraw( item , true , style );
		}

		public static void DrawCloseAnimatedRect(
			System.Windows.Forms.Control ctl ,
			System.Drawing.Rectangle rect ,
			System.Windows.Forms.Control frm , 
			AnimatedDrawStyle style)
		{
			DrawInfoItem item = new DrawInfoItem();
			item.SourceControl = ctl;
			item.SourceRect = rect;
			item.DescControl = frm ;
			InnerDraw( item , false , style );
		}

		public static void DrawAnimateRect(
			System.Windows.Forms.Control SourceControl ,
			System.Drawing.Rectangle SourceRect ,
			System.Windows.Forms.Control DescControl ,
			System.Drawing.Rectangle DescRect ,
			AnimatedDrawStyle style )
		{
			DrawInfoItem item = new DrawInfoItem();
			item.SourceControl = SourceControl ;
			item.SourceRect = SourceRect ;
			item.DescControl = DescControl ;
			item.DescRect = DescRect ;
			InnerDraw( item , true , style );
		}

        #region 内部代码群 ****************************************************
        /// <summary>
        /// 注册项目列表
        /// </summary>
        private System.Collections.ArrayList myItems = new System.Collections.ArrayList();
		/// <summary>
		/// 窗体关闭处理程序委托对象
		/// </summary>
 
		private System.EventHandler _FormClosed = null;
 
        /// <summary>
        /// 窗体打开处理程序委托对象
        /// </summary>
        private System.EventHandler _FormLoad = null;
        /// <summary>
        /// 窗体打开处理过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, System.EventArgs e)
        {
            DrawInfoItem item = GetItem((System.Windows.Forms.Form)sender);
            if (item != null)
            {
				intDrawCount ++ ;
                InnerDraw(item, true , this.intStyle );
            }
        }
		/// <summary>
		/// 窗体关闭处理过程
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
 
		private void Form_Closed( object sender , System.EventArgs e )
 
        {
            DrawInfoItem myItem = GetItem((System.Windows.Forms.Form)sender);
            if (myItem != null)
            {
                myItems.Remove(myItem);
                InnerDraw(myItem, false , this.intStyle );
            }
        }
        /// <summary>
        /// 内部的绘制动画的过程
        /// </summary>
        /// <param name="item">绘制信息对象</param>
        /// <param name="bolOpen">是否显示为打开过程</param>
        /// <returns>是否执行了操作</returns>
		private static bool InnerDraw(
			DrawInfoItem item, 
			bool bolOpen ,
			AnimatedDrawStyle style )
		{
			if( style == AnimatedDrawStyle.None )
				return false ;

			if (item == null)
				return false ;
            
			if (item.SourceControl == null || item.DescControl == null)
				return false ;

			System.Drawing.Rectangle srect = GetScreenRect( item.SourceRect  , item.SourceControl );

			if (srect.IsEmpty)
				return false ;

			System.Drawing.Rectangle trect = GetScreenRect( item.DescRect , item.DescControl );

			if (trect.IsEmpty)
				return false ;

			if( style == AnimatedDrawStyle.Rectangle )
			{
				if( bolOpen )
					MyDrawAnimatedRects( srect , trect );
				else
					MyDrawAnimatedRects( trect , srect );
                return true;
			}
			else if( style == AnimatedDrawStyle.RotateRectangle )
			{
				if( bolOpen )
					MyDrawAnimatedRotateRects( srect , trect );
				else
					MyDrawAnimatedRotateRects( trect , srect );
                return true;
			}
			else if( style == AnimatedDrawStyle.System )
			{
                System.Drawing.Point dp = new System.Drawing.Point(0, 0);
                //dp = item.SourceControl.PointToScreen(new System.Drawing.Point(0, 0));

				RECT rect1 = new RECT();
				rect1.left = srect.Left - dp.X ;
				rect1.top = srect.Top - dp.Y ;
				rect1.right = srect.Right - dp.X ;
				rect1.bottom = srect.Bottom - dp.Y ;

				RECT rect2 = new RECT();
				rect2.left = trect.Left - dp.X ;
				rect2.top = trect.Top - dp.Y ;
				rect2.right = trect.Right - dp.X ;
				rect2.bottom = trect.Bottom - dp.Y ;

                bool result = false;
                IntPtr handle = IntPtr.Zero;
                handle = item.DescControl.Handle;
                if (bolOpen)
					result = DrawAnimatedRects( handle , IDANI_CAPTION, ref rect1, ref rect2);
				else
					result = DrawAnimatedRects( handle , IDANI_CAPTION, ref rect2, ref rect1);
                if (result == false)
                {
                    System.ComponentModel.Win32Exception ext = new System.ComponentModel.Win32Exception();
                }
                return result;
			}
            return false;
		}

		private static void MyDrawAnimatedRects(
			System.Drawing.Rectangle rect1 , 
			System.Drawing.Rectangle rect2 )
		{
			System.Drawing.Rectangle LastRect = System.Drawing.Rectangle.Empty ;
            int tick = System.Environment.TickCount;
			for( int iCount = 0 ; iCount <= 10 ; iCount ++ )
			{
                
				System.Drawing.Rectangle rect = GetMiddleRectangle(
					rect1 , rect2 , iCount / 10.0 );
				if( LastRect.IsEmpty == false )
				{
					System.Windows.Forms.ControlPaint.DrawReversibleFrame( 
						LastRect ,
						System.Drawing.Color.SkyBlue ,
						System.Windows.Forms.FrameStyle.Thick );
				}
				LastRect = rect ;
				System.Windows.Forms.ControlPaint.DrawReversibleFrame( 
					rect , 
					System.Drawing.Color.SkyBlue ,
					System.Windows.Forms.FrameStyle.Thick );
                tick += 30;
                while (System.Environment.TickCount < tick )
                {
                    System.Threading.Thread.Sleep(5);
                }
                //System.Threading.Thread.Sleep( 30 );
			}
			if( LastRect.IsEmpty == false )
			{
				System.Windows.Forms.ControlPaint.DrawReversibleFrame(
					LastRect , 
					System.Drawing.Color.SkyBlue , 
					System.Windows.Forms.FrameStyle.Thick );
			}
		}

		private static void MyDrawAnimatedRotateRects(
            System.Drawing.Rectangle rect1 ,
            System.Drawing.Rectangle rect2 )
		{
			System.Drawing.Rectangle LastRect = System.Drawing.Rectangle.Empty ;
			for( int iCount = 0 ; iCount <= 10 ; iCount ++ )
			{
				System.Drawing.Rectangle rect = GetMiddleRectangle(
					rect1 , rect2 , iCount / 10.0 );
				if( LastRect.IsEmpty == false )
				{
					DrawRotateRect( LastRect , Math.PI * ( iCount - 1 )/ 10 );
				}
				LastRect = rect ;
				DrawRotateRect( rect , Math.PI * iCount / 10 );
				System.Threading.Thread.Sleep( 30 );
			}
			if( LastRect.IsEmpty == false )
			{
				DrawRotateRect( LastRect , Math.PI );
			}
		}
		private static void DrawRotateRect( System.Drawing.Rectangle rect , double angle )
		{
			System.Drawing.Point[] ps = RotateRectanglePoints(
                new System.Drawing.Point( rect.Left + rect.Width / 2 , rect.Top + rect.Height /2 ),
                rect , 
                angle );
            System.Drawing.Point[] ps2 = new System.Drawing.Point[5];
            Array.Copy(ps, ps2, 4);
            ps2[4] = ps[0];
			using( ReversibleDrawer drawer = ReversibleDrawer.FromScreen() )
			{
				drawer.LineWidth = 2;
				drawer.DrawLines( ps2 );
			}
           
		}

        private static System.Drawing.Rectangle GetScreenRect(
            System.Drawing.Rectangle rect, 
            System.Windows.Forms.Control ctl)
        {
            System.Drawing.Rectangle result = System.Drawing.Rectangle.Empty;
            if (ctl == null)
            {
                result = rect;
            }
            else
            {
                //result = GetControlScreenBounds(ctl);
				if (rect.IsEmpty)
				{
					result = GetControlScreenBounds(ctl);
				}
				else
				{
					result = rect;
					result.Location = ctl.PointToScreen(result.Location);
				}
            }
            return result;
        }
//		protected static System.Drawing.Rectangle GetSourceScreenRect( DrawInfoItem item )
//		{
//			return GetScreenRect( item.SourceRect , item.SourceControl );
//		}
        /// <summary>
        /// 获得控件在屏幕中的矩形区域
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <returns>矩形区域对象</returns>
        protected static System.Drawing.Rectangle GetControlScreenBounds(
			System.Windows.Forms.Control ctl)
        {
            if (ctl == null)
                return System.Drawing.Rectangle.Empty;
            if (ctl.IsHandleCreated)
            {
                RECT rect = new RECT();
                if (GetWindowRect(ctl.Handle, ref rect))
                {
                    return new System.Drawing.Rectangle(
                        rect.left, 
                        rect.top, 
                        rect.right - rect.left, 
                        rect.bottom - rect.top);
                }
            }
            return ctl.Bounds;
        }

        #endregion

        #region 声明Win32API函数以及结构 **************************************

        private const int IDANI_CAPTION = 0x3;
        private const int IDANI_CLOSE = 0x2;
        private const int IDANI_OPEN = 0x1;


        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool DrawAnimatedRects(
            IntPtr hwnd, 
            int Ani, 
            ref RECT from, 
            ref RECT to);
        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref RECT rect);

        #endregion

		#region IDisposable 成员

		public void Dispose()
		{
			this.Clear();
		}

		#endregion

        /// <summary>
        /// 获得两个矩形间的一个过渡矩形
        /// </summary>
        /// <param name="rect1">原始矩形</param>
        /// <param name="rect2">目标矩形</param>
        /// <param name="rate">过渡系数，若为0则返回原始矩形，若为1则返回目标矩形</param>
        /// <returns>过渡矩形</returns>
        public static System.Drawing.Rectangle GetMiddleRectangle(
            System.Drawing.Rectangle rect1,
            System.Drawing.Rectangle rect2,
            double rate)
        {
            int left = rect1.Left + (int)((rect2.Left - rect1.Left) * rate);
            int top = rect1.Top + (int)((rect2.Top - rect1.Top) * rate);
            int w = rect1.Width + (int)((rect2.Width - rect1.Width) * rate);
            int h = rect1.Height + (int)((rect2.Height - rect1.Height) * rate);
            return new System.Drawing.Rectangle(left, top, w, h);
        }

        /// <summary>
        /// 逆时针旋转矩形
        /// </summary>
        /// <param name="o">原点</param>
        /// <param name="rect">旋转的矩形</param>
        /// <param name="angle">弧度</param>
        /// <returns>旋转后的矩形的四个顶点的坐标</returns>
        public static System.Drawing.Point[] RotateRectanglePoints(
            System.Drawing.Point o,
            System.Drawing.Rectangle rect,
            double angle)
        {
            System.Drawing.Point[] ps = To4Points(rect);
            for (int iCount = 0; iCount < ps.Length; iCount++)
            {
                ps[iCount] = RotatePoint(o, ps[iCount], angle);
            }
            return ps;
        }

        /// <summary>
        /// 返回表示矩形四个顶点坐标的点结构体数组
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <returns>包含4个元素的点结构体数组</returns>
        public static System.Drawing.Point[] To4Points(System.Drawing.Rectangle rect)
        {
            System.Drawing.Point[] p = new System.Drawing.Point[4];
            p[0] = new System.Drawing.Point(rect.X, rect.Y);
            p[1] = new System.Drawing.Point(rect.Right, rect.Y);
            p[2] = new System.Drawing.Point(rect.Right, rect.Bottom);
            p[3] = new System.Drawing.Point(rect.Left, rect.Bottom);
            return p;
        }

        /// <summary>
        /// 进行逆时针旋转指定弧度的角度处理
        /// </summary>
        /// <param name="o">原点</param>
        /// <param name="p">处理的点</param>
        /// <param name="angle">旋转的角度(弧度)</param>
        /// <returns>处理后的点</returns>
        public static System.Drawing.Point RotatePoint(
            System.Drawing.Point o,
            System.Drawing.Point p,
            double angle)
        {
            if (o.X == p.X && o.Y == p.Y)
                return p;
            double l = (p.X - o.X) * (p.X - o.X) + (p.Y - o.Y) * (p.Y - o.Y);
            l = System.Math.Sqrt(l);
            double alf = System.Math.Atan2(p.Y - o.Y, p.X - o.X);
            alf = alf - angle;
            System.Drawing.Point p2 = System.Drawing.Point.Empty;
            p2.X = (int)(o.X + l * System.Math.Cos(alf));
            p2.Y = (int)(o.Y + l * System.Math.Sin(alf));
            return p2;
        }

	}//public class AnimatedRectDrawer
}