using System.Collections.ObjectModel;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IAppointmentService
    {
        ObservableCollection <AppointmentViewModel> GetAll();
        AppointmentViewModel Get(int id);        
        void CreateAppointment(AppointmentViewModel appointmentModel);
        void DeleteAppointment(int appointmentId);
        void UpdateAppointment(AppointmentViewModel appointmentModel);
    }
}
