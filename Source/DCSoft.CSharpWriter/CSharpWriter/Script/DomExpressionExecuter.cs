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

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 表达式执行器
    /// </summary>
    public class DomExpressionExecuter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomExpressionExecuter()
        {
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        /// <summary>
        /// 自定义的表达式执行委托对象
        /// </summary>
        public DomExpressionEventHandler CustomExecute = null;

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public virtual object Execute(DomExpressionEventArgs args)
        {
            if (args.Expression.Type == DomExpressionType.Simple)
            {
            }
            if (CustomExecute != null)
            {
                CustomExecute(this, args);
            }
            return args.Result;
        }

        /// <summary>
        /// 更新表达式中引用的文档元素列表
        /// </summary>
        /// <param name="args">参数</param>
        public virtual void UpdateReferencedElements(DomExpressionEventArgs args)
        {
            args.Expression.ReferencedElementsRefreshed = true;
        }

        private static string[] SimpleBoolExpressionMethod = new string[]
            {
                ">=",
                "<>",
                "<=",
                ""
            };


        ///// <summary>
        ///// 执行表达式
        ///// </summary>
        ///// <returns></returns>
        //public double ExecuteSimpleExpression( DomExpressionEventArgs args )
        //{
        //    StringBuilder result = new StringBuilder();
        //    string[] items = VariableString.AnalyseVariableString(args.Expression , "[", "]");
        //    for (int iCount = 0; iCount < items.Length; iCount++)
        //    {
        //        if ((iCount % 2) == 0)
        //        {
        //            result.Append(items[iCount]);
        //        }
        //        else
        //        {
        //            object obj = mySourceElements[(iCount - 1) / 2];
        //            if (obj != null)
        //            {
        //                if (obj is DesignElement)
        //                {
        //                    double dbl = XReportExpression.NaN;
        //                    string v = ((DesignElement)obj).ReportValue;
        //                    if (v != null)
        //                    {
        //                        v = v.Trim();
        //                        if (v.Length > 0 && v.Length < 25)
        //                        {
        //                            dbl = XReportExpression.ToDouble(v);
        //                            //v = XReportExpression.RemoveCurrentySymbol(v);
        //                            //if (double.TryParse(v, out dbl) == false)
        //                            //{
        //                            //    dbl = XReportExpression.NaN;
        //                            //}
        //                        }
        //                    }
        //                    if (double.IsNaN(dbl))
        //                    {
        //                        // 若传入是文本就是“NaN”，则转换是会成功的，此处进行额外判断。
        //                        result.Append(XReportExpression.NaN.ToString());
        //                    }
        //                    else
        //                    {
        //                        result.Append(dbl.ToString());
        //                    }
        //                }
        //                else if (obj is ArrayList)
        //                {
        //                    ArrayList list = (ArrayList)obj;
        //                    for (int iCount2 = 0; iCount2 < list.Count; iCount2++)
        //                    {
        //                        if (iCount2 > 0)
        //                            result.Append(",");
        //                        double dbl = XReportExpression.NaN;
        //                        string v = ((DesignElement)list[iCount2]).ReportValue;
        //                        if (v != null)
        //                        {
        //                            v = v.Trim();
        //                            if (v.Length > 0 && v.Length < 25)
        //                            {
        //                                dbl = XReportExpression.ToDouble(v);
        //                                //v = XReportExpression.RemoveCurrentySymbol(v);
        //                                //if (double.TryParse(v, out dbl) == false)
        //                                //{
        //                                //    dbl = XReportExpression.NaN;
        //                                //}
        //                            }
        //                        }
        //                        if (double.IsNaN(dbl))
        //                        {
        //                            // 若传入是文本就是“NaN”，则转换是会成功的，此处进行额外判断。
        //                            result.Append(XReportExpression.NaN.ToString());
        //                        }
        //                        else
        //                        {
        //                            result.Append(dbl.ToString());
        //                        }

        //                        //result.Append(dbl.ToString());
        //                    }//for
        //                }
        //            }
        //        }
        //    }//for
        //    string txt = result.ToString();
        //    double dblResult = 0;
        //    try
        //    {
        //        XReportExpression exp = new XReportExpression(myElement.ReportDocument, txt);
        //        dblResult = Convert.ToDouble(exp.Evaluate());
        //        System.Diagnostics.Debug.WriteLine(string.Format(
        //            XReportStrings.ExecuteExpression_Name_Text_Result,
        //            myElement.ID,
        //            strExpression + " ==> " + txt.Replace(XReportExpression.NaN.ToString(), "NaN"),
        //            dblResult));
        //    }
        //    catch (Exception ext)
        //    {
        //        //System.Diagnostics.Debug.WriteLine( "XXXXXXXXXX " + result.ToString());
        //        System.Diagnostics.Debug.WriteLine(string.Format(
        //            XReportStrings.ExecuteExpression_Name_Text_Result,
        //            myElement.ID,
        //            strExpression + " ==> " + txt.Replace(XReportExpression.NaN.ToString(), "NaN"),
        //            ext.Message));
        //    }
        //    return dblResult;
        //}

        ///// <summary>
        /////  执行简单的表达式
        ///// </summary>
        ///// <param name="args"></param>
        //private void ExecuteSimpleExpression(DomExpressionEventArgs args)
        //{
        //    DomExpression exp = args.Expression;
        //    string sourceValue = null;
        //    if (args.Document.Parameters.Contains( this.SourceElementName))
        //    {
        //        object v = args.Document.Parameters.GetValue(this.SourceElementName);
        //        if (v != null && DBNull.Value.Equals(v) == false)
        //        {
        //            sourceValue = Convert.ToString(v);
        //        }
        //    }
        //    else
        //    {
        //        sourceValue = args.Document.GetFormValue(this.SourceElementName);
        //    }
        //    bool result = false;
        //    switch (this.Method)
        //    {
        //        case "=":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr == 0;
        //            }
        //            break;
        //        case ">":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr > 0;
        //            }
        //            break;
        //        case ">=":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr >= 0;
        //            }
        //            break;
        //        case "<":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr < 0;
        //            }
        //            break;
        //        case "<=":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr <= 0;
        //            }
        //            break;
        //        case "<>":
        //            {
        //                int cr = CompareValue(sourceValue, this.Value);
        //                result = cr != 0;
        //            }
        //            break;
        //        case "like":
        //            break;
        //    }
        //    args.Result = result;
        //    return result;
        //}

        //private int CompareValue(string v1, string v2)
        //{
        //    if (string.IsNullOrEmpty(v1) && string.IsNullOrEmpty(v2))
        //    {
        //        return 0;
        //    }
        //    if (v1 == v2)
        //    {
        //        return 0;
        //    }
        //    double dbl2 = 0;
        //    if (double.TryParse(v2, out dbl2))
        //    {
        //        // 进行数值比较
        //        double dbl1 = 0;
        //        if (double.TryParse(v1, out dbl1))
        //        {
        //            return dbl1.CompareTo(dbl2);
        //        }
        //    }
        //    else
        //    {
        //        if (v1 != null)
        //        {
        //            return v1.CompareTo(v2);
        //        }
        //        else
        //        {
        //            return 0 - v2.CompareTo(v1);
        //        }
        //    }
        //    return 0;
        //}

        /// <summary>
        /// 处理文档元素内容发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public virtual void HandleContentChanged( object sender , ContentChangedEventArgs args)
        {
            this.Document.Enumerate(delegate(object sender2, ElementEnumerateEventArgs args2) 
                {
                    DomExpressionList expressions = null;
                    if (args2.Element is DomContainerElement )
                    {
                        expressions = ((DomContainerElement)args.Element).Expressions;
                    }
                    if (expressions != null)
                    {
                        DomExpressionEventArgs args3 = new DomExpressionEventArgs();
                        args3.Document = this.Document ;
                        args3.Element = args.Element ;
                        foreach (DomExpression item in expressions)
                        {
                            args3.Expression = item ;
                            if (item.ReferencedElementsRefreshed == false)
                            {
                                this.UpdateReferencedElements(args3);
                            }
                            if (item.ReferencedElements != null 
                                && item.ReferencedElements.Contains( args.Element ))
                            {
                                object result = this.Execute(args3);

                            }
                        }
                    }
                } );
        }
    }
}
