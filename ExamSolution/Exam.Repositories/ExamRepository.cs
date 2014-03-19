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
    public class ExamRepository : BaseRepository<ExamEntity>
    {
        private static ExamRepository _repository = null;
        private static object m_syncRoot = new object();

        private ExamRepository()
        {

        }

        public static ExamRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    ExamRepository temp = new ExamRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }
    }
}
