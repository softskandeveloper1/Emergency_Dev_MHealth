using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IServiceCostService
    {
        void AddOrUpdate(mp_service_costing service_cost);
        IQueryable<mp_service_costing> Get();
    }
}
