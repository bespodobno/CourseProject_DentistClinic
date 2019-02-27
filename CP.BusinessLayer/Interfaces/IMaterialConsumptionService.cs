using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IMaterialConsumptionService
    {
        ObservableCollection<MaterialConsumptionViewModel> GetAll();
        MaterialConsumptionViewModel Get(int id);
        void CreateConsumption(MaterialConsumptionViewModel consumptionModel);
        void DeleteConsumption(int consumptionId);
        void UpdateConsumption(MaterialConsumptionViewModel consumptionModel);
    }
}
