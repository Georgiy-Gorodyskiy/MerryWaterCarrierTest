using MerryWaterCarrierTest.DBContexts;
using MerryWaterCarrierTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerryWaterCarrierTest.Services
{
    public class WaterCarrierService
    {
        private WaterCarrierDBContext context;
        public List<Employee> Employees { get { return context.Employees.Include(x => x.Department).ToList(); } }
        public List<Department> Departments { get { return context.Departments.Include(x => x.Leader).ToList(); } }
        public List<Order> Orders { get { return context.Orders.Include(x => x.Employee).Include(x => x.Tags).ToList(); } }
        public List<Tag> Tags { get { return context.Tags.Include(x => x.Orders).ToList(); } }

        public Employee CurentEmployee { get; private set; }
        public Department CurentDepartament { get; private set; }
        public Order CurentOrder { get; private set; }
        public Tag CurentTag { get; private set; }

        public event EventHandler EmployeesUpdated;
        public event EventHandler DepartmentsUpdated;
        public event EventHandler OrdersUpdated;
        public event EventHandler TagsUpdated;

        public event EventHandler CurentEmployeeChanged;
        public event EventHandler CurentDepartmentChanged;
        public event EventHandler CurentOrderChanged;
        public event EventHandler CurentTagChanged;

        public WaterCarrierService(WaterCarrierDBContext waterCarrierDBContext)
        {
            context = waterCarrierDBContext;
        }

        public void ChangeCurentEmployee(Employee? employee)
        {
            CurentEmployee = employee;
            CurentEmployeeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ChangeCurentDepartment(Department? department)
        {
            CurentDepartament = department;
            CurentDepartmentChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ChangeCurentOrder(Order? order)
        {
            CurentOrder = order;
            CurentOrderChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void ChangeCurentTag(Tag? tag)
        {
            CurentTag = tag;
            CurentTagChanged?.Invoke(this, EventArgs.Empty);
        }

        public void EditEmployee(Employee employee)
        {
            if (!Employees.Contains(employee))
            {
                context.Employees.Add(employee);
            }
            context.SaveChanges();

            EmployeesUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void EditDepartament(Department departament)
        {
            if (!Departments.Contains(departament))
            {
                context.Departments.Add(departament);
            }
            context.SaveChanges();

            DepartmentsUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void EditOrder(Order order)
        {
            if (!Orders.Contains(order))
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();

            OrdersUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void EditTag(Tag tag)
        {
            if (!Tags.Contains(tag))
            {
                context.Tags.Add(tag);
            }
            context.SaveChanges();

            TagsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteEmployee(Employee employee)
        {
            if(CurentEmployee == employee)
            {
                ChangeCurentEmployee(null);
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            EmployeesUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal void DeleteDepartment(Department curentDepartament)
        {
            if (CurentDepartament == curentDepartament)
            {
                ChangeCurentDepartment(null);
            }
            context.Departments.Remove(curentDepartament);
            context.SaveChanges();
            DepartmentsUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal void DeleteOrder(Order curentOrder)
        {
            if (CurentOrder == curentOrder)
            {
                ChangeCurentOrder(null);
            }
            context.Orders.Remove(curentOrder);
            context.SaveChanges();
            OrdersUpdated?.Invoke(this, EventArgs.Empty);
        }


        internal void DeleteTag(Tag curentTag)
        {
            if (CurentTag == curentTag)
            {
                ChangeCurentTag(null);
            }
            context.Tags.Remove(curentTag);
            context.SaveChanges();
            TagsUpdated?.Invoke(this, EventArgs.Empty);
        }

    }
}
