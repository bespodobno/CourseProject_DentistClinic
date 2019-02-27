using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Models;


namespace CP.BusinessLayer.Interfaces
{
   public interface IPatientService
    {
        ObservableCollection<PatientViewModel> GetAll();
        PatientViewModel Get(int id);        
        void CreatePatient(PatientViewModel patientModel);
        void DeletePatient(int patientId);
        void UpdatePatient(PatientViewModel patientModel);

    }
}
