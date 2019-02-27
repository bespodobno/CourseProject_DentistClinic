using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Interfaces;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using CP.DataLayer.Repositories;
using AutoMapper;
using System.Collections.ObjectModel;


namespace CP.BusinessLayer.Services
{
    public class TreatmentService:ITreatmentService
    {
        IUnitOfWork dataBase;

        public TreatmentService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<TreatmentViewModel> GetAll()
        {
            var treatmentModels = Mapper.Map<ObservableCollection<TreatmentViewModel>>(dataBase.Treatments.GetAll());
            return treatmentModels;
        }
        public TreatmentViewModel Get(int id)
        {
            var treatmentEntity = dataBase.Treatments.Get(id);
            TreatmentViewModel treatmentModel = Mapper.Map<TreatmentViewModel>(treatmentEntity);
            return treatmentModel;
        }
        public void CreateTreatment(TreatmentViewModel treatmentModel)
        {
            //var treatmentEntity = Mapper.Map<Treatment>(treatmentModel);
            
            var treatmentEntity = new Treatment();
            treatmentEntity.Quantity = treatmentModel.Quantity;
            treatmentEntity.PriceListId = treatmentModel.PriceListId;
            treatmentEntity.VisitId = treatmentModel.VisitId;

            if (treatmentModel.MaterialConsumptions != null)
            {
                foreach (var m in treatmentModel.MaterialConsumptions)
                {
                    // treatmentsDB.Add(new Treatment { PriceListId = t.PriceListId, Quantity = t.Quantity });
                    treatmentEntity.MaterialConsumptions.Add(
                        new MaterialConsumption
                        {
                            ConsumptionQuant = m.ConsumptionQuant,
                            DentalMaterialId = m.DentalMaterialId,
                            SellingPrice = m.SellingPrice,
                        });
                }
            }
            dataBase.Treatments.Create(treatmentEntity);

            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteTreatment(int treatmentId)
        {
            dataBase.Treatments.Delete(treatmentId);
            dataBase.Save();
        }
        public void UpdateTreatment(TreatmentViewModel treatmentModel)
        {
            Treatment treatmentEntityDB = dataBase.Treatments.Get(treatmentModel.TreatmentId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            treatmentEntityDB.Quantity = treatmentModel.Quantity;
            treatmentEntityDB.PriceListId = treatmentModel.PriceListId;
            treatmentEntityDB.VisitId = treatmentModel.VisitId;           

            dataBase.Treatments.Update(treatmentEntityDB);
            dataBase.Save();
        }
    }
}
