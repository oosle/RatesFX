using System;
using System.Collections.Generic;
using RatesDAL.Classes;

namespace RatesDAL.Interfaces
{
    public interface IRatesCurrency
    {
        List<RatesCurrency> GetAll();
        List<RatesCurrency> GetTop(int count, string field, bool desc = false);
        bool Create(RatesCurrency entity);
        bool CreateAuto(RatesCurrency entity);
        RatesCurrency Read(int Id);
        IEnumerable<RatesCurrency> Read(object param);
        bool Update(RatesCurrency entity);
        bool Delete(int Id);
        bool BulkCreate(RatesCurrency entity);
        bool BulkUpdate(RatesCurrency entity);
        bool BulkCommit(int timeout = 120);
    }
}
