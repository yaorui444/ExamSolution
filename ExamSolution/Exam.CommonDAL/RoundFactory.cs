using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class RoundFactory : IEntityFactory<RoundEntity>
    {
        public const string APPID = "AppID";
        public const string ROUNDID = "RoundID";
        public const string ACTORSCODE = "ActorsCode";
        public const string SCORES = "Scores";
        public const string STARTTIME = "Starttime";
        public const string ENDTIME = "Endtime";
        public const string USEDTIME = "Usedtime";

        public RoundEntity BuildEntity(IDataReader reader)
        {
            RoundEntity entity = new RoundEntity()
            {
                AppID = DataHelper.GetInteger(reader[APPID]),
                RoundID = DataHelper.GetInteger(reader[ROUNDID]),
                ActorsCode = DataHelper.GetString(reader[ACTORSCODE]),
                Scores = DataHelper.GetInteger(reader[SCORES]),
                Starttime = DataHelper.GetDateTime(reader[STARTTIME]),
                Endtime = DataHelper.GetDateTime(reader[ENDTIME]),
                Usedtime = DataHelper.GetInteger(reader[USEDTIME])
            };

            return entity;
        }
    }
}
