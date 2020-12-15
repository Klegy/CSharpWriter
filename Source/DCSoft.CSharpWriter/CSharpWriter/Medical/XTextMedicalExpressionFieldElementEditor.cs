using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter;

namespace DCSoft.CSharpWriter.Medical
{
    /// <summary>
    /// 医学表达式编辑器对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XTextMedicalExpressionFieldElementEditor  : ElementEditor 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextMedicalExpressionFieldElementEditor()
        {
        }
        /// <summary>
        /// 判断操作是否支持
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public override bool IsSupportMethod(DCSoft.CSharpWriter.ElementEditMethod method)
        {
            return true;
        }

        /// <summary>
        /// 编辑对象
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override bool Edit(ElementEditEventArgs args)
        {
            using (dlgMedicalExpressionEditor dlg = new dlgMedicalExpressionEditor())
            {
                dlg.SourceEventArgs = args;
                dlg.CurrentContentStyle = args.Document.EditorCurrentStyle;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (args.Method == ElementEditMethod.Edit)
                    {
                        XTextMedicalExpressionFieldElement field = (XTextMedicalExpressionFieldElement)args.Element;
                        //link.AfterLoad(FileFormat.XML);
                        if (args.Method == ElementEditMethod.Edit)
                        {
                            ContentChangedEventArgs args2 = new ContentChangedEventArgs();
                            args2.Document = args.Document;
                            args2.Element = field;
                            args2.LoadingDocument = false;
                            field.RaiseBubbleOnContentChanged(args2);
                        }
                    }
                    return true;
                }
                return false;
            }
             
        }
    }
}
