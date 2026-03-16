using System.Collections.Generic;

namespace CrystalService.Models
{
    public class LiquidacionCompraRenderRequest
    {
        public InfoTributariaLq infoTributaria { get; set; }
        public InfoLiquidacionCompra infoLiquidacionCompra { get; set; }
        public List<DetalleLiquidacion> detalles { get; set; }
        public List<Reembolso> reembolsos { get; set; }
        public List<InfoAdicional> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }
        public string LogoPathOverride { get; set; }
    }

    public class InfoTributariaLq
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

    public class InfoLiquidacionCompra
    {
        public string fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }

        public string tipoIdentificacionProveedor { get; set; }
        public string razonSocialProveedor { get; set; }
        public string identificacionProveedor { get; set; }
        public string direccionProveedor { get; set; }

        public string totalSinImpuestos { get; set; }
        public string totalDescuento { get; set; }

        public string codDocReembolso { get; set; }
        public string totalComprobantesReembolso { get; set; }
        public string totalBaseImponibleReembolso { get; set; }
        public string totalImpuestoReembolso { get; set; }

        public List<TotalConImpuestos> totalConImpuestos { get; set; }

        public string importeTotal { get; set; }
        public string moneda { get; set; }

        public List<Pago> pagos { get; set; }
    }

    public class TotalConImpuestos
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
        public string tarifa { get; set; }
        public string descuentoAdicional { get; set; }
    }

    public class Pago
    {
        public string formaPago { get; set; }
        public string total { get; set; }
        public string plazo { get; set; }
        public string unidadTiempo { get; set; }
    }

    public class DetalleLiquidacion
    {
        public string codigoPrincipal { get; set; }
        public string codigoAuxiliar { get; set; }
        public string descripcion { get; set; }

        public decimal cantidad { get; set; }

        public string precioUnitario { get; set; }
        public string descuento { get; set; }
        public string precioTotalSinImpuesto { get; set; }

        public List<DetalleAdicionalLq> detallesAdicionales { get; set; }
        public List<Impuesto> impuestos { get; set; }

        public string unidadMedida { get; set; }
    }

    public class DetalleAdicionalLq
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }

    public class Impuesto
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
        public string tarifa { get; set; }
    }

    public class Reembolso
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

        public List<DetalleImpuestoReembolso> detalleImpuestos { get; set; }
    }

    public class DetalleImpuestoReembolso
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string tarifa { get; set; }
        public string baseImponibleReembolso { get; set; }
        public string impuestoReembolso { get; set; }
    }

    public class InfoAdicional
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}