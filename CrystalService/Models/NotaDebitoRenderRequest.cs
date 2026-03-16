using System.Collections.Generic;

namespace CrystalService.Models
{
    /// <summary>
    /// Request para renderizar Nota de Débito.
    /// Los nombres de propiedades se mantienen en camelCase para respetar el JSON recibido.
    /// </summary>
    public class NotaDebitoRenderRequest
    {
        public InfoTributariaNd infoTributaria { get; set; }
        public InfoNotaDebitoNd infoNotaDebito { get; set; }
        public List<MotivoNd> motivos { get; set; }
        public List<InfoAdicionalItemNd> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }

        // Opcional
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

        // Campos opcionales internos para Crystal/autorización, si los vas a usar
        public string fechaAuto { get; set; }
        public string horaAuto { get; set; }
    }

    public class InfoNotaDebitoNd
    {
        public string fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }
        public string tipoIdentificacionComprador { get; set; }
        public string razonSocialComprador { get; set; }
        public string identificacionComprador { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string codDocModificado { get; set; }
        public string numDocModificado { get; set; }
        public string fechaEmisionDocSustento { get; set; }
        public string totalSinImpuestos { get; set; }
        public List<ImpuestoNotaDebitoNd> impuestos { get; set; }
        public string valorTotal { get; set; }
        public List<PagoNotaDebitoNd> pagos { get; set; }

        // Opcional, por si tu reporte lo usa
        public string direccionComprador { get; set; }
    }

    public class ImpuestoNotaDebitoNd
    {
        public string codigo { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string valor { get; set; }
        public string tarifa { get; set; }
    }

    public class PagoNotaDebitoNd
    {
        public string formaPago { get; set; }
        public string total { get; set; }
        public string plazo { get; set; }
        public string unidadTiempo { get; set; }
    }

    public class MotivoNd
    {
        public string razon { get; set; }
        public string valor { get; set; }
    }

    public class InfoAdicionalItemNd
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}