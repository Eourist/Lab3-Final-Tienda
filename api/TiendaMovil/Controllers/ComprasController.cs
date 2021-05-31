﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using TiendaMovil.Models;

namespace TiendaMovil.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public ComprasController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(Compra compra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario comprador = contexto.Usuarios.Find(int.Parse(User.Claims.First(c => c.Type == "Id").Value));
                    int VendedorId = compra.Publicacion.UsuarioId;

                    compra.PublicacionId = compra.Publicacion.Id;
                    compra.Precio = compra.Publicacion.Precio * compra.Cantidad;
                    compra.UsuarioId = comprador.Id;
                    compra.Estado = 1;
                    compra.Creacion = DateTime.Now;
                    compra.Publicacion = null;
                    contexto.Compras.Add(compra);

                    await contexto.SaveChangesAsync();

                    //Transacción 1 >> Compra
                    Transaccion trCompra = new Transaccion();
                    trCompra.CompraId = compra.Id;
                    trCompra.UsuarioId = compra.UsuarioId;
                    trCompra.Importe = compra.Precio * compra.Cantidad;
                    comprador.Fondos -= compra.Precio * compra.Cantidad;
                    trCompra.Balance = comprador.Fondos;
                    trCompra.Tipo = 2;
                    trCompra.Estado = 1;
                    trCompra.Creacion = DateTime.Now;
                    contexto.Transacciones.Add(trCompra);

                    //Transacción 2 >> Venta
                    Transaccion trVenta = new Transaccion();
                    trVenta.CompraId = compra.Id;
                    trVenta.UsuarioId = VendedorId;
                    trVenta.Importe = compra.Precio * compra.Cantidad;
                    Usuario vendedor = contexto.Usuarios.Find(trVenta.UsuarioId);
                    vendedor.Fondos += trVenta.Importe * compra.Cantidad;
                    trVenta.Balance = vendedor.Fondos;
                    trVenta.Tipo = 3;
                    trVenta.Estado = 1;
                    trVenta.Creacion = DateTime.Now;
                    contexto.Transacciones.Add(trVenta);

                    //Publicacion
                    Publicacion publicacion = contexto.Publicaciones.Find(compra.PublicacionId);
                    publicacion.Stock -= compra.Cantidad;

                    contexto.SaveChanges();
                    return Ok(true);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}