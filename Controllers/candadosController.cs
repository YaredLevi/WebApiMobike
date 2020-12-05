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
using WebApiMobike;

namespace WebApiMobike.Controllers
{
    public class candadosController : ApiController
    {
        private MobikeDBEntities db = new MobikeDBEntities();

        // GET: api/candados
        public IQueryable<candado> Getcandado()
        {
            return db.candado;
        }

        // GET: api/candados/5
        [ResponseType(typeof(candado))]
        public async Task<IHttpActionResult> Getcandado(int id)
        {
            candado candado = await db.candado.FindAsync(id);
            if (candado == null)
            {
                return NotFound();
            }

            return Ok(candado);
        }

        // PUT: api/candados/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcandado(int id, candado candado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != candado.idcandado)
            {
                return BadRequest();
            }

            db.Entry(candado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!candadoExists(id))
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

        // POST: api/candados
        [ResponseType(typeof(candado))]
        public async Task<IHttpActionResult> Postcandado(candado candado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.candado.Add(candado);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = candado.idcandado }, candado);
        }

        // DELETE: api/candados/5
        [ResponseType(typeof(candado))]
        public async Task<IHttpActionResult> Deletecandado(int id)
        {
            candado candado = await db.candado.FindAsync(id);
            if (candado == null)
            {
                return NotFound();
            }

            db.candado.Remove(candado);
            await db.SaveChangesAsync();

            return Ok(candado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool candadoExists(int id)
        {
            return db.candado.Count(e => e.idcandado == id) > 0;
        }
    }
}