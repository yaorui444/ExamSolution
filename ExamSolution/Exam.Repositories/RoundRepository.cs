using System;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Collections.Generic;
using Exam.Model;
using Exam.Utility;
using Exam.CommonDAL;
using Exam.Interface;

namespace Exam.Repositories
{
    public class RoundRepository : BaseRepository<RoundEntity>
    {
        private static RoundRepository _repository = null;
        private static object m_syncRoot = new object();

        private RoundRepository()
        {

        }

        public static RoundRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    RoundRepository temp = new RoundRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }

        public List<RoundEntity> GetRounds(int appId)
        {
            IEntityFactory<RoundEntity> factory = new RoundFactory();
            List<RoundEntity> entityList = base.GetEntitys(string.Format("SELECT * FROM Rounds WHERE AppID = {0}", appId), factory);
            
            return entityList;
        }

        public RoundEntity GetRound(int appId, int roundId)
        {
            IEntityFactory<RoundEntity> factory = new RoundFactory();
            RoundEntity entity = base.GetSingleEntity(string.Format("SELECT * FROM Rounds WHERE AppID = {0} AND RoundID = {1}", appId, roundId), factory);
            
            return entity;
        }
    }
}
