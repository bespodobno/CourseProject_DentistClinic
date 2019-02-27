using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IDentalMaterialService
    {
        ObservableCollection<DentalMaterialViewModel> GetAll();
        DentalMaterialViewModel Get(int id);       
        void CreateMaterial(DentalMaterialViewModel materialModel);
        void DeleteMaterial(int materialId);
        void UpdateMaterial(DentalMaterialViewModel materialModel);
    }
}
