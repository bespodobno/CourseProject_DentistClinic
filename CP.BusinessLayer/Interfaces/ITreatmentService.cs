using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface ITreatmentService
    {
        ObservableCollection<TreatmentViewModel> GetAll();
        TreatmentViewModel Get(int id);
        void CreateTreatment(TreatmentViewModel treatmentModel);
        void DeleteTreatment(int treatmentId);
        void UpdateTreatment(TreatmentViewModel treatmentModel);
    }
}
