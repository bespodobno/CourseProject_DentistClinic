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
    public class DentalMaterialService:IDentalMaterialService
    {
        IUnitOfWork dataBase;

        public DentalMaterialService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }

        public ObservableCollection<DentalMaterialViewModel> GetAll()
        {
            var dentalMaterialModels = Mapper.Map<ObservableCollection<DentalMaterialViewModel>>(dataBase.DentalMaterials.GetAll());//.OrderBy(t=>t.PurchaseDate)
            return dentalMaterialModels;
        }
        public DentalMaterialViewModel Get(int id)
        {
            var dentalMaterialEntity = dataBase.DentalMaterials.Get(id);
            DentalMaterialViewModel dentalMaterialModel = Mapper.Map<DentalMaterialViewModel>(dentalMaterialEntity);
            return dentalMaterialModel;
        }
        public void CreateMaterial(DentalMaterialViewModel dentalMaterialModel)
        {
            var dentalMaterialEntity = Mapper.Map<DentalMaterial>(dentalMaterialModel);
          
            dataBase.DentalMaterials.Create(dentalMaterialEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteMaterial(int dentalMaterialId)
        {
            dataBase.DentalMaterials.Delete(dentalMaterialId);
            dataBase.Save();
        }
        public void UpdateMaterial(DentalMaterialViewModel dentalMaterialModel)
        {
            DentalMaterial dentalMaterialEntityDB = dataBase.DentalMaterials.Get(dentalMaterialModel.DentalMaterialId);
            dentalMaterialEntityDB.PurchaseDate = dentalMaterialModel.PurchaseDate;
            dentalMaterialEntityDB.MaterialName = dentalMaterialModel.MaterialName;
            dentalMaterialEntityDB.MaterialPrice = dentalMaterialModel.MaterialPrice;
            dentalMaterialEntityDB.MaterialQuantity = dentalMaterialModel.MaterialQuantity;
            dentalMaterialEntityDB.Unit = dentalMaterialModel.Unit;
            dentalMaterialEntityDB.TypeMaterial = dentalMaterialModel.TypeMaterial;         

            dataBase.DentalMaterials.Update(dentalMaterialEntityDB);
            dataBase.Save();
        }
    }
}
