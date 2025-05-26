using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelingColombia.Filtros
{
    public class FiltroViajesViewModel
    {
        public int IdViaje { get; set; }

        public int IdDestinoIda { get; set; }

        public int IdDestinoLlegada { get; set; }

        public TimeOnly HoraSalida { get; set; }

        public TimeOnly HoraLlegada { get; set; }

        public DateOnly FechaViaje { get; set; }

        public decimal PrecioViaje { get; set; }

        public int CantidadPuestos { get; set; }

        public int IdAerolinea { get; set; }

        [NotMapped]
        public IFormFile ImagenArchivo { get; set; }

        public string? Imagen { get; set; }
    }
}
