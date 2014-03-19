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
    public class VotesRepository : BaseRepository<VoteEntity>
    {
        private static VotesRepository _repository = null;
        private static object m_syncRoot = new object();

        private VotesRepository()
        {

        }

        public static VotesRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    VotesRepository temp = new VotesRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }

        public List<VoteEntity> GetVotes(int appId)
        {
            IEntityFactory<VoteEntity> factory = new VoteFactory();
            List<VoteEntity> entityList = base.GetEntitys(string.Format("SELECT * FROM Votes WHERE AppID = {0}", appId), factory);
            
            return entityList;
        }

        public List<VoteEntity> GetVotes(int appId, int voteId)
        {
            IEntityFactory<VoteEntity> factory = new VoteFactory();
            List<VoteEntity> entityList = base.GetEntitys(string.Format("SELECT * FROM Votes WHERE AppID = {0}", appId), factory);
            
            return entityList;
        }
    }
}
