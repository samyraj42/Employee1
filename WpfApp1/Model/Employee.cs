using System;

namespace WpfApp1.Model
{
    public  class Employee : ModelBase
    {
        private int _NO;
        public int NO
        {
            get { return _NO; }
            set { SetProperty(ref _NO, value); }
        }
        private int _empid;
        public int EmpID
        {
            get { return _empid; }
            set { SetProperty(ref _empid, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
       
        private string _age;
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }
        private string _contactnumber;
        public string ContactNumber
        {
            get { return _contactnumber; }
            set { SetProperty(ref _contactnumber, value); }
        }
        private string _nationality;
        public string Nationality
        {
            get { return _nationality; }
            set { SetProperty(ref _nationality, value); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { SetProperty(ref _error, value); }
        }
    }
}
