using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Collections.Generic;
using Exam.Model;
using Exam.Utility;
using Exam.CommonDAL;
using Exam.Interface;

namespace Exam.Repositories
{
    public class SysConfigRepository : BaseRepository<SysConfigEntity>
    {
        private static SysConfigRepository _repository = null;
        private static object m_syncRoot = new object();

        private SysConfigRepository()
        {

        }

        public static SysConfigRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    SysConfigRepository temp = new SysConfigRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }

        public SysConfigEntity GetSysConfig(int appId)
        {
            IEntityFactory<SysConfigEntity> factory = new SysConfigFactory();
            SysConfigEntity entity = base.GetSingleEntity(string.Format("SELECT * FROM SysConfig WHERE AppID = {0}", appId), factory);

            return entity;
        }

        public SysConfigEntity AddSysConfig(SysConfigEntity entity)
        {
            SysConfigEntity ret = entity;

            if (entity != null)
            {
                StringBuilder addSql = new StringBuilder();
                addSql.AppendFormat("INSERT INTO SysConfig (AppID, RoundNumber, RoundExams, RoundLenght, Doingtime, Readingtime, State) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, '{6}')",
                    entity.AppID, entity.RoundNumber, entity.RoundExams, entity.RoundLenght, entity.Doingtime, entity.Readingtime, entity.State ? "1" : "0");

                string getIdSql = "SELECT MAX(AppID) FROM SysConfig";

                ret.AppID = base.Add(addSql.ToString(), getIdSql);
            }

            return ret;
        }
    }
}