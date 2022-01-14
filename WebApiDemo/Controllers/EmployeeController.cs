using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeModel;

namespace WebApiDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        List<Employee> emplist; 
        public IEnumerable<Employee> Get()
        {
            using (EmpDBEntities db = new EmpDBEntities())
            {
                return db.Employees.ToList();
            }          
        }

        public HttpResponseMessage Get(int id)
        {
            using (EmpDBEntities db = new EmpDBEntities())
            {
                var ent =  db.Employees.FirstOrDefault(x => x.ID == id);  
                if (ent != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody]Employee emp)
        {
            try
            {
                using (EmpDBEntities db = new EmpDBEntities())
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                }
                var message = Request.CreateResponse(HttpStatusCode.Created);
                message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }

    }
}
