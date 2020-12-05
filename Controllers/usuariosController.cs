﻿using System;
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
    public class usuariosController : ApiController
    {
        private MobikeDBEntities db = new MobikeDBEntities();

        // GET: api/usuarios
        public IQueryable<usuario> Getusuario()
        {
            return db.usuario;
        }

        // GET: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Getusuario(int id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putusuario(int id, usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idusuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarioExists(id))
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

        // POST: api/usuarios
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Postusuario(usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuario.Add(usuario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usuario.idusuario }, usuario);
        }

        // DELETE: api/usuarios/5
        [ResponseType(typeof(usuario))]
        public async Task<IHttpActionResult> Deleteusuario(int id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.usuario.Remove(usuario);
            await db.SaveChangesAsync();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarioExists(int id)
        {
            return db.usuario.Count(e => e.idusuario == id) > 0;
        }
    }
}