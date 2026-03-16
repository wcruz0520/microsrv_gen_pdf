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
    public class GuiaRemisionRenderRequest
    {
        public InfoTributariaGr infoTributaria { get; set; }

        public infoGuiaRemision infoGuiaRemision { get; set; }

        public List<destinatarios> destinatarios { get; set; }

        public List<InfoAdicionalItemGr> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }

        // Opcional: si quieres pasar el logo por ruta o usar el default del servidor
        public string LogoPathOverride { get; set; }
    }

    public class InfoTributariaGr
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

    public class infoGuiaRemision
    {
        public string dirEstablecimiento { get; set; }
        public string dirPartida { get; set; }
        public string razonSocialTransportista { get; set; }
        public string tipoIdentificacionTransportista { get; set; }
        public string rucTransportista { get; set; }
        public string rise { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string fechaIniTransporte { get; set; }
        public string fechaFinTransporte { get; set; }
        public string placa { get; set; }
    }

    public class destinatarios
    {
        public string identificacionDestinatario { get; set; }
        public string razonSocialDestinatario { get; set; }
        public string dirDestinatario { get; set; }
        public string motivoTraslado { get; set; }
        public string codEstabDestino { get; set; }
        public string codDocSustento { get; set; }
        public string numDocSustento { get; set; }
        public string numAutDocSustento { get; set; }
        public string fechaEmisionDocSustento { get; set; }
        public string docAduaneroUnico { get; set; }
        public string ruta { get; set; }
        public List<DetalleGr> detalles { get; set; }
    }

    public class DetalleGr
    {
        public string codigoPrincipal { get; set; }
        public string codigoAuxiliar { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public List<DetalleAdicionalGr> detallesAdicionales { get; set; }
    }

    public class DetalleAdicionalGr
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }

    public class InfoAdicionalItemGr
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}
