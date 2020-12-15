/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;

namespace DCSoft.Common
{
	/// <summary>
	/// 数据对象的帮助模块
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class DataObjectHelper
	{
        public const string Format_FileNameW = "FileNameW";

		/// <summary>
		/// 根据系统剪切板创建一个对象实例
		/// </summary>
		/// <returns>创建的对象</returns>
		public static DataObjectHelper CreateFromClipBoard()
		{
			return new DataObjectHelper( System.Windows.Forms.Clipboard.GetDataObject());
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="obj">数据对象</param>
		public DataObjectHelper( System.Windows.Forms.IDataObject obj )
		{
			myObject = obj ;
		}

		/// <summary>
		/// 初始化对象
		/// </summary>
		/// <param name="args">拖拽事件参数对象</param>
		public DataObjectHelper( System.Windows.Forms.DragEventArgs args )
		{
			myObject = args.Data ;
		}

		private System.Windows.Forms.IDataObject myObject = null;
		/// <summary>
		/// 内置的数据对象
		/// </summary>
		public System.Windows.Forms.IDataObject DataObject
		{
			get{ return myObject ;}
		}
		/// <summary>
		/// 包含的所有的数据格式的名称
		/// </summary>
		public string[] Formats
		{
			get
			{
				if( myObject != null )
					return myObject.GetFormats();
				else
					return null;
			}
		}
		/// <summary>
		/// 判断是否存在指定格式的数据
		/// </summary>
		/// <param name="strFormatName">格式名称</param>
		/// <returns>是否存在指定格式的数据</returns>
		public bool GetDataPresent( string strFormatName )
		{
			if( myObject == null )
				return false;
			else
				return myObject.GetDataPresent( strFormatName );
		}
		/// <summary>
		/// 获得默认数值
		/// </summary>
		/// <returns>获得的数值</returns>
		public object GetData()
		{
			string[] formats = myObject.GetFormats();
			if( formats != null && formats.Length > 0 )
				return myObject.GetData( formats[ 0 ]);
			else
				return null;
		}
		/// <summary>
		/// 判断是否有位图数据
		/// </summary>
		public bool HasBitmap
		{
			get
			{
				if( myObject == null )
					return false;
				return myObject.GetDataPresent( System.Windows.Forms.DataFormats.Bitmap ); 
			}
		}
		/// <summary>
		/// 设置/返回位图数据
		/// </summary>
		public System.Drawing.Bitmap Bitmap
		{
			get
			{
				if( myObject == null )
					return null;
				return myObject.GetData( System.Windows.Forms.DataFormats.Bitmap ) as System.Drawing.Bitmap ;
			}
			set
			{
				if( myObject != null )
				myObject.SetData( System.Windows.Forms.DataFormats.Bitmap , value ); 
			}
		}

		/// <summary>
		/// 判断是否有纯文本数据
		/// </summary>
		public bool HasText
		{
			get
			{
                if (myObject == null)
                {
                    return false;
                }
				return myObject.GetDataPresent( System.Windows.Forms.DataFormats.Text ); 
			}
		}
		/// <summary>
		/// 设置/返回纯文本数据
		/// </summary>
		public string Text
		{
			get
			{
				if( myObject == null )
					return null;
				return myObject.GetData( System.Windows.Forms.DataFormats.Text ) as string ;
			}
			set
			{
				if( myObject == null )
					myObject.SetData( System.Windows.Forms.DataFormats.Text  , value ); 
			}
		}

		/// <summary>
		/// 判断是否有Html数据
		/// </summary>
		public bool HasHtml
		{
			get
			{
				if( myObject == null )
					return false;
				return myObject.GetDataPresent( System.Windows.Forms.DataFormats.Html ); 
			}
		}
		/// <summary>
		/// 设置/返回HTML数据
		/// </summary>
		public string Html
		{
			get
			{
				if( myObject == null )
					return null;
				return myObject.GetData( System.Windows.Forms.DataFormats.Html ) as string ;
			}
			set
			{
				if( myObject == null )
					myObject.SetData( System.Windows.Forms.DataFormats.Html , value );
			}
		}


        /// <summary>
        /// 判断是否有RTF数据
        /// </summary>
        public bool HasRtf
        {
            get
            {
                if (myObject == null)
                    return false;
                return myObject.GetDataPresent(System.Windows.Forms.DataFormats.Rtf);
            }
        }
        /// <summary>
        /// 设置/返回RTF数据
        /// </summary>
        public string Rtf
        {
            get
            {
                if (myObject == null)
                    return null;
                return myObject.GetData(System.Windows.Forms.DataFormats.Rtf) as string;
            }
            set
            {
                if (myObject == null)
                    myObject.SetData(System.Windows.Forms.DataFormats.Rtf, value);
            }
        }


		/// <summary>
		/// 判断是否存在文件名清单
		/// </summary>
		public bool HasFileNames
		{
			get
			{
				if( myObject == null )
					return false;
                return myObject.GetDataPresent(Format_FileNameW);
			}
		}
		/// <summary>
		/// 获得文件名清单
		/// </summary>
		public string[] FileNames
		{
			get
			{
				if( myObject == null )
					return null;
                System.Collections.IEnumerable names = myObject.GetData(Format_FileNameW)
                    as System.Collections.IEnumerable;
				if( names != null )
				{
					System.Collections.ArrayList list = new System.Collections.ArrayList();
					foreach( string name in names )
					{
						if( name != null && name.Length > 0 )
							list.Add( name );
					}
					if( list.Count > 0 )
						return ( string[] ) list.ToArray( typeof( string ));
				}
				return null;
			}
		}
		/// <summary>
		/// 获得文件名清单中的第一个文件名
		/// </summary>
		public string FirstFileName
		{
			get
			{
				string[] fs = FileNames ;
                if (fs != null && fs.Length > 0)
                {
                    return fs[0];
                }
                else
                {
                    return null;
                }
			}
		}
		/// <summary>
		/// 将数据保存到系统剪切板中
		/// </summary>
		public void SetToClipboard()
		{
            if (myObject != null)
            {
                System.Windows.Forms.Clipboard.SetDataObject(this.myObject);
            }
		}
		
	}//public sealed class DataObjectHelper
}
