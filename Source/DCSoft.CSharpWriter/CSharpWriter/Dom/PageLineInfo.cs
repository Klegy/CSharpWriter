/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.Printing ;


namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 分页线信息
	/// </summary>
	public class PageLineInfo
	{
        private DomElement _SourceElement = null;
        /// <summary>
        /// 直接导致分页的文档元素对象
        /// </summary>
        public DomElement SourceElement
        {
            get { return _SourceElement; }
            set { _SourceElement = value; }
        }

        private XPageSettings _PageSettings = null;
        /// <summary>
        /// 当前使用的页面设置
        /// </summary>
        public XPageSettings PageSettings
        {
            get { return _PageSettings; }
            set { _PageSettings = value; }
        }

        private int _StdPageContentHeight = 0;
        /// <summary>
        /// 标准页面内容高度
        /// </summary>
        public int StdPageContentHeight
        {
            get { return _StdPageContentHeight; }
            set { _StdPageContentHeight = value; }
        }

        private int _MinPageContentHeight = 50;
        /// <summary>
        /// 最小允许的页面内容高度
        /// </summary>
        public int MinPageContentHeight
        {
            get { return _MinPageContentHeight; }
            set { _MinPageContentHeight = value; }
        }

        private PageViewMode _PageViewMode = PageViewMode.Page;
        /// <summary>
        /// 当前页面视图方式
        /// </summary>
        public PageViewMode PageViewMode
        {
            get { return _PageViewMode; }
            set { _PageViewMode = value; }
        }

        private bool _ForJumpPrint = false;
        /// <summary>
        /// 正在为续打而计算分页位置
        /// </summary>
        public bool ForJumpPrint
        {
            get { return _ForJumpPrint; }
            set { _ForJumpPrint = value; }
        }

        private int _LastPosition = 0;
        /// <summary>
        /// 上一次的分页线位置
        /// </summary>
        public int LastPosition
        {
            get
            {
                return _LastPosition; 
            }
            set
            {
                _LastPosition = value; 
            }
        }

        internal int _CurrentPoistion = 0;
        /// <summary>
        /// 当前分页线位置
        /// </summary>
        public int CurrentPoistion
        {
            get
            {
                return _CurrentPoistion; 
            }
            set
            {
                if (_CurrentPoistion > value )
                {
                    // 分页线位置只能变小，不能变大。
                    _CurrentPoistion = value;
                    _Modified = true;
                }
            }
        }

        private bool _Modified = false;
        /// <summary>
        /// 分页位置发生改变标记
        /// </summary>
        public bool Modified
        {
            get { return _Modified; }
            set { _Modified = value; }
        }

        ///// <summary>
        ///// 初始化对象
        ///// </summary>
        ///// <param name="vFirstPos">第一个分页线位置</param>
        ///// <param name="vLastPos">上一个分页线位置</param>
        //public PageLineInfo( int vFirstPos , int vLastPos , int vPos , int vPageIndex )
        //{
        //    intFirstPos = vFirstPos ;
        //    intLastPos = vLastPos ;
        //    intPos = vPos ;
        //    intPageIndex = vPageIndex ;
        //}

        //private int intMaxBottom = 0;

        ///// <summary>
        ///// 当前处理的元素的最大的低端位置
        ///// </summary>
        //public int MaxBottom
        //{
        //    get 
        //    {
        //        return intMaxBottom; 
        //    }
        //    set 
        //    {
        //        intMaxBottom = value; 
        //    }
        //}

        //public void UpdateMaxBottom(int bottom)
        //{
        //    if (bottom > intMaxBottom)
        //    {
        //        intMaxBottom = bottom;
        //    }
        //}

        //private bool bolForNewPage = false;
        ///// <summary>
        ///// 为强制分页而进行分页
        ///// </summary>
        //public bool ForNewPage
        //{
        //    get
        //    {
        //        return bolForNewPage; 
        //    }
        //    set
        //    {
        //        bolForNewPage = value; 
        //    }
        //}

        
        //private bool bolForJumpPrint = false;
        ///// <summary>
        ///// 正在计算续打位置
        ///// </summary>
        //public bool ForJumpPrint
        //{
        //    get
        //    {
        //        return bolForJumpPrint ;
        //    }
        //    set
        //    {
        //        bolForJumpPrint = value;
        //    }
        //}

        //private int intMinPageHeight = 0 ;
        //internal int MinPageHeight
        //{
        //    get
        //    { 
        //        return intMinPageHeight ;
        //    }
        //    set
        //    {
        //        intMinPageHeight = value;
        //    }
        //}

        //private int intPageIndex = 0 ;
        ///// <summary>
        ///// 当前处理的页号
        ///// </summary>
        //public int PageIndex
        //{
        //    get
        //    {
        //        return intPageIndex ;
        //    }
        //}

        //private int intFirstPos = 0 ;
        ///// <summary>
        ///// 第一个分页线位置
        ///// </summary>
        //public int FirstPos
        //{
        //    get
        //    {
        //        return intFirstPos ;
        //    }
        //}

        //private int intLastPos = 0 ;
        ///// <summary>
        ///// 上一个分页线位置
        ///// </summary>
        //public int LastPos
        //{
        //    get
        //    {
        //        return intLastPos ;
        //    }
        //}
         

        //public bool CanSet( int vPos )
        //{
        //    if( vPos > intLastPos && vPos < intPos )
        //    {
        //        if( intMinPageHeight > 0 )
        //        {
        //            if( vPos - intLastPos < intMinPageHeight )
        //                return false;
        //        }
        //        return true;
        //    }
        //    return false ;
        //}

        //private int intMinPos = 0;
        ///// <summary>
        ///// 分页线允许的最小位置
        ///// </summary>
        //public int MinPos
        //{
        //    get { return intMinPos; }
        //    set { intMinPos = value; }
        //}

        ///// <summary>
        ///// 检查分页线最小位置后设置分页线的位置
        ///// </summary>
        ///// <param name="pos"></param>
        ///// <returns></returns>
        //public bool SetPosWithCheckMinPos(int pos)
        //{
        //    if (pos >= intMinPos || intMinPos == 0 )
        //    {
        //        if (intPos != pos)
        //        {
        //            intPos = pos;
        //            bolModified = true;
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private int intPos = 0 ;
        ///// <summary>
        ///// 当前分页线位置
        ///// </summary>
        //public int Pos
        //{
        //    get
        //    {
        //        return intPos ;
        //    }
        //    set
        //    {
        //        if( value > intLastPos && value < intPos )
        //        {
        //            if( intMinPageHeight > 0 )
        //            {
        //                if( value - intLastPos < intMinPageHeight )
        //                    return ;
        //            }
        //            intPos = value;
        //            bolModified = true;
        //        }
        //    }
        //}
        //private bool bolModified = false;
        ///// <summary>
        ///// 当前分页线位置是否改变标记
        ///// </summary>
        //public bool Modified
        //{
        //    get
        //    {
        //        return bolModified ;
        //    }
        //    set
        //    {
        //        bolModified = value;
        //    }
        //}

        //private int intBodyHeightIncrease = 0;
        ///// <summary>
        ///// 报表表身高度增加值
        ///// </summary>
        //public int BodyHeightIncrease
        //{
        //    get 
        //    {
        //        return intBodyHeightIncrease; 
        //    }
        //    set
        //    {
        //        intBodyHeightIncrease = value; 
        //    }
        //}

        ///// <summary>
        ///// 分页线是否在指定的区域中
        ///// </summary>
        ///// <param name="Top">顶端位置</param>
        ///// <param name="Bottom">低端位置</param>
        ///// <returns>是否在指定的区域中</returns>
        //public bool Match( int Top , int Bottom )
        //{
        //    return intPos >= Top && intPos < Bottom ;
        //}
        ///// <summary>
        ///// 分页线是否在指定的区域中
        ///// </summary>
        ///// <param name="rect">区域矩形</param>
        ///// <returns>是否在指定的区域中</returns>
        //public bool Match( System.Drawing.Rectangle rect )
        //{
        //    return intPos >= rect.Top && intPos < rect.Bottom ;
        //}
	}//public class PageLineInfo
}