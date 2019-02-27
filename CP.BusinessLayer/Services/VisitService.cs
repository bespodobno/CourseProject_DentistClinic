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
   public  class VisitService: IVisitService
    {
        IUnitOfWork dataBase;

        public VisitService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<VisitViewModel> GetAll()
        {
            var visitModels = Mapper.Map<ObservableCollection<VisitViewModel>>(dataBase.Visits.GetAll());
            return visitModels;
        }
        public VisitViewModel Get(int id)
        {
            var visitEntity = dataBase.Visits.Get(id);
            VisitViewModel visitModel = Mapper.Map<VisitViewModel>(visitEntity);
            return visitModel;
        }
        public void CreateVisit(VisitViewModel visitModel)
        {
            // var visitEntity = Mapper.Map<Visit>(visitModel);
            Visit visitEntity = new Visit();
            visitEntity.VisitDate = visitModel.VisitDate;
            visitEntity.VisitNumber = visitModel.VisitNumber;
            visitEntity.Diagnose = visitModel.Diagnose;
            visitEntity.MaterialCost = visitModel.MaterialCost;
            visitEntity.VisitCost = visitModel.VisitCost;
            visitEntity.TotalCost = visitModel.TotalCost;
            visitEntity.DoctorId = visitModel.DoctorId;
            visitEntity.PatientId = visitModel.PatientId;

            if (visitModel.Treatments != null)
            {
                for (int i = 0; i < visitModel.Treatments.Count; i++)
                {
                    visitEntity.Treatments.Add(new Treatment
                    {
                        PriceListId = visitModel.Treatments[i].PriceListId,
                        Quantity = visitModel.Treatments[i].Quantity
                    });
                    if (visitModel.Treatments[i].MaterialConsumptions != null)
                    {
                        for (int j = 0; j < visitModel.Treatments[i].MaterialConsumptions.Count; j++)
                        {
                            MaterialConsumptionViewModel m = visitModel.Treatments[i].MaterialConsumptions[j];

                            visitEntity.Treatments[i].MaterialConsumptions.Add(new MaterialConsumption
                            {
                                ConsumptionQuant = m.ConsumptionQuant,
                                DentalMaterialId = m.DentalMaterialId,
                                SellingPrice = m.SellingPrice
                            });
                            
                        }
                    }
                }
            }
            
            //добавить запись на прием
            dataBase.Visits.Create(visitEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteVisit(int visitId)
        {
            dataBase.Visits.Delete(visitId);
            
            dataBase.Save();
        }
        public void UpdateVisit(VisitViewModel visitModel)
        {
            Visit visitEntityDB = dataBase.Visits.Get(visitModel.VisitId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            visitEntityDB.VisitDate = visitModel.VisitDate;
            visitEntityDB.Diagnose = visitModel.Diagnose;
            visitEntityDB.VisitCost = visitModel.VisitCost;
            visitEntityDB.MaterialCost = visitModel.MaterialCost;
            visitEntityDB.TotalCost = visitModel.TotalCost;
            visitEntityDB.DoctorId = visitModel.DoctorId;
            visitEntityDB.PatientId = visitModel.PatientId;


           

            dataBase.Visits.Update(visitEntityDB);
            dataBase.Save();
        }
    }
}
//foreach (var t in treatmentsOld)
//{
//    //удалить расход материалов
//    foreach (var m in t.MaterialConsumptions)
//    {
//        materialConsumptionService.DeleteConsumption(m.MaterialConsumptionId);
//    }
//    treatmentService.DeleteTreatment(t.TreatmentId);

//    //добавить материалы на склад
//}

//foreach (var t in visitModel.Treatments)
//{
//    treatmentService.CreateTreatment(t);
//    //и создать все потребление материалов
//    //добавить расход материалов на складе
//}