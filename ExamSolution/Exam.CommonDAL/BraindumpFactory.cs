using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class BraindumpFactory : IEntityFactory<BraindumpEntity>
    {
        public const string EXAMID = "ExamID";
        public const string QUESTION = "Question";
        public const string ANSWER = "Answer";
        public const string SCORES = "Scores";
        public const string DIFF = "Diff";

        public BraindumpEntity BuildEntity(IDataReader reader)
        {
            BraindumpEntity entity = new BraindumpEntity()
            {
                ExamID = DataHelper.GetInteger(reader[EXAMID]),
                Question = DataHelper.GetString(reader[QUESTION]),
                Answer = DataHelper.GetDouble(reader[QUESTION]),
                Scores = DataHelper.GetInteger(reader[SCORES]),
                Diff = DataHelper.GetInteger(reader[DIFF])
            };

            return entity;
        }
    }
}
