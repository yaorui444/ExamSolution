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
    public class ActorRepository : BaseRepository<ActorEntity>
    {
        private static ActorRepository _repository = null;
        private static object m_syncRoot = new object();

        private ActorRepository()
        {

        }

        public static ActorRepository Instance
        {
            get
            {
                if (_repository != null) return _repository;

                Monitor.Enter(m_syncRoot);

                if (_repository == null)
                {
                    ActorRepository temp = new ActorRepository();
                    Interlocked.Exchange(ref _repository, temp);
                }

                Monitor.Exit(m_syncRoot);

                return _repository;
            }
        }

        public List<ActorEntity> GetActors(int appId)
        {
            IEntityFactory<ActorEntity> factory = new ActorFactory();
            List<ActorEntity> entityList = base.GetEntitys(string.Format("SELECT * FROM Actors WHERE AppID = {0}", appId), factory);

            return entityList;
        }

        public ActorEntity GetActor(int actorsId)
        {
            IEntityFactory<ActorEntity> factory = new ActorFactory();
            ActorEntity entity = base.GetSingleEntity(string.Format("SELECT * FROM Actors WHERE ActorsID = {0}", actorsId), factory);

            return entity;
        }
    }
}
