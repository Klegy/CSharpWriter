/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.Drawing ;

namespace DCSoft.Common
{
	/// <summary>
	/// 图片对象帮助模块
	/// </summary>
    /// <remarks>编制 袁永福</remarks>
	public sealed class ImageHelper
	{
		static ImageHelper()
		{
            _Formats = new Dictionary<string, ImageFormat>();
            _Formats["bmp"] = System.Drawing.Imaging.ImageFormat.Bmp;
            _Formats["emf"] = System.Drawing.Imaging.ImageFormat.Emf;
            _Formats["exif"] = System.Drawing.Imaging.ImageFormat.Exif;
            _Formats["gif"] = System.Drawing.Imaging.ImageFormat.Gif;
            _Formats["icon"] = System.Drawing.Imaging.ImageFormat.Icon;
			//myExtersion["bmp"] = System.Drawing.Imaging.ImageFormat.MemoryBmp ;
            _Formats["png"] = System.Drawing.Imaging.ImageFormat.Png;
            _Formats["tiff"] = System.Drawing.Imaging.ImageFormat.Tiff;
            _Formats["wmf"] = System.Drawing.Imaging.ImageFormat.Wmf;
            _Formats["jpeg"] = System.Drawing.Imaging.ImageFormat.Jpeg;
            _Formats["jpg"] = System.Drawing.Imaging.ImageFormat.Jpeg;

            _ContentTypes = new Dictionary<string, string>();
            _ContentTypes["bmp"] = "image/bmp";
            _ContentTypes["gif"] = "image/gif";
            _ContentTypes["jpg"] = "image/jpg";
            _ContentTypes["jpeg"] = "image/jpeg";
            _ContentTypes["tif"] = "image/tif";
            _ContentTypes["tiff"] = "image/tif";
            _ContentTypes["ico"] = "image/x-icon";
		}



        private static Dictionary<string, ImageFormat> _Formats = null;
        private static Dictionary<string, string> _ContentTypes = null;

        public static string GetContentType(string extensition)
        {
            foreach (string ext in _ContentTypes.Keys)
            {
                if (string.Compare(ext, extensition, true) == 0)
                {
                    return _ContentTypes[ext];
                }
            }
            return null;
        }

		public static string GetFileExtersion( ImageFormat format )
		{
			if( format == null )
			{
				return null;
			}
            foreach (string name in _Formats.Keys)
			{
                if (_Formats.Equals(false))
                {
                    return name;
                }
			}
			return null;
		}



        public static ImageFormat GetFormatByFileName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            int lastIndex = fileName.LastIndexOf('.');
            if (lastIndex >= 0)
            {
                fileName = fileName.Substring(lastIndex).Trim().ToLower();
                if (_Formats.ContainsKey(fileName))
                {
                    return _Formats[fileName];
                }
            }
            return null;
        }

        public static string GetContentTypeByFileName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }
            int lastIndex = fileName.LastIndexOf('.');
            if (lastIndex >= 0)
            {
                fileName = fileName.Substring(lastIndex).Trim().ToLower();
                if ( _ContentTypes.ContainsKey(fileName))
                {
                    return _ContentTypes[fileName];
                }
            }
            return null;
        }


		public static ImageFormat GetFormat( string Extersion )
		{
			if( Extersion == null )
			{
				return null;
			}
			Extersion = Extersion.Trim().ToLower();
            if (Extersion.StartsWith("."))
            {
                Extersion = Extersion.Substring(1);
            }
			Extersion = Extersion.Trim();
            if (_Formats.ContainsKey(Extersion))
            {
                return _Formats[Extersion];
            }
            else
            {
                return null;
            }
		}

		/// <summary>
		/// 判断是否是一个图片文件名
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>是否是图片文件名</returns>
		public static bool IsImageFileName( string strFileName )
		{
            if (strFileName == null)
            {
                return false;
            }
            strFileName = strFileName.Trim().ToLower();
            foreach (string key in _Formats.Keys)
            {
                if (strFileName.EndsWith("." + key))
                {
                    return true;
                }
            }
            return false;
		}

		/// <summary>
		/// 安全的从一个文件中加载图片对象
		/// </summary>
		/// <param name="strFileName">文件名</param>
		/// <returns>加载的对象,若发生错误则返回空引用</returns>
		public static System.Drawing.Image SafeLoadImage( string strFileName )
		{
			try
			{
				if( strFileName == null )
					return null;
				if( System.IO.File.Exists( strFileName ) == false )
					return null;
				return System.Drawing.Image.FromFile( strFileName );
			}
			catch( Exception ext )
			{
				System.Console.WriteLine("SafeLoadImage :" + ext.ToString());
			}
			return null;
		}

		private ImageHelper()
		{
		}
	}//public sealed class ImageHelper
}