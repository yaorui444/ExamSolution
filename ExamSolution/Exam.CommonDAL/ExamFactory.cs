using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class ExamFactory : IEntityFactory<ExamEntity>
    {
        public const string APPID = "AppID";
        public const string SERIAL = "Serial";
        public const string SCORES = "Scores";
        public const string CORRECT = "Correct";
        public const string STARTTIME = "Starttime";
        public const string ENDTIME = "Endtime";
        public const string USEDTIME = "Usedtime";
        public const string DOINGVIDEO = "DoingVideo";
        public const string DVPATH = "Dvpath";
        public const string READINGVIDEO = "ReadingVideo";
        public const string RVPATH = "Rvpath";

        public ExamEntity BuildEntity(IDataReader reader)
        {
            BraindumpEntity examEntity = new BraindumpFactory().BuildEntity(reader);
            RoundEntity roundEntity = new RoundFactory().BuildEntity(reader);
            ActorEntity actorEntity = new ActorFactory().BuildEntity(reader);

            ExamEntity entity = new ExamEntity()
            {
                AppID = DataHelper.GetInteger(reader[APPID]),
                Question = examEntity,
                Round = roundEntity,
                Actor = actorEntity,
                Serial = DataHelper.GetInteger(reader[SERIAL]),
                Scores = DataHelper.GetInteger(reader[SCORES]),
                Correct = DataHelper.GetBoolean(reader[CORRECT]),
                Starttime = DataHelper.GetDateTime(reader[STARTTIME]),
                Endtime = DataHelper.GetDateTime(reader[ENDTIME]),
                Usedtime = DataHelper.GetInteger(reader[USEDTIME]),
                DoingVideo = DataHelper.GetString(reader[DOINGVIDEO]),
                Dvpath = DataHelper.GetString(reader[DVPATH]),
                ReadingVideo = DataHelper.GetString(reader[READINGVIDEO]),
                Rvpath = DataHelper.GetString(reader[RVPATH])
            };

            return entity;
        }
    }
}
