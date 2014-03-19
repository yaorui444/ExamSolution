using System;
using System.IO;
using System.Text;

namespace Exam.Utility
{
    /// <summary>
    /// 异常
    /// </summary>
    public class ENNException : Exception
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger("logger-name");

        #region 客户端异常日志.


        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest,   //目标设备的句柄
            int nXDest,   //   目标对象的左上角的X坐标
            int nYDest,   //   目标对象的左上角的X坐标
            int nWidth,   //   目标对象的矩形的宽度
            int nHeight,   //   目标对象的矩形的长度
            IntPtr hdcSrc,   //   源设备的句柄
            int nXSrc,   //   源对象的左上角的X坐标
            int nYSrc,   //   源对象的左上角的X坐标
            System.Int32 dwRop   //   光栅的操作值
            );

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr CreateDC(
            string lpszDriver,   //   驱动名称
            string lpszDevice,   //   设备名称
            string lpszOutput,   //   无用，可以设定位"NULL"
            IntPtr lpInitData   //   任意的打印机数据
            );



        /// <summary>   
        /// 默认构造函数   
        /// </summary>   
        public ENNException() { }
        public ENNException(string message)
            : base(message) { }
        public ENNException(string message, Exception inner)
            : base(message, inner) { }
        public ENNException(Exception inner)
            : base(inner.Message, inner)
        {
            //
        }

        /// <summary>
        /// 写异常信息
        /// </summary>
        public static void WriteException(Exception ex)
        {
            //记录异常,已便
            //log.Error(ex.Message);

            try
            {
                // 文件名基准
                // 年月日 
                string baseFileName = "ExceptionLog_" + DateTime.Now.ToString("yyyyMMdd");

                // 取得当前应用程序的目录.
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Log";

                // 如果日志目录不存在，创建.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 写日志文件.
                WriteClientException(ex, path + "\\" + baseFileName + ".log");

                // 截屏
                //WriteClientImage(path + "\\" + baseFileName + ".jpg");


                //MessageBox.Show("操作过程可能发生了错误，请尝试联系系统管理人员。", "错误", MessageBoxButtons.OK);
            }
            catch
            {
            }
            ex = null;
        }

        /// <summary>
        /// 向日志文件写入异常信息。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="fileName"></param>
        public static void WriteClientException(Exception exp, string fileName)
        {
            StreamWriter sw = null;
            try
            {
                // 首先判断，文件是否已经存在
                if (File.Exists(fileName))
                {
                    // 如果文件已经存在，那么删除掉.
                    //File.Delete(fileName);
                }

                // 注意第2个参数：
                // 确定是否将数据追加到文件。如果该文件存在，并且 append 为 false，则该文件被覆盖。
                // 如果该文件存在，并且 append 为 true，则数据被追加到该文件中。否则，将创建新文件。
                // 也就是说，如果第2个参数 是 false， 可以不用写前面的 判断文件存在则删除的代码.

                // 第3个参数为编码方式， 读取和写入，尽可能使用统一的编码
                sw = new StreamWriter(fileName, true, Encoding.UTF8);

                sw.WriteLine("-------Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "---------");
                // 写入异常信息.
                sw.WriteLine(exp.Message);
                sw.WriteLine(exp.StackTrace);
                sw.WriteLine("----------------------------------------------");
                //从控制台输出
                Console.WriteLine("-------Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "---------");
                // 写入异常信息.
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
                Console.WriteLine("----------------------------------------------");
                // 关闭文件.
                sw.Close();

                sw = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("在写入文件的过程中，发生了异常！");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                    }
                    catch
                    {
                        // 最后关闭文件，无视 关闭是否会发生错误了.
                    }
                }
            }
        }


        ///// <summary>
        ///// 对当前客户端屏幕做截屏操作.
        ///// </summary>
        ///// <param name="fileName"></param>
        //public static void WriteClientImage(string fileName)
        //{
        //    IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
        //    //创建显示器的DC
        //    Graphics g1 = Graphics.FromHdc(dc1);
        //    //由一个指定设备的句柄创建一个新的Graphics对象
        //    Bitmap MyImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 25, g1);
        //    //根据屏幕大小创建一个与之相同大小的Bitmap对象
        //    Graphics g2 = Graphics.FromImage(MyImage);
        //    //获得屏幕的句柄
        //    IntPtr dc3 = g1.GetHdc();
        //    //获得位图的句柄
        //    IntPtr dc2 = g2.GetHdc();
        //    //把当前屏幕捕获到位图对象中
        //    BitBlt(dc2, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 25, dc3, 0, 0, 13369376);
        //    //把当前屏幕拷贝到位图中
        //    g1.ReleaseHdc(dc3);
        //    //释放屏幕句柄
        //    g2.ReleaseHdc(dc2);
        //    //释放位图句柄
        //    MyImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        //}



        #endregion


    }

    /// <summary>
    /// 程序异常
    /// </summary>
    public class ENNApplicationException : ApplicationException
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger("logger-name");

        /// <summary>   
        /// 默认构造函数   
        /// </summary>   
        public ENNApplicationException()
        {
            //
        }
        public ENNApplicationException(string message)
            : base(message) { }
        public ENNApplicationException(string message, Exception inner)
            : base(message, inner) { }


        /// <summary>
        /// 写异常信息
        /// </summary>
        public static void WriteException(ApplicationException ex)
        {
            //log.Error(ex.Message);
            //记录异常,已便
            //System.Windows.Forms.MessageBox.Show(ex.ToString());
            try
            {
                // 文件名基准
                // 年月日 
                string baseFileName = "ExceptionLog_" + DateTime.Now.ToString("yyyyMMdd");

                // 取得当前应用程序的目录.
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Log";

                // 如果日志目录不存在，创建.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // 写日志文件.
                WriteClientException(ex, path + "\\" + baseFileName + ".log");

                // 截屏
                //WriteClientImage(path + "\\" + baseFileName + ".jpg");


                //MessageBox.Show("操作过程可能发生了错误，请尝试联系系统管理人员。", "错误", MessageBoxButtons.OK);
            }
            catch
            {
            }
            ex = null;
        }

        /// <summary>
        /// 向日志文件写入异常信息。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="fileName"></param>
        public static void WriteClientException(Exception exp, string fileName)
        {
            StreamWriter sw = null;
            try
            {
                // 首先判断，文件是否已经存在
                if (File.Exists(fileName))
                {
                    // 如果文件已经存在，那么删除掉.
                    //File.Delete(fileName);
                }

                // 注意第2个参数：
                // 确定是否将数据追加到文件。如果该文件存在，并且 append 为 false，则该文件被覆盖。
                // 如果该文件存在，并且 append 为 true，则数据被追加到该文件中。否则，将创建新文件。
                // 也就是说，如果第2个参数 是 false， 可以不用写前面的 判断文件存在则删除的代码.

                // 第3个参数为编码方式， 读取和写入，尽可能使用统一的编码
                sw = new StreamWriter(fileName, true, Encoding.UTF8);

                sw.WriteLine("-------Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "---------");
                // 写入异常信息.
                sw.WriteLine(exp.Message);
                sw.WriteLine(exp.StackTrace);
                sw.WriteLine("----------------------------------------------");


                //从控制台输出
                Console.WriteLine("-------Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + "---------");
                // 写入异常信息.
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
                Console.WriteLine("----------------------------------------------");

                // 关闭文件.
                sw.Close();

                sw = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("在写入文件的过程中，发生了异常！");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                    }
                    catch
                    {
                        // 最后关闭文件，无视 关闭是否会发生错误了.
                    }
                }
            }
        }


        ///// <summary>
        ///// 对当前客户端屏幕做截屏操作.
        ///// </summary>
        ///// <param name="fileName"></param>
        //public static void WriteClientImage(string fileName)
        //{
        //    IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
        //    //创建显示器的DC
        //    Graphics g1 = Graphics.FromHdc(dc1);
        //    //由一个指定设备的句柄创建一个新的Graphics对象
        //    Bitmap MyImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 25, g1);
        //    //根据屏幕大小创建一个与之相同大小的Bitmap对象
        //    Graphics g2 = Graphics.FromImage(MyImage);
        //    //获得屏幕的句柄
        //    IntPtr dc3 = g1.GetHdc();
        //    //获得位图的句柄
        //    IntPtr dc2 = g2.GetHdc();
        //    //把当前屏幕捕获到位图对象中
        //    BitBlt(dc2, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 25, dc3, 0, 0, 13369376);
        //    //把当前屏幕拷贝到位图中
        //    g1.ReleaseHdc(dc3);
        //    //释放屏幕句柄
        //    g2.ReleaseHdc(dc2);
        //    //释放位图句柄
        //    MyImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        //}


    }
}
