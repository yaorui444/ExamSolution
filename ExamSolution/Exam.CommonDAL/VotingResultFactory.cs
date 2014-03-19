using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class VotingResultFactory : IEntityFactory<VotingResultEntity>
    {
        public const string RESULT = "Result";

        public VotingResultEntity BuildEntity(IDataReader reader)
        {
            VoteEntity voteEntity = new VoteFactory().BuildEntity(reader);
            ActorEntity actorEntity = new ActorFactory().BuildEntity(reader);

            VotingResultEntity entity = new VotingResultEntity()
            {
                Vote = voteEntity,
                Actor = actorEntity,
                Result = DataHelper.GetInteger(reader[RESULT])
            };

            return entity;
        }
    }
}
