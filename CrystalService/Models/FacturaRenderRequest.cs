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
    public class FacturaRenderRequest
    {
        public InfoTributaria infoTributaria { get; set; }
        public InfoFactura infoFactura { get; set; }

        public List<DetalleFactura> detalles { get; set; }
        public List<RetencionFactura> retenciones { get; set; }
        public List<InfoAdicionalItem> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }

        // Opcional: si quieres pasar el logo por ruta o usar el default del servidor
        public string LogoPathOverride { get; set; }
    }

    public class InfoTributaria
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
    }

    public class InfoFactura
    {
        public string fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string tipoIdentificacionComprador { get; set; }
        public string guiaRemision { get; set; }
        public string razonSocialComprador { get; set; }
        public string identificacionComprador { get; set; }
        public string direccionComprador { get; set; }
        public string totalSinImpuestos { get; set; }
        public string totalDescuento { get; set; }
        public List<TotalConImpuesto> totalConImpuestos { get; set; }
        public string propina { get; set; }
        public string importeTotal { get; set; }
        public string moneda { get; set; }
        public List<PagoFactura> pagos { get; set; }

        public string valorRetIva { get; set; }
        public string valorRetRenta { get; set; }
        public string comercioExterior { get; set; }
        public string IncoTermFactura { get; set; }
        public string lugarIncoTerm { get; set; }
        public string paisOrigen { get; set; }
        public string puertoEmbarque { get; set; }
        public string paisDestino { get; set; }
        public string paisAdquisicion { get; set; }
        public string incoTermTotalSinImpuestos { get; set; }
        public string fleteInternacional { get; set; }
        public string seguroInternacional { get; set; }
        public string gastosAduaneros { get; set; }
        public string gastosTransporteOtros { get; set; }
        public string codDocReembolso { get; set; }
        public string totalComprobantesReembolso { get; set; }
        public string totalBaseImponibleReembolso { get; set; }
        public string totalImpuestoReembolso { get; set; }
        public List<ReembolsoFactura> reembolsos { get; set; }
    }

    public class TotalConImpuesto
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
    }

    public class PagoFactura
    {
        public string formaPago { get; set; }
        public string total { get; set; }
        public string plazo { get; set; }
        public string unidadTiempo { get; set; }
    }

    public class ReembolsoFactura
    {
        public string tipoIdentificacionProveedorReembolso { get; set; }
        public string identificacionProveedorReembolso { get; set; }
        public string codPaisPagoProveedorReembolso { get; set; }
        public string tipoProveedorReembolso { get; set; }
        public string codDocReembolso { get; set; }
        public string estabDocReembolso { get; set; }
        public string ptoEmiDocReembolso { get; set; }
        public string secuencialDocReembolso { get; set; }
        public string fechaEmisionDocReembolso { get; set; }
        public string numeroautorizacionDocReemb { get; set; }
        public List<ReembolsoDetalleImpuesto> detalleImpuestos { get; set; }
    }

    public class ReembolsoDetalleImpuesto
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponibleReembolso { get; set; }
        public string tarifa { get; set; }
        public string impuestoReembolso { get; set; }
    }

    public class DetalleFactura
    {
        public string codigoPrincipal { get; set; }
        public string codigoAuxiliar { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public string precioUnitario { get; set; }
        public string descuento { get; set; }
        public string precioTotalSinImpuesto { get; set; }
        public List<DetalleAdicional> detallesAdicionales { get; set; }
        public List<ImpuestoDetalle> impuestos { get; set; }
    }

    public class DetalleAdicional
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }

    public class ImpuestoDetalle
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
        public string tarifa { get; set; }
    }

    public class RetencionFactura
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string tarifa { get; set; }
        public string valor { get; set; }
    }

    public class InfoAdicionalItem
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}
