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
    public class VotingResultsRepository : BaseRepository<VotingResultEntity>
    {
        private static VotingResultsRepository _repository = null;
        private static object m_syncRoot = new object();

        private VotingResultsRepository()
        {

        }

        public static VotingResultsRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    VotingResultsRepository temp = new VotingResultsRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }
    }
}
