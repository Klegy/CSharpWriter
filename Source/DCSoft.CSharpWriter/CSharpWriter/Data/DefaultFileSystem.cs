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
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace DCSoft.CSharpWriter.Data
{

    /// <summary>
    /// 默认使用的文件系统操作对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class DefaultFileSystem : IFileSystem
    {
        private static DefaultFileSystem _Instance = null;

        /// <summary>
        /// 对象唯一静态实例
        /// </summary>
        public static DefaultFileSystem Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DefaultFileSystem();
                }
                return _Instance; 
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public DefaultFileSystem()
        {
        }

        #region IFileSystem 成员
         
        public virtual Stream Open( IServiceContainer services , string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            WriterDebugger debugger = ( WriterDebugger ) services.GetService(typeof( WriterDebugger ));
            //if( debugger != null && debugger.Enabled )
            //{
            //    debugger.WriteLine( string.Format(
            //        WriterStrings.Loading_FileName ,
            //        fileName ));
            //}
            UrlStream stream = UrlStream.Open(fileName);
            //if (debugger != null)
            //{
            //    if (stream == null)
            //    {
            //        debugger.WriteLine(WriterStrings.Fail);
            //    }
            //    else
            //    {
            //        debugger.WriteLine(string.Format(
            //            WriterStrings.LoadComplete_Size,
            //            WriterUtils.FormatByteSize( (int)stream.Length)));
            //    }
            //}
            return stream;
        }

        public virtual Stream Save(IServiceContainer services , string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            UrlStream stream= UrlStream.Save(fileName);
            //WriterDebugger debugger = (WriterDebugger)services.GetService(typeof(WriterDebugger));
            //if (debugger != null && debugger.Enabled)
            //{
            //    debugger.WriteLine(string.Format(
            //        WriterStrings.Saving_FileName , 
            //        fileName));
            //}
            return stream;
        }

        public virtual VFileInfo GetFileInfo(IServiceContainer services, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            VFileInfo info = new VFileInfo();
            try
            {
                if (fileName.IndexOf(":") < 0)
                {
                    fileName = System.IO.Path.Combine(System.Environment.CurrentDirectory, fileName);
                    info = new VFileInfo(new System.IO.FileInfo( fileName ));
                    info.Format = WriterUtils.GetFormat(fileName).ToString();
                }
                else
                {
                    Uri uri = new Uri(fileName);
                    if (uri.Scheme == Uri.UriSchemeFile)
                    {
                        info = new VFileInfo(new System.IO.FileInfo(uri.LocalPath));
                        info.Format = WriterUtils.GetFormat(uri.LocalPath).ToString();
                    }
                    else
                    {
                        info.Exists = true;
                        info.Format = FileFormat.XML.ToString();
                        info.Name = fileName;
                        info.FullPath = fileName;
                        info.BasePath = WriterUtils.GetBaseURL(fileName);
                    }
                }
            }
            catch
            {
                // 出现错误，认为文件不存在
                info.Exists = false;
                info.Name = fileName;
                info.FullPath = fileName;
            }
            return info;
        }
          
        private string _OpenFileFilter = null;
        /// <summary>
        /// 打开文件使用的文件名过滤字符串
        /// </summary>
        public string OpenFileFilter
        {
            get 
            {
                return _OpenFileFilter; 
            }
            set
            {
                _OpenFileFilter = value; 
            }
        }

        public string BrowseOpen(IServiceContainer services, string initalizeFileName)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (string.IsNullOrEmpty(initalizeFileName) == false)
                {
                    dlg.FileName = initalizeFileName;
                }
                dlg.Filter = this.OpenFileFilter;
                dlg.CheckFileExists = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.FileName;
                }
            }
            return null;
        }

        private string _SaveFileFilter = null;
        /// <summary>
        /// 保存时使用的文件名过滤条件
        /// </summary>
        public string SaveFileFilter
        {
            get { return _SaveFileFilter; }
            set { _SaveFileFilter = value; }
        }

        public string BrowseSave(IServiceContainer services, string initalizeFileName)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (string.IsNullOrEmpty(initalizeFileName) == false)
                {
                    dlg.FileName = initalizeFileName;
                }
                dlg.FileName = initalizeFileName;
                dlg.Filter = this.SaveFileFilter;
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return dlg.FileName;
                }
            }
            return null;
        }

        #endregion
    }
}
