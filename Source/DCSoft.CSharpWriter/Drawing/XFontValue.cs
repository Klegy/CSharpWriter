/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.ComponentModel ;
using System.ComponentModel.Design ;
using System.ComponentModel.Design.Serialization ;
using System.Collections.Generic;
using System.Drawing ;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 字体信息类型，本对象可以参与XML和二进制的序列化及反序列化。
	/// </summary>
	[Serializable()]
	[System.ComponentModel.DefaultProperty("Value")]
	[System.ComponentModel.TypeConverter( 
        typeof( XFontValueTypeConverter ))]
	[System.ComponentModel.Editor(
        typeof( XFontValueEditor ) , 
        typeof( System.Drawing.Design.UITypeEditor ))]
    [System.ComponentModel.ToolboxItem( false )]
	public class XFontValue : System.ICloneable , IComponent 
	{
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XFontValue()
        {
            DefaultFont = System.Windows.Forms.Control.DefaultFont;
            DefaultFontName = DefaultFont.Name;
            DefaultFontSize = DefaultFont.Size;
        }

        //public const string DefaultValueString = "宋体,9";
        /// <summary>
        /// 建议的对象变量名称
        /// </summary>
        public static string SuggestBaseName = "Font";

        /// <summary>
        /// 默认字体
        /// </summary>
        [NonSerialized()]
        public static System.Drawing.Font DefaultFont = null;
		/// <summary>
		/// 默认字体名称
		/// </summary>
        [System.NonSerialized()]
        public static string DefaultFontName = null;
        /// <summary>
        /// 默认字体大小
        /// </summary>
        [System.NonSerialized()]
        public static float DefaultFontSize = 9;

		/// <summary>
		/// 初始化对象
		/// </summary>
		public XFontValue()
		{
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="name">字体名称</param>
		/// <param name="size">字体大小</param>
		public XFontValue( string name , float size )
		{
			strName = name ;
			fSize = size ;
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="name">字体名称</param>
		/// <param name="size">字体大小</param>
		/// <param name="style">字体样式</param>
		public XFontValue(
            string name , 
            float size , 
            System.Drawing.FontStyle style )
		{
			strName = name ;
			fSize = size ;
			this.Style = style ;
		}

        /// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="name">字体名称</param>
		/// <param name="size">字体大小</param>
		/// <param name="style">字体样式</param>
		public XFontValue(
            string name , 
            float size , 
            System.Drawing.FontStyle style ,
            GraphicsUnit unit )
		{
			strName = name ;
			fSize = size ;
			this.Style = style ;
            this.Unit = unit ;
		}

        

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="f">字体对象</param>
		public XFontValue( System.Drawing.Font f )
		{
			this.Value = f ;
		}

        /// <summary>
        /// 判断当前字体是否是默认字体
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool IsDefault
        {
            get
            {
                if (strName == DefaultFontName
                    && fSize == DefaultFontSize
                    && bolItalic == false
                    && bolUnderline == false
                    && bolBold == false
                    && bolStrikeout == false)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 判断当前字体名称是否是默认字体
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsDefaultName
        {
            get
            {
                return strName == DefaultFontName;
            }
        }

		private string strName = DefaultFontName ;
		/// <summary>
		/// 字体名称
		/// </summary>
        [System.ComponentModel.Editor(
            "DCSoft.WinForms.Design.FontNameUITypeEditor",
            typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultFontNameValueAttribute()]
		public string Name
		{
			get
			{
				return strName ;
			}
			set
			{
				if( strName != value )
				{
					strName = value;
                    if (strName == null || strName.Length == 0)
                    {
                        strName = DefaultFontName;
                    }
					myValue = null;
				}
			}
		}

        /// <summary>
        /// 判断当前字体是否是默认大小
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsDefaultSize
        {
            get
            {
                return fSize == DefaultFontSize;
            }
        }

        private float fSize = DefaultFontSize;
		/// <summary>
		/// 字体大小
		/// </summary>
        [System.ComponentModel.Editor( 
            "DCSoft.Editor.FontSizeEditor" ,
            typeof( System.Drawing.Design.UITypeEditor ))]
		[System.ComponentModel.DefaultValue( 9f )]
		public float Size
		{
			get
			{
				return fSize ;
			}
			set
			{
				if( fSize != value )
				{
					fSize = value;
                    if (fSize <= 0)
                    {
                        fSize = DefaultFontSize;
                    }
					myValue = null; 
				}
			}
		}


        private GraphicsUnit _Unit = GraphicsUnit.Point;
        /// <summary>
        /// 字体大小的度量单位
        /// </summary>
        [DefaultValue( GraphicsUnit.Point)]
        public GraphicsUnit Unit
        {
            get 
            {
                return _Unit; 
            }
            set
            {
                _Unit = value; 
            }
        }

		private bool bolBold = false;
		/// <summary>
		/// 是否粗体
		/// </summary>
		[System.ComponentModel.DefaultValue( false )]
		public bool Bold
		{
			get
			{
				return bolBold ;
			}
			set
			{ 
				if( bolBold != value )
				{
					bolBold = value;
					myValue = null;
				}
			}
		}
		private bool bolItalic = false;
		/// <summary>
		/// 是否斜体
		/// </summary>
		[System.ComponentModel.DefaultValue( false )]
		public bool Italic
		{
			get
			{
				return bolItalic ;
			}
			set
			{
				if( bolItalic != value )
				{
					bolItalic = value;
					myValue = null;
				}
			}
		}

		private bool bolUnderline = false;
		/// <summary>
		/// 下划线
		/// </summary>
		[System.ComponentModel.DefaultValue( false )]
		public bool Underline
		{
			get
			{
				return bolUnderline ;
			}
			set
			{
				if( bolUnderline != value )
				{
					bolUnderline = value;
					myValue = null;
				}
			}
		}

		private bool bolStrikeout = false;
		/// <summary>
		/// 删除线
		/// </summary>
		[System.ComponentModel.DefaultValue( false )]
		public bool Strikeout
		{
			get
			{
				return bolStrikeout ;
			}
			set
			{
				if( bolStrikeout != value )
				{
					bolStrikeout = value;
					myValue = null;
				}
			}
		}
		
		/// <summary>
		/// 字体样式
		/// </summary>
		[System.ComponentModel.DefaultValue(
            System.Drawing.FontStyle.Regular )]
		[System.Xml.Serialization.XmlIgnore()]
		[System.ComponentModel.Browsable( false )]
		public System.Drawing.FontStyle Style
		{
			get
			{
				System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular ;
				if( bolBold )
					style = System.Drawing.FontStyle.Bold ;
				if( bolItalic )
					style = style | System.Drawing.FontStyle.Italic ;
				if( bolUnderline )
					style = style | System.Drawing.FontStyle.Underline ;
				if( bolStrikeout )
					style = style | System.Drawing.FontStyle.Strikeout ;
				return style ;
			}
			set
			{
				if( this.Style != value )
				{
					bolBold = GetStyle( value , System.Drawing.FontStyle.Bold );
					bolItalic = GetStyle( value , System.Drawing.FontStyle.Italic );
					bolUnderline = GetStyle( value , System.Drawing.FontStyle.Underline );
					bolStrikeout = GetStyle( value , System.Drawing.FontStyle.Strikeout );
					myValue = null;
				}
			}
		}

		private bool GetStyle(
            System.Drawing.FontStyle intValue , 
            System.Drawing.FontStyle MaskFlag )
		{
			return ( intValue & MaskFlag ) == MaskFlag ;
		}

        /// <summary>
        /// 修正字体名称,使得字体在本系统中有效
        /// </summary>
        /// <returns>操作是否修改了字体名称</returns>
        public bool FixFontName()
        {
            string name = FixFontName(this.strName);
            if (name != strName)
            {
                strName = name;
                return true;
            }
            else
            {
                return false;
            }
        }

        [Browsable( false )]
        public int CellAscent
        {
            get
            {
                return this.Value.FontFamily.GetCellAscent(this.Style);
            }
        }

        [Browsable(false)]
        public int CellDescent
        {
            get
            {
                return this.Value.FontFamily.GetCellDescent(this.Style);
            }
        }

        [Browsable(false)]
        public int LineSpacing
        {
            get
            {
                return this.Value.FontFamily.GetLineSpacing(this.Style);
            }
        }

        [Browsable(false)]
        public int EmHeight
        {
            get
            {
                return this.Value.FontFamily.GetEmHeight(this.Style);
            }
        }

		/// <summary>
		/// 缓存字体的列表
		/// </summary>
		[System.NonSerialized()]
		private static List<System.Drawing.Font> myBuffer
                = new List<System.Drawing.Font>();
		/// <summary>
		/// 内部缓存字体对象的列表
		/// </summary>
		[System.ComponentModel.Browsable( false )]
        public static List<System.Drawing.Font> Buffer
		{
			get
            { 
                return myBuffer ;
            }
		}

		/// <summary>
		/// 失败的字体名称列表
		/// </summary>
		[System.NonSerialized()]
		private static List<string> BadFontNames = new List<string>();
		/// <summary>
		/// 清空内置的字体缓冲区
		/// </summary>
		public static void ClearBuffer()
		{
			myBuffer.Clear();
			BadFontNames.Clear();
			System.GC.Collect();
		}

        /// <summary>
        /// 修正字体名称
        /// </summary>
        /// <param name="name">字体名称</param>
        /// <returns>修正后的字体名称</returns>
        public static string FixFontName(string name)
        {
            if (name == null || name.Trim().Length == 0)
            {
                return DefaultFontName;
            }
            name = name.Trim();
            if (BadFontNames.Count > 0)
            {
                foreach (string fn in BadFontNames)
                {
                    if (string.Compare(name, fn, true) == 0)
                    {
                        return DefaultFontName;
                    }
                }
            }
            try
            {
                System.Drawing.FontFamily ff 
                    = new System.Drawing.FontFamily(name);
                return ff.Name;
            }
            catch (Exception)
            {
                BadFontNames.Add(name);
            }
            return DefaultFontName;
        }

		[System.NonSerialized()]
		private System.Drawing.Font myValue = null;
		/// <summary>
		/// 字体对象
		/// </summary>
		[System.ComponentModel.Browsable( false )]
		[System.Xml.Serialization.XmlIgnore()]
		public System.Drawing.Font Value
		{
			get
			{
				if( myValue == null )
				{
					if( myBuffer.Count > 200 )
					{
						return DefaultFont ;
						//throw new System.Exception("缓存的字体过多");
					}

					string fname = strName ;
					float fsize = this.fSize ;
					System.Drawing.FontStyle fstyle = this.Style ;
					
					// 判断是否是曾经失败的字体名称
					if( BadFontNames.Count > 0 )
					{
						foreach( string name in BadFontNames )
						{
							if( string.Compare( name , fname , true ) == 0 )
							{
								fname = DefaultFontName ;
								break;
							}
						}
					}

					if( fname == DefaultFontName
						&& fsize == DefaultFontSize
						&& fstyle == System.Drawing.FontStyle.Regular )
					{
						myValue = DefaultFont ;
					}
					else
					{
						foreach( System.Drawing.Font f in myBuffer )
						{
							if( fname == f.Name 
								&& fsize == f.Size
								&& fstyle == f.Style 
                                && _Unit == f.Unit )
							{
								myValue = f ;
								break;
							}
						}
					}
					if( myValue == null )
					{
						System.Drawing.FontFamily ff = null;
						try
						{
							ff = new System.Drawing.FontFamily( fname );
                            if (ff.Name != fname)
                            {
                                // 创建字体时发生异常，则认为是不存在的字体
                                // 指定的字体名称是失败的
                                ff = new System.Drawing.FontFamily(DefaultFontName);
                                bool find = false;
                                foreach (string name in BadFontNames)
                                {
                                    if (string.Compare(name, strName, true) == 0)
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (find == false)
                                {
                                    BadFontNames.Add(strName);
                                }
                            }
						}
						catch( Exception )
						{
							// 指定的字体名称是失败的
							ff = new System.Drawing.FontFamily( DefaultFontName );
							bool find = false;
							foreach( string name in BadFontNames )
							{
								if( string.Compare( name , strName , true ) == 0 )
								{
									find = true ;
									break;
								}
							}
							if( find == false )
							{
								BadFontNames.Add( strName );
							}
						}
                        try
                        {
                            if (ff.IsStyleAvailable(this.Style) == false)
                            {
                                //某些字体不支持当前字体样式,则重新搜索设置合适的字体样式.
                                foreach (System.Drawing.FontStyle st in new System.Drawing.FontStyle[] { 
                                    System.Drawing.FontStyle.Regular ,
                                    System.Drawing.FontStyle.Bold ,
                                    System.Drawing.FontStyle.Italic ,
                                    System.Drawing.FontStyle.Underline ,
                                    System.Drawing.FontStyle.Strikeout ,
                                    System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic })
                                {
                                    if (ff.IsStyleAvailable(st))
                                    {
                                        this.Style = st;
                                        break;
                                    }
                                }
                            }
                            myValue = new System.Drawing.Font(ff, fSize, this.Style , this.Unit );
                            myBuffer.Add(myValue);
                        }
                        catch (Exception ext)
                        {
                            System.Diagnostics.Debug.WriteLine( ext.Message );
                            myValue = DefaultFont;
                        }
					}
				}
				return myValue ;
			}
			set
			{
				if( value == null )
				{
                    value = DefaultFont ;
				}
				if( EqualsValue( value ) == false )
				{
					strName = value.Name ;
					fSize = value.Size ;
					bolBold = value.Bold ;
					bolItalic = value.Italic ;
					bolUnderline = value.Underline ;
					bolStrikeout = value.Strikeout ;
                    _Unit = value.Unit ;
					myValue = value ;
				}
			}
		}

        /// <summary>
        /// 获得字体的以像素为单位的高度
        /// </summary>
        /// <returns>字体高度</returns>
        public float GetHeight()
        {
            System.Drawing.Font f = this.Value;
            if (f == null)
            {
                return 0;
            }
            else
            {
                return f.GetHeight();
            }
        }

        /// <summary>
        /// 获得字体的高度
        /// </summary>
        /// <param name="g">绘图对象</param>
        /// <returns>字体高度</returns>
        public float GetHeight(System.Drawing.Graphics g)
        {
            System.Drawing.Font f = this.Value;
            if (f == null)
            {
                return 0;
            }
            else
            {
                return f.GetHeight(g);
            }
        }
        /// <summary>
        /// 获得指定分辨率的字体的高度
        /// </summary>
        /// <param name="dpi">分辨率</param>
        /// <returns>字体高度</returns>
        public float GetHeight(float dpi)
        {
            System.Drawing.Font f = this.Value;
            if (f == null)
            {
                return 0;
            }
            else
            {
                return f.GetHeight(dpi);
            }
        }

        /// <summary>
        /// 获得指定度量单位下的字体高度
        /// </summary>
        /// <param name="unit">指定的度量单位</param>
        /// <returns>字体高度</returns>
        public float GetHeight(GraphicsUnit unit)
        {
            return GraphicsUnitConvert.Convert(this.Value.SizeInPoints, GraphicsUnit.Point, unit);
        }

		/// <summary>
		/// 将指定字体对象的设置复制到本对象
		/// </summary>
		/// <param name="SourceFont">来源字体对象</param>
		public void CopySettings( XFontValue SourceFont )
		{
			strName = SourceFont.strName ;
			fSize = SourceFont.fSize ;
			bolBold = SourceFont.bolBold ;
			bolItalic = SourceFont.bolItalic ;
			bolUnderline = SourceFont.bolUnderline ;
			bolStrikeout = SourceFont.bolStrikeout ;
            _Unit = SourceFont._Unit ;
		}

		/// <summary>
		/// 比较对象和指定字体的设置是否一致
		/// </summary>
		/// <param name="f">字体对象</param>
		/// <returns>是否一致</returns>
		public bool EqualsValue( System.Drawing.Font f )
		{
			if( f == null )
            {
				return false;
            }
			if( strName != f.Name )
            {
				return false;
            }
			if( fSize != f.Size )
            {
				return false;
            }
			if( bolBold != f.Bold )
			{
                return false;
            }
			if( bolItalic != f.Italic )
			{
                return false;
            }
			if( bolUnderline != f.Underline )
			{
                return false;
            }
			if( bolStrikeout != f.Strikeout )
			{
                return false;
            }
            if( _Unit != f.Unit )
            {
                return false ;
            }
			return true;
		}

		/// <summary>
		/// 比较对象和指定字体的设置是否一致
		/// </summary>
		/// <param name="f">字体对象</param>
		/// <returns>是否一致</returns>
		public bool EqualsValue( XFontValue f )
		{
			if( f == null )
            {
				return false;
            }
            if (this == f)
            {
                return true;
            }
			if( strName != f.strName )
			{
                return false;
            }
			if( fSize != f.fSize )
			{
                return false;
            }
			if( bolBold != f.bolBold )
			{
                return false;
            }
			if( bolItalic != f.bolItalic )
			{
                return false;
            }
			if( bolUnderline != f.bolUnderline )
			{
                return false;
            }
			if( bolStrikeout != f.bolStrikeout )
			{
                return false;
            }
            if( _Unit != f._Unit )
            {
                return false ;
            }
			return true;
		}

		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>复制品</returns>
		public XFontValue Clone()
		{
			XFontValue font = new XFontValue();
			font.CopySettings( this );
			return font ;
		}
		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>复制品</returns>
		object System.ICloneable.Clone()
		{
			XFontValue font = new XFontValue();
			font.CopySettings( this );
			return font ;
		}

        /// <summary>
        /// 比较两个对象内容是否相同
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>内容是否相同</returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is XFontValue))
                return false;
            XFontValue f = (XFontValue)obj;
            return f.bolBold == this.bolBold
                && f.bolItalic == this.bolItalic
                && f.bolStrikeout == this.bolStrikeout
                && f.bolUnderline == this.bolUnderline
                && f.fSize == this.fSize
                && f.strName == this.strName
                && f._Unit == this._Unit ;
        }

        /// <summary>
        /// 获得对象的哈希代码
        /// </summary>
        /// <returns>哈希代码</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 获得表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            list.Add( this.Name);
            list.Add( this.Size.ToString());
            if ( this.Style != System.Drawing.FontStyle.Regular)
            {
                list.Add("style=" + this.Style.ToString("G"));
            }
            if( this._Unit != GraphicsUnit.Point )
            {
                list.Add( this._Unit.ToString("G"));
            }
            return string.Join(", ", (string[])list.ToArray(typeof(string)));
        }
        /// <summary>
        /// 解析字符串，设置对象数据
        /// </summary>
        /// <param name="Text">要解析的字符串</param>
        public void Parse(string Text)
        {
            if (Text == null)
            {
                return;
            }
            string[] items = Text.Split(',');
            if (items.Length < 1)
            {
                throw new ArgumentException("必须符合 name,size,style=Bold,Italic,Underline,Strikeout 样式");
            }
            string name = items[0];

            float size = 9f;
            if (items.Length >= 2)
            {
                size = float.Parse(items[1].Trim());
            }

            System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
            bool flag = false;
            for (int iCount = 2; iCount < items.Length; iCount++)
            {
                string item = items[iCount].Trim().ToLower();
                if (flag == false)
                {
                    if (item.StartsWith("style"))
                    {
                        int index = item.IndexOf("=");
                        if (index > 0)
                        {
                            flag = true;
                            item = item.Substring(index + 1);
                        }
                    }
                }
                if (flag)
                {
                    if( Enum.IsDefined( typeof( FontStyle ) , item.Trim()))
                    {
                        FontStyle s2 = ( FontStyle)Enum.Parse(
                            typeof( FontStyle), item.Trim(), true);
                        style = style | s2;
                    }
                    else if( Enum.IsDefined( typeof( GraphicsUnit ) , item.Trim()))
                    {
                        this._Unit = ( GraphicsUnit)Enum.Parse(
                            typeof( GraphicsUnit ) , item.Trim() , true );
                    }
                }
            }
            this.Name = name;
            this.Size = size;
            this.Style = style;
        }

        #region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
        public event EventHandler Disposed = null ;

        private ISite mySite = null;
        /// <summary>
        /// 组件站点对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get
            {
                return mySite;
            }
            set
            {
                mySite = value;
            }
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            if (Disposed != null)
            {
                Disposed(this, new EventArgs());
            }
        }

        #endregion
    }//public class XFontValue : System.ICloneable
     
	/// <summary>
    /// 默认字体名称特性
    /// </summary>
    [System.AttributeUsage( AttributeTargets.All ,AllowMultiple=false)]
    public class DefaultFontNameValueAttribute : System.ComponentModel.DefaultValueAttribute
    {
        static DefaultFontNameValueAttribute()
        {
            DefaultFontName = System.Windows.Forms.Control.DefaultFont.Name ;
        }
        /// <summary>
        /// 默认字体名称
        /// </summary>
        public static string DefaultFontName = null ;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultFontNameValueAttribute() : base( DefaultFontName )
        {
        }
	}

//#endif

	/// <summary>
	/// 字体属性编辑器,设计器内部使用
	/// </summary>
	public class XFontValueEditor : System.Drawing.Design.UITypeEditor
	{
        /// <summary>
        /// 编辑对象数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="provider">参数</param>
        /// <param name="Value">旧数据</param>
        /// <returns>新数据</returns>
		public override object EditValue(
            ITypeDescriptorContext context, 
            IServiceProvider provider, 
            object Value)
		{
			using( System.Windows.Forms.FontDialog dlg = new System.Windows.Forms.FontDialog())
			{
				dlg.ShowApply = false ;
				dlg.ShowColor = false;
                if (Value != null)
                {
                    if (Value is System.Drawing.Font)
                    {
                        dlg.Font = (System.Drawing.Font)Value;
                    }
                    else if (Value is XFontValue)
                    {
                        dlg.Font = ((XFontValue)Value).Value;
                    }
                }
				if( dlg.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
				{
                    Type pt = context.PropertyDescriptor.PropertyType;
                    if( pt.Equals( typeof( System.Drawing.Font )))
					{
						Value = dlg.Font ;
					}
                    else if ( pt.Equals( typeof( XFontValue )))
					{
						Value = new XFontValue( dlg.Font );
					}
					else
					{
						Value = dlg.Font ;
					}
				}
			}//if
			return Value ;
		}

        /// <summary>
        /// 获得对象数据的编辑样式
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>编辑样式</returns>
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
		{
			return System.Drawing.Design.UITypeEditorEditStyle.Modal;
		}
	}

	/// <summary>
	/// 字体类型转换器,设计器内部使用
	/// </summary>
	[System.ComponentModel.Browsable( false )]
	public class XFontValueTypeConverter : TypeConverter
	{
        /// <summary>
        /// 支持列出对象属性
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>支持</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 获得对象属性列表
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="Value">对象</param>
        /// <param name="attributes">参数</param>
        /// <returns>获得的对象属性列表</returns>
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context, object Value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XFontValue), attributes).Sort(
                new string[] { "Name", "Size", "Bold", "Italic", "Underline", "Strikeout" ,"Unit"});
        }

        /// <summary>
        /// 判断能否进行数据类型转换
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="sourceType">原始数据类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(System.Drawing.Font))
                return true;
            return ((sourceType == typeof(string))
                || base.CanConvertFrom(context, sourceType));
        }
        /// <summary>
        /// 判断能否进行数据类型转换
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="destinationType">目标数据类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(System.Drawing.Font))
                return true;
            if (destinationType == typeof( string ))
                return true;
            return ((destinationType == typeof(InstanceDescriptor)) 
                || base.CanConvertTo(context, destinationType));
        }

        /// <summary>
        /// 将数据转换为字体对象
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="Value">输入的数据</param>
        /// <returns>转换结果</returns>
        public override object ConvertFrom(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object Value)
        {
            if (Value is System.Drawing.Font)
            {
                return new XFontValue((System.Drawing.Font)Value);
            }
            if (!(Value is string))
            {
                return base.ConvertFrom(context, culture, Value);
            }
            string str = ((string)Value).Trim();
            if (str.Length == 0)
            {
                return null;
            }
            XFontValue font = new XFontValue();
            font.Parse(str);
            return font;
        }

        /// <summary>
        /// 将字体转换为其他类型
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="Value">对象数据</param>
        /// <param name="destinationType">目标数据类型</param>
        /// <returns>转换结果</returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object Value,
            Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (destinationType == typeof(string))
            {
                XFontValue font = (XFontValue)Value;
                if (font == null)
                {
                    return "错误的类型";
                }
                return font.ToString();
            }
            if (destinationType == typeof(System.Drawing.Font))
            {
                XFontValue font = (XFontValue)Value;
                return font.Value;
            }
            if ((destinationType == typeof(InstanceDescriptor))
                && (Value is XFontValue))
            {
                XFontValue font = (XFontValue)Value;
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                System.Collections.ArrayList types = new System.Collections.ArrayList();
                list.Add(font.Name);
                types.Add(typeof(string));
                list.Add(font.Size);
                types.Add(typeof(float));
                if (font.Style != System.Drawing.FontStyle.Regular)
                {
                    list.Add(font.Style);
                    types.Add(typeof(System.Drawing.FontStyle));
                }

                System.Reflection.MemberInfo constructor = typeof(XFontValue).GetConstructor(
                    (Type[])types.ToArray(typeof(Type)));
                if (constructor != null)
                {
                    return new InstanceDescriptor(constructor, list.ToArray());
                }
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }

        private bool IsTrue(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Convert.ToBoolean(obj);
        }
	}//public class XFontValueTypeConverter : TypeConverter
}