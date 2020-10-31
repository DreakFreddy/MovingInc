// ------------------------------------------------------------------------------------------------
// <copyright file="MudanzasContext.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa el contexto de Mudanzas/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.Repositorio
{
    using Microsoft.EntityFrameworkCore;
    using MovingInc.Entidades.Modelos;
    using System;

    public class MudanzasContext : DbContext
    {
        public MudanzasContext(DbContextOptions<MudanzasContext> options) : base(options)
        {
        }
        public virtual DbSet<LogMudanza> LogMudanzas { get; set; }

    }
}

