/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.ComponentModel;

namespace DCSoft.Printing
{
    /// <summary>
    /// 打印机状态
    /// </summary>
    [Flags()]
    public enum PrinterStatus
    {
        None = 0,
        PAUSED = 0x00000001,
        ERROR = 0x00000002,
        PENDING_DELETION = 0x00000004,
        PAPER_JAM = 0x00000008,
        PAPER_OUT = 0x00000010,
        MANUAL_FEED = 0x00000020,
        PAPER_PROBLEM = 0x00000040,
        OFFLINE = 0x00000080,
        IO_ACTIVE = 0x00000100,
        BUSY = 0x00000200,
        PRINTING = 0x00000400,
        OUTPUT_BIN_FULL = 0x00000800,
        NOT_AVAILABLE = 0x00001000,
        WAITING = 0x00002000,
        PROCESSING = 0x00004000,
        INITIALIZING = 0x00008000,
        WARMING_UP = 0x00010000,
        TONER_LOW = 0x00020000,
        NO_TONER = 0x00040000,
        PAGE_PUNT = 0x00080000,
        USER_INTERVENTION = 0x00100000,
        OUT_OF_MEMORY = 0x00200000,
        DOOR_OPEN = 0x00400000,
        SERVER_UNKNOWN = 0x00800000,
        POWER_SAVE = 0x01000000
    }
    /// <summary>
    /// 打印机信息对象
    /// </summary>
    public class PrinterInformation
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public PrinterInformation()
        {
        }

        /// <summary>
        /// 初始化对象，获得指定打印机的所有的打印任务列表
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        public PrinterInformation(string printerName)
        {
            strPrinterName = printerName;
            Refresh();
        }

        private string strPrinterName = null;
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrinterName
        {
            get { return strPrinterName; }
            set { strPrinterName = value; }
        }


        private PRINTER_INFO_2 myInfo = new PRINTER_INFO_2();

        /// <summary>
        /// 打印机状态
        /// </summary>
        public PrinterStatus Status
        {
            get { return ( PrinterStatus) myInfo.Status ; }
            //set { intStatus = value; }
        }

        public string ServerName
        {
            get { return myInfo.pServerName ; }
        }
        public string ShareName
        {
            get { return myInfo.pShareName ; }
        }

        private PrintJobList myJobs = new PrintJobList() ;
        /// <summary>
        /// 打印任务列表
        /// </summary>
        public PrintJobList Jobs
        {
            get
            {
                return myJobs; 
            }
        }

        /// <summary>
        /// 刷新对象数据
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        public void Refresh()
        {
            myJobs = new PrintJobList();

            int printer = 0;
            Win32Exception ext = null;

            int result = OpenPrinterA(strPrinterName, ref printer, 0);

            if (result != 0)
            {
                int bufferSize = 0;
                int recordNumber = 0;

                GetPrinter(printer, 2, IntPtr.Zero, 0, ref bufferSize);
                if (bufferSize > 0)
                {
                    IntPtr buf = Marshal.AllocHGlobal(bufferSize);
                    if (GetPrinter(printer, 2, buf, bufferSize, ref bufferSize) != 0)
                    {
                        this.myInfo = (PRINTER_INFO_2)Marshal.PtrToStructure(buf, typeof(PRINTER_INFO_2));
                    }
                    Marshal.FreeHGlobal(buf);
                }

                int structureSize = Marshal.SizeOf(typeof(JOB_INFO_1));
                EnumJobsA(printer, 0, 10000, 1, IntPtr.Zero, 0, ref bufferSize, ref recordNumber);
                if (bufferSize > 0)
                {

                    IntPtr p2 = Marshal.AllocHGlobal(bufferSize);
                    if (EnumJobsA(printer, 0, 1000, 1, p2, bufferSize, ref bufferSize, ref recordNumber) != 0)
                    {
                        for (int iCount = 0; iCount < recordNumber; iCount++)
                        {
                            IntPtr p3 = new IntPtr((long)p2 + iCount * structureSize);
                            PrintJob job = new PrintJob();
                            UpdatePrintJob(job, p3);
                            job.PrinterName = strPrinterName;
                            this.Jobs.Add(job);
                        }
                    }
                    else
                    {
                        Marshal.FreeHGlobal(p2);
                        ClosePrinter(printer);
                        ext = new Win32Exception(Marshal.GetLastWin32Error());
                        throw ext;
                    }
                    Marshal.FreeHGlobal(p2);
                }
                ClosePrinter(printer);
            }
            else
            {
                ext = new Win32Exception(Marshal.GetLastWin32Error());
                throw ext;
            }
        }

        /// <summary>
        /// 控制打印任务
        /// </summary>
        /// <param name="job"></param>
        /// <param name="command"></param>
        /// <param name="throwException"></param>
        /// <returns></returns>
        internal static bool ControlJob(PrintJob job, PrintJobControlCommand command , bool throwException )
        {
            if (job == null)
                throw new ArgumentNullException("job");
            int printer = 0;
            Win32Exception ext = null;
            if (OpenPrinterA(job.PrinterName, ref printer, 0) != 0)
            {
                bool result = SetJob(printer, job.JobId, 0, IntPtr.Zero, ( int )  command);
                if (UpdatePrintJob(printer, job, throwException) == false)
                    result = false;
                ClosePrinter(printer);
                return result;
            }
            else
            {
                ext = new Win32Exception(Marshal.GetLastWin32Error());
                if (throwException)
                    throw ext;
                else
                    return false;
            }
        }


        internal static bool RefreshPrintJob(PrintJob job, bool throwException)
        {
            int printer = 0;
            Win32Exception ext = null;
            if (OpenPrinterA(job.PrinterName, ref printer, 0) != 0)
            {
                return UpdatePrintJob(printer, job, throwException);
            }
            else
            {
                if (throwException)
                {
                    ext = new Win32Exception(Marshal.GetLastWin32Error());
                    throw ext;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool UpdatePrintJob(int printer, PrintJob job , bool throwException )
        {
            Win32Exception ext = null;
            int size = 0;
            GetJobA(printer, job.JobId, 1, IntPtr.Zero, 0, ref size);
            bool result = false;
            if (size > 0)
            {
                IntPtr p = Marshal.AllocHGlobal(size);
                if (GetJobA(printer, job.JobId, 1, p, size, ref size) != 0)
                {
                    UpdatePrintJob(job, p);
                    Marshal.FreeHGlobal(p);
                    result = true;
                }
                else
                {
                    Marshal.FreeHGlobal(p);
                    ext = new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                ext = new Win32Exception(Marshal.GetLastWin32Error());
                if (ext.NativeErrorCode == 87)
                    ext = null;
            }
            ClosePrinter(printer);
            if (ext != null)
            {
                if (throwException)
                    throw ext;
                else
                    return false;
            }
            return result ;
        }

        private static void UpdatePrintJob(PrintJob job, IntPtr p)
        {
            JOB_INFO_1 obj = (JOB_INFO_1)Marshal.PtrToStructure(p, typeof(JOB_INFO_1));
            job.Datatype = obj.pDatatype;
            job.Document = obj.pDocument;
            job.JobId = obj.JobId;
            job.MachineName = obj.pMachineName;
            job.PagesPrinted = obj.PagesPrinted;
            job.Position = obj.Position;
            job.NativePrinterName = obj.pPrinterName;
            job.Priority = obj.Priority;
            job.Status = (PrintJobStatus)obj.Status;
            job.StatusText = obj.pStatus;

            FILETIME ft = new FILETIME();
            SystemTimeToFileTime(ref obj.Submitted, ref ft);
            FILETIME ft2 = new FILETIME();
            FileTimeToLocalFileTime(ref ft, ref ft2);
            SYSTEMTIME st2 = new SYSTEMTIME();
            FileTimeToSystemTime(ref ft2, ref st2);
            job.Submitted = new DateTime(st2.wYear, st2.wMonth, st2.wDay, st2.wHour, st2.wMinute, st2.wSecond);

            job.TotalPages = obj.TotalPages;
            job.UserName = obj.pUserName;
        }


        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(strPrinterName + " " + this.Jobs.Count);
            foreach (PrintJob job in this.Jobs)
            {
                str.Append(" " + job.Document);
            }
            return str.ToString();
        }

        #region 声明WIN32API函数和结构

        [DllImport("winspool.drv", EntryPoint = "OpenPrinter", SetLastError = true)]
        private static extern int OpenPrinterA(
            string pPrinterName,
            ref int phPrinter,
            int pDefault);

        [DllImport("winspool.drv", EntryPoint = "GetPrinter", SetLastError = true)]
        private static extern int GetPrinter(
            int hPrinter,
            int level,
            IntPtr pPrinter,
            int cbBuf,
            ref int pcbNeeded);

        [DllImport("winspool.drv", EntryPoint = "ClosePrinter", SetLastError = true)]
        private static extern int ClosePrinter(int phPrinter);


        [DllImport("winspool.drv", EntryPoint = "EnumJobs", SetLastError = true)]
        private static extern int EnumJobsA(
            int hPrinter,
            int FirstJob,
            int NoJobs,
            int Level,
            IntPtr jobs,
            int cdBuf,
            ref int pcbNeeded,
            ref int pcReturned);

        [DllImport("winspool.drv", EntryPoint = "GetJob", SetLastError = true)]
        private static extern int GetJobA(
            int hPrinter,
            int JobId,
            int Level,
            IntPtr pJob,
            int cdBuf,
            ref int pcbNeeded);

        [DllImport("winspool.drv", EntryPoint = "SetJob", SetLastError = true)]
        private static extern bool SetJob(
            int hPrinter,
            int JobId,
            int Level,
            IntPtr pJob,
            int Command );

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FileTimeToSystemTime(ref FILETIME fileTime, ref SYSTEMTIME systemTime);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SystemTimeToFileTime(ref SYSTEMTIME systemTime, ref FILETIME fileTime);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FileTimeToLocalFileTime(ref FILETIME systemTime, ref FILETIME fileTime);

        [StructLayout(LayoutKind.Sequential)]
        private struct FILETIME
        {
            int LowDateTime;
            int HighDateTime;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct JOB_INFO_1
        {
            public int JobId;
            public string pPrinterName;
            public string pMachineName;
            public string pUserName;
            public string pDocument;
            public string pDatatype;
            public string pStatus;
            public int Status;
            public int Priority;
            public int Position;
            public int TotalPages;
            public int PagesPrinted;
            public SYSTEMTIME Submitted;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }
         

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public short dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;
            public short dmPaperWidth;
            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmICCManufacturer;
            public int dmICCModel;
            public int dmPanningWidth;
            public int dmPanningHeight;
            public override string ToString()
            {
                return string.Concat(new object[] { 
                        "[DEVMODE: dmDeviceName=", this.dmDeviceName, ", dmSpecVersion=", this.dmSpecVersion, ", dmDriverVersion=", this.dmDriverVersion, ", dmSize=", this.dmSize, ", dmDriverExtra=", this.dmDriverExtra, ", dmFields=", this.dmFields, ", dmOrientation=", this.dmOrientation, ", dmPaperSize=", this.dmPaperSize, 
                        ", dmPaperLength=", this.dmPaperLength, ", dmPaperWidth=", this.dmPaperWidth, ", dmScale=", this.dmScale, ", dmCopies=", this.dmCopies, ", dmDefaultSource=", this.dmDefaultSource, ", dmPrintQuality=", this.dmPrintQuality, ", dmColor=", this.dmColor, ", dmDuplex=", this.dmDuplex, 
                        ", dmYResolution=", this.dmYResolution, ", dmTTOption=", this.dmTTOption, ", dmCollate=", this.dmCollate, ", dmFormName=", this.dmFormName, ", dmLogPixels=", this.dmLogPixels, ", dmBitsPerPel=", this.dmBitsPerPel, ", dmPelsWidth=", this.dmPelsWidth, ", dmPelsHeight=", this.dmPelsHeight, 
                        ", dmDisplayFlags=", this.dmDisplayFlags, ", dmDisplayFrequency=", this.dmDisplayFrequency, ", dmICMMethod=", this.dmICMMethod, ", dmICMIntent=", this.dmICMIntent, ", dmMediaType=", this.dmMediaType, ", dmDitherType=", this.dmDitherType, ", dmICCManufacturer=", this.dmICCManufacturer, ", dmICCModel=", this.dmICCModel, 
                        ", dmPanningWidth=", this.dmPanningWidth, ", dmPanningHeight=", this.dmPanningHeight, "]"
                     });
            }
        }
        #endregion

    }

    public class PrintJobList : System.Collections.CollectionBase
    {

        public PrintJob this[int index]
        {
            get
            {
                return (PrintJob)this.List[index];
            }
        }

        public int Add(PrintJob item)
        {
            return this.List.Add(item);
        }
    }

    /// <summary>
    /// 打印任务状态标志
    /// </summary>
    [System.Flags()]
    public enum PrintJobStatus : int
    {
        None = 0,
        PAUSED = 0x00000001,
        ERROR = 0x00000002,
        DELETING = 0x00000004,
        SPOOLING = 0x00000008,
        PRINTING = 0x00000010,
        OFFLINE = 0x00000020,
        PAPEROUT = 0x00000040,
        PRINTED = 0x00000080,
        DELETED = 0x00000100,
        BLOCKED_DEVQ = 0x00000200,
        USER_INTERVENTION = 0x00000400,
        RESTART = 0x00000800
    }

    /// <summary>
    /// 打印任务控制命令
    /// </summary>
    public enum PrintJobControlCommand
    {
        PAUSE = 1,
        RESUME = 2,
        CANCEL = 3,
        RESTART = 4,
        DELETE = 5,
        SENT_TO_PRINTER = 6,
        LAST_PAGE_EJECTED = 7
    }
    /// <summary>
    /// 打印任务对象
    /// </summary>
    public class PrintJob
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public PrintJob()
        {
        }

        private int intJobId = 0;
        /// <summary>
        /// 任务编号
        /// </summary>
        public int JobId
        {
            get
            {
                return intJobId;
            }
            set
            {
                intJobId = value;
            }
        }

        private string strPrinterName = null;
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrinterName
        {
            get
            {
                return strPrinterName;
            }
            set
            {
                strPrinterName = value;
            }
        }


        private string strNativePrinterName = null;
        /// <summary>
        /// 原始的打印机名称
        /// </summary>
        public string NativePrinterName
        {
            get
            {
                return strNativePrinterName;
            }
            set
            {
                strNativePrinterName = value;
            }
        }

        private string strMachineName = null;
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName
        {
            get
            {
                return strMachineName;
            }
            set
            {
                strMachineName = value;
            }
        }
        private string strUserName = null;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return strUserName;
            }
            set
            {
                strUserName = value;
            }
        }
        private string strDocument = null;
        /// <summary>
        /// 文档标题
        /// </summary>
        public string Document
        {
            get
            {
                return strDocument;
            }
            set
            {
                strDocument = value;
            }
        }
        private string strDatatype = null;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string Datatype
        {
            get
            {
                return strDatatype;
            }
            set
            {
                strDatatype = value;
            }
        }
        private string strStatusText = null;
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText
        {
            get
            {
                return strStatusText;
            }
            set
            {
                strStatusText = value;
            }
        }
        private PrintJobStatus intStatus = PrintJobStatus.None;
        /// <summary>
        /// 任务状态
        /// </summary>
        public PrintJobStatus Status
        {
            get
            {
                return intStatus;
            }
            set
            {
                intStatus = value;
            }
        }
        private int intPriority = 0;
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority
        {
            get
            {
                return intPriority;
            }
            set
            {
                intPriority = value;
            }
        }
        private int intPosition = 0;
        /// <summary>
        /// 任务位置
        /// </summary>
        public int Position
        {
            get
            {
                return intPosition;
            }
            set
            {
                intPosition = value;
            }
        }
        private int intTotalPages = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return intTotalPages;
            }
            set
            {
                intTotalPages = value;
            }
        }
        private int intPagesPrinted = 0;
        /// <summary>
        /// 打印页数
        /// </summary>
        public int PagesPrinted
        {
            get
            {
                return intPagesPrinted;
            }
            set
            {
                intPagesPrinted = value;
            }
        }
        private DateTime dtmSubmitted = DateTime.MinValue;
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime Submitted
        {
            get
            {
                return dtmSubmitted;
            }
            set
            {
                dtmSubmitted = value;
            }
        }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public void Refresh()
        {
            PrinterInformation.RefreshPrintJob(this, true);
        }

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Cancel()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.CANCEL, false);
        }
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Pause()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.PAUSE, false);
        }
        /// <summary>
        /// 重新启动打印任务
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Restart()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.RESTART, false);
        }
        /// <summary>
        /// 恢复打印任务
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Resume()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.RESUME, false);
        }
        
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool Delete()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.DELETE, false);
        }
        
        /// <summary>
        /// 发送到镜像打印端口
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool SendToPrinter()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.SENT_TO_PRINTER, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool LastPageEjected()
        {
            return PrinterInformation.ControlJob(this, PrintJobControlCommand.LAST_PAGE_EJECTED, false);
        }

        /// <summary>
        /// 是否正在执行任务
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (PrinterInformation.RefreshPrintJob(this, false) == false)
                {
                    return false ;
                }
                if (this.Status == PrintJobStatus.DELETED)
                    return false ;
                if (this.Status == PrintJobStatus.PRINTED)
                    return false ;
                return true;
            }
        }

        /// <summary>
        /// 是否处于成功完成任务的状态
        /// </summary>
        public bool IsSuccessStatus
        {
            get
            {
                if ((this.Status & PrintJobStatus.ERROR) == PrintJobStatus.ERROR)
                    return false;
                if ((this.Status & PrintJobStatus.DELETING) == PrintJobStatus.DELETING)
                    return false;
                if ((this.Status & PrintJobStatus.DELETED) == PrintJobStatus.DELETED)
                    return false;
                if ((this.Status & PrintJobStatus.OFFLINE) == PrintJobStatus.OFFLINE)
                    return false;

                if ((this.Status & PrintJobStatus.PRINTING) == PrintJobStatus.PRINTING
                    || (this.Status & PrintJobStatus.PRINTED) == PrintJobStatus.PRINTED)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 等待任务完成
        /// </summary>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public bool WaitForExit( System.ComponentModel.CancelEventHandler callBack )
        {
            while (true)
            {
                if (this.IsRunning == false)
                    break;
                if (callBack != null)
                {
                    System.ComponentModel.CancelEventArgs args = new CancelEventArgs();
                    args.Cancel = false;
                    callBack(this, args);
                    if (args.Cancel)
                    {
                        this.Cancel();
                        break;
                    }
                }
                System.Threading.Thread.Sleep(100);
            }//while

            if ((this.Status & PrintJobStatus.ERROR) == PrintJobStatus.ERROR)
                return false;
            if ((this.Status & PrintJobStatus.DELETING) == PrintJobStatus.DELETING)
                return false;
            if ((this.Status & PrintJobStatus.DELETED) == PrintJobStatus.DELETED)
                return false;
            if ((this.Status & PrintJobStatus.OFFLINE) == PrintJobStatus.OFFLINE)
                return false;

            if ((this.Status & PrintJobStatus.PRINTING) == PrintJobStatus.PRINTING
                || (this.Status & PrintJobStatus.PRINTED) == PrintJobStatus.PRINTED)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return this.PrinterName + "#" + this.Document + "#" + this.Status;
        }
    }
}
