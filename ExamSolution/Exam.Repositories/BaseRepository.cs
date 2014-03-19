using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Exam.Utility;
using Exam.Interface;

namespace Exam.Repositories
{
    public abstract class BaseRepository<TEntity>
    {
        public TEntity GetSingleEntity(string sql, IEntityFactory<TEntity> factory)
        {
            TEntity entity = default(TEntity);
            try
            {
                DbCommand cmd = DBConnection.Instance.GetSqlStringCommond(sql);
                using (IDataReader reader = DBConnection.Instance.ExecuteReader(cmd))
                {
                    if (reader.Read())
                    {
                        entity = factory.BuildEntity(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                ENNException.WriteException(ex);
            }
            return entity;
        }

        public List<TEntity> GetEntitys(string sql, IEntityFactory<TEntity> factory)
        {
            List<TEntity> entityList = new List<TEntity>();
            try
            {
                DbCommand cmd = DBConnection.Instance.GetSqlStringCommond(sql);
                using (IDataReader reader = DBConnection.Instance.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        TEntity entity = factory.BuildEntity(reader);
                        entityList.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                ENNException.WriteException(ex);
            }
            return entityList;
        }

        public bool Add(string sql)
        {
            bool ret = false;

            try
            {
                DbCommand cmd = DBConnection.Instance.GetSqlStringCommond(sql);
                int ds = DBConnection.Instance.ExecuteNonQuery(cmd);
                if (ds > 1) ret = true;
            }
            catch (Exception ex)
            {
                ENNException.WriteException(ex);
            }
            return ret;
        }

        public int Add(string sql, string getIndexSql)
        {
            int id = 0;

            if (Add(sql))
            {
                try
                {
                    DbCommand cmd = DBConnection.Instance.GetSqlStringCommond(getIndexSql);
                    id = Convert.ToInt32(DBConnection.Instance.ExecuteScalar(cmd));
                }
                catch (Exception ex)
                {
                    ENNException.WriteException(ex);
                }
            }

            return id;
        }
    }
}
