using System;
using System.Collections.Generic;
using RatesDAL.Classes;

namespace RatesDAL.Interfaces
{
    public interface IRatesLookup
    {
        List<RatesLookup> GetAll();
        List<RatesLookup> GetTop(int count, string field, bool desc = false);
        bool Create(RatesLookup entity);
        bool CreateAuto(RatesLookup entity);
        RatesLookup Read(int Id);
        IEnumerable<RatesLookup> Read(object param);
        bool Update(RatesLookup entity);
        bool Delete(int Id);
        bool BulkCreate(RatesLookup entity);
        bool BulkUpdate(RatesLookup entity);
        bool BulkCommit(int timeout = 120);
    }
}
