using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档域对象
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlType("DField")]
    [System.Diagnostics.DebuggerDisplay("Field")]
    public class DomDocumentFieldElement : DomFieldElementBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomDocumentFieldElement()
        {
        }

        private string _Code = null;
        /// <summary>
        /// 域编码
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Code
        {
            get
            {
                return _Code; 
            }
            set
            {
                _Code = value; 
            }
        }

        private bool _AutoUpdateResult = false;
        /// <summary>
        /// 自动更新域结果
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool AutoUpdateResult
        {
            get
            {
                return _AutoUpdateResult; 
            }
            set
            {
                _AutoUpdateResult = value; 
            }
        }

        public override void AfterLoad(FileFormat format)
        {
            if (this.AutoUpdateResult)
            {
                UpdateResult( true );
            }
            base.AfterLoad(format);
        }

        /// <summary>
        /// 更新域结果
        /// </summary>
        public virtual void UpdateResult( bool fastMode )
        {
            DomDocument document = this.OwnerDocument ;
            DomElementList list = new DomElementList();
            string code = this.Code;
            if (string.Equals(code, WriterConst.FieldCode_Page, StringComparison.CurrentCultureIgnoreCase))
            {
                DomPageInfoElement pi = new DomPageInfoElement();
                pi.OwnerDocument = this.OwnerDocument;
                pi.Parent = this;
                int startIndex = 0;
                if (this.FirstContentElement != null)
                {
                    startIndex = this.FirstContentElement.ViewIndex;
                }
                this.Elements.Clear();
                this.Elements.Add(pi);
                if (fastMode == false)
                {
                    DomDocumentContentElement dce = this.DocumentContentElement;
                    dce.RefreshPrivateContent(startIndex);
                }
            }
            else if (string.Equals(code, WriterConst.FieldCode_NumPages, StringComparison.CurrentCultureIgnoreCase))
            {
                string text = null;
                if (document.Pages == null || document.Pages.Count == 0)
                {
                    text = document.Info.NumOfPage.ToString();
                }
                else
                {
                    text = document.Pages.Count.ToString();
                }
                if (fastMode)
                {
                    this.SetInnerTextFast(text);
                }
                else
                {
                    this.SetEditorTextExt(text, DomAccessFlags.None, true);
                }
            }
            if (fastMode == false)
            {
                this.UpdateContentVersion();
            }
        }
    }
}