using Dapper;
using System;
using RatesDAL.Classes;
using RatesDAL.Interfaces;
using RatesDAL.Repositories;

namespace RatesDAL
{
    public class DataAccessLayer
    {
        private readonly string _dbString;

        public readonly RatesBaseRepo _RatesBaseRepo;
        public readonly RatesCurrencyRepo _RatesCurrencyRepo;
        public readonly RatesLookupRepo _RatesLookupRepo;

        public DataAccessLayer(string dbString)
        {
            _dbString = dbString;

            _RatesBaseRepo = new RatesBaseRepo(dbString);
            _RatesCurrencyRepo = new RatesCurrencyRepo(dbString);
            _RatesLookupRepo = new RatesLookupRepo(dbString);
        }
    }
}
