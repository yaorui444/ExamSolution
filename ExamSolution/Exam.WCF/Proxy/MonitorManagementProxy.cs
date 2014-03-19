using System.ServiceModel;
using Exam.WCF.Interface;
using System;

namespace Exam.WCF.Proxy
{
    public class MonitorManagementProxy : ClientBase<IMonitorManagement>
    {
        private IClientChannel m_duplexChannel;

        public IClientChannel DuplexChannel
        {
            get
            {
                return this.m_duplexChannel;
            }
            set
            {
                if (this.m_duplexChannel != null && this.m_duplexChannel.State == CommunicationState.Opened)
                {
                    try
                    {
                        this.m_duplexChannel.Close();
                    }
                    finally { }
                }

                this.m_duplexChannel = value;
            }
        }

        public MonitorManagementProxy()
        {
            this.m_duplexChannel = null;
        }
    }
}
