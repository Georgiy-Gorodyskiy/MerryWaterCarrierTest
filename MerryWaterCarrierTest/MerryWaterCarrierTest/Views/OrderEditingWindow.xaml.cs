using MerryWaterCarrierTest.Models;
using MerryWaterCarrierTest.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для TagEditingWindow.xaml
    /// </summary>
    public partial class OrderEditingWindow : Window
    {
        private Order? curentOrder;
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public OrderEditingWindow(bool isEdit = false)
        {
            try
            {
                InitializeComponent();
                curentOrder = isEdit ? service.CurentOrder : null;
                EmployeeCmb.ItemsSource = service.Employees;
                EmployeeCmb.DisplayMemberPath = "DisplayName";
                TagsList.ItemsSource = service.Tags;
                TagsList.DisplayMemberPath = "Name";
                if (curentOrder != null)
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
            NumberTxt.Text = curentOrder?.Number.ToString();
            NameTxt.Text = curentOrder?.Name;
            EmployeeCmb.SelectedItem = curentOrder?.Employee;
            if(curentOrder.Tags != null)
            {
                foreach(var tag in curentOrder.Tags)
                {
                    TagsList.SelectedItems.Add(TagsList.Items[TagsList.Items.IndexOf(tag)]);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curentOrder == null)
                {
                    curentOrder = new Order();
                    curentOrder.Tags = new List<Tag>();
                }
                curentOrder.Number = int.Parse(NumberTxt.Text);
                curentOrder.Name = NameTxt.Text;
                curentOrder.Employee = (Employee)EmployeeCmb.SelectedItem;

                foreach(var tag in curentOrder.Tags)
                {
                    tag.Orders.Remove(curentOrder);
                }
                curentOrder.Tags = new List<Tag>();

                foreach(Tag tag in TagsList.SelectedItems)
                {
                    curentOrder.Tags.Add(tag);
                    tag.Orders.Add(curentOrder);
                }


                service.EditOrder(curentOrder);
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

        private void NumberTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
        }

    }
}
