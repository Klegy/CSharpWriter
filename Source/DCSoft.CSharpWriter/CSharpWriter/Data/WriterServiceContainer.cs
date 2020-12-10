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
using System.ComponentModel;
using System.ComponentModel.Design;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 服务容器对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class WriterServiceContainer : System.ComponentModel.Design.IServiceContainer 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterServiceContainer()
        {
        }
          
        internal WriterDebugger Debugger
        {
            get
            {
                return (WriterDebugger)this.GetService(typeof(WriterDebugger));
            }
            set
            {
                this.AddService(typeof(WriterDebugger), value);
            }
        }

        private Dictionary<Type, object> _Values = new Dictionary<Type, object>();

        public object GetService(Type serviceType)
        {
            if (_Values.ContainsKey(serviceType))
            {
                return _Values[serviceType];
            }
            else
            {
                return null;
            }
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (callback != null)
            {
                object obj = callback(this, serviceType);
                CheckServiceType(serviceType, obj);
                _Values[serviceType] = obj;
            }
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            AddService(serviceType, callback, true);
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            AddService(serviceType, serviceInstance);
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            CheckServiceType(serviceType, serviceInstance);
            _Values[serviceType] = serviceInstance;
        }

        public void RemoveService(Type serviceType, bool promote)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (_Values.ContainsKey(serviceType))
            {
                _Values.Remove(serviceType);
            }
        }

        public void RemoveService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (_Values.ContainsKey(serviceType))
            {
                _Values.Remove(serviceType);
            }
        }

        private void CheckServiceType(Type serviceType, object instance)
        {
            if (instance != null)
            {
                if (serviceType.IsInstanceOfType(instance) == false)
                {
                    throw new InvalidCastException(
                        instance.GetType().FullName + "!=" + serviceType.FullName);
                }
            }
        }
    }
}
