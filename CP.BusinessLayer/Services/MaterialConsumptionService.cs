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
   public  class MaterialConsumptionService:IMaterialConsumptionService
    {
        IUnitOfWork dataBase;

        public MaterialConsumptionService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<MaterialConsumptionViewModel> GetAll()
        {
            var materialConsumptionModels = Mapper.Map<ObservableCollection<MaterialConsumptionViewModel>>(dataBase.MaterialConsumptions.GetAll());
            return materialConsumptionModels;
        }
        public MaterialConsumptionViewModel Get(int id)
        {
            var materialConsumptionEntity = dataBase.MaterialConsumptions.Get(id);
            var materialConsumptionModel = Mapper.Map<MaterialConsumptionViewModel>(materialConsumptionEntity);
            return materialConsumptionModel;
        }
        public void CreateConsumption(MaterialConsumptionViewModel materialConsumptionModel)
        {
            var materialConsumptionEntity = Mapper.Map<MaterialConsumption>(materialConsumptionModel);
            //добавить запись на прием
            dataBase.MaterialConsumptions.Create(materialConsumptionEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteConsumption(int id)
        {
            dataBase.MaterialConsumptions.Delete(id);
            dataBase.Save();
        }
        public void UpdateConsumption(MaterialConsumptionViewModel materialConsumptionModel)
        {
            MaterialConsumption materialConsumptionEntityDB = dataBase.MaterialConsumptions.Get(materialConsumptionModel.MaterialConsumptionId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            materialConsumptionEntityDB.ConsumptionQuant = materialConsumptionModel.ConsumptionQuant;
            materialConsumptionEntityDB.SellingPrice = materialConsumptionModel.SellingPrice;
            materialConsumptionEntityDB.TreatmentId = materialConsumptionModel.TreatmentId;
            materialConsumptionEntityDB.DentalMaterialId = materialConsumptionModel.DentalMaterialId;

            dataBase.MaterialConsumptions.Update(materialConsumptionEntityDB);
            dataBase.Save();
        }
    }
}
