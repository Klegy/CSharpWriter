/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Controls ;

namespace DCSoft.CSharpWriter.Commands
{
	/// <summary>
	/// 编辑器动作基础类型
	/// </summary>
	public abstract class WriterCommand
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public WriterCommand()
		{
		}

		/// <summary>
		/// 动作名称
		/// </summary>
		protected string strName = null;
		/// <summary>
		/// 动作名称
		/// </summary>
		public string Name
		{
			get{ return strName ;}
            set { strName = value; }
		}


        private System.Windows.Forms.Keys _ShortcutKey = System.Windows.Forms.Keys.None;
        /// <summary>
        /// 快捷键
        /// </summary>
        public System.Windows.Forms.Keys ShortcutKey
        {
            get { return _ShortcutKey; }
            set { _ShortcutKey = value; }
        }


        private string _Description = null;
        /// <summary>
        /// 动作说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


		public virtual void Invoke( WriterCommandEventArgs args )
		{

		}


        /// <summary>
        /// 动作对象在工具条上的图标图片对象
        /// </summary>
        protected System.Drawing.Image myToolbarImage = null;
        /// <summary>
        /// 动作对象在工具条上的图标图片对象
        /// </summary>
        public virtual System.Drawing.Image ToolbarImage
        {
            get
            {
                if (myToolbarImage == null)
                {
                    System.Drawing.ToolboxBitmapAttribute attr = Attribute.GetCustomAttribute(
                        this.GetType(),
                        typeof(System.Drawing.ToolboxBitmapAttribute),
                        false)
                        as System.Drawing.ToolboxBitmapAttribute;
                    if (attr != null)
                    {
                        System.Drawing.Image img = attr.GetImage(this.GetType());
                        if (img == null)
                        {
                            throw new Exception("Miss image resource " + this.GetType().FullName);
                            //throw new Exception(string.Format(
                            //    ActionStrings.ActionMissImageResource_Name , 
                            //    this.GetType().FullName ));
                        }
                        if (img is System.Drawing.Bitmap)
                        {
                            System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)img;
                            bmp.MakeTransparent(bmp.GetPixel(0, bmp.Height - 1));
                            //((System.Drawing.Bitmap)img).MakeTransparent();
                        }
                        myToolbarImage = img;
                    }
                }
                return myToolbarImage;
            }
            set
            {
                myToolbarImage = value;
            }
        }

        public override string ToString()
        {
            return "Command:" + this.Name;
        }
	}
}