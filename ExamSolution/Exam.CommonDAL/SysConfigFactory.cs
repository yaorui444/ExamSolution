using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class SysConfigFactory : IEntityFactory<SysConfigEntity>
    {
        public const string APPID = "AppID";
        public const string ROUNDNUMBER = "RoundNumber";
        public const string ROUNDEXAMS = "RoundExams";
        public const string ROUNDLENGHT = "RoundLenght";
        public const string DOINGTIME = "Doingtime";
        public const string READINGTIME = "Readingtime";
        public const string STATE = "State";

        public SysConfigEntity BuildEntity(IDataReader reader)
        {
            SysConfigEntity entity = new SysConfigEntity()
            {
                AppID = DataHelper.GetInteger(reader[APPID]),
                RoundNumber = DataHelper.GetInteger(reader[ROUNDNUMBER]),
                RoundExams = DataHelper.GetInteger(reader[ROUNDEXAMS]),
                RoundLenght = DataHelper.GetInteger(reader[ROUNDLENGHT]),
                Doingtime = DataHelper.GetInteger(reader[DOINGTIME]),
                Readingtime = DataHelper.GetInteger(reader[READINGTIME]),
                State = DataHelper.GetBoolean(reader[STATE])
            };

            return entity;
        }
    }
}
