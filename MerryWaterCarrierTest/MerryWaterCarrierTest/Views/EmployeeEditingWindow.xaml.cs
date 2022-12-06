using MerryWaterCarrierTest.DisplayConverters;
using MerryWaterCarrierTest.Models;
using MerryWaterCarrierTest.Models.Enums;
using MerryWaterCarrierTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditingWindow.xaml
    /// </summary>
    public partial class EmployeeEditingWindow : Window
    {
        private Employee? curentEmployee;
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        private List<Tuple<GenderEnum, string>> genderList;
        public EmployeeEditingWindow(bool isEdit = false)
        {
            try
            {
                InitializeComponent();
                
                curentEmployee = isEdit ? service.CurentEmployee : null;
                var enumDescriptionConverter = new EnumDescriptionConverter();
                genderList = new List<Tuple<GenderEnum, string>>();
                foreach (GenderEnum gender in System.Enum.GetValues(typeof(GenderEnum)))
                {
                    genderList.Add(new Tuple<GenderEnum, string>(gender, enumDescriptionConverter.GetEnumDescription(gender)));
                }
                GenderCmb.ItemsSource = genderList;
                GenderCmb.DisplayMemberPath = "Item2";
                GenderCmb.SelectedIndex = 0;
                DepartmentCmb.ItemsSource = service.Departments;
                DepartmentCmb.DisplayMemberPath = "Name";
                if (curentEmployee != null)
                {
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load()
        {
            NameTxt.Text = curentEmployee?.Name;
            SurnameTxt.Text = curentEmployee?.Surname;
            PatronymicTxt.Text = curentEmployee?.Patronymic;
            GenderCmb.SelectedItem = genderList.FirstOrDefault(x=> x.Item1 == curentEmployee?.Gender);
            DepartmentCmb.SelectedItem = curentEmployee?.Department;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curentEmployee == null)
                    curentEmployee = new Employee();
                curentEmployee.Name = NameTxt.Text;
                curentEmployee.Surname = SurnameTxt.Text;
                curentEmployee.Patronymic = PatronymicTxt.Text;
                curentEmployee.Gender = ((Tuple<GenderEnum, string>)GenderCmb.SelectedItem).Item1;
                curentEmployee.Department = (Department)DepartmentCmb.SelectedItem;
                service.EditEmployee(curentEmployee);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
