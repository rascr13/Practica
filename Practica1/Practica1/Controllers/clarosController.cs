using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Practica1.Models;

namespace Practica1.Controllers
{
    public class clarosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/claros
        [Authorize]
        public IQueryable<claros> Getclaros()
        {
            return db.claros;
        }

        // GET: api/claros/5
        [Authorize]
        [ResponseType(typeof(claros))]
        public IHttpActionResult Getclaros(int id)
        {
            claros claros = db.claros.Find(id);
            if (claros == null)
            {
                return NotFound();
            }

            return Ok(claros);
        }

        // PUT: api/claros/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putclaros(int id, claros claros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != claros.clarosID)
            {
                return BadRequest();
            }

            db.Entry(claros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clarosExists(id))
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

        // POST: api/claros
        [Authorize]
        [ResponseType(typeof(claros))]
        public IHttpActionResult Postclaros(claros claros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.claros.Add(claros);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = claros.clarosID }, claros);
        }

        // DELETE: api/claros/5
        [Authorize]
        [ResponseType(typeof(claros))]
        public IHttpActionResult Deleteclaros(int id)
        {
            claros claros = db.claros.Find(id);
            if (claros == null)
            {
                return NotFound();
            }

            db.claros.Remove(claros);
            db.SaveChanges();

            return Ok(claros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clarosExists(int id)
        {
            return db.claros.Count(e => e.clarosID == id) > 0;
        }
    }
}