using System;
using System.Collections.Generic;
using RatesDAL.Classes;

namespace RatesDAL.Interfaces
{
    public interface IRatesBase
    {
        List<RatesBase> GetAll();
        List<RatesBase> GetTop(int count, string field, bool desc = false);
        bool Create(RatesBase entity);
        bool CreateAuto(RatesBase entity);
        RatesBase Read(int Id);
        IEnumerable<RatesBase> Read(object param);
        bool Update(RatesBase entity);
        bool Delete(int Id);
        bool BulkCreate(RatesBase entity);
        bool BulkUpdate(RatesBase entity);
        bool BulkCommit(int timeout = 120);
    }
}
