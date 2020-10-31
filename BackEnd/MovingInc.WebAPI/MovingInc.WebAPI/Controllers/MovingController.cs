// ------------------------------------------------------------------------------------------------
// <copyright file="MovingController.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa la controladora de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingIncProject.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MovingInc.Negocio.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [ApiController]
    [Route("[controller]")]
    public class MovingController : ControllerBase
    {
        private readonly ILogger<MovingController> _logger;

        private readonly IMudanzasNegocio mudanzasNegocio;

        public MovingController(IMudanzasNegocio mudanzasNegocio, ILogger<MovingController> logger)
        {
            this.mudanzasNegocio = mudanzasNegocio;
            _logger = logger;
        }

        [HttpPost]
        [Route("ProcesarArchivo/{documento}")]
        public IActionResult ProcesarArchivo([FromForm(Name = "ArchivoSeleccionado")] List<IFormFile> archivos, int documento)
        {
            try
            {
                string resultadoArchivo = mudanzasNegocio.ProcesarArchivo(archivos, documento);
                byte[] bytes = Encoding.ASCII.GetBytes(resultadoArchivo);
                var result = File(bytes, "application/octet-stream");
                return result;

            }
            catch (Exception ex)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(ex.Message);
                var result = File(bytes, "application/octet-stream");
                return result;
                throw;
            }
        }
    }
}