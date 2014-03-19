using System.Threading;

namespace Exam.Utility
{
    public class DBConnection : DbHelper
    {
        private static DBConnection m_instance = null;
        private static object m_syncRoot = new object();

        /// <summary>
        /// 实例化数据池
        /// </summary>
        /// <returns>实例</returns>
        public static DBConnection Instance
        {
            get
            {
                if (m_instance != null) return m_instance;

                Monitor.Enter(m_syncRoot);

                if (m_instance == null)
                {
                    DBConnection temp = new DBConnection();
                    Interlocked.Exchange(ref m_instance, temp);
                }

                Monitor.Exit(m_syncRoot);

                return m_instance;
            }
        }

        private DBConnection() { }
    }
}
