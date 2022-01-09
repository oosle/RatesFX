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
    public class RatesCurrencyRepo : IRatesCurrency
    {
        private readonly string _dbString;
        private List<string> bulk = new List<string>();

        public RatesCurrencyRepo(string dbString)
        {
            _dbString = dbString;

            var mapper = (SqlMapper.ITypeMap)Activator.CreateInstance(typeof(ColumnAttributeTypeMapper<>)
                .MakeGenericType(typeof(RatesCurrency)));
            SqlMapper.SetTypeMap(typeof(RatesCurrency), mapper);
        }

        public List<RatesCurrency> GetAll()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesCurrency]";

                    return (db.Query<RatesCurrency>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.GetAll()", ex);
            }
        }

        public List<RatesCurrency> GetTop(int count)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = $"SELECT TOP {count} * FROM [RatesCurrency]";

                    return (db.Query<RatesCurrency>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.GetTop()", ex);
            }
        }

        public List<RatesCurrency> GetTop(int count, string field, bool desc = false)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string order = (desc ? "DESC" : "ASC");
                    string sql = $"SELECT TOP {count} * FROM [RatesCurrency] ORDER BY {field} {order}";

                    return (db.Query<RatesCurrency>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.GetTop()", ex);
            }
        }

        public bool Create(RatesCurrency entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SET IDENTITY_INSERT [RatesCurrency] ON; INSERT INTO [RatesCurrency] ([id], [base_id], [currency], [value], [country], [created], [c_user], [updated], [u_user], [deleted], [d_user]) VALUES (@Id, @BaseId, @Currency, @Value, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser); SET IDENTITY_INSERT [RatesCurrency] OFF;";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.Create()", ex);
            }
        }

        public bool CreateAuto(RatesCurrency entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesCurrency] VALUES (@BaseId, @Currency, @Value, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.CreateAuto()", ex);
            }
        }

        public RatesCurrency Read(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesCurrency] WHERE [id] = @Id";

                    return (db.Query<RatesCurrency>(sql, new { Id }).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.Read()", ex);
            }
        }

        public IEnumerable<RatesCurrency> Read(object param)
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
                    string sql = "SELECT * FROM RatesCurrency WHERE " + string.Join(" AND ", where.ToArray());

                    return (db.Query<RatesCurrency>(sql, param));
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.Read()", ex);
            }
        }

        public bool Update(RatesCurrency entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesCurrency] SET [base_id] = @BaseId, [currency] = @Currency, [value] = @Value, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.Update()", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "DELETE FROM [RatesCurrency] WHERE [id] = @Id";
                    int affected = db.Execute(sql, new { Id });

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.Delete()", ex);
            }
        }

        public bool BulkCreate(RatesCurrency entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesCurrency] VALUES (@BaseId, @Currency, @Value, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.BulkCreate()", ex);
            }
        }

        public bool BulkUpdate(RatesCurrency entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesCurrency] SET [base_id] = @BaseId, [currency] = @Currency, [value] = @Value, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.BulkUpdate()", ex);
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
                    throw new DALException("RatesCurrency.BulkCommit(): Bulk operation SQL is empty.");
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesCurrency.BulkCommit()", ex);
            }
        }
    }
}
