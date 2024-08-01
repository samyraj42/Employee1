using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfApp1.DataBase;
using WpfApp1.Model;
using System.Data;
using WpfApp1.Commands;
using System.Text.RegularExpressions;

namespace WpfApp1.NewFolder
{
    public class EmployeeViewModel : Employee
    {
        public ObservableCollection<Employee> Students { get; set; }
        xmldb _xmldb;
        private string _title = "Add Student";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _windowtag = "N";
        public string TAG
        {
            get { return _windowtag; }
            set { SetProperty(ref _windowtag, value); }
        }
        public EmployeeViewModel() 
        {
            _xmldb = new xmldb();
            this.Students = new ObservableCollection<Employee>();
            this.Students.CollectionChanged += CollectionChanged;
            this.AddStudentsRecords();           
        }
        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                for (int i = 0; i < this.Students.Count; i++)
                {
                    this.Students[i].NO = i + 1;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                for (int i = 0; i < this.Students.Count; i++)
                {
                    this.Students[i].NO = i + 1;
                }
            }
        }
        public ObservableCollection<Employee> AddStudentsRecords()
        {
            this.Students.Clear();
            return _xmldb.GetStudentRecords(this.Students);
        }
        public void Editstudent(Employee emp)
        {
            this.Name = emp.Name;
            this.Age = emp.Age;
            this.Nationality = emp.Nationality;
            this.ContactNumber = emp.ContactNumber;
            this.EmpID = emp.EmpID;
        }
        public void Deletestudent(int id, object obj)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)obj);

            MessageBoxResult result = MessageBox.Show("Do you want delete employee", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _xmldb.DeleteStudent(id);
                MessageBox.Show("Employee deleted successfully.");
                this.AddStudentsRecords();
            }
        }
       
        public bool ValidateAndInsert()
        {
            bool result = false;
            if (string.IsNullOrEmpty(this.Name))
            {
                this.Error = "Please enter name";
            }
            else if (string.IsNullOrEmpty(this.Age))
            {
                this.Error = "Please enter age";
            }
            else if (string.IsNullOrEmpty(this.ContactNumber) || !this.IsPhoneNumber(this.ContactNumber))
            {
                this.Error = "Please enter valid phone number";
            }
            else if (string.IsNullOrEmpty(this.Nationality))
            {
                this.Error = "Please enter nationality";
            }           
            else if (this.Students.Any(x => x.EmpID== this.EmpID) && this.TAG == "N")
            {
                this.Error = "student alreday exits.";
            }
            else
            {
                this.Error = string.Empty;
                if (this.TAG == "N")
                    result = _xmldb.InsertStudent(this);
                else if (this.TAG == "U")
                    result = _xmldb.UpdateStudent(this);
            }
            this.AddStudentsRecords();
            return result;
        }
        private  bool IsPhoneNumber(string number)
        {
            Regex expr = new Regex(@"^[0-9]{10}$");
            if (expr.IsMatch(number))
            {
                return true;
            }
            else return false;
        }
    }
}
