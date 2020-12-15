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
using DCSoft.Common;
using System.Xml.Serialization;
using System.ComponentModel;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 文档样式容器
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable()]
    public class ContentStyleContainer : System.ICloneable , IDisposable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ContentStyleContainer()
        {
            _Default = CreateStyleInstance();
        }

        private ContentStyle _Default = null;
        /// <summary>
        /// 默认样式
        /// </summary>
        public virtual ContentStyle Default
        {
            get
            {
                return _Default;
            }
            set
            {
                _Default = value;
                this.ClearRuntimeStyleList();
            }
        }

        private ContentStyleList _Styles = new ContentStyleList();
        /// <summary>
        /// 样式列表
        /// </summary>
        /// <remarks>在此声明本属性不能进行XML序列化，为得是能在应用中重载本属性，然后添加
        /// 扩展的样式成员类型，这样就不会造成由于样式对象重载而导致的XML序列化的不方便。郁闷啊。</remarks>
        [System.Xml.Serialization.XmlIgnore]
        public virtual ContentStyleList Styles
        {
            get
            {
                return _Styles;
            }
            set
            {
                _Styles = value;
                this.ClearRuntimeStyleList();
            }
        }

        //internal void UpdateStyleIndex()
        //{
        //    int icount = 0;
        //    //foreach (DocumentContentStyle style in this.Styles)
        //    //{
        //    //    style.Index = icount++;
        //    //}
        //}

        public ContentStyle GetStyle(int styleIndex)
        {
            if (styleIndex < 0
                || styleIndex >= this.Styles.Count)
            {
                return _Default;
            }
            else
            {
                return this.Styles[styleIndex];
            }
        }

        public int GetStyleIndex(ContentStyle style)
        {
            if (style == null
                || style == _Default
                || _Default.EqualsStyleValue(style))
            {
                return -1;
            }
            else
            {
                int index = this.Styles.IndexOfExt(style);
                if (index < 0)
                {
                    if (style.RemoveSameStyle(this.Default) > 0)
                    {
                        index = this.Styles.IndexOfExt(style);
                        if (index < 0)
                        {
                            index = this.Styles.Add(style);
                        }
                        ////style.Index = index;
                        //if ( this.Document != null && this.Document.CanLogUndo)
                        //{
                        //    this.Document.UndoList.Add(
                        //        new XTextUndoModifyArrayItem(
                        //            XTextUndoModifyListAction.Insert,
                        //            this.Styles,
                        //            index,
                        //            null,
                        //            style));
                        //}

                        ClearRuntimeStyleList();
                    }
                    else
                    {
                        return -1;
                    }
                }
                return index;
            }
        }

        public virtual ContentStyle CreateStyleInstance()
        {
            return new ContentStyle();
        }

        public void Clear()
        {
            this._Default = this.CreateStyleInstance();
            //this._Current = (DocumentContentStyle)this._Default.Clone();
            this._Styles.Clear();
        }

        [NonSerialized()]
        private Dictionary<ContentStyle, ContentStyle> runtimeStyles = null;

        public void ClearRuntimeStyleList()
        {
            runtimeStyles = null;
        }

        /// <summary>
        /// 获得运行时的样式
        /// </summary>
        /// <param name="styleIndex"></param>
        /// <returns></returns>
        public ContentStyle GetRuntimeStyle(int styleIndex)
        {
            ContentStyle style = GetStyle(styleIndex);
            if (style == _Default)
            {
                return style;
            }
            else
            {
                if (runtimeStyles == null)
                {
                    runtimeStyles =
                        new Dictionary<ContentStyle, ContentStyle>();
                }
                if (runtimeStyles.ContainsKey(style))
                {
                    return runtimeStyles[style];
                }
                else
                {
                    ContentStyle rs = (ContentStyle)style.Clone();
                    //rs.Merge(this.Default);
                    XDependencyObject.MergeValues(this.Default, rs, false);
                    if (string.IsNullOrEmpty(rs.DefaultValuePropertyNames) == false)
                    {
                        foreach (string name in rs.DefaultValuePropertyNames.Split(','))
                        {
                            XDependencyObject.RemoveProperty(rs, name);
                        }//foreach
                    }
                    runtimeStyles[style] = rs;
                    return rs;
                }
            }
        }

        ///// <summary>
        ///// 更新所有的样式对象的内部状态
        ///// </summary>
        ///// <param name="g"></param>
        //public void UpdateState(System.Drawing.Graphics g)
        //{
        //    if (g == null)
        //    {
        //        throw new ArgumentNullException("g");
        //    }
        //    if (this.Default != null)
        //    {
        //        this.Default.UpdateState(g);
        //    }
        //    //if (this. != null)
        //    //{
        //    //    this.Current.UpdateState(g);
        //    //}
        //    foreach (DocumentContentStyle style in this.Styles)
        //    {
        //        style.UpdateState(g);
        //    }
        //    this.RefreshRuntimeStyleList();
        //}


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            ContentStyleContainer c = (ContentStyleContainer)this.MemberwiseClone();
            c._Default = (ContentStyle)this._Default.Clone();
            c._Styles = new ContentStyleList();
            foreach (ContentStyle item in this._Styles)
            {
                c._Styles.Add((ContentStyle)item.Clone());
            }
            c.runtimeStyles = new Dictionary<ContentStyle, ContentStyle>();
            return c;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ContentStyleContainer Clone()
        {
            return (ContentStyleContainer)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public virtual void Dispose()
        {
            if (this._Default != null)
            {
                this._Default.Dispose();
                this._Default = null;
            }
            if (this._Styles != null)
            {
                foreach (ContentStyle style in this._Styles)
                {
                    style.Dispose();
                }
                this._Styles = null;
            }
            if (this.runtimeStyles != null)
            {
                foreach (ContentStyle style in this.runtimeStyles.Values )
                {
                    style.Dispose();
                }
                this.runtimeStyles = null;
            }
        }
    }
}
