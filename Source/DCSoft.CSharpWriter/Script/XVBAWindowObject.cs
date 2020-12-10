/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
﻿using System;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections;

namespace DCSoft.Script
{
    /// <summary>
    /// window global object type use in VB.NET script
    /// </summary>
    public class XVBAWindowObject : System.IDisposable
    {
        /// <summary>
        /// static initialize type
        /// </summary>
        static XVBAWindowObject()
        {
            bolHasUserInterface = System.Windows.Forms.SystemInformation.UserInteractive;
        }

        private static XVBAWindowObject myInstance = new XVBAWindowObject();
        /// <summary>
        /// the only instance
        /// </summary>
        public static XVBAWindowObject Instance
        {
            get
            {
                return myInstance;
            }
            set
            {
                myInstance = value;
            }
        }

        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="win">object bind window</param>
        /// <param name="engine">script engine</param>
        /// <param name="systemName">system name</param>
        public XVBAWindowObject(
            System.Windows.Forms.IWin32Window win,
            XVBAEngine engine,
            string systemName)
        {
            this.myParentWindow = win;
            this.myEngine = engine;
            strSystemName = systemName;
        }

        public XVBAWindowObject()
        {
        }

        protected static string strSystemName = "Application";
        /// <summary>
        /// system name
        /// </summary>
        public static string SystemName
        {
            get
            {
                return strSystemName;
            }
            set
            {
                strSystemName = value;
            }
        }

        private static bool bolHasUserInterface = true;
        /// <summary>
        /// Whether has user interface
        /// </summary>
        public static bool HasUserInterface
        {
            get
            {
                return bolHasUserInterface; 
            }
            set
            {
                bolHasUserInterface = value; 
            }
        }

        protected XVBAEngine myEngine = null;
        /// <summary>
        /// Script engine
        /// </summary>
        public XVBAEngine Engine
        {
            get 
            { 
                return myEngine;
            }
            set
            {
                myEngine = value;
            }
        }

        protected System.Windows.Forms.IWin32Window myParentWindow = null;
        /// <summary>
        /// Parent window
        /// </summary>
        public System.Windows.Forms.IWin32Window ParentWindow
        {
            get
            { 
                return myParentWindow;
            }
            set
            { 
                myParentWindow = value;
            }
        }

        /// <summary>
        /// Screen width
        /// </summary>
        public int ScreenWidth
        {
            get
            {
                if (bolHasUserInterface)
                {
                    return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Screen height
        /// </summary>
        public int ScreenHeight
        {
            get
            {
                if (bolHasUserInterface)
                {
                    return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        #region codes for delay call ******************************************************************

        private int intScriptVersionBack = 0;
        private string strTimeoutMethod = null;
        private System.Windows.Forms.Timer myTimer;
        /// <summary>
        /// set dely call
        /// </summary>
        /// <param name="MinSecend">delay time span ,in millisecond </param>
        /// <param name="MethodName">method name</param>
        public void SetTimeout(int MinSecend, string MethodName)
        {
            if (bolHasUserInterface == false)
                return;
            if (myEngine == null)
                return;
            if (myIntervalTimer != null)
            {
                myIntervalTimer.Stop();
            }
            strTimerIntervalMethod = null;
            if (myTimer == null)
            {
                myTimer = new System.Windows.Forms.Timer();
                myTimer.Tick += new EventHandler(myTimer_Tick);
            }
            myTimer.Interval = MinSecend;
            strTimeoutMethod = MethodName;
            intScriptVersionBack = myEngine.ScriptVersion;
            myTimer.Start();
        }
        /// <summary>
        /// Clear delay call
        /// </summary>
        public void ClearTimeout()
        {
            if (myTimer != null)
            {
                myTimer.Stop();
            }
            strTimeoutMethod = null;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            myTimer.Stop();
            if (myEngine != null
                && strTimeoutMethod != null
                && strTimeoutMethod.Trim().Length > 0
                && myEngine.ScriptVersion == intScriptVersionBack )
            {
                string m = strTimeoutMethod.Trim();
                strTimeoutMethod = null;
                if (myEngine.HasMethod(m))
                {
                    myEngine.ExecuteSub(m);
                }
            }
        }

        #endregion

        #region codes for interval call ******************************************************************

        private System.Windows.Forms.Timer myIntervalTimer = null;
        private string strTimerIntervalMethod = null;

        /// <summary>
        /// set interval call
        /// </summary>
        /// <param name="MinSecend">interval time span, in millisecond</param>
        /// <param name="MethodName">method name</param>
        public void SetInterval(int MinSecend, string MethodName)
        {
            if (bolHasUserInterface == false)
            {
                return;
            }
            if (MethodName == null || MethodName.Trim().Length == 0)
            {
                return;
            }
            if (this.myEngine == null)
            {
                return;
            }

            if (myTimer != null)
            {
                myTimer.Stop();
            }
            strTimeoutMethod = null;

            if (myEngine.HasMethod(MethodName.Trim()) == false)
                return;
            strTimerIntervalMethod = MethodName.Trim();

            if (myIntervalTimer == null)
            {
                myIntervalTimer = new System.Windows.Forms.Timer();
                myIntervalTimer.Tick += new EventHandler(myIntervalTimer_Tick);
            }

            myIntervalTimer.Interval = MinSecend;
            intScriptVersionBack = myEngine.ScriptVersion;
        }

        /// <summary>
        /// clear interval call
        /// </summary>
        public void ClearInterval()
        {
            if (myIntervalTimer != null)
            {
                myIntervalTimer.Stop();
            }
            strTimerIntervalMethod = null;
        }
        private void myIntervalTimer_Tick(object sender, EventArgs e)
        {
            if (myIntervalTimer != null)
            {
                strTimerIntervalMethod = strTimerIntervalMethod.Trim();
            }
            if (strTimerIntervalMethod == null
                || strTimerIntervalMethod.Length == 0
                || myEngine == null
                || myEngine.HasMethod(strTimerIntervalMethod) == false 
                || myEngine.ScriptVersion == intScriptVersionBack )
            {
                if (myIntervalTimer != null)
                {
                    myIntervalTimer.Stop();
                }
                return;
            }
            myEngine.ExecuteSub(strTimerIntervalMethod);
        }

        #endregion

        #region codes for set window  **************************************************

        /// <summary>
        /// title
        /// </summary>
        public string Title
        {
            get
            {
                if (bolHasUserInterface == false)
                {
                    return "";
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm == null)
                {
                    return "";
                }
                else
                {
                    return frm.Text;
                }
            }
            set
            {
                if (bolHasUserInterface == false)
                {
                    return;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm != null)
                {
                    frm.Text = value;
                }
            }
        }

        /// <summary>
        /// left position
        /// </summary>
        public int Left
        {
            get
            {
                if (bolHasUserInterface == false)
                {
                    return 0;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm == null)
                    return 0;
                else
                    return frm.Left;
            }
            set
            {
                if (bolHasUserInterface == false)
                {
                    return;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm != null)
                {
                    frm.Left = value;
                }
            }
        }

        /// <summary>
        /// top pisition
        /// </summary>
        public int Top
        {
            get
            {
                if (bolHasUserInterface == false)
                {
                    return 0;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm == null)
                    return 0;
                else
                    return frm.Top;
            }
            set
            {
                if (bolHasUserInterface == false)
                {
                    return;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm != null)
                {
                    frm.Top = value;
                }
            }
        }

        /// <summary>
        /// width
        /// </summary>
        public int Width
        {
            get
            {
                if (bolHasUserInterface == false)
                {
                    return 0;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm == null)
                    return 0;
                else
                    return frm.Width;
            }
            set
            {
                if (bolHasUserInterface == false)
                {
                    return;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm != null)
                {
                    frm.Width = value;
                }
            }
        }

        /// <summary>
        /// height
        /// </summary>
        public int Height
        {
            get
            {
                if (bolHasUserInterface == false)
                {
                    return 0;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm == null)
                    return 0;
                else
                    return frm.Height;
            }
            set
            {
                if (bolHasUserInterface == false)
                {
                    return;
                }
                System.Windows.Forms.Form frm = myParentWindow as System.Windows.Forms.Form;
                if (frm != null)
                {
                    frm.Height = value;
                }
            }
        }

        #endregion
        /// <summary>
        /// convert object to text for dispaly
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        protected string GetDisplayText(object objData)
        {
            if (objData == null)
            {
                return "[null]";
            }
            else
            {
                return Convert.ToString(objData);
            }
        }

        /// <summary>
        /// show a message box
        /// </summary>
        /// <param name="objText">text</param>
        public void Alert(object objText)
        {
            if (bolHasUserInterface == false)
                return;
            System.Windows.Forms.MessageBox.Show(
                myParentWindow,
                GetDisplayText(objText),
                SystemName,
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }
        /// <summary>
        /// show a error message box
        /// </summary>
        /// <param name="objText">text</param>
        public void AlertError(object objText)
        {
            if (bolHasUserInterface == false)
                return;
            System.Windows.Forms.MessageBox.Show(
                myParentWindow,
                GetDisplayText(objText),
                SystemName,
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// show a message box and wait for user's choose
        /// </summary>
        /// <param name="objText">text</param>
        /// <returns>whether user confirm message</returns>
        public bool ConFirm(object objText)
        {
            if (bolHasUserInterface == false)
                return false;
            return (System.Windows.Forms.MessageBox.Show(
                myParentWindow,
                GetDisplayText(objText),
                SystemName,
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes);
        }

        /// <summary>
        /// show a input text box and wait for user's input
        /// </summary>
        /// <param name="objCaption">title</param>
        /// <param name="objDefault">initialize value</param>
        /// <returns>user's input</returns>
        public string Prompt(object objCaption, object objDefault)
        {
            if (bolHasUserInterface == false)
                return null;
            return dlgInputBox.InputBox(
                myParentWindow,
                GetDisplayText(objCaption),
                SystemName,
                GetDisplayText(objDefault));
        }

        /// <summary>
        /// display a file dialog and wait for user's selec
        /// </summary>
        /// <param name="objCaption">title</param>
        /// <param name="objFilter">file name filter</param>
        /// <returns>file name which user select , if user cancel then return null</returns>
        public string BrowseFile(object objCaption, object objFilter)
        {
            if (bolHasUserInterface == false)
            {
                return null;
            }
            using (System.Windows.Forms.OpenFileDialog dlg
                       = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                if (objCaption != null)
                {
                    dlg.Title = this.GetDisplayText(objCaption);
                }
                if (objFilter != null)
                    dlg.Filter = GetDisplayText(objFilter);
                if (dlg.ShowDialog(myParentWindow) == System.Windows.Forms.DialogResult.OK)
                    return dlg.FileName;
            }
            return null;
        }
        /// <summary>
        /// display a folder dialog and wait for user's select.
        /// </summary>
        /// <param name="objCaption">title</param>
        /// <returns>return folder's name which user seleced, if user cancel then return null</returns>
        public string BrowseFolder(object objCaption)
        {
            if (bolHasUserInterface == false)
            {
                return null;
            }
            using (System.Windows.Forms.FolderBrowserDialog dlg
                       = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (objCaption != null)
                {
                    dlg.Description = this.GetDisplayText(objCaption);
                }
                dlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(myParentWindow) == System.Windows.Forms.DialogResult.OK)
                    return dlg.SelectedPath;
                else
                    return null;
            }
        }

        private string strLogFileName = null;
        /// <summary>
        /// log file name , script can use DebugPrintLine() write some text to this file
        /// </summary>
        public string LogFileName
        {
            get
            {
                return strLogFileName; 
            }
            set
            {
                if (strLogFileName != value)
                {
                    strLogFileName = value;
                    bolWriteLogFileError = false;
                }
            }
        }
        /// <summary>
        /// error flag for write log file.
        /// </summary>
        private bool bolWriteLogFileError = false;

        /// <summary>
        /// output debug text
        /// </summary>
        /// <param name="objText">text</param>
        public void DebugPrintLine(object objText)
        {
            string text = GetDisplayText(objText);
            System.Diagnostics.Debug.WriteLine("Script:" +  text );
            if (bolWriteLogFileError == false)
            {
                if (strLogFileName != null && strLogFileName.Trim().Length > 0)
                {
                    // write log file
                    try
                    {
                        string dir = System.IO.Path.GetDirectoryName(strLogFileName);
                        if (dir != null && System.IO.Directory.Exists(dir))
                        {
                            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(strLogFileName, true, Encoding.GetEncoding(936)))
                            {
                                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ">" + text);
                            }
                        }
                    }
                    catch (Exception ext)
                    {
                        System.Console.WriteLine(ext.Message);
                        bolWriteLogFileError = true;
                    }
                }
            }
        }

        /// <summary>
        /// sleep 
        /// </summary>
        /// <param name="MilliSecond">time span , millisecond</param>
        public void Sleep(int MilliSecond)
        {
            System.Threading.Thread.Sleep(MilliSecond);
        }

        [System.ComponentModel.Browsable(false)]
        public void Dispose()
        {
            if (myTimer != null)
            {
                myTimer.Dispose();
                myTimer = null;
            }
            if (myIntervalTimer != null)
            {
                myIntervalTimer.Dispose();
                myIntervalTimer = null;
            }
        }
    }//public class XVBAWindowObject : System.IDisposable
}
