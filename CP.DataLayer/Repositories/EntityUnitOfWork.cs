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
    public class EntityUnitOfWork : IUnitOfWork
    {
        CourseProjectContext context;
        PatientsRepository patientsRepository;
        DoctorsRepository doctorsRepository;
        AppointmentsRepository appointmentsRepository;
        DentalMaterialsRepository dentalMaterialsRepository;
        NormConsumptionRatesRepository normConsumptionRatesRepository;
        VisitsRepository visitsRepository;
        TreatmentsRepository treatmentsRepository;
        PriceListsRepository priceListsRepository;
        MaterialConsumptionsRepository materialConsumptionsRepository;
        AuthorizationRepository authorizationRepository;

        public EntityUnitOfWork(string name)
        {
            context = new CourseProjectContext(name);
        }

        public IRepository<Patient> Patients
        {
            get
            {
                if (patientsRepository == null)
                {
                    patientsRepository = new PatientsRepository(context);

                }
                return patientsRepository;
            }
        }

        public IRepository<Doctor> Doctors
        {
            get
            {
                if (doctorsRepository == null)
                {
                    doctorsRepository = new DoctorsRepository(context);

                }
                return doctorsRepository;
            }
        }
        public IRepository<Appointment> Appointments
        {
            get
            {
                if (appointmentsRepository == null)
                {
                    appointmentsRepository = new AppointmentsRepository(context);

                }
                return appointmentsRepository;
            }
        }

        public IRepository<Visit> Visits
        {
            get
            {
                if (visitsRepository == null)
                {
                    visitsRepository = new VisitsRepository(context);

                }
                return visitsRepository;
            }
        }

        public IRepository<PriceList> PriceLists
        {
            get
            {
                if (priceListsRepository == null)
                {
                    priceListsRepository = new PriceListsRepository(context);

                }
                return priceListsRepository;
            }
        }

        public IRepository<DentalMaterial> DentalMaterials
        {
            get
            {
                if (dentalMaterialsRepository == null)
                {
                    dentalMaterialsRepository = new DentalMaterialsRepository(context);

                }
                return dentalMaterialsRepository;
            }
        }

        public IRepository<NormConsumptionRate> NormConsumptionRates
        {
            get
            {
                if (normConsumptionRatesRepository == null)
                {
                    normConsumptionRatesRepository = new NormConsumptionRatesRepository(context);

                }
                return normConsumptionRatesRepository;
            }
        }

        public IRepository<Treatment> Treatments
        {
            get
            {
                if (treatmentsRepository == null)
                {
                    treatmentsRepository = new TreatmentsRepository(context);

                }
                return treatmentsRepository;
            }
        }

        public IRepository<MaterialConsumption> MaterialConsumptions
        {
            get
            {
                if (materialConsumptionsRepository == null)
                {
                    materialConsumptionsRepository = new MaterialConsumptionsRepository(context);

                }
                return materialConsumptionsRepository;
            }
        }

        public IRepository<Authorization> Authorizations
        {
            get
            {
                if (authorizationRepository == null)
                {
                    authorizationRepository = new AuthorizationRepository(context);

                }
                return authorizationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            //garbage collector
            GC.SuppressFinalize(this);
        }
    }
}
