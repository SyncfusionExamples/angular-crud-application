using EmployeeWebAPIApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeWebAPIApplication.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("name=EmployeeContext")
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}