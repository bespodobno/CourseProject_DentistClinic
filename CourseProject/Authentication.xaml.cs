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
using System.ComponentModel;
using CP.BusinessLayer.Models;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        AuthorizationViewModel authorizationView = new AuthorizationViewModel();

        public Authentication()
        {
            InitializeComponent();
        }

        public Authentication(AuthorizationViewModel authorizationView): this()
        {
            this.authorizationView = authorizationView;           
           
        }
            private void btOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            authorizationView.User = tbUserName.Text;
            authorizationView.Password = tbPassword.Text;
            
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Программа будет закрыта.", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                //this.Close();
                Environment.Exit(0);
            }
        }
       
        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = ""; 
            }
        }
        void DataWindow_Closing(object sender, CancelEventArgs  e)
        {           
           if(this.DialogResult!=true  )
            {
                //this.Close();
                Environment.Exit(0);
            }            
        }
    }
}
