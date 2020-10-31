// ------------------------------------------------------------------------------------------------
// <copyright file="MudanzasRepositorio.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa la lógica del repositorio de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.Repositorio.Clases
{
    using MovingInc.Entidades.Modelos;
    using MovingInc.Repositorio.Interfaces;

    public class MudanzasRepositorio: IMudanzasRepositorio
    {
        private readonly MudanzasContext mudanzasContext;

        public MudanzasRepositorio(MudanzasContext mudanzasContext)
        {
            this.mudanzasContext = mudanzasContext;
        }
        public void GuardarLog(LogMudanza log)
        {
            mudanzasContext.LogMudanzas.Add(log);
            mudanzasContext.SaveChanges();             
        }
    }
}
