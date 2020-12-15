using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 文档内容链接对象编辑器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XTextContentLinkElementEditor : ElementEditor
    {
        /// <summary>
        /// 判断是否支持指定的编辑方法
        /// </summary>
        /// <param name="method">编辑方法类型</param>
        /// <returns>是否支持</returns>
        public override bool IsSupportMethod(ElementEditMethod method)
        {
            return true;
        }
        /// <summary>
        /// 编辑内容
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgContentLinkEditor dlg = new dlgContentLinkEditor())
            {
                dlg.SourceEventArgs = args;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (args.Method == ElementEditMethod.Edit)
                    {
                        DomContentLinkFieldElement link = (DomContentLinkFieldElement)args.Element;
                        if (args.Document.Options.BehaviorOptions.DesignMode == false)
                        {
                            link.UpdateContent(false);
                        }
                        //link.AfterLoad(FileFormat.XML);
                        if (args.Method == ElementEditMethod.Edit)
                        {
                            ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                            args2.Document = args.Document;
                            args2.Element = link;
                            args2.LoadingDocument = false;
                            link.RaiseBubbleOnContentChanged(args2);
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }

    
}
