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
    class DoctorsRepository :IRepository<Doctor>
    {
        CourseProjectContext context;

        public DoctorsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<Doctor> GetAll() // public IEnumerable<Doctor> GetAll(params string[] includes)
        {
            //IQueryable<Doctor> query = context.Set<Doctor>();
            //foreach (var include in includes)
            //    query = query.Include(include);

            //return query;
            return context.Doctors.Include(g => g.Appointments).Include(g => g.Visits);
            
        }
        public Doctor Get(int id)
        {
            return context.Doctors.Find(id);
        }
        public IEnumerable<Doctor> Find(Func<Doctor, bool> predicate)
        {
            return context.Doctors.Include(g => g.Appointments).Include(g => g.Visits).Where(predicate).ToList();
        }
        public void Create(Doctor t)
        {
            context.Doctors.Add(t);
        }
        public void Update(Doctor t)
        {
            context.Entry<Doctor>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var doctor = context.Doctors.Find(id);
            context.Doctors.Remove(doctor);
        }
    }
}
