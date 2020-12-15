/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 基于URL的文件流对象，可访问本地文件系统和WEB服务器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class UrlStream : Stream
    {
        /// <summary>
        /// 打开文件流
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>创建的流对象</returns>
        public static UrlStream Open(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            url = url.Trim();
            if (url.Length == 0)
            {
                throw new ArgumentNullException("url");
            }

            if (url.IndexOf(":") > 0)
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    string fn = uri.LocalPath;
                    if (System.IO.File.Exists(fn) == false)
                    {
                        throw new System.IO.FileNotFoundException(fn);
                    }
                    UrlStream stream = new UrlStream();
                    stream._BaseStream = new System.IO.FileStream(
                        fn,
                        FileMode.Open,
                        FileAccess.Read);
                    return stream;
                }
                else
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        //Uri uri2 = new Uri(url);
                        byte[] bs = client.DownloadData(url);
                        UrlStream stream = new UrlStream();
                        stream._BaseStream = new System.IO.MemoryStream(bs);
                        return stream;
                    }
                }
            }
            else
            {
                // 可能是相对路径,读取本地文件路径
                string fn = System.IO.Path.Combine(System.Environment.CurrentDirectory, url);
                UrlStream stream = new UrlStream();
                stream._BaseStream = new System.IO.FileStream(
                    fn,
                    FileMode.Open,
                    FileAccess.Read);
                return stream;
            }
            //return null;
        }

        /// <summary>
        /// 打开保存数据的流
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <returns>创建的流对象</returns>
        public static UrlStream Save(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            url = url.Trim();
            if (url.Length == 0)
            {
                throw new ArgumentNullException("url");
            }

            if (url.IndexOf(":") > 0)
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    string fn = uri.LocalPath;
                    UrlStream stream = new UrlStream();
                    stream._BaseStream = new System.IO.FileStream(
                        fn,
                        FileMode.Create ,
                        FileAccess.Write);
                    return stream;
                }
                else
                {
                    UrlStream stream = new UrlStream();
                    stream._BaseStream = new System.IO.MemoryStream();
                    stream._SaveUrl = url;
                    return stream;
                }
            }
            else
            {
                // 可能是相对路径,读取本地文件路径
                string fn = System.IO.Path.Combine(System.Environment.CurrentDirectory, url);
                UrlStream stream = new UrlStream();
                stream._BaseStream = new System.IO.FileStream(
                    fn,
                    FileMode.Create,
                    FileAccess.Write);
                return stream;
            }
        }

        private UrlStream()
        {
        }

        private string _SaveUrl = null;

        private Stream _BaseStream = null;

        /// <summary>
        /// 关闭流对象
        /// </summary>
        public override void Close()
        {
            base.Close();
            if (_BaseStream != null)
            {
                _BaseStream.Close();
            }
            if (string.IsNullOrEmpty(_SaveUrl) == false && _BaseStream is System.IO.MemoryStream  )
            {
                byte[] bs = ((System.IO.MemoryStream)_BaseStream).ToArray();
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.UploadData(this._SaveUrl, bs);
                }
            }
        }

        public override bool CanRead
        {
            get
            {
                return _BaseStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _BaseStream.CanSeek ;
            }
        }

        public override bool CanWrite
        {
            get { return _BaseStream.CanWrite; }
        }

        public override void Flush()
        {
            _BaseStream.Flush();
        }

        public override long Length
        {
            get { return _BaseStream.Length;}
        }

        public override long Position
        {
            get
            {
                return _BaseStream.Position;
            }
            set
            {
                _BaseStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _BaseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _BaseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _BaseStream.Write(buffer, offset, count);
        }
    }
}
