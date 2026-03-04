using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalService.Models
{
    public class FacturaRenderRequest
    {
        public FacturaCab Cabecera { get; set; }
        public List<FacturaDet> Detalles { get; set; }
        public List<FacturaTotImp> TotalesImpuestos { get; set; }
        public List<FacturaPago> Pagos { get; set; }

        // Opcional: si quieres pasar el logo por ruta o usar el default del servidor
        public string LogoPathOverride { get; set; }
    }

    public class FacturaCab
    {
        public string ClaveAcceso { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }

        public string Estab { get; set; }
        public string PtoEmi { get; set; }
        public string Secuencial { get; set; }
        public string FechaEmision { get; set; }

        public string DirMatriz { get; set; }
        public string DirEstablecimiento { get; set; }

        public string IdentificacionComprador { get; set; }
        public string RazonSocialComprador { get; set; }

        public decimal TotalSinImpuestos { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Moneda { get; set; }
    }

    public class FacturaDet
    {
        public int LineNum { get; set; }
        public string CodigoPrincipal { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioTotalSinImpuesto { get; set; }
    }

    public class FacturaTotImp
    {
        public string Codigo { get; set; }
        public string CodigoPorcentaje { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal Valor { get; set; }
    }

    public class FacturaPago
    {
        public string FormaPago { get; set; }
        public decimal Total { get; set; }
        public int Plazo { get; set; }
        public string UnidadTiempo { get; set; }
    }
}