using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalService.Models
{
    /// <summary>
    /// Request para renderizar Factura 01 (Crystal). Modela la estructura completa del JSON.
    /// Nota: Los nombres de propiedades se mantienen en camelCase para que el binding JSON
    /// funcione sin configuraciones adicionales si tu serializer respeta los nombres.
    /// </summary>
    public class NotaDebitoRenderRequest
    {
        public InfoTributariaNd infoTributaria { get; set; }

        public infoNotaDebito infoNotaDebito { get; set; }

        public List<DetalleNd> detalles { get; set; }

        public List<InfoAdicionalItemNd> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }

        // Opcional: si quieres pasar el logo por ruta o usar el default del servidor
        public string LogoPathOverride { get; set; }
    }

    public class InfoTributariaNd
    {
        public string ambiente { get; set; }
        public string tipoEmision { get; set; }
        public string claveAcceso { get; set; }
        public string razonSocial { get; set; }
        public string nombreComercial { get; set; }
        public string ruc { get; set; }
        public string codDoc { get; set; }
        public string estab { get; set; }
        public string ptoEmi { get; set; }
        public string secuencial { get; set; }
        public string dirMatriz { get; set; }
        public string diaEmission { get; set; }
        public string mesEmission { get; set; }
        public string anioEmission { get; set; }
        public string fechaAuto { get; set; }
        public string horaAuto { get; set; }
    }

    public class infoNotaDebito
    {
        public string fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }
        public string tipoIdentificacionComprador { get; set; }
        public string razonSocialComprador { get; set; }
        public string identificacionComprador { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string direccionComprador { get; set; }
        public string codDocModificado { get; set; }
        public string numDocModificado { get; set; }
        public string fechaEmisionDocSustento { get; set; }
        public string totalSinImpuestos { get; set; }
        public string valorTotal { get; set; }
        public List<TotalConImpuestoNd> totalConImpuestos { get; set; }
        public List<PagoNotaDebito> pagos { get; set; }
    }

    public class TotalConImpuestoNd
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
    }

    public class DetalleNd
    {
        public string codigoPrincipal { get; set; }
        public string codigoAuxiliar { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public string precioUnitario { get; set; }
        public string descuento { get; set; }
        public string precioTotalSinImpuesto { get; set; }
        public List<DetalleAdicionalNd> detallesAdicionales { get; set; }
        public List<ImpuestoDetalleNd> impuestos { get; set; }
    }

    public class DetalleAdicionalNd
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }

    public class ImpuestoDetalleNd
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
        public string tarifa { get; set; }
    }

    public class PagoNotaDebito
    {
        public string formaPago { get; set; }
        public string total { get; set; }
        public string plazo { get; set; }
        public string unidadTiempo { get; set; }
    }

    public class InfoAdicionalItemNd
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}
