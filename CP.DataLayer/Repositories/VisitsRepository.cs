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
    class VisitsRepository:IRepository<Visit>
    {
        CourseProjectContext context;

        public VisitsRepository(CourseProjectContext context)
        {
            this.context = context;
            //context.Database.Log = Console.Write;
        }

        public IEnumerable<Visit> GetAll()
        {
            IEnumerable<Visit> all = context.Visits
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .Include(v => v.Treatments)                
                .Include(v => v.Treatments.Select(x => x.PriceList))
                .Include(v => v.Treatments.Select(x=> x.MaterialConsumptions))
                .ToList();
            
            return all;
        }
        public Visit Get(int id)
        {
            return context.Visits.Find(id);
        }
        public IEnumerable<Visit> Find(Func<Visit, bool> predicate)
        {
            return context.Visits.Include(g => g.Treatments).
                                  Include(v => v.Treatments.Select(x => x.MaterialConsumptions))
                                  .Where(predicate)
                                  .OrderBy(v => v.VisitDate).ToList();
        }
        public void Create(Visit t)
        {
            context.Visits.Add(t);
        }
        public void Update(Visit t)
        {
            if (t == null) return;
            
            context.Entry<Visit>(t).State = EntityState.Modified;
            
        }
        public void Delete(int id)
        {
            var visit = context.Visits.Find(id);
            context.Visits.Remove(visit);// что именно удалится?
        }
    }
}
