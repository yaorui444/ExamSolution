using System.Data;

namespace Exam.Interface
{
    public interface IEntityFactory<T>
    {
        T BuildEntity(IDataReader reader);
    }
}
