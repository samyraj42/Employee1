using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.NewFolder;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddStudentRecordForm.xaml
    /// </summary>
    public partial class AddEmployeedForm : UserControl
    {
        public AddEmployeedForm()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow(sender);
        }
        private void PhoneNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void CloseWindow(object sender)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var vm = (this.DataContext as EmployeeViewModel);
            if (vm.ValidateAndInsert())
            {
                Window parentWindow = Window.GetWindow((DependencyObject)this);
                string msg = vm.TAG == "N" ? "added" : "updated";
                MessageBox.Show("Category "+ msg + " successfully.");
                CloseWindow(sender);
            }
        }
    }
}
