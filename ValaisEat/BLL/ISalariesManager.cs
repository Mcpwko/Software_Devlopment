using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ISalariesManager
    {
        List<Salaries> GetSalaries();
        Salaries GetSalary(int id);
        Salaries AddSalary(Salaries salary);
        int UpdateSalary(Salaries salary);
        int DeleteSalary(int idSalary);
    }
}
