using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Model;
using WpfApp1.NewFolder;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EmployeeForm : Window
    {
        public EmployeeForm()
        {
            InitializeComponent();

            EmployeeViewModel viewModel = new EmployeeViewModel();    
            this.DataContext = viewModel;
            txtSearch.eventHandler += StudentSearchFiler;
        }

        private void imgBtn_Click(object sender, RoutedEventArgs e)
        {
            var selecteditem = dgEmployees.SelectedItem as Employee;
            Window parentWindow = Window.GetWindow((DependencyObject)this);

            var vm = ((this.DataContext) as EmployeeViewModel);
            vm.Editstudent(selecteditem);
            vm.Title = "Update Student";
            vm.TAG = "U";

            Window window = new Window
            {
                WindowStyle = WindowStyle.None,
                Width = 800,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                DataContext = vm,
                Content = new AddEmployeedForm()
            };

            window.Owner = parentWindow;
            window.ShowDialog();
            dgEmployees.ItemsSource = vm.Students;
        }

        private void imgDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = (dgEmployees.SelectedItem as Employee).EmpID;
            var vm = ((this.DataContext) as EmployeeViewModel);
            vm.Deletestudent(id, this);
            dgEmployees.ItemsSource = vm.Students;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window
            {
                WindowStyle = WindowStyle.None,
                Width = 800,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                DataContext = new EmployeeViewModel(),
                Content = new AddEmployeedForm()
            };
            window.Owner = this;
            window.ShowDialog();

            var vm = ((this.DataContext) as EmployeeViewModel);
            dgEmployees.ItemsSource = vm.AddStudentsRecords();
        }
        public void StudentSearchFiler(object sender, EventArgs e)
        {
            var text = sender as string;

            ICollectionView cv = CollectionViewSource.GetDefaultView(dgEmployees.ItemsSource);
            cv.Filter = o =>
            {
                Employee obj = o as Employee;
                return (obj.Name.ToLower().Contains(text.ToLower().Trim()) || obj.ContactNumber.ToLower().Contains(text.ToLower().Trim()));
            };
        }
    }
}