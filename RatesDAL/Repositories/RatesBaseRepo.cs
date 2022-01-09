using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using RatesDAL.Classes;
using RatesDAL.Interfaces;

namespace RatesDAL.Repositories
{
    public class RatesBaseRepo : IRatesBase
    {
        private readonly string _dbString;
        private List<string> bulk = new List<string>();

        public RatesBaseRepo(string dbString)
        {
            _dbString = dbString;

            var mapper = (SqlMapper.ITypeMap)Activator.CreateInstance(typeof(ColumnAttributeTypeMapper<>)
                .MakeGenericType(typeof(RatesBase)));
            SqlMapper.SetTypeMap(typeof(RatesBase), mapper);
        }

        public List<RatesBase> GetAll()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesBase]";

                    return (db.Query<RatesBase>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.GetAll()", ex);
            }
        }

        public List<RatesBase> GetTop(int count)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = $"SELECT TOP {count} * FROM [RatesBase]";

                    return (db.Query<RatesBase>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.GetTop()", ex);
            }
        }

        public List<RatesBase> GetTop(int count, string field, bool desc = false)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string order = (desc ? "DESC" : "ASC");
                    string sql = $"SELECT TOP {count} * FROM [RatesBase] ORDER BY {field} {order}";

                    return (db.Query<RatesBase>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.GetTop()", ex);
            }
        }

        public bool Create(RatesBase entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SET IDENTITY_INSERT [RatesBase] ON; INSERT INTO [RatesBase] ([id], [currency], [trade], [country], [created], [c_user], [updated], [u_user], [deleted], [d_user]) VALUES (@Id, @Currency, @Trade, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser); SET IDENTITY_INSERT [RatesBase] OFF;";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.Create()", ex);
            }
        }

        public bool CreateAuto(RatesBase entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesBase] VALUES (@Currency, @Trade, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.CreateAuto()", ex);
            }
        }

        public RatesBase Read(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesBase] WHERE [id] = @Id";

                    return (db.Query<RatesBase>(sql, new { Id }).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.Read()", ex);
            }
        }

        public IEnumerable<RatesBase> Read(object param)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    var where = new List<string>();
                    foreach (var key in param.GetType().GetProperties())
                    {
                        if (null == key.GetValue(param))
                        {
                            where.Add($"{key.Name} IS NULL");
                        }
                        else
                        {
                            where.Add($"{key.Name} = @{key.Name}");
                        }
                    }
                    string sql = "SELECT * FROM RatesBase WHERE " + string.Join(" AND ", where.ToArray());

                    return (db.Query<RatesBase>(sql, param));
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.Read()", ex);
            }
        }

        public bool Update(RatesBase entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesBase] SET [currency] = @Currency, [trade] = @Trade, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.Update()", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "DELETE FROM [RatesBase] WHERE [id] = @Id";
                    int affected = db.Execute(sql, new { Id });

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.Delete()", ex);
            }
        }

        public bool BulkCreate(RatesBase entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesBase] VALUES (@Currency, @Trade, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.BulkCreate()", ex);
            }
        }

        public bool BulkUpdate(RatesBase entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesBase] SET [currency] = @Currency, [trade] = @Trade, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.BulkUpdate()", ex);
            }
        }

        public bool BulkCommit(int timeout = 120)
        {
            try
            {
                if (bulk.Count > 0)
                {
                    using (var transaction = new TransactionScope())
                    using (IDbConnection db = new SqlConnection(_dbString))
                    {
                        string sql = string.Join(";", bulk);

                        db.Execute(sql, commandTimeout: timeout);
                        bulk.Clear();
                        transaction.Complete();

                        return (true);
                    }
                }
                else
                {
                    throw new DALException("RatesBase.BulkCommit(): Bulk operation SQL is empty.");
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesBase.BulkCommit()", ex);
            }
        }
    }
}
