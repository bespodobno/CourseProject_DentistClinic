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
    class AppointmentsRepository : IRepository<Appointment>
    {
        CourseProjectContext context;

        public AppointmentsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return context.Appointments.OrderBy(v=>v.Date);
        }
        public Appointment Get(int id)
        {
            return context.Appointments.Find(id);
        }
        public IEnumerable<Appointment> Find(Func<Appointment, bool> predicate)
        {
            return context.Appointments.Where(predicate).ToList();
        }
        public void Create(Appointment t)
        {
            context.Appointments.Add(t);
        }
        public void Update(Appointment t)
        {
            context.Entry<Appointment>(t).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var appointment = context.Appointments.Find(id);
            context.Appointments.Remove(appointment);
        }
    }
}
