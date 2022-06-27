using EmployeeCase.Business;
using EmployeeCase.Business.Models;
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
using System.Windows.Shapes;

namespace EmployeeCase
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeWindow(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee
            {
                name = textBoxName.Text,
                salary = Convert.ToDecimal(textBoxSalary.Text),
                age = Convert.ToDecimal(textBoxAge.Text)
            };

            if(_employeeRepository.Create(emp))
            {
                MessageBox.Show("Employee is added.");
            }
            else
            {
                MessageBox.Show("An error occured.");
            }
        }

        private void textBoxNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9]+"); 
        private static bool IsAllowed(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}
