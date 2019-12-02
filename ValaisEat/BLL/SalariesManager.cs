using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class SalariesManager
    {
        public ISalariesDB salaryDB { get; }

        public SalariesManager(IConfiguration configuration)
        {
            salaryDB = new SalariesDB(configuration);
        }



        public List<Salary> GetSalaries()
        {
            return salaryDB.GetSalaries();
        }

        public Salary GetSalary(int id)
        {
            return salaryDB.GetSalary(id);
        }
        public Salary AddSalary(Salary salary)
        {
            return salaryDB.AddSalary(salary);
        }
        public int UpdateCity(Salary salary)
        {
            return salaryDB.UpdateSalary(salary);
        }
        public int DeleteCity(int idSalary)
        {
            return salaryDB.DeleteSalary(idSalary);
        }
    }
}
