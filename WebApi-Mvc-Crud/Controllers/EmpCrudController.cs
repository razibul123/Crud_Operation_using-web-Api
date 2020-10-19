using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApi_Mvc_Crud.Models;

namespace WebApi_Mvc_Crud.Controllers
{
    public class EmpCrudController : ApiController

        
    {
        EmpDBEntities em = new EmpDBEntities();

        public IHttpActionResult  getemp()
        {

            
            var results = em.Newempregs.ToList();

            return Ok(results);
        }


        [HttpPost]

        public IHttpActionResult empinsert(Newempreg empinsert)

        {

            em.Newempregs.Add(empinsert);

            em.SaveChanges();
            return Ok();
        }
       
        public IHttpActionResult GetEmpid(int id)
        {

            EmpClass empdetails = null;
            empdetails = em.Newempregs.Where(x => x.Empid == id).Select(x => new EmpClass()
            {

                Empid = x.Empid,
                Empname = x.Empname,
                Empemail = x.Empemail,
                Empdesgination = x.EmpDesgination,
                Emplocation = x.Emplocation,

            }).FirstOrDefault<EmpClass>();

            if(empdetails==null)
            {

                return NotFound();
            }

            return Ok(empdetails);
        }

        public IHttpActionResult Put(EmpClass ec)
        {

            var updateemp = em.Newempregs.Where(x => x.Empid == ec.Empid).FirstOrDefault<Newempreg>();
            if(updateemp!=null)
            {

                updateemp.Empid = ec.Empid;
                updateemp.Empname = ec.Empname;
                updateemp.Emplocation = ec.Emplocation;
                updateemp.EmpDesgination = ec.Empdesgination;
                em.SaveChanges();

            }
            else
            {

                return NotFound();

            }

            return Ok();
        }


        public IHttpActionResult Delete(int id)
        {

            var emdel = em.Newempregs.Where(x => x.Empid == id).FirstOrDefault();
            em.Entry(emdel).State = System.Data.Entity.EntityState.Deleted;
            em.SaveChanges();
            return Ok();
        }

    }
}
