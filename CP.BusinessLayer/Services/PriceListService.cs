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
    public class PriceListService :IPriceListService
    {
        IUnitOfWork dataBase;

        public PriceListService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<PriceListViewModel> GetAll()
        {
            var priceListModels = Mapper.Map<ObservableCollection<PriceListViewModel>>(dataBase.PriceLists.GetAll());
            return priceListModels;
        }
        public PriceListViewModel Get(int id)
        {
            var pricelistEntity = dataBase.PriceLists.Get(id);
            PriceListViewModel pricelistModel = Mapper.Map<PriceListViewModel>(pricelistEntity);
            return pricelistModel;
        }
        public void CreatePriceList(PriceListViewModel pricelistModel)
        {
            //var pricelistEntity = Mapper.Map<PriceList>(pricelistModel);
            
            var pricelistEntity = new PriceList();
            pricelistEntity.Price = pricelistModel.Price;
            pricelistEntity.ServiceCode = pricelistModel.ServiceCode;
            pricelistEntity.ServiceName = pricelistModel.ServiceName;

            if (pricelistModel.NormConsumptionRates != null)
            {
                foreach (var norm in pricelistModel.NormConsumptionRates)
                {
                    // treatmentsDB.Add(new Treatment { PriceListId = t.PriceListId, Quantity = t.Quantity });
                    pricelistEntity.NormConsumptionRates.Add(
                        new NormConsumptionRate
                        {
                            TypeMaterial = norm.TypeMaterial,
                            NameMaterial = norm.NameMaterial,
                            Norm = norm.Norm,
                            PriceMaterial = norm.PriceMaterial,
                            AutoSelection = norm.AutoSelection
                        });
                }
            }
            dataBase.PriceLists.Create(pricelistEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeletePriceList(int pricelistId)
        {
            dataBase.PriceLists.Delete(pricelistId);
            dataBase.Save();
        }
        public void UpdatePriceList(PriceListViewModel pricelistModel)
        {
            PriceList pricelistEntityDB = dataBase.PriceLists.Get(pricelistModel.PriceListId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            pricelistEntityDB.Price = pricelistModel.Price;
            pricelistEntityDB.ServiceCode = pricelistModel.ServiceCode;
            pricelistEntityDB.ServiceName = pricelistModel.ServiceName;

            dataBase.PriceLists.Update(pricelistEntityDB);
            dataBase.Save();
        }
    }
}
