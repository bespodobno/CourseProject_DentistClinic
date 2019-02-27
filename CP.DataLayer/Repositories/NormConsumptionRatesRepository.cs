using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.EFContext;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using System.Data.Entity;

namespace CP.DataLayer.Repositories
{
    class NormConsumptionRatesRepository : IRepository<NormConsumptionRate>
    {
        CourseProjectContext context;

        public NormConsumptionRatesRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<NormConsumptionRate> GetAll()
        {
            return context.NormConsumptionRates.Include(n=>n.PriceList);
        }
        public NormConsumptionRate Get(int id)
        {
            return context.NormConsumptionRates.Find(id);
        }
        public IEnumerable<NormConsumptionRate> Find(Func<NormConsumptionRate, bool> predicate)
        {
            return context.NormConsumptionRates.Where(predicate).ToList();
        }
        public void Create(NormConsumptionRate t)
        {
            context.NormConsumptionRates.Add(t);
        }
        public void Update(NormConsumptionRate t)
        {
            context.Entry<NormConsumptionRate>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var normconsumption = context.NormConsumptionRates.Find(id);
            context.NormConsumptionRates.Remove(normconsumption);
        }
    }

}
