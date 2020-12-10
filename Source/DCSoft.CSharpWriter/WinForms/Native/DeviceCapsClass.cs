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
	/// 获得指定设备上下文配置信息的类型,本类型为Win32API函数GetDeviceCaps的托管封装
	/// </summary>
	/// <remarks>编制 袁永福 2006-5-4
    /// 本类型在创建时就读取图形设备上下文中的所有的配置信息并缓存起来,因此不能获得实时
    /// 的数据,但当相关的图形设备上下文销毁掉后本类型中的数据还可读取.
    /// </remarks>
    //[DCSoft.Common.ObfuscationLevel()]
	public class DeviceCapsClass
	{
		#region 静态成员 **************************************************************************

		/// <summary>
		/// 对象的唯一静态实例
		/// </summary>
		private static DeviceCapsClass myDisplayInstance = null;
		/// <summary>
		/// 针对屏幕的设备上下文信息对象
		/// </summary>
		/// <remarks>程序可能经常访问屏幕的设备上下文信息，在此专门提供静态成员给予访问</remarks>
		public static DeviceCapsClass DispalyInstance
		{
			get
			{
				if( myDisplayInstance == null)
				{
					myDisplayInstance = new DeviceCapsClass();
					myDisplayInstance.ResetForDisplay();
				}
				return myDisplayInstance ;
			}
		}

		#endregion

		#region 构造函数 **************************************************************************

		/// <summary>
		/// 无作为的初始化对象
		/// </summary>
		public DeviceCapsClass()
		{
		}
		/// <summary>
		/// 使用指定设备上下文初始化对象
		/// </summary>
		/// <param name="hdc">设备上下文句柄</param>
		public DeviceCapsClass( IntPtr hdc )
		{
			Reset( hdc );
		}
		/// <summary>
		/// 使用指定设备名初始化对象
		/// </summary>
		/// <remarks>本函数根据名称创建设备上下文,获得所需数据后立即删除设备上下文</remarks>
		/// <param name="DriverName">设备名</param>
		public DeviceCapsClass( string vDriverName )
		{
			Reset( vDriverName );
		}

		#endregion

		private string strDriverName = null;
		/// <summary>
		/// 设备名称
		/// </summary>
		public string DriverName
		{
			get{ return strDriverName ;}
		}
		/// <summary>
		/// 根据指定的设备上下文刷新对象数据
		/// </summary>
		/// <param name="hDC">设备上下文句柄</param>
		public void Reset( IntPtr hDC )
		{
			strDriverName = null;
			System.Array myValues = System.Enum.GetValues( typeof( enumDeviceCapsConst ));
			intValues = new int[ myValues.Length * 2 ];
			for(int iCount = 0 ; iCount < myValues.Length ; iCount ++ )
			{
				int CapsValue = Convert.ToInt32( myValues.GetValue( iCount ));
				intValues[iCount * 2 ] = CapsValue ;
				intValues[iCount * 2 + 1] = GetDeviceCaps( hDC , CapsValue );
			}
		}
		/// <summary>
		/// 根据指定的设备名称刷新对象数据
		/// </summary>
		/// <param name="vDriverName">设备名称</param>
		public void Reset( string vDriverName )
		{
			IntPtr hdc = CreateDC( vDriverName , null , 0 , 0 );
			if( hdc.ToInt32() != 0 )
			{
				Reset( hdc );
				DeleteDC( hdc );
				strDriverName = vDriverName ;
			}
		}
		/// <summary>
		/// 根据计算机屏幕刷新对象数据
		/// </summary>
		public void ResetForDisplay( )
		{
			Reset( "DISPLAY" );
		}

		/// <summary>
		/// 已重载:获得对象所有内容的字符串
		/// </summary>
		/// <returns>字符串</returns>
		public override string ToString()
		{
			if( intValues == null)
				return "DeviceCapsClass:对象尚未初始化";
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			myStr.Append("DeviceCapsClass");
			if( strDriverName != null)
				myStr.Append(" Driver:" + strDriverName );
			System.Array myValues = System.Enum.GetValues( typeof( enumDeviceCapsConst ));
			foreach( enumDeviceCapsConst dc in myValues )
			{
				myStr.Append( System.Environment.NewLine );
				myStr.Append( dc.ToString() + " = " + GetValue( dc ) );
			}
			return myStr.ToString();
		}

		#region 获得数据的属性群 ******************************************************************

		/// <summary>
		/// Device driver version
		/// </summary>
		public int DRIVERVERSION{ get { return GetValue( enumDeviceCapsConst.DRIVERVERSION );} }
		/// <summary>
		/// Device classification
		/// </summary>
		public int TECHNOLOGY { get { return GetValue( enumDeviceCapsConst.TECHNOLOGY );} }
		/// <summary>
		/// Horizontal size in millimeters
		/// </summary>
		public int HORZSIZE { get { return GetValue( enumDeviceCapsConst.HORZSIZE );} }
		/// <summary>
		///  Vertical size in millimeters
		/// </summary>
		public int VERTSIZE { get { return GetValue( enumDeviceCapsConst.VERTSIZE );} }
		/// <summary>
		///  Horizontal width in pixels
		/// </summary>
		public int HORZRES { get { return GetValue( enumDeviceCapsConst.HORZRES );} }
		/// <summary>
		/// Vertical width in pixels
		/// </summary>
		public int VERTRES { get { return GetValue( enumDeviceCapsConst.VERTRES );} }
		/// <summary>
		/// Logical pixels/inch in X
		/// </summary>
		public int LOGPIXELSX { get { return GetValue( enumDeviceCapsConst.LOGPIXELSX );} }
		/// <summary>
		/// Logical pixels/inch in Y
		/// </summary>
		public int LOGPIXELSY { get { return GetValue( enumDeviceCapsConst.LOGPIXELSY );} }
		/// <summary>
		/// Number of planes
		/// </summary>
		public int PLANES { get { return GetValue( enumDeviceCapsConst.PLANES );} }
		/// <summary>
		/// Number of brushes the device has
		/// </summary>
		public int NUMBRUSHES { get { return GetValue( enumDeviceCapsConst.NUMBRUSHES );} }
		/// <summary>
		/// Number of colors the device supports
		/// </summary>
		public int NUMCOLORS { get { return GetValue( enumDeviceCapsConst.NUMCOLORS );} }
		/// <summary>
		/// Number of fonts the device has
		/// </summary>
		public int NUMFONTS { get { return GetValue( enumDeviceCapsConst.NUMFONTS );} }
		/// <summary>
		/// Number of pens the device has
		/// </summary>
		public int NUMPENS { get { return GetValue( enumDeviceCapsConst.NUMPENS );} }
		/// <summary>
		/// Length of the X leg
		/// </summary>
		public int ASPECTX { get { return GetValue( enumDeviceCapsConst.ASPECTX );} }
		/// <summary>
		/// Length of the hypotenuse
		/// </summary>
		public int ASPECTXY { get { return GetValue( enumDeviceCapsConst.ASPECTXY );} }
		/// <summary>
		/// Length of the Y leg
		/// </summary>
		public int ASPECTY { get { return GetValue( enumDeviceCapsConst.ASPECTY );} }
		/// <summary>
		/// Size required for device descriptor
		/// </summary>
		public int PDEVICESIZE { get { return GetValue( enumDeviceCapsConst.PDEVICESIZE );} }
		/// <summary>
		/// Clipping capabilities
		/// </summary>
		public int CLIPCAPS { get { return GetValue( enumDeviceCapsConst.CLIPCAPS );} }
		/// <summary>
		/// Number of entries in physical palette
		/// </summary>
		public int SIZEPALETTE { get { return GetValue( enumDeviceCapsConst.SIZEPALETTE );} }
		/// <summary>
		/// Number of reserved entries in palette
		/// </summary>
		public int NUMRESERVED { get { return GetValue( enumDeviceCapsConst.NUMRESERVED );} }
		/// <summary>
		/// Actual color resolution
		/// </summary>
		public int COLORRES { get { return GetValue( enumDeviceCapsConst.COLORRES );} }
		/// <summary>
		/// Physical Printable Area x margin
		/// </summary>
		public int PHYSICALOFFSETX { get { return GetValue( enumDeviceCapsConst.PHYSICALOFFSETX );} }
		/// <summary>
		/// Physical Printable Area y margin
		/// </summary>
		public int PHYSICALOFFSETY { get { return GetValue( enumDeviceCapsConst.PHYSICALOFFSETY );} }
		/// <summary>
		/// Physical Height in device units
		/// </summary>
		public int PHYSICALHEIGHT{ get { return GetValue( enumDeviceCapsConst.PHYSICALHEIGHT );} }
		/// <summary>
		/// Physical Width in device units
		/// </summary>
		public int PHYSICALWIDTH { get { return GetValue( enumDeviceCapsConst.PHYSICALWIDTH );} }
		/// <summary>
		/// Scaling factor x
		/// </summary>
		public int SCALINGFACTORX { get { return GetValue( enumDeviceCapsConst.SCALINGFACTORX );} }
		/// <summary>
		/// Scaling factor y
		/// </summary>
		public int SCALINGFACTORY { get { return GetValue( enumDeviceCapsConst.SCALINGFACTORY );} }
		public int LISTEN_OUTSTANDING{ get { return GetValue( enumDeviceCapsConst.LISTEN_OUTSTANDING );} }
		/// <summary>
		///  Curve capabilities
		/// </summary>
		public int CURVECAPS{ get { return GetValue( enumDeviceCapsConst.CURVECAPS );} }
		/// <summary>
		/// Line capabilities
		/// </summary>
		public int LINECAPS{ get { return GetValue( enumDeviceCapsConst.LINECAPS );} }
		/// <summary>
		/// Polygonal capabilities
		/// </summary>
		public int POLYGONALCAPS { get { return GetValue( enumDeviceCapsConst.POLYGONALCAPS );} }
		/// <summary>
		/// Text capabilities
		/// </summary>
		public int TEXTCAPS { get { return GetValue( enumDeviceCapsConst.TEXTCAPS );} }

		#endregion

		#region 声明 Win32 API 函数及常量 *********************************************************

		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int GetDeviceCaps(IntPtr hDC , int index );
		
		[System.Runtime.InteropServices.DllImport("User32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int ReleaseDC(IntPtr hWnd, int hDC);

		
		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern IntPtr CreateDC( string strDriver , string strDevice , int Output , int InitData );

		
		[System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		private static extern int DeleteDC( IntPtr Hdc );

		/// <summary>
		/// 为Win32API函数GetDeviceCaps的第二个参数配备的枚举变量
		/// </summary>
		private enum enumDeviceCapsConst
		{
			/// <summary>
			/// Device driver version
			/// </summary>
			DRIVERVERSION = 0      ,   
			/// <summary>
			/// Device classification
			/// </summary>
			TECHNOLOGY = 2         ,  
			/// <summary>
			/// Horizontal size in millimeters
			/// </summary>
			HORZSIZE = 4           , 
			/// <summary>
			///  Vertical size in millimeters
			/// </summary>
			VERTSIZE = 6           , 
			/// <summary>
			///  Horizontal width in pixels
			/// </summary>
			HORZRES = 8            , 
			/// <summary>
			/// Vertical width in pixels
			/// </summary>
			VERTRES = 10           , 
			/// <summary>
			/// Logical pixels/inch in X
			/// </summary>
			LOGPIXELSX = 88        , 
			/// <summary>
			/// Logical pixels/inch in Y
			/// </summary>
			LOGPIXELSY = 90        , 
			/// <summary>
			/// Number of planes
			/// </summary>
			PLANES = 14            , 
			/// <summary>
			/// Number of brushes the device has
			/// </summary>
			NUMBRUSHES = 16        , 
			/// <summary>
			/// Number of colors the device supports
			/// </summary>
			NUMCOLORS = 24         , 
			/// <summary>
			/// Number of fonts the device has
			/// </summary>
			NUMFONTS = 22          , 
			/// <summary>
			/// Number of pens the device has
			/// </summary>
			NUMPENS = 18           , 
			/// <summary>
			/// Length of the X leg
			/// </summary>
			ASPECTX = 40           , 
			/// <summary>
			/// Length of the hypotenuse
			/// </summary>
			ASPECTXY = 44          , 
			/// <summary>
			/// Length of the Y leg
			/// </summary>
			ASPECTY = 42           , 
			/// <summary>
			/// Size required for device descriptor
			/// </summary>
			PDEVICESIZE = 26       , 
			/// <summary>
			/// Clipping capabilities
			/// </summary>
			CLIPCAPS = 36          , 
			/// <summary>
			/// Number of entries in physical palette
			/// </summary>
			SIZEPALETTE = 104      , 
			/// <summary>
			/// Number of reserved entries in palette
			/// </summary>
			NUMRESERVED = 106      , 
			/// <summary>
			/// Actual color resolution
			/// </summary>
			COLORRES = 108         ,  
			/// <summary>
			/// Physical Printable Area x margin
			/// </summary>
			PHYSICALOFFSETX = 112  , 
			/// <summary>
			/// Physical Printable Area y margin
			/// </summary>
			PHYSICALOFFSETY = 113  , 
			/// <summary>
			/// Physical Height in device units
			/// </summary>
			PHYSICALHEIGHT = 111   , 
			/// <summary>
			/// Physical Width in device units
			/// </summary>
			PHYSICALWIDTH = 110   , 
			/// <summary>
			/// Scaling factor x
			/// </summary>
			SCALINGFACTORX = 114   , 
			/// <summary>
			/// Scaling factor y
			/// </summary>
			SCALINGFACTORY = 115   , 
			LISTEN_OUTSTANDING = 1 ,
			/// <summary>
			///  Curve capabilities
			/// </summary>
			CURVECAPS = 28         , 
			/// <summary>
			/// Line capabilities
			/// </summary>
			LINECAPS = 30          , 
			/// <summary>
			/// Polygonal capabilities
			/// </summary>
			POLYGONALCAPS = 32     , 
			/// <summary>
			/// Text capabilities
			/// </summary>
			TEXTCAPS = 34          , 
		}

		#endregion

		#region 内部私有成员 **********************************************************************

		private int[] intValues = null;

		/// <summary>
		/// 获得指定样式的数据
		/// </summary>
		/// <param name="CapsValue"></param>
		/// <returns></returns>
		private int GetValue( enumDeviceCapsConst CapsValue)
		{
			if( intValues == null)
				return int.MinValue ;
			for(int iCount = 0 ; iCount < intValues.Length ; iCount += 2 )
			{
				if( intValues[iCount] == Convert.ToInt32( CapsValue) )
					return intValues[iCount+1];
			}
			return int.MinValue ;
		}

		#endregion

	}//public class DeviceCapsClass
}