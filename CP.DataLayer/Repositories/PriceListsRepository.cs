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
    class PriceListsRepository : IRepository<PriceList>
    {
        CourseProjectContext context;

        public PriceListsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<PriceList> GetAll()
        {           
            return context.PriceLists.Include(v => v.NormConsumptionRates)
                                     .Include(v => v.Treatments)
                                     .OrderBy(v=> 
                                         v.ServiceCode);
        }
        public PriceList Get(int id)
        {
            return context.PriceLists.Find(id);
        }
        public IEnumerable<PriceList> Find(Func<PriceList, bool> predicate)
        {
            return context.PriceLists.Include(v => v.NormConsumptionRates)
                                     .Include(v => v.Treatments).Where(predicate).ToList();
        }
        public void Create(PriceList t)
        {
            context.PriceLists.Add(t);
        }
        public void Update(PriceList t)
        {
            context.Entry<PriceList>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var pricelist = context.PriceLists.Find(id);
            context.PriceLists.Remove(pricelist);
        }
    }
}
