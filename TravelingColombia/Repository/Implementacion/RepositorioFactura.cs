using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;
using Repository.Implementacion;
using TravelingColombia.Models;
using TravelingColombia.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using TravelingColombia.ViewModels;

namespace TravelingColombia.Repository.Implementacion
{
    public class RepositorioFactura : RepositoryGeneric<Factura, int>, IRepositorioFactura
    {
        private readonly TravelingColombiabdContext _context;

        public RepositorioFactura(TravelingColombiabdContext context) : base(context)
        {
            _context = context;

        }

        public async Task<FacturaVIewModel> EnviarFacturaHtmlAsync(string correoDestino, int idFactura)
        {

            var resultado = (from f in _context.Facturas
                             join r in _context.Reservas on f.IdReserva equals r.IdReserva
                             join p in _context.Pagos on f.IdPago equals p.IdPago
                             join mp in _context.MetodoPagos on p.IdMetodo equals mp.IdMetodo
                             join b in _context.Bancos on p.IdBanco equals b.IdBanco
                             where f.IdFactura == idFactura
                             select new FacturaVIewModel
                             {
                                 idFactura = f.IdFactura,
                                 FechaFactura = f.FechaFactura,
                                 SubTotal = f.SubTotal,
                                 Descuento = f.Descuento,
                                 Total = f.Total,
                                 Nombre = p.Nombre,
                                 Cedula = p.Cedula,
                                 Monto = p.Monto,
                                 Metodo_Pago = mp.MetodoPago1,
                                 Nombre_Banco = b.NombreBanco
                             }).FirstOrDefault();

            var mensaje = new MimeMessage();
            mensaje.From.Add(MailboxAddress.Parse("travelingcolombia49@gmail.com")); // remitente
            mensaje.To.Add(MailboxAddress.Parse(correoDestino));          // destinatario
            mensaje.Subject = "Factura Pago TravelingColombia";

            var builder = new BodyBuilder
            {
                HtmlBody = $@"<!DOCTYPE html>
                            <html lang='es'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>Factura</title>
                            </head>
                            <body style='background-color:#f3f4f6; padding:2rem; font-family:Arial, sans-serif;'>
                                <div style='max-width:600px; margin:0 auto; background-color:#ffffff; padding:1.5rem; border-radius:0.5rem; box-shadow:0 0 10px rgba(0,0,0,0.1);'>
                                    <h2 style='text-align:center; font-size:1.5rem; margin-bottom:1rem;'>Factura</h2>
                                    <div style='display:flex; justify-content:space-between; font-size:0.9rem; margin-bottom:1.5rem;'>
                                        <div style='display:flex; justify-content:space-center;'>
                                            <p><strong>Fecha:</strong> {resultado.FechaFactura}</p>
                                            <p><strong>Nombre:</strong> {resultado.Nombre}</p>
                                            <p><strong>Cédula:</strong> {resultado.Cedula}</p>
                                        </div>
                                        <div style='display:flex; justify-content:space-center;'>
                                            <p><strong>Método de Pago:</strong> {resultado.Metodo_Pago}</p>
                                            <p><strong>Cédula:</strong> {resultado.Nombre_Banco}</p>
                                            
                                        </div>
                                    </div>
                                    <table style='width:100%; border-collapse:collapse; font-size:0.9rem;'>
                                        <thead>
                                            <tr style='background-color:#e5e7eb;'>
                                                <th style='border:1px solid #d1d5db; padding:0.5rem; text-align:left;'>Descripción</th>
                                                <th style='border:1px solid #d1d5db; padding:0.5rem; text-align:right;'>Valor</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem;'>Subtotal</td>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem; text-align:right;'>${resultado.SubTotal}</td>
                                            </tr>
                                            <tr>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem;'>Descuento</td>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem; text-align:right;'>-${resultado.Descuento}</td>
                                            </tr>
                                            <tr style='font-weight:bold;'>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem;'>Total</td>
                                                <td style='border:1px solid #d1d5db; padding:0.5rem; text-align:right;'>${resultado.Total}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <p style='text-align:center; font-size:0.75rem; color:#6b7280; margin-top:1.5rem;'>Gracias por su compra</p>
                                </div>
                            </body>
                            </html>"
            };

            mensaje.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Usa tu correo de Gmail y tu contraseña de aplicación
            await smtp.AuthenticateAsync("travelingcolombia49@gmail.com", "ivpfnddbokvtrxbq");
            await smtp.SendAsync(mensaje);
            await smtp.DisconnectAsync(true);

            return resultado;
        }

        public async Task<FacturaVIewModel> FacturaViewModel(int idFactura)
        {

            var resultado =await (from f in _context.Facturas
                            join r in _context.Reservas on f.IdReserva equals r.IdReserva
                            join p in _context.Pagos on f.IdPago equals p.IdPago
                            join mp in _context.MetodoPagos on p.IdMetodo equals mp.IdMetodo
                            join b in _context.Bancos on p.IdBanco equals b.IdBanco
                            where f.IdFactura == idFactura
                            select new FacturaVIewModel
                            {
                                idFactura = f.IdFactura,
                                FechaFactura = f.FechaFactura,
                                SubTotal = f.SubTotal,
                                Descuento = f.Descuento,
                                Total = f.Total,
                                Nombre = p.Nombre,
                                Cedula = p.Cedula,
                                Monto = p.Monto,
                                Metodo_Pago = mp.MetodoPago1,
                                Nombre_Banco = b.NombreBanco
                            }).FirstOrDefaultAsync();

            return resultado;
        }

    }
}