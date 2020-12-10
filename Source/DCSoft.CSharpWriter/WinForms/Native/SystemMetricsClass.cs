/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;

namespace DCSoft.WinForms.Native
{
	/// <summary>
	/// 获得Windows32操作系统一些系统设置
	/// </summary>
    /// <remarks>
    /// 本类型是WindowsAPI函数GetSystemMetrics的一个托管封装,本类型内部不缓存数据,而是
    /// 调用GetSystemMetrics函数,因此本类型的各个属性能返回实时数据.
    /// 编写 袁永福
    /// </remarks>
    public sealed class SystemMetricsClass
	{
		#region 获得数据的属性群 ******************************************************************

		/// <summary>
		/// 获得主显示器象素宽度
		/// </summary>
		public static int CXSCREEN             
		{
			get 
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXSCREEN);
			}
		}
		/// <summary>
		/// 显示主显示器象素高度
		/// </summary>
		public static int CYSCREEN
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYSCREEN );
			}
		}
		/// <summary>
		/// 垂直滚动条象素宽度
		/// </summary>
		public static int CXVSCROLL
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXVSCROLL );
			}
		}
		/// <summary>
		/// 水平滚动条象素高度
		/// </summary>
		public static int CYHSCROLL
		{ get
		  {
			  return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYHSCROLL);
		  }
		}
		/// <summary>
		/// 窗体标题栏象素高度
		/// </summary>
		public static int CYCAPTION 
		{
			get
			{ return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYCAPTION);
			}
		}
		/// <summary>
		/// 窗体边框线象素宽度
		/// </summary>
		public static int CXBORDER 
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXBORDER );
			}
		}
		/// <summary>
		/// 窗体边框线象素高度
		/// </summary>
		public static int CYBORDER
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYBORDER);
			}
		}
		/// <summary>
		/// 等于CXFIXEDFRAME
		/// </summary>
		public static int CXDLGFRAME
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXDLGFRAME);
			}
		}
		/// <summary>
		/// 等于CYFIXEDFRAME
		/// </summary>
		public static int CYDLGFRAME
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYDLGFRAME );
			}
		}
		/// <summary>
		/// 垂直滚动条上滚动块的高度
		/// </summary>
		public static int CYVTHUMB
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYVTHUMB );
			}
		}
		/// <summary>
		/// 滚动块在水平滚动条上的大小
		/// </summary>
		public static int CXHTHUMB
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXHTHUMB );
			}
		}
		/// <summary>
		/// 标准图标象素宽度
		/// </summary>
		public static int CXICON
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXICON );
			}
		}
		/// <summary>
		/// 标准图标象素高度
		/// </summary>
		public static int CYICON
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYICON);
			}
		}
		/// <summary>
		/// 鼠标光标象素宽度
		/// </summary>
		public static int CXCURSOR
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXCURSOR);
			}
		}
		/// <summary>
		/// 鼠标光标象素高度
		/// </summary>
		public static int CYCURSOR 
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYCURSOR);
			}
		}
		/// <summary>
		/// 单行菜单栏的象素高度
		/// </summary>
		public static int CYMENU 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMENU );
			}
		}
		/// <summary>
		/// 主显示器中最大化窗体的客户区象素宽度
		/// </summary>
		public static int CXFULLSCREEN 
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXFULLSCREEN);
			}
		}
		/// <summary>
		/// 主显示器中最大化窗体的客户区的象素高度
		/// </summary>
		public static int CYFULLSCREEN
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYFULLSCREEN);
			}
		}
		/// <summary>
		/// Kanji窗口的象素高度
		/// </summary>
		public static int CYKANJIWINDOW
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYKANJIWINDOW );
			}
		}
		/// <summary>
		/// 鼠标是否安装,非0则鼠标安装了,0则没安装鼠标
		/// </summary>
		public static int MOUSEPRESENT
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_MOUSEPRESENT );
			}
		}
		/// <summary>
		/// 垂直滚动条的象素宽度
		/// </summary>
		public static int CYVSCROLL
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYVSCROLL );
			}
		}
		/// <summary>
		/// 水平滚动条的象素高度
		/// </summary>
		public static int CXHSCROLL
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXHSCROLL );
			}
		}
		/// <summary>
		/// 若为true或非0则使用了调试版本的user.exe,若为false或0则没安装调试版本的user.exe
		/// </summary>
		public static int DEBUG 
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_DEBUG );
			}
		}
		/// <summary>
		/// 若为true或非0则鼠标左右键交换,否则没有进行交换.
		/// </summary>
		public static int SWAPBUTTON
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_SWAPBUTTON );
			}
		}
		/// <summary>
		/// 保留1
		/// </summary>
		public static int RESERVED1
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_RESERVED1 );
			}
		}
		/// <summary>
		/// 保留2
		/// </summary>
		public static int RESERVED2
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_RESERVED2 );
			}
		}
		/// <summary>
		/// 保留3
		/// </summary>
		public static int RESERVED3 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_RESERVED3 );
			}
		}
		/// <summary>
		/// 保留4
		/// </summary>
		public static int RESERVED4 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_RESERVED4 );
			}
		}
		/// <summary>
		/// 窗体的最小象素宽度
		/// </summary>
		public static int CXMIN 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMIN );
			}
		}
		/// <summary>
		/// 窗体的最小象素高度
		/// </summary>
		public static int CYMIN 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMIN );
			}
		}
		/// <summary>
		/// 窗体标题栏上按钮的象素宽度
		/// </summary>
		public static int CXSIZE 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXSIZE );
			}
		}
		/// <summary>
		/// 窗体标题栏上按钮的象素高度
		/// </summary>
		public static int CYSIZE 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYSIZE );
			}
		}
		/// <summary>
		/// 等于 CXSIZEFRAME
		/// </summary>
		public static int CXFRAME
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXFRAME );
			}
		}
		/// <summary>
		/// 等于 CXSIZEFRAME
		/// </summary>
		public static int CYFRAME   
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYFRAME );
			}
		}
		/// <summary>
		/// 窗体的最小拖拉象素宽度,用户拖拽窗体时窗体的宽度不可能小于该值,
		/// 窗体可以通过处理WM_GETMINMAXINFO消息来改变这个值
		/// </summary>
		public static int CXMINTRACK
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMINTRACK );
			}
		}
		/// <summary>
		/// 窗体的最小拖拉象素高度,用户拖拽窗体时窗体的宽度不可能小于该值,
		/// 窗体可以通过处理WM_GETMINMAXINFO消息来改变这个值
		/// </summary>
		public static int CYMINTRACK 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMINTRACK );
			}
		}
		/// <summary>
		/// 系统双击判断区域最小象素宽度,可以使用标志 SPI_SETDOUBLECLKWIDTH 
		/// 调用 SystemParametersInfo 函数来修改该值.
		/// </summary>
		public static int CXDOUBLECLK 
		{ 
			get 
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXDOUBLECLK );
			}
		}
		/// <summary>
		/// 系统双击判断区域最小象素高度,可以使用标志 SPI_SETDOUBLECLKHEIGHT
		/// 调用 SystemParametersInfo 函数来修改该值.
		/// </summary>
		public static int CYDOUBLECLK
		{
			get 
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYDOUBLECLK );
			}
		}
		/// <summary>
		/// 处于大图标视图模式下放置各个项目的网格的象素宽度,该数值
		/// 通常大于或等于 CXICON.
		/// </summary>
		public static int CXICONSPACING 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXICONSPACING );
			}
		}
		/// <summary>
		/// 处于大图标视图模式下放置各个项目的网格的象素高度,该数值
		/// 通常大于或等于 CYICON.
		/// </summary>
		public static int CYICONSPACING 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYICONSPACING );
			}
		}
		/// <summary>
		/// 若为true或非0则下拉菜单采用右对齐,否则采用左对齐.
		/// </summary>
		public static int MENUDROPALIGNMENT
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_MENUDROPALIGNMENT );
			}
		}
		/// <summary>
		/// 若为true或非0则微软Windows安装了画笔窗口,否则没有安装.
		/// </summary>
		public static int PENWINDOWS
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_PENWINDOWS );
			}
		}
		/// <summary>
		/// 若为true或非0则 user32.dll支持DBCS,否则不支持.
		/// </summary>
		public static int DBCSENABLED 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_DBCSENABLED );
			}
		}
		/// <summary>
		/// 鼠标按键的个数,若为0则没有安装鼠标.
		/// </summary>
		public static int CMOUSEBUTTONS
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CMOUSEBUTTONS );
			}
		}
		/// <summary>
		/// 若为true则使用了安全系统,否则没有安全系统.
		/// </summary>
		public static int SECURE 
		{
			get 
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_SECURE );
			}
		}
		/// <summary>
		/// 3D边框的象素宽度.
		/// </summary>
		public static int CXEDGE
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXEDGE );
			}
		}
		/// <summary>
		/// 3D边框的象素高度.
		/// </summary>
		public static int CYEDGE
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYEDGE );
			}
		}
		/// <summary>
		/// 放置最小化窗体的网格象素宽度,该值通常大于或等于 CXMINMIZED.
		/// </summary>
		public static int CXMINSPACING 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMINSPACING );
			}
		}
		/// <summary>
		/// 放置最小化窗体的网格象素高度,该值通常大于或等于 CYMINMIZED.
		/// </summary>
		public static int CYMINSPACING 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMINSPACING );
			}
		}
		/// <summary>
		/// 建议小图标的象素宽度,小图标通常显示在窗体图标或小图标视图模式中.
		/// </summary>
		public static int CXSMICON
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXSMICON );
			}
		}
		/// <summary>
		/// 建议小图标的象素高度,小图标通常显示在窗体图标或小图标视图模式中.
		/// </summary>
		public static int CYSMICON
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYSMICON );
			}
		}
		/// <summary>
		/// 小标题栏的象素高度.
		/// </summary>
		public static int CYSMCAPTION 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYSMCAPTION );
			}
		}
		/// <summary>
		/// 小标题栏上按钮的象素宽度.
		/// </summary>
		public static int CXSMSIZE 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXSMSIZE );
			}
		}
		/// <summary>
		/// 小标题栏上按钮的象素高度.
		/// </summary>
		public static int CYSMSIZE
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYSMSIZE );
			}
		}
		/// <summary>
		/// 菜单栏上按钮的象素宽度.
		/// </summary>
		public static int CXMENUSIZE
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMENUSIZE );
			}
		}
		/// <summary>
		/// 菜单栏上按钮的象素高度.
		/// </summary>
		public static int CYMENUSIZE
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMENUSIZE );
			}
		}
		/// <summary>
		/// 描述系统如何排布最小化窗体的标志.
		/// </summary>
		public static int ARRANGE
		{
			get 
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_ARRANGE );
			}
		}
		/// <summary>
		/// 最小化窗体的象素宽度.
		/// </summary>
		public static int CXMINIMIZED 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMINIMIZED );
			}
		}
		/// <summary>
		/// 最小化窗体的象素高度.
		/// </summary>
		public static int CYMINIMIZED 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMINIMIZED );
			}
		}
		/// <summary>
		/// 带标题栏或边框的窗体的最大拖拽宽度,用户拖拽窗体时不可能
		/// 使其宽度大于该值,窗体可以处理WM_GETMINMAXINFO消息来修改该值.
		/// </summary>
		public static int CXMAXTRACK 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMAXTRACK );
			}
		}
		/// <summary>
		/// 带标题栏或边框的窗体的最大拖拽宽度,用户拖拽窗体时不可能
		/// 使其宽度大于该值,窗体可以处理WM_GETMINMAXINFO消息来修改该值.
		/// </summary>
		public static int CYMAXTRACK
		{ 
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMAXTRACK );
			}
		}
		/// <summary>
		/// 主显示器上最大化的顶级窗体的象素宽度.
		/// </summary>
		public static int CXMAXIMIZED
		{
			get 
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMAXIMIZED );
			}
		}
		/// <summary>
		/// 主显示器上最大化的顶级窗体的象素高度.
		/// </summary>
		public static int CYMAXIMIZED
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMAXIMIZED );
			}
		}
		/// <summary>
		/// 设置了最小字位则系统网络有效,否则网络无效,其他字位保留.
		/// </summary>
		public static int NETWORK
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_NETWORK );
			}
		}
		/// <summary>
		/// 系统启动方式 0:正常启动 1:安全模式启动 2:带网络的安全模式启动.
		/// </summary>
		public static int CLEANBOOT
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CLEANBOOT );
			}
		}
		/// <summary>
		/// 鼠标拖拽判断矩形的象素宽度.
		/// </summary>
		public static int CXDRAG
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXDRAG );
			}
		}
		/// <summary>
		/// 鼠标拖拽判断矩形的象素高度.
		/// </summary>
		public static int CYDRAG 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYDRAG );
			}
		}
		/// <summary>
		/// 若为true或非0,则应用程序显示提示时播放声音,否则不播放声音.
		/// </summary>
		public static int SHOWSOUNDS
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_SHOWSOUNDS );
			}
		}
		/// <summary>
		/// 菜单中选择标记图片的象素宽度.
		/// </summary>
		public static int CXMENUCHECK
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXMENUCHECK );
			}
		}
		/// <summary>
		/// 菜单中选择标记图片的象素高度.
		/// </summary>
		public static int CYMENUCHECK
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYMENUCHECK );
			}
		}
		/// <summary>
		/// 若为true在计算机系统响应缓慢.
		/// </summary>
		public static int SLOWMACHINE 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_SLOWMACHINE );
			}
		}
		/// <summary>
		/// 若为true则允许了希伯来和阿拉伯语
		/// </summary>
		public static int MIDEASTENABLED
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_MIDEASTENABLED );
			}
		}
		/// <summary>
		/// 若为true或非0则安装了鼠标滚轮,否则没有安装.
		/// </summary>
		public static int MOUSEWHEELPRESENT 
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_MOUSEWHEELPRESENT );
			}
		}
		/// <summary>
		/// 虚拟屏幕的左上端位置,虚拟屏幕是包含了所有显示器的边框.
		/// </summary>
		public static int XVIRTUALSCREEN
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_XVIRTUALSCREEN );
			}
		}
		/// <summary>
		/// 虚拟屏幕的顶端位置.
		/// </summary>
		public static int YVIRTUALSCREEN
		{
			get
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_YVIRTUALSCREEN );
			}
		}
		/// <summary>
		/// 虚拟屏幕的象素宽度.
		/// </summary>
		public static int CXVIRTUALSCREEN
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CXVIRTUALSCREEN );
			}
		}
		/// <summary>
		/// 虚拟屏幕的象素高度.
		/// </summary>
		public static int CYVIRTUALSCREEN
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CYVIRTUALSCREEN );
			}
		}
		/// <summary>
		/// 系统安装的显示器的个数.只返回实际安装的显示器硬件个数,不累计虚拟显示器个数.
		/// </summary>
		public static int CMONITORS 
		{ 
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CMONITORS );
			}
		}
		/// <summary>
		/// 若为true则左右的显示器使用相同的颜色格式,否则采用不同的颜色格式.
		/// 注意,两个显示器可以使用相同的字位深度,但不同的颜色格式,例如红,
		/// 绿蓝象素编码成不同个数的字位,或者这些字位在一个象素颜色值中占据
		/// 不同的位置.
		/// </summary>
		public static int SAMEDISPLAYFORMAT 
		{
			get 
			{ 
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_SAMEDISPLAYFORMAT );
			}
		}
		/// <summary>
		/// 未知.
		/// </summary>
		public static int CMETRICS 
		{
			get
			{
				return GetSystemMetrics( (int)_SystemMetricsConst.SM_CMETRICS );
			}
		}

		#endregion

		/// <summary>
		/// 返回包含所有数据的字符串
		/// </summary>
		/// <returns>字符串</returns>
		new public static string ToString()
		{
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			System.Array values = System.Enum.GetValues( typeof( _SystemMetricsConst ));
			foreach( _SystemMetricsConst sm in values )
			{
				myStr.Append( System.Environment.NewLine );
				myStr.Append( sm.ToString() + "=" + GetSystemMetrics( (int) sm ));
			}
			return myStr.ToString();
		}

		#region 内部代码群 ************************************************************************

		private SystemMetricsClass(){}
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		private static extern int GetSystemMetrics( int nIndex );
		
		/// <summary>
		/// 获得系统信息的编号,本变量传送给Win32API GetSystemMetrics
		/// </summary>
		private enum _SystemMetricsConst : int
		{
			SM_CXSCREEN             = 0 , 
			SM_CYSCREEN             = 1 ,
			SM_CXVSCROLL            = 2 ,
			SM_CYHSCROLL            = 3 ,
			SM_CYCAPTION            = 4 ,
			SM_CXBORDER             = 5 ,
			SM_CYBORDER             = 6 ,
			SM_CXDLGFRAME           = 7 ,
			SM_CYDLGFRAME           = 8 ,
			SM_CYVTHUMB             = 9 ,
			SM_CXHTHUMB             = 10,
			SM_CXICON               = 11,
			SM_CYICON               = 12,
			SM_CXCURSOR             = 13,
			SM_CYCURSOR             = 14,
			SM_CYMENU               = 15,
			SM_CXFULLSCREEN         = 16,
			SM_CYFULLSCREEN         = 17,
			SM_CYKANJIWINDOW        = 18,
			SM_MOUSEPRESENT         = 19,
			SM_CYVSCROLL            = 20,
			SM_CXHSCROLL            = 21,
			SM_DEBUG                = 22,
			SM_SWAPBUTTON           = 23,
			SM_RESERVED1            = 24,
			SM_RESERVED2            = 25,
			SM_RESERVED3            = 26,
			SM_RESERVED4            = 27,
			SM_CXMIN                = 28,
			SM_CYMIN                = 29,
			SM_CXSIZE               = 30,
			SM_CYSIZE               = 31,
			SM_CXFRAME              = 32,
			SM_CYFRAME              = 33,
			SM_CXMINTRACK           = 34,
			SM_CYMINTRACK           = 35,
			SM_CXDOUBLECLK          = 36,
			SM_CYDOUBLECLK          = 37,
			SM_CXICONSPACING        = 38,
			SM_CYICONSPACING        = 39,
			SM_MENUDROPALIGNMENT    = 40,
			SM_PENWINDOWS           = 41,
			SM_DBCSENABLED          = 42,
			SM_CMOUSEBUTTONS        = 43,
			SM_SECURE               = 44,
			SM_CXEDGE               = 45,
			SM_CYEDGE               = 46,
			SM_CXMINSPACING         = 47,
			SM_CYMINSPACING         = 48,
			SM_CXSMICON             = 49,
			SM_CYSMICON             = 50,
			SM_CYSMCAPTION          = 51,
			SM_CXSMSIZE             = 52,
			SM_CYSMSIZE             = 53,
			SM_CXMENUSIZE           = 54,
			SM_CYMENUSIZE           = 55,
			SM_ARRANGE              = 56,
			SM_CXMINIMIZED          = 57,
			SM_CYMINIMIZED          = 58,
			SM_CXMAXTRACK           = 59,
			SM_CYMAXTRACK           = 60,
			SM_CXMAXIMIZED          = 61,
			SM_CYMAXIMIZED          = 62,
			SM_NETWORK              = 63,
			SM_CLEANBOOT            = 67,
			SM_CXDRAG               = 68,
			SM_CYDRAG               = 69,
			SM_SHOWSOUNDS           = 70,
			SM_CXMENUCHECK          = 71,   /* Use instead of GetMenuCheckMarkDimensions()! */
			SM_CYMENUCHECK          = 72,
			SM_SLOWMACHINE          = 73,
			SM_MIDEASTENABLED       = 74,
			SM_MOUSEWHEELPRESENT    = 75,
			SM_XVIRTUALSCREEN       = 76,
			SM_YVIRTUALSCREEN       = 77,
			SM_CXVIRTUALSCREEN      = 78,
			SM_CYVIRTUALSCREEN      = 79,
			SM_CMONITORS            = 80,
			SM_SAMEDISPLAYFORMAT    = 81,
			//CMETRICS             = 76, // (WINVER < 0x0500) && (!defined(_WIN32_WINNT) || (_WIN32_WINNT < 0x0400))
			SM_CMETRICS             = 83
		}// public enum _SystemMetricsConst : int

		#endregion

	}//public sealed class SystemMetricsClass
}