using EmployeeCase.Business;
using EmployeeCase.Business.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeCase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        public MainWindow(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            InitializeComponent();

            foreach (var item in _employeeRepository.Get())
            {
                dataGridEmployee.Items.Add(item);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var row = (Employee)dataGridEmployee.SelectedItem;
            MessageBox.Show("ID: " + row.id + Environment.NewLine +
                            "Name: " + row.name + Environment.NewLine +
                            "Age: " + row.age + Environment.NewLine +
                            "Salary: " + row.salary);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = (Employee)dataGridEmployee.SelectedItem;
            if (_employeeRepository.Delete(row.id))
            {
                dataGridEmployee.Items.RemoveAt(dataGridEmployee.SelectedIndex);
                MessageBox.Show("Record is deleted.");
            }
            else
            {
                MessageBox.Show("An error occured.");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow dialog = new EmployeeWindow(_employeeRepository);
            dialog.ShowDialog();
        }
    }
}
