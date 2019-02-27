using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        AppointmentViewModel appointment;

        public EditAppointment()
        {
            InitializeComponent();
        }

        public EditAppointment(AppointmentViewModel appointment, ObservableCollection<PatientViewModel> patientsModel,
              ObservableCollection<DoctorViewModel> doctorsModel) : this()
        {
            this.appointment = appointment;            
            cbPatientName.ItemsSource = patientsModel;
            cbDoctor.ItemsSource = doctorsModel;
            this.DataContext = appointment;


        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            var doctor = cbDoctor.SelectedItem as DoctorViewModel;

            if (doctor != null)
            {
                var patient = cbPatientName.SelectedItem as PatientViewModel;

                if (patient != null)
                {
                    appointment.DoctorId = doctor.DoctorId;
                    appointment.PatientId = patient.PatientId;

                    if (dpDate.SelectedDate != null)
                    {
                        var res = AdditionalFunctions.IsDateTime(dpDate.SelectedDate.Value.ToShortDateString());

                        if (res)
                        {
                            appointment.Date = (DateTime)dpDate.SelectedDate;

                            var result = AdditionalFunctions.IsTimeSpan(TbtbTime.Text);

                            if (!result)
                            {
                                MessageBox.Show("Неправильный формат времени!");
                            }
                            else
                            {
                                appointment.Time = TimeSpan.Parse(TbtbTime.Text);
                                this.DialogResult = true;
                            }
                        }
                        else MessageBox.Show("Введите правильный формат даты!");
                    }
                    else MessageBox.Show("Введите дату!");
                }
                else MessageBox.Show("Выберите пациента!");
            }
            else MessageBox.Show("Выберите доктора!");            
        }
    }
}
