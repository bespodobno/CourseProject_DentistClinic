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
    public class NormConsumptionRateService:INormConsumptionRateService
    {
        IUnitOfWork dataBase;

        public NormConsumptionRateService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<NormConsumptionRateViewModel> GetAll()
        {
            var normConsumptionModels = Mapper.Map<ObservableCollection<NormConsumptionRateViewModel>>(dataBase.NormConsumptionRates.GetAll());
            return normConsumptionModels;
        }
        public NormConsumptionRateViewModel Get(int id)
        {
            var normConsumptionEntity = dataBase.NormConsumptionRates.Get(id);
            var normConsumptionModel = Mapper.Map<NormConsumptionRateViewModel>(normConsumptionEntity);
            return normConsumptionModel;
        }
        public void CreateNormConsumption(NormConsumptionRateViewModel normConsumptionModel)
        {
            var normConsumptionEntity = Mapper.Map<NormConsumptionRate>(normConsumptionModel);
            //добавить запись на прием
            dataBase.NormConsumptionRates.Create(normConsumptionEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteNormConsumption(int id)
        {
            dataBase.NormConsumptionRates.Delete(id);
            dataBase.Save();
        }
        public void UpdateNormConsumption(NormConsumptionRateViewModel normConsumptionModel)
        {
            NormConsumptionRate normConsumptionEntityDB = dataBase.NormConsumptionRates.Get(normConsumptionModel.NormConsumptionRateId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            normConsumptionEntityDB.Norm = normConsumptionModel.Norm;
            normConsumptionEntityDB.TypeMaterial = normConsumptionModel.TypeMaterial;
            normConsumptionEntityDB.PriceListId = normConsumptionModel.PriceListId;           

            dataBase.NormConsumptionRates.Update(normConsumptionEntityDB);
            dataBase.Save();
        }
    }
}
