using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeWebAPIApplication.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmployeeAddResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class EmployeeListResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<Employee> EmployeesList { get; set; }
    }
}