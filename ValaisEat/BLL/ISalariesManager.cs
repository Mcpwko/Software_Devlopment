using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ISalariesManager
    {
        List<Salary> GetSalaries();
        Salary GetSalary(int id);
        Salary AddSalary(Salary salary);
        int UpdateSalary(Salary salary);
        int DeleteSalary(int idSalary);
    }
}
