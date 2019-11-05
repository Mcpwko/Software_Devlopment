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



        public List<Salaries> GetSalaries()
        {
            return salaryDB.GetSalaries();
        }

        public Salaries GetSalary(int id)
        {
            return salaryDB.GetSalary(id);
        }
        public Salaries AddSalary(Salaries salary)
        {
            return salaryDB.AddSalary(salary);
        }
        public int UpdateCity(Salaries salary)
        {
            return salaryDB.UpdateSalary(salary);
        }
        public int DeleteCity(int idSalary)
        {
            return salaryDB.DeleteSalary(idSalary);
        }
    }
}
