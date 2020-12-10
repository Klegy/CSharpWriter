/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.Reflection ;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace DCSoft.Script
{
    public class DotNetAssemblyInfoConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(
            ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture,
            object Value, 
            Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                DotNetAssemblyInfo info = ( DotNetAssemblyInfo ) Value;
                System.Reflection.ConstructorInfo ctor = typeof(DotNetAssemblyInfo).GetConstructor(
                    new Type[] {
                        typeof( string ) , 
                        typeof( string ) ,
                        typeof( string ) ,
                        typeof( string ) ,
                        typeof( string ) , 
                        typeof( AssemblyNameFlags ) });
                return new InstanceDescriptor(
                    ctor, 
                    new object[] { 
                        info.Name, 
                        info.VersionString,
                        info.RuntimeVersionString,
                        info.CodeBase, 
                        info.FileName,
                        info.Flags } ,
                        true);
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }
    }
    /// <summary>
    /// .NET assembly information
    /// </summary>
    [Serializable()]
    [System.ComponentModel.TypeConverter( typeof( DotNetAssemblyInfoConverter ))]
    public class DotNetAssemblyInfo : System.ICloneable
    {
        /// <summary>
        /// Returns the version number of a specified file.
        /// </summary>
        /// <param name="szFilename">The path of the file to be examined</param>
        /// <param name="szBuffer">The buffer allocated for the version information that is returned</param>
        /// <param name="cchBuffer">The size, in wide characters, of szBuffer</param>
        /// <param name="dwLength">The size, in bytes, of the returned szBuffer</param>
        /// <returns>Returns the version of the CLR for the file, or empty string if the file is not a .NET assembly. </returns>
        [DllImport("mscoree.dll")]
        private static extern int GetFileVersion(
                        [MarshalAs(UnmanagedType.LPWStr)] string szFilename,
                        [MarshalAs(UnmanagedType.LPWStr)] StringBuilder szBuffer,
                        int cchBuffer,
                        out int dwLength);

        /// <summary>
        /// Returns the .NET runtime version associated with the specified file
        /// </summary>
        /// <param name="fileName">Name of the assembly</param>
        /// <returns>The version of the .NET runtime</returns>
        [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static string GetRuntimeVersion(string fileName)
        {

            const int CCH = 0x400;
            StringBuilder sb = new StringBuilder(CCH);
            int len;
            int hr = GetFileVersion(fileName, sb, sb.Capacity, out len);

            if (hr == unchecked((int)0x8007000B))       //Not a .NET assembly
            {
                return string.Empty;
            }
            else if (hr < 0)                            //Other problem
            {
                throw new System.ComponentModel.Win32Exception(hr);
            }
            else                                        //Proper .NET assembly
            {
                return sb.ToString(0, len - 1);
            }
        }

        /// <summary>
        /// Checks if the specified file is a .NET assembly
        /// </summary>
        /// <param name="path">Path to the assembly</param>
        /// <returns>True, if the specified file is a .NET assembly</returns>
        public static bool IsDotNetAssembly(string path)
        {
            string version = GetRuntimeVersion(path);

            return version != null && version.Trim().Length > 0 ;

        }

        public static bool IsManagedAssemblyByReadFile(string fileName)
        {
            uint peHeader;
            uint peHeaderSignature;
            ushort machine;
            ushort sections;
            uint timestamp;
            uint pSymbolTable;
            uint noOfSymbol;
            ushort optionalHeaderSize;
            ushort characteristics;
            ushort dataDictionaryStart;
            uint[] dataDictionaryRVA = new uint[16];
            uint[] dataDictionarySize = new uint[16];

            using (Stream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);

                //PE Header starts @ 0x3C (60). Its a 4 byte header.
                fs.Position = 0x3C;
                peHeader = reader.ReadUInt32();

                //Moving to PE Header start location...
                fs.Position = peHeader;
                peHeaderSignature = reader.ReadUInt32();

                //We can also show all these value, but we will be       
                //limiting to the CLI header test.
                machine = reader.ReadUInt16();
                sections = reader.ReadUInt16();
                timestamp = reader.ReadUInt32();
                pSymbolTable = reader.ReadUInt32();
                noOfSymbol = reader.ReadUInt32();
                optionalHeaderSize = reader.ReadUInt16();
                characteristics = reader.ReadUInt16();

                // Now we are at the end of the PE Header and from here, the PE Optional Headers starts... To go directly to the datadictionary, we'll increase the stream’s current position to with 96 (0x60). 96 because, 28 for Standard fields 68 for NT-specific fields From here DataDictionary starts...and its of total 128 bytes. DataDictionay has 16 directories in total, doing simple maths 128/16 = 8. So each directory is of 8 bytes. In this 8 bytes, 4 bytes is of RVA and 4 bytes of Size. btw, the 15th directory consist of CLR header! if its 0, its not a CLR file :}
                dataDictionaryStart = Convert.ToUInt16(Convert.ToUInt16(fs.Position) + 0x60);
                fs.Position = dataDictionaryStart;
                for (int i = 0; i < 15; i++)
                {
                    dataDictionaryRVA[i] = reader.ReadUInt32();
                    dataDictionarySize[i] = reader.ReadUInt32();
                }
                fs.Close();
            }
            if (dataDictionaryRVA[14] == 0)
                return false;
            else 
                return true;
        }

        private static string strRuntimePath = null;
        /// <summary>
        /// the directory of clr runtime library
        /// </summary>
        public static string RuntimePath
        {
            get
            {
                if (strRuntimePath == null)
                {
                    strRuntimePath = GetFileNameByCodeBase(typeof(string).Assembly.CodeBase);
                    strRuntimePath = System.IO.Path.GetDirectoryName(strRuntimePath);
                }
                return strRuntimePath;
            }
        }

        public static string GetFileNameByCodeBase(string codeBase)
        {
            if (codeBase != null)
            {
                Uri uri = new Uri(codeBase);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    return uri.LocalPath;
                }
            }
            return null;
        }

        public static DotNetAssemblyInfo CreateByFileName(string FileName)
        {
            DotNetAssemblyInfo info = new DotNetAssemblyInfo(AssemblyName.GetAssemblyName(FileName));
            info.FileName = FileName;
            if (string.Compare(
                                System.IO.Path.GetDirectoryName(FileName),
                                DotNetAssemblyInfo.RuntimePath,
                                true) == 0)
            {
                info.intSourceStyle = AssemblySourceStyle.Standard;
            }
            else
            {
                info.intSourceStyle = AssemblySourceStyle.ThirdPart;
            }
            return info;
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        public DotNetAssemblyInfo()
        {
        }

        public DotNetAssemblyInfo(AssemblyName name)
        {
            strName = name.Name;
            strFullName = name.FullName;
            strCodeBase = name.CodeBase;
            if (strCodeBase != null)
            {
                Uri uri = new Uri(strCodeBase);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    strFileName = uri.LocalPath;
                }
            }
            myVersion = name.Version;
            intFlags = name.Flags;
        }

        public DotNetAssemblyInfo(
            string name,
            string version,
            string runtimeversion,
            string codeBase ,
            string fileName,
            AssemblyNameFlags flags)
        {
            strName = name;
            this.VersionString = version;
            this.RuntimeVersionString = runtimeversion;
            strCodeBase = codeBase;
            strFileName = fileName;
            intFlags = flags;
        }

        public DotNetAssemblyInfo(System.Reflection.Assembly asm)
        {
            strName = asm.GetName().Name;
            strCodeBase = asm.CodeBase;
            Uri uri = new Uri(asm.CodeBase);
            if (uri.Scheme == Uri.UriSchemeFile)
            {
                strFileName = uri.LocalPath;
            }
            else
            {
                strFileName = asm.Location;
            }
            //strFileName = CodeBaseToFileName(asm.CodeBase);
            strFullName = asm.FullName;
            string v = asm.ImageRuntimeVersion;
            if (v.StartsWith("v") || v.StartsWith("V"))
                v = v.Substring(1);
            this.RuntimeVersionString = v;
            intFlags = asm.GetName().Flags;
 
            this.Version = asm.GetName().Version;
 
            
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="name">Assembly name , can be CodeBase or short name</param>
        public DotNetAssemblyInfo(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            name = name.Trim();
            if (name.Length == 0)
            {
                throw new ArgumentNullException("name");
            }

            if (name.IndexOf(":") > 0)
            {
                Uri uri = new Uri(name);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    strFileName = uri.LocalPath;
                }
                else
                {
                    strFileName = uri.AbsoluteUri;
                }
                strName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
            }
            else
            {
                if (name.ToLower().EndsWith(".dll") || name.ToLower().EndsWith(".exe"))
                {
                    strName = System.IO.Path.GetFileNameWithoutExtension(name);
                    strFileName = name;
                }
                else
                {
                    strName = name;
                    strFileName = name + ".dll";
                }
            }
            if (DotNetAssemblyInfoList.IsInRuntimePath(strFileName))
                intSourceStyle = AssemblySourceStyle.Standard;
            else
                intSourceStyle = AssemblySourceStyle.ThirdPart;
        }


        public static string CodeBaseToFileName(string codeBase)
        {
            if (codeBase == null)
                return null;
            if (codeBase.IndexOf(":") > 0)
            {
                Uri uri = new Uri(codeBase);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    return uri.LocalPath;
                }
                else
                {
                    return uri.AbsoluteUri;
                }
            }
            return codeBase;
        }


        private string strName = null;

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        private string strFullName = null;

        public string FullName
        {
            get { return strFullName; }
            set { strFullName = value; }
        }

        private string strCodeBase = null;

        public string CodeBase
        {
            get { return strCodeBase; }
            set { strCodeBase = value; }
        }

        private string strFileName = null;

        public string FileName
        {
            get 
            {
                if (strFileName == null)
                {
                    strFileName = strName;
                    if (strFileName.ToLower().EndsWith(".dll") == false )
                    {
                        strFileName = strFileName + ".dll";
                    }
                    if (intSourceStyle == AssemblySourceStyle.Standard)
                    {
                        strFileName = System.IO.Path.Combine(DotNetAssemblyInfo.RuntimePath, strFileName);
                    }
                }
                return strFileName; 
            }
            set
            {
                strFileName = value; 
            }
        }

        private Version myVersion = new Version();
        /// <summary>
        /// assembly version
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Version Version
        {
            get { return myVersion; }
            set { myVersion = value; }
        }

        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement()]
        public string VersionString
        {
            get
            {
                return myVersion.ToString();
            }
            set
            {
                if (value != null && value.Trim().Length > 0)
                {
                    myVersion = new Version(value);
                }
                else
                {
                    myVersion = new Version();
                }
            }
        }

        private Version myRuntimeVersion = new Version();
        /// <summary>
        /// clr runtime version
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Version RuntimeVersion
        {
            get
            {
                return myRuntimeVersion; 
            }
            set
            {
                myRuntimeVersion = value; 
            }
        }

        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement()]
        public string RuntimeVersionString
        {
            get
            {
                return myRuntimeVersion.ToString(); 
            }
            set
            {
                if (value != null && value.Trim().Length > 0)
                {
                    myRuntimeVersion = new Version(value);
                }
                else
                {
                    myRuntimeVersion = new Version();
                }
            }
        }

        private AssemblyNameFlags intFlags = AssemblyNameFlags.None;

        public AssemblyNameFlags Flags
        {
            get { return intFlags; }
            set { intFlags = value; }
        }
        private AssemblySourceStyle intSourceStyle = AssemblySourceStyle.Custom;
        /// <summary>
        /// assembly source style
        /// </summary>
        public AssemblySourceStyle SourceStyle
        {
            get 
            {
                return intSourceStyle; 
            }
            set
            {
                intSourceStyle = value; 
            }
        }
        /// <summary>
        /// clone instance
        /// </summary>
        /// <returns>new instance</returns>
        object System.ICloneable.Clone()
        {
            DotNetAssemblyInfo info = new DotNetAssemblyInfo();
            info.strName = this.strName;
            info.strFileName = this.strFileName;
            info.strCodeBase = this.strCodeBase;
            info.strFullName = this.strFullName;
            info.myVersion = ( Version ) this.myVersion.Clone();
            info.intSourceStyle = this.intSourceStyle;
            info.intFlags = this.intFlags;
            return info;
        }

        /// <summary>
        /// clone instance
        /// </summary>
        /// <returns>new instance</returns>
        public DotNetAssemblyInfo Clone()
        {
            DotNetAssemblyInfo info = new DotNetAssemblyInfo();
            info.strName = this.strName;
            info.strFileName = this.strFileName;
            info.strCodeBase = this.strCodeBase;
            info.strFullName = this.strFullName;
            info.myVersion = (Version)this.myVersion.Clone();
            info.intSourceStyle = this.intSourceStyle;
            info.intFlags = this.intFlags;
            return info;
        }
    }
    
    public enum AssemblySourceStyle
    {
        /// <summary>
        /// .NET framework standard assembly
        /// </summary>
        Standard ,
        /// <summary>
        /// The third part assembly
        /// </summary>
        ThirdPart ,
        /// <summary>
        /// owner assembly
        /// </summary>
        Custom ,
    }

    [Serializable()]
    public class DotNetAssemblyInfoList : System.Collections.CollectionBase , System.ICloneable
    {
#if DEBUG
        static internal void Test()
        {
            foreach ( DotNetAssemblyInfo info in DotNetAssemblyInfoList.GlobalList )
            {
                System.Console.WriteLine(info.Name
                    + " " + info.Version 
                    + " " + info.FileName );
            }
            System.Console.ReadLine();
        }
#endif

        private static DotNetAssemblyInfoList myGlobalList = null;

        /// <summary>
        /// refrsh global assembly list
        /// </summary>
        public static void RefreshGlobalList()
        {
            myGlobalList = null;
        }

        /// <summary>
        /// global assembly list in host system
        /// </summary>
        public static DotNetAssemblyInfoList GlobalList
        {
            get
            {
                if (myGlobalList == null)
                {
                    lock ( typeof( DotNetAssemblyInfoList ) )
                    {
                        DotNetAssemblyInfoList tempList = new DotNetAssemblyInfoList();
                        System.Collections.ArrayList myFiles = new System.Collections.ArrayList();
                        GetAssemblyNames(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders", myFiles);
                        Version rv = System.Environment.Version;
                        GetAssemblyNames(@"SOFTWARE\Microsoft\.NETFramework\v"
                            + rv.Major + "." + rv.Minor + "." + rv.Build
                            + "\\AssemblyFoldersEx", myFiles);
                        GetAssmeblyNamesByPath(DotNetAssemblyInfo.RuntimePath, myFiles);
                        for (int iCount = 0; iCount < myFiles.Count; iCount++)
                        {
                            string strFileName = (string)myFiles[iCount];

                            if (strFileName.ToLower().EndsWith("system.enterpriseservices.wrapper.dll"))
                            {
                                continue;
                            }
                            try
                            {
                                //if (strFileName.ToLower().EndsWith("mscorlib.dll"))
                                //{
                                //    continue;
                                //}
                                string rv2 = DotNetAssemblyInfo.GetRuntimeVersion(strFileName);
                                if (rv2 == null || rv2.Trim().Length == 0)
                                {
                                    continue;
                                }
                                rv2 = rv2.Trim().ToUpper();
                                if (rv2.StartsWith("V"))
                                    rv2 = rv2.Substring(1);
                                AssemblyName name = AssemblyName.GetAssemblyName(strFileName);
                                DotNetAssemblyInfo info = new DotNetAssemblyInfo(name);
                                info.FileName = strFileName;
                                info.RuntimeVersion = new Version(rv2);
                                if ( IsInRuntimePath( strFileName ) )
                                {
                                    info.SourceStyle = AssemblySourceStyle.Standard;
                                }
                                else
                                {
                                    info.SourceStyle = AssemblySourceStyle.ThirdPart;
                                }
                                tempList.Add(info);
                            }
                            catch (BadImageFormatException)
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine("Analyse " + strFileName + " error.");
#endif
                            }
                            catch (Exception ext)
                            {
                                System.Diagnostics.Debug.WriteLine(strFileName + ":" + ext.Message);
                            }
                        }
                        tempList.InnerList.Sort(new AssemblyInfoComparer());
                        myGlobalList = tempList;
                    }//lock
                }
                return myGlobalList;
            }
        }

        public static bool IsInRuntimePath(string fileName)
        {
            if (System.IO.Path.IsPathRooted(fileName))
            {
                fileName = fileName.Trim().ToLower();
                string path = DotNetAssemblyInfo.RuntimePath.ToLower();
                return fileName.StartsWith(path);
            }
            return false;
        }       
         
        private class AssemblyInfoComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return string.Compare(((DotNetAssemblyInfo)x).Name, ((DotNetAssemblyInfo)y).Name, true);
            }
        }

        private static void GetAssemblyNames(
            string keyPath,
            System.Collections.ArrayList myFiles)
        {
            Microsoft.Win32.RegistryKey key =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath);
            if (key != null)
            {
                foreach (string subName in key.GetSubKeyNames())
                {
                    Microsoft.Win32.RegistryKey subKey = key.OpenSubKey(subName);
                    string path = Convert.ToString(subKey.GetValue(null));
                    GetAssmeblyNamesByPath(path, myFiles);
                    subKey.Close();
                }//foreach
                key.Close();
            }
        }

        private static void GetAssmeblyNamesByPath(
            string path, 
            System.Collections.ArrayList myFiles)
        {
            if (System.IO.Directory.Exists(path))
            {
                foreach (string fileName in 
                    System.IO.Directory.GetFiles(path, "*.dll"))
                {
                    bool find = false;
                    foreach (string file in myFiles)
                    {
                        if (string.Compare(file, fileName, true) == 0)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        myFiles.Add(fileName);
                    }
                }
            }//if
        }
        /// <summary>
        /// get assembly information specify index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>object</returns>
        public DotNetAssemblyInfo this[int index]
        {
            get
            {
                return (DotNetAssemblyInfo)this.List[index];
            }
        }
        /// <summary>
        /// get assembly information specify name
        /// </summary>
        /// <param name="name">assembly name</param>
        /// <returns>object</returns>
        public DotNetAssemblyInfo this[string name]
        {
            get
            {
                foreach (DotNetAssemblyInfo info in this)
                {
                    if (string.Compare(info.Name, name, true) == 0)
                        return info;
                }
                return null;
            }
        }

        public string[] FileNames
        {
            get
            {
                string[] result = new string[this.Count];
                for (int iCount = 0; iCount < this.Count; iCount++)
                {
                    result[iCount] = ((DotNetAssemblyInfo)this.List[iCount]).FileName;
                }
                return result;
            }
        }
            //public DotNetAssemblyInfo Search(string name)
            //{
            //    if (name == null)
            //        return null;
            //    name = name.Trim();
            //    if (name.Length == 0)
            //        return null;
            //    DotNetAssemblyInfo info = this[name];
            //    if (info != null)
            //        return info;
            //    if (name.ToLower().EndsWith(".dll"))
            //    {

            //    }
            //}

        /// <summary>
        /// add new item
        /// </summary>
        /// <param name="info">new item</param>
        /// <returns>index of new item in this list</returns>
        public int Add(DotNetAssemblyInfo info)
        {
            int index = IndexOf(info.Name);
            if (index >= 0)
            {
                this.List.RemoveAt(index);
            }
            return this.List.Add(info);
        }

        public int AddStandard(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            name = name.Trim();
            if (name.Length == 0)
                throw new ArgumentNullException("name");
            DotNetAssemblyInfo info = new DotNetAssemblyInfo();
            info.Name = name;
            info.SourceStyle = AssemblySourceStyle.Standard;
            if (name.ToLower().EndsWith(".dll") == false)
                name = name + ".dll";
            info.FileName = System.IO.Path.Combine(DotNetAssemblyInfo.RuntimePath, name);
            return this.Add(info);
        }

        public int AddByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            name = name.Trim();
            if (name.Length == 0)
                throw new ArgumentNullException("name");

            DotNetAssemblyInfo info = new DotNetAssemblyInfo(name);
            if (myGlobalList != null)
            {
                DotNetAssemblyInfo info2 = GlobalList[info.Name];
                if (info2 != null)
                    info = info2;
            }
            return Add(info);
        }

        public int AddByType(Type t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }
            DotNetAssemblyInfo info = new DotNetAssemblyInfo(t.Assembly);
            int result = this.Add(info);
            Type[] its = t.GetInterfaces();
            if (its != null && its.Length > 0)
            {
                foreach (Type it in its)
                {
                    DotNetAssemblyInfo info2 = new DotNetAssemblyInfo(it.Assembly);
                    this.Add(info2);
                }
            }
            Type st = t.BaseType;
            while (st != null)
            {
                DotNetAssemblyInfo info2 = new DotNetAssemblyInfo(st.Assembly);
                this.Add(info2);
                st = st.BaseType;
            }
            return result;
        }

        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="info">item</param>
        public void Remove(DotNetAssemblyInfo info)
        {
            this.List.Remove(info);
        }

        public int IndexOf(string name)
        {
            for (int iCount = 0; iCount < this.Count; iCount++)
            {
                DotNetAssemblyInfo info = (DotNetAssemblyInfo)this.List[iCount];
                if (string.Compare(info.Name, name, true) == 0)
                {
                    return iCount;
                }
            }
            return -1;
        }
        /// <summary>
        /// clone instance
        /// </summary>
        /// <returns>instance</returns>
        object System.ICloneable.Clone()
        {
            DotNetAssemblyInfoList list = new DotNetAssemblyInfoList();
            list.InnerList.AddRange(this);
            return list;
        }
        /// <summary>
        /// clone instance
        /// </summary>
        /// <returns>instance</returns>
        public DotNetAssemblyInfoList Clone()
        {
            return (DotNetAssemblyInfoList)((ICloneable)this).Clone();
        }
    }
}
