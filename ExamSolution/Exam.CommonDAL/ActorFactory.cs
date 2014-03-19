using System.Data;
using Exam.Model;
using Exam.Utility;
using Exam.Interface;

namespace Exam.CommonDAL
{
    public class ActorFactory : IEntityFactory<ActorEntity>
    {
        public const string APPID = "AppID";
        public const string CODE = "Code";
        public const string ACTORSID = "ActorsID";
        public const string IP = "IP";

        public ActorEntity BuildEntity(IDataReader reader)
        {
            ActorEntity entity = new ActorEntity()
            {
                AppID = DataHelper.GetInteger(reader[APPID]),
                ActorsID = DataHelper.GetInteger(reader[ACTORSID]),
                Code = DataHelper.GetInteger(reader[CODE]),
                IP = DataHelper.GetString(reader[IP])
            };

            return entity;
        }
    }
}
