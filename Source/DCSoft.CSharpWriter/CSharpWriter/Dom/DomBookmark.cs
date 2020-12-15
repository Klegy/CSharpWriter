using System;



namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 书签对象
	/// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XBookMark")]
	public class DomBookmark : DomElement
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomBookmark()
		{
		}

		private string strName = null;
		/// <summary>
		/// 对象名称
		/// </summary>
		public string Name
		{
			get{ return strName ;}
			set{ strName = value;}
		}

        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseMove)
            {
                args.Cursor = System.Windows.Forms.Cursors.Arrow;
                args.Tooltip = "Bookmark \"" + strName + "\"";
            }
            base.HandleDocumentEvent(args);
        }

        //public override void OnMouseMove(DocumentEventArgs args)
        //{
        //    args.Cursor = System.Windows.Forms.Cursors.Arrow ;
        //    args.Tooltip = "Bookmark \"" + strName + "\"" ;
        //}

		/// <summary>
		/// 激活书签
		/// </summary>
		public void Active()
		{
			System.Drawing.RectangleF bounds = this.AbsBounds ;
			if( this.OwnerDocument.EditorControl != null )
			{
				this.OwnerDocument.Content.AutoClearSelection = true ;
				this.OwnerDocument.Content.MoveSelectStart( this );
				this.OwnerDocument.EditorControl.ScrollToView(
					(int )bounds.Left ,
                    (int)bounds.Top,
                    (int)bounds.Width,
                    (int)bounds.Height,
                    DCSoft.WinForms.ScrollToViewStyle.Middle );
			}
		}

		/// <summary>
		/// 输出对象数据到HTML文档中
		/// </summary>
		/// <param name="writer">HTML文档书写器</param>
        public override void WriteHTML(DCSoft.CSharpWriter.Html.WriterHtmlDocumentWriter writer)
		{
			writer.WriteStartElement("a");
			writer.WriteAttributeString("name" , this.Name );
			writer.WriteEndElement();
		}

        public override void WriteRTF( DCSoft.CSharpWriter.RTF.RTFContentWriter writer)
        {
            writer.WriteStartBookmark(this.Name);
            writer.WriteEndBookmark(this.Name);
        }
	}
}