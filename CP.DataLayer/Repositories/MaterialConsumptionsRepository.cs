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
    class MaterialConsumptionsRepository :IRepository<MaterialConsumption>
    {
        CourseProjectContext context;

        public MaterialConsumptionsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<MaterialConsumption> GetAll()
        {
            return context.MaterialConsumptions;
        }
        public MaterialConsumption Get(int id)
        {
            return context.MaterialConsumptions.Find(id);
        }
        public IEnumerable<MaterialConsumption> Find(Func<MaterialConsumption, bool> predicate)
        {
            return context.MaterialConsumptions.Where(predicate).ToList();
        }
        public void Create(MaterialConsumption t)
        {
            context.MaterialConsumptions.Add(t);
        }
        public void Update(MaterialConsumption t)
        {
            context.Entry<MaterialConsumption>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var consumption = context.MaterialConsumptions.Find(id);
            context.MaterialConsumptions.Remove(consumption);
        }
    }
}
