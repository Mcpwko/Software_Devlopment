using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ISalariesDB
    {
        IConfiguration Configuration { get; }

        List<Salary> GetSalaries();
        Salary GetSalary(int id);
        Salary AddSalary(Salary salaires);
        int UpdateSalary(Salary salaires);
        int DeleteSalary(int iDSalaries);
    }
}
