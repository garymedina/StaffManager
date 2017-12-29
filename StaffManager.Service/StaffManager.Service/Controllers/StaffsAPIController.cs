using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using StaffManager.Service.Models;

namespace StaffManager.Service.Controllers
{
    public class StaffsAPIController : ApiController
    {
        private StaffManagerContext db = new StaffManagerContext();

        // GET: api/StaffsAPI
        public IQueryable<Staff> GetStaffs()
        {
            return db.Staffs;
        }

        // GET: api/StaffsAPI/5
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> GetStaff(int id)
        {
            Staff staff = await db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // PUT: api/StaffsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStaff(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.Id)
            {
                return BadRequest();
            }

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StaffsAPI
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> PostStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(staff);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = staff.Id }, staff);
        }

        // DELETE: api/StaffsAPI/5
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> DeleteStaff(int id)
        {
            Staff staff = await db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(staff);
            await db.SaveChangesAsync();

            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.Id == id) > 0;
        }
    }
}