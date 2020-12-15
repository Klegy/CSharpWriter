/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Runtime.Serialization ;
using System.Runtime ;
using System.ComponentModel ;
using System.ComponentModel.Design ;
using System.ComponentModel.Design.Serialization ;
using System.Drawing;
using System.Drawing.Imaging;

namespace DCSoft.Drawing
{
	/// <summary>
	/// 设计文档图片数据对象
	/// </summary>
	/// <remarks>
	/// 设计文档图片数据对象。它是System.Drawing.Image的一个封装，这个对象保存图片对象，还保存构造图片对象的原始二进制数据。
	/// <br />在设计器的属性列表中，需要从一个文件中加载图片数据，为了保持原始数据的完整性，在保存设计文档时是保存加载图片
	/// 的二进制数据的，加载设计文档时，会从这个原始的二进制数据来加载图片，这样保证的设计的完整性。本对象就是图片和二进制
	/// 数据的混合封装。方便程序加载和保存图片数据。
	/// <br />本对象内部使用了 System.Drawing.Image 对象,可能使用了非托管资源,因此实现了IDisposable 接口,可以用来显式的释放
	/// 非托管资源.
	/// </remarks>
	[System.Serializable()]
	[System.ComponentModel.DefaultProperty("ImageData")]
	[System.ComponentModel.TypeConverter( typeof( XImageValueTypeConverter ))]
	[System.ComponentModel.Editor(
       "DCSoft.WinForms.Design.ImageUITypeEditor",
        typeof( System.Drawing.Design.UITypeEditor ))]
    [System.ComponentModel.ToolboxItem(false)]
	public class XImageValue : System.ICloneable , System.IDisposable , IComponent
	{
       

        /// <summary>
        /// 对象变量的建议名称前缀
        /// </summary>
        public static string SuggestBaseName = "Image";

		/// <summary>
		/// 初始化对象
		/// </summary>
		public XImageValue()
		{
            this._ContentVersion = this.GetHashCode();
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="img">图片数据</param>
		public XImageValue( System.Drawing.Image img )
		{
			this.Value = img ;
            this._ContentVersion = this.GetHashCode();
		}

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="stream">包含图片数据的流对象</param>
        public XImageValue(System.IO.Stream stream)
        {
            byte[] bs = new byte[1024];
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            while (true)
            {
                int len = stream.Read(bs, 0, bs.Length);
                if (len <= 0)
                    break;
                ms.Write(bs, 0, len);
            }
            this.ImageData = ms.ToArray();
            this._ContentVersion = this.GetHashCode();
        }

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="bs">图片数据</param>
		public XImageValue( byte[] bs )
		{
			this.ImageData = bs ;
            this._ContentVersion = this.GetHashCode();
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="url">要加载图片数据的URL</param>
		public XImageValue( string url )
		{
			if( this.Load( url ) <= 0 )
			{
				throw new Exception("从" + url + "加载图片数据错误");
			}
            this._ContentVersion = this.GetHashCode();
		}

        private int _ContentVersion = 0;
        /// <summary>
        /// 内容版本号,对象数据发生任何改变都会修改此版本号
        /// </summary>
        [Browsable( false )]
        public int ContentVersion
        {
            get
            {
                return _ContentVersion; 
            }
        }

        /// <summary>
        /// 对象是否包含数据
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool HasContent
        {
            get
            {
                if (myValue != null)
                {
                    return true;
                }
                if (bsImage != null && bsImage.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }

		[System.NonSerialized()]
		private System.Drawing.Image myValue = null;
		/// <summary>
		/// 显示的图片对象
		/// </summary>
		[System.Xml.Serialization.XmlIgnore()]
		[System.ComponentModel.Browsable( false )]
		public virtual System.Drawing.Image Value
		{
			get
			{
				if( myValue == null )
				{
					if( bsImage != null )
					{
						byte[] bs = this.bsImage ;
						this.ImageData = bs ;
					}
				}
				return myValue;
			}
			set
			{
				myValue = value;
				bsImage = null;
				ms = null;
                _ContentVersion++;
			}
		}

		private byte[] bsImage = null;

		[System.NonSerialized()]
		private System.IO.MemoryStream ms = null;
		/// <summary>
		/// 保存图形数据的二进制数据
		/// </summary>
		[System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
		public byte[] ImageData
		{
			get
			{
				if( bsImage == null && myValue != null )
				{
					ms = new System.IO.MemoryStream();
					myValue.Save( ms , System.Drawing.Imaging.ImageFormat.Png );
					bsImage = ms.ToArray();
				}
				return bsImage ;
			}
			set
			{
				if( myValue != null )
				{
					myValue.Dispose();
				}
				bsImage = value;
				myValue = null;
				ms = null;
				if( bsImage != null && bsImage.Length > 0 )
				{
                    //if( DCSoft.Common.BinaryHelper.StartsWith(
                    //    bsImage , 
                    //    new byte[]{ 0x36 , 0x55 , 0 , 0 , 0x42 , 0x4d }))
                    //{
                    //    byte[] bs = new byte[ bsImage.Length - 4 ];
                    //    Array.Copy( bsImage , 4 , bs , 0 , bs.Length );
                    //    bsImage = bs ;
                    //}
					ms = new System.IO.MemoryStream( bsImage );
					myValue = System.Drawing.Image.FromStream( ms );
				}
                _ContentVersion++;
			}
		}

        /// <summary>
        /// 包含图片数据的Base64字符串
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement()]
        [System.ComponentModel.DefaultValue( null )]
        public string ImageDataBase64String
        {
            get
            {
                byte[] bs = this.ImageData;
                if (bs != null && bs.Length > 0)
                {
                    string txt = Convert.ToBase64String(bs);
                    txt = DCSoft.Common.StringFormatHelper.GroupFormatString(txt, 8, 16);
                    return txt;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    this.ImageData = Convert.FromBase64String(value);
                    _ContentVersion++;
                }
            }
        }

		/// <summary>
		/// 图片格式
		/// </summary>
		public System.Drawing.Imaging.ImageFormat RawFormat
		{
			get
			{
				if( this.Value != null )
				{
					return this.Value.RawFormat ;
				}
				return null;
			}
		}

		/// <summary>
		/// 图片宽度
		/// </summary>
		public int Width
		{
			get
			{
                if (this.Value == null)
                {
                    return 0;
                }
                else
                {
                    return this.Value.Width;
                }
			}
		}
		/// <summary>
		/// 图片高度
		/// </summary>
		public int Height
		{
			get
			{
                if (this.Value == null)
                {
                    return 0;
                }
                else
                {
                    return this.Value.Height;
                }
			}
		}
		/// <summary>
		/// 图片大小
		/// </summary>
		[System.ComponentModel.Browsable( false )]
		public System.Drawing.Size Size
		{
			get
			{
                if (this.Value == null)
                {
                    return System.Drawing.Size.Empty;
                }
                else
                {
                    return this.Value.Size;
                }
			}
		}

        /// <summary>
        /// 以指定的单位获取图像的界限。
        /// </summary>
        /// <param name="unit">System.Drawing.GraphicsUnit 值之一，指示边框的测量单位。</param>
        /// <returns>System.Drawing.RectangleF，以指定的单位表示图像的界限。</returns>
        public System.Drawing.RectangleF GetBounds(ref System.Drawing.GraphicsUnit unit)
        {
            if (this.Value != null)
            {
                return this.Value.GetBounds(ref unit);
            }
            else
            {
                return System.Drawing.RectangleF.Empty;
            }
        }

        /// <summary>
        /// 创建指定大小的缩略图片
        /// </summary>
        /// <param name="thumbWidth">缩略图宽度</param>
        /// <param name="thumbHeight">缩略图高度</param>
        /// <returns>生成的缩略图对象</returns>
        public XImageValue GetThumbnailImage(int thumbWidth, int thumbHeight)
        {
            System.Drawing.Image img = this.Value;
            if (img != null)
            {
                System.Drawing.Image img2 = img.GetThumbnailImage(
                    thumbWidth, 
                    thumbHeight, 
                    new System.Drawing.Image.GetThumbnailImageAbort(this.ThumbnailCallback),
                    IntPtr.Zero);
                return new XImageValue(img2);
            }
            return null;
        }

        private bool ThumbnailCallback()
        {
            return false;
        }


		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>复制后的对象</returns>
		public XImageValue Clone()
		{
			XImageValue obj = new XImageValue();
			if( myValue != null )
			{
				obj.myValue = ( System.Drawing.Image ) myValue.Clone();
			}
			if( bsImage != null )
			{
				obj.bsImage = new byte[ bsImage.Length ];
				Array.Copy( bsImage , 0 , obj.bsImage , 0 , bsImage.Length );
			}
            obj._ContentVersion = this._ContentVersion;
			return obj ;
		}

		/// <summary>
		/// 复制对象
		/// </summary>
		/// <returns>复制后的对象</returns>
		object System.ICloneable.Clone()
		{
			XImageValue obj = new XImageValue();
			if( myValue != null )
			{
				obj.myValue = ( System.Drawing.Image ) myValue.Clone();
			}
			if( bsImage != null )
			{
				obj.bsImage = new byte[ bsImage.Length ];
				Array.Copy( bsImage , 0 , obj.bsImage , 0 , bsImage.Length );
			}
            obj._ContentVersion = this._ContentVersion;
			return obj ;
		}

		/// <summary>
		/// 从指定URL加载图片数据
		/// </summary>
		/// <param name="strUrl">URL字符串</param>
		/// <returns>加载的字节数</returns>
		public int Load( string strUrl )
		{
			System.Uri uri = new Uri( strUrl );
			if( uri.Scheme == Uri.UriSchemeFile )
			{
                if (System.IO.File.Exists(strUrl) == false)
                {
                    return -1;
                }
                byte[] bs = null;
				using( System.IO.FileStream file = new System.IO.FileStream(
                    strUrl ,
                    System.IO.FileMode.Open , 
                    System.IO.FileAccess.Read ))
				{
					bs = new byte[ file.Length ];
					file.Read( bs , 0 , bs.Length );
					file.Close();
				}
                this.ImageData = bs;
                if (bs == null)
                {
                    return -1;
                }
                else
                {
                    return bs.Length;
                }
            }
			else if( uri.Scheme == Uri.UriSchemeHttp )
			{
				using( System.Net.WebClient client = new System.Net.WebClient())
				{
					byte[] bs = client.DownloadData( strUrl );
                    this.ImageData = bs;
                    if (bs != null)
                    {
                        return bs.Length;
                    }
				}
			}
			return 0;
		}

		/// <summary>
		/// 保存图片数据到指定文件中
		/// </summary>
		/// <param name="FileName">文件名</param>
		/// <returns>操作是否成功</returns>
		public bool Save( string FileName )
		{
			if( myValue != null || bsImage != null )
			{
				using( System.IO.FileStream stream = new System.IO.FileStream( 
						   FileName ,
						   System.IO.FileMode.Create ,
						   System.IO.FileAccess.Write ))
				{
					if( bsImage != null )
					{
						stream.Write( bsImage , 0 , bsImage.Length );
					}
					else
					{

						myValue.Save( stream , GetFormat( FileName ));
                    }
				}
				return true ;
			}
			return false;
		}

        public static ImageFormat GetFormat(string fileName)
        {
            if (fileName != null)
            {
                fileName = fileName.Trim().ToLower();
                if (fileName.EndsWith(".bmp"))
                {
                    return ImageFormat.Bmp;
                }
                else if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg"))
                {
                    return ImageFormat.Jpeg;
                }
                else if (fileName.EndsWith(".png"))
                {
                    return ImageFormat.Png;
                }
                else if (fileName.EndsWith(".gif"))
                {
                    return ImageFormat.Gif;
                }
                else if (fileName.EndsWith(".wmf"))
                {
                    return ImageFormat.Wmf;
                }
            }
            return ImageFormat.Png;
        }
		/// <summary>
		/// 销毁对象
		/// </summary>
        public void Dispose()
        {
            if (myValue != null)
            {
                myValue.Dispose();
                myValue = null;
            }
            if (ms != null)
            {
                ms = null;
            }
            if (bsImage != null)
            {
                bsImage = null;
            }
            if (Disposed != null)
            {
                Disposed(this, new EventArgs());
            }
        }

        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			System.Drawing.Image img = this.Value ;
			byte[] bs = this.ImageData ;
			if( bsImage == null || myValue == null )
			{
				return "None";
			}
			return Convert.ToInt32( bsImage.Length/1024 ) + "KB " + img.Width + "*" + img.Height ;
		}

        #region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
        public event EventHandler Disposed = null;

        private ISite mySite = null;
        /// <summary>
        /// 对象站点
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
         

	}//public class XImageValue : System.ICloneable , System.IDisposable

	

	/// <summary>
	/// 图片数据类型转换器,设计器内部使用
	/// </summary>
	public class XImageValueTypeConverter : TypeConverter
	{
        /// <summary>
        /// 判断能否将指定类型的数据转换为图片
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sourceType">指定的数据类型</param>
        /// <returns>能否转换</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(byte[]))
                || sourceType == typeof( string ) 
                || base.CanConvertFrom(context, sourceType));
		}

        /// <summary>
        /// 判断能否将图片转换为指定的类型
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="destinationType">指定的数据类型</param>
        /// <returns>能否转换</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if( destinationType == null )
			{
				return false;
			}
			return ( destinationType.Equals( typeof( byte[] ))
				|| destinationType.Equals( typeof( InstanceDescriptor ))
				|| base.CanConvertTo( context , destinationType ));
		}

        /// <summary>
        /// 将指定的数据转换为图片对象
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">区域信息</param>
        /// <param name="Value">要转换的数据</param>
        /// <returns>转换结果</returns>
		public override object ConvertFrom(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, 
            object Value)
		{
			if (Value is byte[])
			{
				return new XImageValue( ( byte[] ) Value );
			}
            if (Value is string)
            {
                string str = (string)Value;
                if (str == null || str.Trim().Length == 0)
                {
                    return new XImageValue();
                }
                else
                {
                    foreach (char c in str)
                    {
                        if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/= \r\n\t".IndexOf(c) < 0)
                        {
                            // 不是合格的BASE64字符串
                            return new XImageValue();
                        }
                    }
                    byte[] bs = Convert.FromBase64String(str);
                    return new XImageValue(bs);
                }
            }
			return base.ConvertFrom(context, culture, Value);
		}

        /// <summary>
        /// 将图片转换为指定类型的数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">区域信息</param>
        /// <param name="Value">图片数据</param>
        /// <param name="destinationType">指定的类型</param>
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
			
			XImageValue img = ( XImageValue ) Value ;
			if( img == null )
			{
				return "[NULL]";
			}
			if (destinationType == typeof(string))
			{
				return img.ToString();
			}

			if (destinationType == typeof(byte[]))
			{
				return img.ImageData ;
			}
		
			if ( destinationType == typeof(InstanceDescriptor))
			{
				byte[] bs = img.ImageData ;
				if( bs == null || bs.Length == 0 )
				{
					return new InstanceDescriptor( typeof( XImageValue).GetConstructor(
                        new Type[]{}) ,
                        new object[]{});
				}
				else
				{
					System.Reflection.MemberInfo constructor = typeof( XImageValue ).GetConstructor( new Type[]{ typeof( byte[] ) });
					return new InstanceDescriptor(constructor , new object[]{ img.ImageData });
				}
			}

			return base.ConvertTo(context, culture, Value, destinationType);
		}

        /// <summary>
        /// 获得图片对象的属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="Value">图片数据</param>
        /// <param name="attributes">特性</param>
        /// <returns>属性列表</returns>
		public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context, 
            object Value, 
            Attribute[] attributes)
		{
            return TypeDescriptor.GetProperties(typeof(XImageValue), attributes).Sort(new string[] { "Width", "Height", "RawFormat" });
		}

        /// <summary>
        /// 支持获得图片对象的属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>是否支持获得属性</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}