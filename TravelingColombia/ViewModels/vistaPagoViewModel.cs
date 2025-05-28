using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelingColombia.Models;

namespace TravelingColombia.ViewModels
{
    public class vistaPagoViewModel
    {
        public PlanViewModel Plan { get; set; }
        public ViajesViewModel viaje { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public Reserva reserva{ get; set; }
        public transaccionReservaViewModel transaccionReserva { get; set; } = new transaccionReservaViewModel();


    }
}