using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class VoteFactory : IEntityFactory<VoteEntity>
    {
        public const string APPID = "AppID";
        public const string ROUNDID = "RoundID";
        public const string VOTEID = "VoteID";
        public const string VOTE = "Vote";
        public const string ARITH = "Arith";

        public VoteEntity BuildEntity(IDataReader reader)
        {
            VoteEntity entity = new VoteEntity()
            {
                AppID = DataHelper.GetInteger(reader[APPID]),
                RoundID = DataHelper.GetInteger(reader[ROUNDID]),
                VoteID = DataHelper.GetInteger(reader[VOTEID]),
                Vote = DataHelper.GetString(reader[VOTE]),
                Arith = DataHelper.GetString(reader[ARITH])
            };

            return entity;
        }
    }
}
