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
using DCSoft.CSharpWriter.Dom ;
using System.Xml.Serialization;
 
using System.Xml ;
using DCSoft.CSharpWriter.Security;
using System.Drawing;

namespace DCSoft.CSharpWriter.Xml
{
    internal class MyXmlSerializeHelper
    {
        static MyXmlSerializeHelper()
        {
            // 初始化标准的文档XML序列化器
            SetDocumentElementTypes( typeof( DomDocument ) ,new Type[]{
                    typeof( DomCharElement ),
                    typeof( DomParagraphElement ),
                    typeof( DomParagraphFlagElement ),
                     
                    typeof( DomLineBreakElement ),
                    
                    typeof( DomStringElement ),
                    typeof( XTextDocumentHeaderElement ),
                    typeof( XTextDocumentBodyElement ),
                    typeof( XTextDocumentFooterElement ),
                   
                    typeof( DomPageBreakElement)
                     
                }); 
        }

        private static Dictionary<Type, Type[]> _documentElementTypes
            = new Dictionary<Type, Type[]>();

        /// <summary>
        /// 设置文档支持的文档元素类型
        /// </summary>
        /// <param name="documentType">文档类型</param>
        /// <param name="elementTypes">支持的文档元素类型</param>
        public static void SetDocumentElementTypes(Type documentType, Type[] elementTypes)
        {
            // 检查参数
            if (documentType == null)
            {
                throw new ArgumentNullException("documentType");
            }
            if( documentType.Equals( typeof( DomDocument ) ) == false 
                && documentType.IsSubclassOf( typeof( DomDocument )) == false )
            {
                throw new ArgumentException( documentType.FullName + "<>" + typeof( DomDocument ).FullName );
            }

            if (elementTypes == null)
            {
                throw new ArgumentNullException("elementTypes");
            }
            foreach (Type t in elementTypes)
            {
                if (t.IsSubclassOf(typeof(DomElement)) == false)
                {
                    throw new ArgumentException(t.FullName + "<>" + typeof(DomElement).FullName);
                }
            }
            // 设置映射表
            _documentElementTypes[documentType] = elementTypes;
            if (_xmlSerializers.ContainsKey(documentType))
            {
                // 删除旧的XML序列器
                _xmlSerializers.Remove(documentType);
            }
        }

        private static Dictionary<Type, XmlSerializer> _xmlSerializers 
            = new Dictionary<Type, XmlSerializer>();
        /// <summary>
        /// 获得文档对象的XML序列化/反序列化对象
        /// </summary>
        /// <returns></returns>
        public static XmlSerializer GetDocumentXmlSerializer( Type documentType )
        {
            if (_xmlSerializers.ContainsKey(documentType))
            {
                return _xmlSerializers[documentType];
            }
            else
            {
                XmlSerializer ser = null;
                if (_documentElementTypes.ContainsKey(documentType))
                {
                    ser = new XmlSerializer(documentType, _documentElementTypes[documentType]);
                }
                else
                {
                    ser = new XmlSerializer(documentType);
                }
                _xmlSerializers[documentType] = ser;
                return ser;
            }
        }

        private static Dictionary<Type, XmlSerializer> _elementXmlSerializers
            = new Dictionary<Type, XmlSerializer>();
        /// <summary>
        /// 获得指定类型的文档元素类型的XML序列化器
        /// </summary>
        /// <param name="elementType">文档元素类型</param>
        /// <returns>获得的XML序列化器</returns>
        public static XmlSerializer GetElementXmlSerializer(Type elementType)
        {
            if (elementType == null)
            {
                throw new ArgumentNullException("elementType");
            }
            if (_elementXmlSerializers.ContainsKey(elementType))
            {
                return _elementXmlSerializers[elementType];
            }
            else
            {
                XmlSerializer ser = new XmlSerializer(elementType);
                _elementXmlSerializers[elementType] = ser;
                return ser;
            }
        }
         
    }
}
