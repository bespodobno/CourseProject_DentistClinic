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
    class TreatmentsRepository : IRepository<Treatment>
    { 
     CourseProjectContext context;

        public TreatmentsRepository(CourseProjectContext context)
    {
        this.context = context;
    }

    public IEnumerable<Treatment> GetAll()
    {
        return context.Treatments.Include(v => v.MaterialConsumptions);
    }
    public Treatment Get(int id)
    {
        return context.Treatments.Find(id);
    }
    public IEnumerable<Treatment> Find(Func<Treatment, bool> predicate)
    {
        return context.Treatments.Include(v => v.MaterialConsumptions).Where(predicate).ToList();
    }
    public void Create(Treatment t)
    {
        context.Treatments.Add(t);
    }
    public void Update(Treatment t)
    {
        context.Entry<Treatment>(t).State = EntityState.Modified;
    }
    public void Delete(int id)
    {
        var treatment = context.Treatments.Find(id);
        context.Treatments.Remove(treatment);
    }
}
}
