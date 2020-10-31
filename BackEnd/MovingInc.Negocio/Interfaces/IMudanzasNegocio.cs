// ------------------------------------------------------------------------------------------------
// <copyright file="IMudanzas.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa la lógica de negocio de Mudanzas Interfaz/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.Negocio.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IMudanzasNegocio
    {
        string ProcesarArchivo(List<IFormFile> archivos, int documento);

        void ValidarArchivo(List<string> lista);
    }
}
