
namespace MovingInc.Entidades.Modelos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    [Table("LogMudanzas")]
    public class LogMudanza
    {
        [Key]
        public Guid IdLog { get; set; }

        [Required]
        public int DocumentoUsuario { get; set; }

        public string EntradaArchivo { get; set; }

        public string SalidaArchivo { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }


    }
}
