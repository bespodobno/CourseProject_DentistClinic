using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IVisitService
    {
        ObservableCollection<VisitViewModel> GetAll();
        VisitViewModel Get(int id);        
        void CreateVisit(VisitViewModel visitModel);
        void DeleteVisit(int visitId);
        void UpdateVisit(VisitViewModel visitModel);
    }
}
