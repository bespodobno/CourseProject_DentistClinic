using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Interfaces;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using CP.DataLayer.Repositories;
using AutoMapper;

namespace CP.BusinessLayer.Services
{    
    public class PatientService :IPatientService
    {
        IUnitOfWork dataBase;
        public PatientService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;

        }
        public ObservableCollection<PatientViewModel> GetAll()
        {
            var patientModels = Mapper.Map<ObservableCollection<PatientViewModel>>(dataBase.Patients.GetAll());
            return patientModels;
        }
        public PatientViewModel Get(int id)
        {
            var patientEntity = dataBase.Patients.Get(id);
            PatientViewModel patientModel = Mapper.Map<PatientViewModel>(patientEntity);
            return patientModel;
        }
        //void AddAppointmentToPatient(int patientId, AppointmentViewModel appointmentModel, int doctorId)
        //{
        //    var patientEntity = dataBase.Patients.Get(patientId);            
        //    //отображение объекта AppointmentViewModel на объект Schedule
        //    var appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
        //    //добавить запись на прием
        //    patientEntity.Appointments.Add(appointmentEntity);
        //    //Сохранить изменения
        //    dataBase.Save();
        //}
        //void RemoveAppointmentFromPatient(int patientId, int appointmentId)
        //{
        //    dataBase.Appointments.Delete(appointmentId);
        //    dataBase.Save();
        //}
        public void CreatePatient(PatientViewModel patientModel)
        {
            var patientEntity = Mapper.Map<Patient>(patientModel);
            dataBase.Patients.Create(patientEntity);
            dataBase.Save();
        }
        public void DeletePatient(int patientId)
        {
            dataBase.Patients.Delete(patientId);
            dataBase.Save();
        }
        public void UpdatePatient(PatientViewModel patientModel)
        {
            Patient patientEntityDB = dataBase.Patients.Get(patientModel.PatientId);
            //  Patient patientEntity = Mapper.Map<PatientViewModel,Patient>(patientModel);//тут сваливается пишет null
            patientEntityDB.Name = patientModel.Name;
            patientEntityDB.DateOfBirth = patientModel.DateOfBirth;
            patientEntityDB.Sex = patientModel.Sex;
            patientEntityDB.Address = patientModel.Address;
            patientEntityDB.Contract = patientModel.Contract;
            patientEntityDB.Telephone = patientModel.Telephone;
            dataBase.Patients.Update(patientEntityDB);
            dataBase.Save();
        }

    }
}
