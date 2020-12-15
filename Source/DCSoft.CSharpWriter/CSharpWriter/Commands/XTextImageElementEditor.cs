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
using DCSoft.CSharpWriter.Dom;
using System.Windows.Forms;
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 图片内容编辑器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XTextImageElementEditor : ElementEditor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextImageElementEditor()
        {
        }

        public override bool IsSupportMethod(ElementEditMethod method)
        {
            return true;
        }

        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgImageElementEditor dlg = new dlgImageElementEditor())
            {
                dlg.SourceEventArgs = args;
                if (dlg.ShowDialog( args.ParentWindow ) == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //XTextImageElement img = (XTextImageElement)args.Element;
            //using (OpenFileDialog dlg = new OpenFileDialog())
            //{
            //    dlg.Filter = WriterStrings.ImageFileFilter;
            //    dlg.CheckFileExists = true;
            //    dlg.ShowReadOnly = false;
            //    if (dlg.ShowDialog(args.ParentWindow) == DialogResult.OK)
            //    {
            //        img.OwnerDocument = args.Document;
            //        XImageValue newValue = new XImageValue();
            //        if (newValue.Load(dlg.FileName) > 0 )
            //        {
            //            SizeF oldSize = new SizeF(img.Width, img.Height);
            //            XImageValue oldValue = img.Image ;
            //            img.Image = newValue;
            //            img.UpdateSize();
            //            if (args.LogUndo && args.Document.CanLogUndo )
            //            {
            //                // 记录撤销操作信息
            //                args.Document.UndoList.AddProperty("Width", oldSize.Width, img.Width, img);
            //                args.Document.UndoList.AddProperty("Height", oldSize.Height, img.Height, img);
            //                args.Document.UndoList.AddProperty("Image", oldValue, img.Image, img);
            //            }
            //            if (args.Method == ElementEditMethod.Edit)
            //            {
            //                img.ContentElement.RefreshPrivateContent(img.ViewIndex);
            //                img.UpdateContentVersion();
            //                ContentChangedEventArgs args2 = new ContentChangedEventArgs();
            //                args2.Document = args.Document;
            //                args2.Element = img;
            //                args2.LoadingDocument = false;
            //                img.Parent.RaiseBubbleOnContentChanged(args2);
            //            }
            //            return true;
            //        }
            //    }
            //}
            //return false;
        }
    }
}