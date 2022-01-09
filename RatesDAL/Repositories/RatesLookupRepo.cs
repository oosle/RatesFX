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
    public class RatesLookupRepo : IRatesLookup
    {
        private readonly string _dbString;
        private List<string> bulk = new List<string>();

        public RatesLookupRepo(string dbString)
        {
            _dbString = dbString;

            var mapper = (SqlMapper.ITypeMap)Activator.CreateInstance(typeof(ColumnAttributeTypeMapper<>)
                .MakeGenericType(typeof(RatesLookup)));
            SqlMapper.SetTypeMap(typeof(RatesLookup), mapper);
        }

        public List<RatesLookup> GetAll()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesLookup]";

                    return (db.Query<RatesLookup>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.GetAll()", ex);
            }
        }

        public List<RatesLookup> GetTop(int count)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = $"SELECT TOP {count} * FROM [RatesLookup]";

                    return (db.Query<RatesLookup>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.GetTop()", ex);
            }
        }

        public List<RatesLookup> GetTop(int count, string field, bool desc = false)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string order = (desc ? "DESC" : "ASC");
                    string sql = $"SELECT TOP {count} * FROM [RatesLookup] ORDER BY {field} {order}";

                    return (db.Query<RatesLookup>(sql).ToList());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.GetTop()", ex);
            }
        }

        public bool Create(RatesLookup entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SET IDENTITY_INSERT [RatesLookup] ON; INSERT INTO [RatesLookup] ([id], [code], [currency], [country], [created], [c_user], [updated], [u_user], [deleted], [d_user]) VALUES (@Id, @Code, @Currency, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser); SET IDENTITY_INSERT [RatesLookup] OFF;";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.Create()", ex);
            }
        }

        public bool CreateAuto(RatesLookup entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesLookup] VALUES (@Code, @Currency, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.CreateAuto()", ex);
            }
        }

        public RatesLookup Read(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "SELECT * FROM [RatesLookup] WHERE [id] = @Id";

                    return (db.Query<RatesLookup>(sql, new { Id }).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.Read()", ex);
            }
        }

        public IEnumerable<RatesLookup> Read(object param)
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
                    string sql = "SELECT * FROM RatesLookup WHERE " + string.Join(" AND ", where.ToArray());

                    return (db.Query<RatesLookup>(sql, param));
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.Read()", ex);
            }
        }

        public bool Update(RatesLookup entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesLookup] SET [code] = @Code, [currency] = @Currency, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    int affected = db.Execute(sql, entity);

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.Update()", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "DELETE FROM [RatesLookup] WHERE [id] = @Id";
                    int affected = db.Execute(sql, new { Id });

                    return (affected > 0);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.Delete()", ex);
            }
        }

        public bool BulkCreate(RatesLookup entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "INSERT INTO [RatesLookup] VALUES (@Code, @Currency, @Country, @Created, @CUser, @Updated, @UUser, @Deleted, @DUser)";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.BulkCreate()", ex);
            }
        }

        public bool BulkUpdate(RatesLookup entity)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_dbString))
                {
                    string sql = "UPDATE [RatesLookup] SET [code] = @Code, [currency] = @Currency, [country] = @Country, [created] = @Created, [c_user] = @CUser, [updated] = @Updated, [u_user] = @UUser, [deleted] = @Deleted, [d_user] = @DUser WHERE [id] = @Id";
                    bulk.Add(Utils.Replace(entity, sql));

                    return (true);
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.BulkUpdate()", ex);
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
                    throw new DALException("RatesLookup.BulkCommit(): Bulk operation SQL is empty.");
                }
            }
            catch (Exception ex)
            {
                throw new DALException("RatesLookup.BulkCommit()", ex);
            }
        }
    }
}
