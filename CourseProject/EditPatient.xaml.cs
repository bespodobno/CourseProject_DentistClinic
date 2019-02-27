using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditPatient.xaml
    /// </summary>
    public partial class EditPatient : Window
    {
        PatientViewModel patient;
        public EditPatient()
        {
            InitializeComponent();
            cbSex.Items.Add(Sex.жен);
            cbSex.Items.Add(Sex.муж);
        }
        public EditPatient(PatientViewModel patient) : this()
        {
            this.patient = patient;
            this.DataContext = patient;
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (cbSex.SelectedItem != null )
            {
                string name = tbName.Text.ToString();

                if (name.Length != 0)
                {
                    if (dpDateOfBirth.SelectedDate != null)
                    {

                        bool resultDate = AdditionalFunctions.IsDateTime(dpDateOfBirth.SelectedDate.Value.ToShortDateString());


                        if (!resultDate)
                        {
                            MessageBox.Show("Введите правильный формат даты");
                        }

                        else
                        {
                            this.DialogResult = true;
                        }
                    }
                    else MessageBox.Show("Введите дату!");
                }
                else MessageBox.Show("Введите имя!");
            }
            else MessageBox.Show("Выберите пол");
                       
        }
    }
}
