using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Commands;

namespace DCSoft.CSharpWriter.Medical
{
    [WriterCommandDescription(ModuleName_Medical)]
    public class WriterCommandModuleMedical : WriterCommandModule
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterCommandModuleMedical()
        {
        }

        public const string ModuleName_Medical = "Medical";

        public const string CommandName_InsertMedicalExpression = "InsertMedicalExpression";

        /// <summary>
        /// 插入医学表达式命令
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        [WriterCommandDescription( CommandName_InsertMedicalExpression )]
        protected void InsertMedicalExpression(object sender, WriterCommandEventArgs args)
        {
            if (args.Mode == WriterCommandEventMode.QueryState)
            {
                args.Enabled =  args.DocumentControler.CanInsertElementAtCurrentPosition(
                    typeof(XTextMedicalExpressionFieldElement));
            }
            else if (args.Mode == WriterCommandEventMode.Invoke)
            {
                XTextMedicalExpressionFieldElement newElement = null;
                if ( args.Parameter is XTextMedicalExpressionFieldElement )
                {
                    newElement = (XTextMedicalExpressionFieldElement)args.Parameter;
                }
                if (newElement == null)
                {
                    newElement = new XTextMedicalExpressionFieldElement();
                    newElement.OwnerDocument = args.Document;
                    newElement.SetInnerTextFast("Value1,Value2,Value2,Value4");
                }
                    
                if (args.ShowUI)
                {
                    newElement.OwnerDocument = args.Document;
                    if (DCSoft.CSharpWriter.Commands.WriterCommandModuleInsert.CallElementEdtior(
                        args,
                        newElement,
                        ElementEditMethod.Insert) == false)
                    {
                        newElement.Dispose();
                        newElement = null;
                    }
                }
                if (newElement != null)
                {
                    DomElement element = args.Document.CurrentElement;
                    newElement.EditMode = false;
                    newElement.StyleIndex = element.StyleIndex;
                    newElement.StartElement.StyleIndex = element.StyleIndex;
                    newElement.EndElement.StyleIndex = element.StyleIndex;
                    if (newElement.ExpressionStyle == MedicalExpressionStyle.FourValues)
                    {
                        newElement.SetInnerTextFast("Value1,Value2,Value2,Value4");
                    }
                    else if (newElement.ExpressionStyle == MedicalExpressionStyle.ThreeValues)
                    {
                        newElement.SetInnerTextFast("Value1,Value2,Value2");
                    }
                    //newElement.EditMode = true;
                    foreach (DomElement sube in newElement.Elements)
                    {
                        sube.StyleIndex = element.StyleIndex;
                    }
                    newElement.UpdateDataBinding(true);
                    args.DocumentControler.InsertElement(newElement);
                    args.RefreshLevel = UIStateRefreshLevel.All;
                    args.Result = newElement;
                }
            }
        }
    }
}
