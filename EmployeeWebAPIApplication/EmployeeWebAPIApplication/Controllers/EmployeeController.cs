using EmployeeWebAPIApplication.Context;
using EmployeeWebAPIApplication.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeWebAPIApplication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {

        //Creating Instance of DatabaseContext class  
        private EmployeeDbContext dbContext = new EmployeeDbContext();
        public EmployeeController() 
        {
        }


        [HttpGet]
        [Route("getAllEmployee")]
        public HttpResponseMessage GetAllEmployee()
        {
            var response = new EmployeeListResponse();
            try
            {
                List<Employee> employeesList = (from emp in dbContext.Employees
                                                where emp.IsDeleted == false
                                                select emp).ToList();
                if (employeesList.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employeesList);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No employee found";
                    response.EmployeesList = null;

                    return Request.CreateResponse(HttpStatusCode.NotFound, response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return Request.CreateResponse(HttpStatusCode.Unauthorized, response);
            }
        }

        [HttpGet]
        [Route("getEmployeeDetailById")]
        public HttpResponseMessage GetEmployeeDetailById(int employeeId)
        {
            var response = new HttpResponseMessage();
            try
            {
                var employeeDetail = (from emp in dbContext.Employees
                                     where emp.Id == employeeId && emp.IsDeleted == false
                                     select emp).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, employeeDetail);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }


        [HttpPost]
        [Route("saveEmployee")]
        public HttpResponseMessage SaveEmployee(Employee employee)
        {
            var employeeAddResponse = new EmployeeAddResponse();

            var response = new HttpResponseMessage();
            try
            {
                var employeeDetail = (from emp in dbContext.Employees
                                      where emp.Id == employee.Id && emp.IsDeleted == false
                                      select emp).FirstOrDefault();
                if (employeeDetail != null)
                {
                    employeeDetail.FirstName = employee.FirstName;
                    employeeDetail.LastName = employee.LastName;
                    employeeDetail.Email = employee.Email;
                    employeeDetail.Address = employee.Address;
                    employeeDetail.Phone = employee.Phone;
                    employeeDetail.UpdatedDate = DateTime.Now;
                    employeeDetail.IsDeleted = false;

                    dbContext.SaveChanges();

                    employeeAddResponse.IsSuccess = true;
                    employeeAddResponse.Message = "Employee was updated successfully";

                    return Request.CreateResponse(HttpStatusCode.OK, employeeAddResponse);
                }
                else
                {
                    var Employee = new Employee
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        Address = employee.Address,
                        Phone = employee.Phone,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsDeleted = false
                    };

                    dbContext.Employees.Add(Employee);
                    dbContext.SaveChanges();

                    employeeAddResponse.IsSuccess = true;
                    employeeAddResponse.Message = "Employee was added successfully";

                    return Request.CreateResponse(HttpStatusCode.OK, employeeAddResponse);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

      
        [HttpPost]
        [Route("deleteEmployeeById")]
        public HttpResponseMessage DeleteEmployeeById(int employeeId)
        {
            var employeeAddResponse = new EmployeeAddResponse();
            try
            {
                var employeeDetail = (from emp in dbContext.Employees
                                      where emp.Id == employeeId && emp.IsDeleted == false
                                      select emp).FirstOrDefault();
                if (employeeDetail != null)
                {
                    employeeDetail.UpdatedDate = DateTime.Now;
                    employeeDetail.IsDeleted = true;

                    dbContext.SaveChanges();

                    employeeAddResponse.IsSuccess = true;
                    employeeAddResponse.Message = "Employee was deleted successfully";

                    return Request.CreateResponse(HttpStatusCode.OK, employeeAddResponse);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }
    }
}