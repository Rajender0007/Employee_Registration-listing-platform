using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Registrationproject1.Models;

namespace RegistrationWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RegisterWebController : ApiController
    {
        DataAccessLayer objDataAccessLayer = new DataAccessLayer();

        [HttpGet]
        public IEnumerable<Employee> EmpDisplay(int? id = null)
        {
            var DisplayRecords = objDataAccessLayer.EmployeeDisplay(id);
            return DisplayRecords;
        }

        [HttpGet]
        // [DisableCors] it won't allow to execute
        public IEnumerable<Employee> EmpFind(int? ActiveStatus, int? TextBoxFind)
        {
            var EmpRecord = objDataAccessLayer.EmployeeDisplay(ActiveStatus, TextBoxFind);
            return EmpRecord;
        }
        [HttpPost]
        public string EmpAdd([FromBody] Employee EmpValues)
        {
            var status = objDataAccessLayer.AddEmployee(EmpValues);
            return status;
        }
        [HttpPut]
        public string EmpUpdate(Employee EmpValues)
        {
            EmpValues.IsActive = true;
            var message = objDataAccessLayer.EmployeeUpdate1(EmpValues);
            return message;
        }
        [HttpGet]
        public string EmailValidation([FromUri] string emailid)
        {
            if (objDataAccessLayer.EmailValidation(emailid))
                return "Found";
            else
                return "Not Found";
        }
        [HttpPut]
        public string EmpDelete([FromUri] int id)
        {
            var record = objDataAccessLayer.EmployeeDisplay(id).Find(emp => emp.EmpId == id);
            record.IsActive = false;
            var result = objDataAccessLayer.EmployeeUpdate2(record);
            return result;
        }
    }
}
