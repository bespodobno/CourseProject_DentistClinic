using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface INormConsumptionRateService
    {
        ObservableCollection<NormConsumptionRateViewModel> GetAll();
        NormConsumptionRateViewModel Get(int id);
        void CreateNormConsumption(NormConsumptionRateViewModel normConsumptionModel);
        void DeleteNormConsumption(int normConsumptionId);
        void UpdateNormConsumption(NormConsumptionRateViewModel normConsumptionModel);
    }
}
