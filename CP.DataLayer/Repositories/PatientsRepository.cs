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
    class PatientsRepository: IRepository<Patient>
    {
        CourseProjectContext context;
        public PatientsRepository(CourseProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return context.Patients.Include(g => g.Appointments).Include(g => g.Visits).OrderBy(v => v.Name);
        }
        public Patient Get(int id)
        {
            return context.Patients.Find(id);
        }
        public  IEnumerable<Patient> Find(Func<Patient, bool> predicate)
        {
            return context.Patients.Include(g => g.Appointments).Include(g => g.Visits).Where(predicate).ToList();
        }
        public void Create(Patient t)
        {
            context.Patients.Add(t);
        }
        public void Update(Patient t)
        {
            //context.Patients.Attach(t);
            //context.Patients.ToList().ForEach(e => {
            //    Console.WriteLine(context.Entry<Patient>(e).State.ToString());
            //} );
            //Console.WriteLine(context.Entry<Patient>(t).State.ToString());// = EntityState.Modified;

            //if (context.Entry<Patient>(t).State == EntityState.Detached || context.Entry<Patient>(t).State == EntityState.Modified)
            //{
            context.Entry<Patient>(t).State = EntityState.Modified; //do it here

            //context.Set<Patient>().Attach(t); //attach

            //   
            //}
        }
        public void Delete(int id)
        {
            var patient = context.Patients.Find(id);
            context.Patients.Remove(patient);
        }
    }
}
