/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using DCSoft.CSharpWriter.RTF;
using DCSoft.CSharpWriter.Dom;

using DCSoft.CSharpWriter.Xml;

namespace DCSoft.CSharpWriter
{
    public class DocumentSaver
    {
        
        /// <summary>
        /// 获得文档对象的XML序列化/反序列化对象
        /// </summary>
        /// <returns></returns>
        public static XmlSerializer GetDocumentXmlSerializer( Type documentType )
        {
            return MyXmlSerializeHelper.GetDocumentXmlSerializer(documentType);
        }

        #region 输出XML文档 **********************************************************

        public static void SaveXmlFile(string fileName , DomDocument document )
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            using (System.IO.FileStream stream = new System.IO.FileStream(
                fileName,
                System.IO.FileMode.Create,
                System.IO.FileAccess.Write))
            {
                SaveXmlFile(stream , document);
            }
        }

        public static void SaveXmlFile(System.IO.Stream stream , DomDocument document )
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            XmlSerializer ser = GetDocumentXmlSerializer(document.GetType());
            //document._xmlSerializing = true;
            //document.ContentStyles.UpdateStyleIndex();
            document.EditorVersion = DomDocument.CurrentEditorVersion;
            document._ElementsForSerialize = null;
            document._Deserializing = false;
            document.DeleteUselessStyle();
            document.ContentStyles.Styles.UpdateStyleIndex();
            ser.Serialize(stream, document);
            ClearElementsForSerialize(document);
            //document._xmlSerializing = false;
        }


        public static void SaveXmlFile(System.IO.TextWriter writer, DomDocument document)
        {
            if ( writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            XmlSerializer ser = GetDocumentXmlSerializer(document.GetType());
            //document._xmlSerializing = true;
            //document.ContentStyles.UpdateStyleIndex();
            document.EditorVersion  = DomDocument.CurrentEditorVersion;
            document.DeleteUselessStyle();
            document._ElementsForSerialize = null;
            document._Deserializing = false;
            document.ContentStyles.Styles.UpdateStyleIndex();
            document.FixDomState();
            //ClearElementsForSerialize(document);
            ser.Serialize(writer, document);
            document.FixDomState();
            //ClearElementsForSerialize(document);
            //document._xmlSerializing = false;
        }


        private static void ClearElementsForSerialize(DomContainerElement root)
        {
            root._ElementsForSerialize = null;
            foreach (DomElement element in root.Elements)
            {
                if (element is DomContainerElement)
                {
                    ClearElementsForSerialize((DomContainerElement)element);
                }
            }
        }

        #endregion

        #region 输出RTF文档 *************************************************************

        public static void SaveRTFFile(string fileName, DomDocument document)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            using (StreamWriter writer = new StreamWriter(
                fileName,
                false ,
                Encoding.Default ))
            {
                RTFContentWriter RTFwriter = new RTFContentWriter( writer );
                RTFwriter.Document = document;
                RTFwriter.IncludeSelectionOnly = false;
                RTFwriter.WriteAllDocument();
                writer.Close();
            }
        }

        public static void SaveRTFFile(
            Stream stream,
            DomDocument document)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            using (StreamWriter writer = new StreamWriter(
                stream,
                Encoding.Default ))
            {
                RTFContentWriter RTFwriter = new RTFContentWriter( writer );
                RTFwriter.Document = document;
                RTFwriter.IncludeSelectionOnly = false;
                RTFwriter.WriteAllDocument();
                writer.Close();
            }
        }

        public static void SaveRTFFile(
            TextWriter writer,
            DomDocument document)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            RTFContentWriter RTFwriter = new RTFContentWriter(writer);
            RTFwriter.Document = document;
            RTFwriter.IncludeSelectionOnly = false;
            RTFwriter.WriteAllDocument();
            writer.Close();
        }

        #endregion
         

        #region 输出纯文本文档 *************************************************

        /// <summary>
        /// 保存纯文本文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="document">文档对象</param>
        public static void SaveTextFile(string fileName, DomDocument document)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            using (StreamWriter writer = new StreamWriter(
               fileName,
               false ,
               Encoding.Default ))
            {
                writer.Write(document.Text);
            }
        }

        /// <summary>
        /// 保存纯文本文件
        /// </summary>
        /// <param name="writer">文本书写器</param>
        /// <param name="document">文档对象</param>
        public static void SaveTextFile(System.IO.TextWriter writer , DomDocument document)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            writer.Write(document.Text);
        }

        /// <summary>
        /// 使用系统默认的编码格式保存纯文本文件
        /// </summary>
        /// <param name="stream">数据输出流</param>
        /// <param name="document">文档对象</param>
        public static void SaveTextFile(
            Stream stream,
            DomDocument document)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
            using (StreamWriter writer = new StreamWriter(
                stream,
                Encoding.Default ))
            {
                writer.Write(document.Text);
            }
        }

        #endregion

    }
}
