using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingColombia.ViewModels
{
    public class FacturaVIewModel
    {
        public int idFactura { get; set; }
        public DateOnly FechaFactura { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public decimal Monto { get; set; }
        public string Metodo_Pago { get; set; }
        public string Nombre_Banco { get; set; }
        
    }
}