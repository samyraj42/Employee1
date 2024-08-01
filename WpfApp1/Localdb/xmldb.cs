using System;
using System.Data.SqlClient;
using System.Data;
using WpfApp1.Model;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Net.Security;
using System.IO;
using System.Reflection.Metadata;
using System.Collections.ObjectModel;

namespace WpfApp1.DataBase
{
    public class xmldb
    {
        string rootPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        string xmlpath;
        XDocument xmldoc;
        public xmldb() 
        {
            this.LoadXml();
        }   
        public void LoadXml()
        {
            xmlpath = rootPath + @"\EmployeeDataBase.xml";
            if (File.Exists(xmlpath))
                xmldoc = XDocument.Load(xmlpath);
        }
        public  bool InsertStudent(Employee student)
        {
            try
            {              
                if (!File.Exists(xmlpath))
                {
                    var document =  new XDocument( new XElement("Employees",
                                    new XElement("Employee",
                                    new XElement("Id", student.EmpID + 1),
                                    new XElement("Name", student.Name),
                                    new XElement("Age", student.Age),
                                    new XElement("ContactNo", student.ContactNumber),
                                    new XElement("Nationality", student.Nationality))));                 
                    document.Save(xmlpath);
                }
                else if(File.Exists(xmlpath))
                {
                    var count = xmldoc.Descendants("Employee").Count() + 1;
                    XElement emp =  new XElement("Employee",
                                    new XElement("Id", count),
                                    new XElement("Name", student.Name),
                                    new XElement("Age", student.Age),
                                    new XElement("ContactNo", student.ContactNumber),
                                    new XElement("Nationality", student.Nationality));
                    xmldoc.Root.Add(emp);
                    xmldoc.Save(xmlpath);
                }              
                return true;
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "XML Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Code Exception");
            }
            return false;
        }
        public  bool UpdateStudent(Employee student)
        {
            try
            {
                XElement emp = xmldoc.Descendants("Employee").FirstOrDefault(p => p.Element("Id").Value ==Convert.ToString(student.EmpID));
                if (emp != null)
                {
                    emp.Element("Id").Value =Convert.ToString( student.EmpID);
                    emp.Element("Name").Value = student.Name;
                    emp.Element("Age").Value = student.Age;
                    emp.Element("ContactNo").Value = student.Age;
                    emp.Element("Nationality").Value = student.Nationality;
                    //xmldoc.Root.Add(emp);
                    xmldoc.Save(xmlpath);
                }
                return true;
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "XML Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Code Exception");
            }
            return false;
        }
        public  bool DeleteStudent(int id)
        {
            try
            {
                XElement emp = xmldoc.Descendants("Employee").FirstOrDefault(p => p.Element("Id").Value ==Convert.ToString( id));
                if (emp != null)
                {
                    emp.Remove();
                    xmldoc.Save(xmlpath);
                }
                return true;
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "SQL Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Code Exception");
            }
            return false;
        }
        public ObservableCollection<Employee> GetStudentRecords(ObservableCollection<Employee> employees)
        {
            if(xmldoc is null) return new ObservableCollection<Employee>();
            this.LoadXml();
            var datas = xmldoc.Descendants("Employee").Select(p => new
            {
                Id = p.Element("Id").Value,
                Name = p.Element("Name").Value,
                Age = p.Element("Age").Value,
                ContactNo = p.Element("ContactNo").Value,
                Nationality = p.Element("Nationality").Value,
            }).OrderBy(p => p.Id);
            foreach(var dat in datas)
            {
                employees.Add(new Employee()
                {
                    EmpID =Convert.ToInt32(dat.Id),
                    Name = dat.Name,
                    Age = dat.Age,
                    ContactNumber = dat.ContactNo,
                    Nationality = dat.Nationality,
                });
            }
            return employees;
        }
    }
}
