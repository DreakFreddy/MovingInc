// ------------------------------------------------------------------------------------------------
// <copyright file="IMudanzasRepositorio.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa la lógica de la interfaz repositorio de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.Repositorio.Interfaces
{
    using MovingInc.Entidades.Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMudanzasRepositorio
    {
        void GuardarLog(LogMudanza log);
    }
}
