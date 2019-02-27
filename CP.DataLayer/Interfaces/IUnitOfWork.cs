using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.Entites;

namespace CP.DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Patient> Patients { get; }
        IRepository<Doctor> Doctors { get; }
        IRepository<Appointment> Appointments { get; }
        IRepository<DentalMaterial> DentalMaterials { get; }
        IRepository<Treatment> Treatments { get; }
        IRepository<Visit> Visits { get; }
        IRepository<MaterialConsumption> MaterialConsumptions { get; }
        IRepository<PriceList> PriceLists { get; }
        IRepository<NormConsumptionRate> NormConsumptionRates { get; }
        IRepository <Authorization> Authorizations { get; }

        void Save();
    }
}
