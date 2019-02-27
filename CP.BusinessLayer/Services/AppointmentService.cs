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
    public class AppointmentService:IAppointmentService
    {
        IUnitOfWork dataBase;
        public AppointmentService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
           
        }
        public ObservableCollection<AppointmentViewModel> GetAll()
        {
            var appointmentModels = Mapper.Map<ObservableCollection<AppointmentViewModel>>(dataBase.Appointments.GetAll());
            return appointmentModels;
        }
        public AppointmentViewModel Get(int id)
        {
            var appointmentEntity = dataBase.Appointments.Get(id);
            AppointmentViewModel appointmentModel = Mapper.Map<AppointmentViewModel>(appointmentEntity);
            return appointmentModel;
        }
        public void CreateAppointment(AppointmentViewModel appointmentModel)
        {
            var appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            //добавить запись на прием
            dataBase.Appointments.Create(appointmentEntity);
              //Сохранить изменения
              dataBase.Save();
        }
            public void DeleteAppointment(int appointmentId)
        {
            dataBase.Appointments.Delete(appointmentId);
            dataBase.Save();
        }
        public void UpdateAppointment(AppointmentViewModel appointmentModel)
        {
            Appointment appointmentEntityDB = dataBase.Appointments.Get(appointmentModel.AppointmentId);
           // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            appointmentEntityDB.Date = appointmentModel.Date;
            appointmentEntityDB.Time = appointmentModel.Time;
            appointmentEntityDB.DoctorId= appointmentModel.DoctorId;
            appointmentEntityDB.PatientId = appointmentModel.PatientId;

            dataBase.Appointments.Update(appointmentEntityDB);
            dataBase.Save();
        }
    }
}
