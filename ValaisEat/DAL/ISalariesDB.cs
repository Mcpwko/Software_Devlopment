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

        List<Salaries> GetSalaries();
        Salaries GetSalary(int id);
        Salaries AddSalary(Salaries salaires);
        int UpdateSalary(Salaries salaires);
        int DeleteSalary(int iDSalaries);
    }
}
