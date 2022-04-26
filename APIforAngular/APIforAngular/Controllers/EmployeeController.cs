using APIforAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIforAngular.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmployeeController : ApiController
    {
        CrudAngularEntities objEntity = new CrudAngularEntities();

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<EmployeeDetail> GetEmaployee()
        {
            try
            {
                return objEntity.EmployeeDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmaployeeById(string employeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = objEntity.EmployeeDetails.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { ok = true, objEmp });
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmaployee(EmployeeDetail data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.EmployeeDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { ok=true, data});
        }

        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmaployeeMaster(EmployeeDetail employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = objEntity.EmployeeDetails.Find(employee.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.EmailId = employee.EmailId;
                    objEmp.DateOfBirth = employee.DateOfBirth;
                    objEmp.Gender = employee.Gender;
                    objEmp.pinCode = employee.pinCode;

                }
                int i = this.objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { ok = true, employee });
        }
      
        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmaployeeDelete(int id)
        {
            //int empId = Convert.ToInt32(id);
            EmployeeDetail emaployee = objEntity.EmployeeDetails.Find(id);
            if (emaployee == null)
            {
                return NotFound();            
            }
            objEntity.EmployeeDetails.Remove(emaployee);
            objEntity.SaveChanges();
            return Ok(new { ok = true, emaployee });
        }
    }
}
