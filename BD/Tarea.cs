using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD
{
    public partial class Tarea
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
