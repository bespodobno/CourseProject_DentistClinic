using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CP.DataLayer.Entites;

namespace CP.DataLayer.EFContext
{
    public class CourseProjectContext : DbContext
    {
        public CourseProjectContext(string name) : base(name)
        {
            Database.SetInitializer(new CourseProjectInitializer());
        }
        public DbSet<Patient>             Patients { get; set; }
        public DbSet<Doctor>              Doctors { get; set; }
        public DbSet<Appointment>         Appointments { get; set; }
        public DbSet<DentalMaterial>      DentalMaterials { get; set; }
        public DbSet<Visit>               Visits { get; set; }
        public DbSet<NormConsumptionRate> NormConsumptionRates { get; set; }
        public DbSet<PriceList>           PriceLists { get; set; }
        public DbSet<Treatment>           Treatments { get; set; }
        public DbSet<MaterialConsumption> MaterialConsumptions { get; set; }
        public DbSet<Authorization>       Authorizations { get; set; }
    }
}
