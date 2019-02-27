using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IDoctorService
    {
        ObservableCollection<DoctorViewModel> GetAll();
        DoctorViewModel Get(int id);        
        void CreateDoctor(DoctorViewModel doctorModel);
        void DeleteDoctor(int doctorId);
        void UpdateDoctor(DoctorViewModel doctorModel);
    }
}
