using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalService.Models
{
    public class RetencionRenderRequest
    {
        public InfoTributariaRt infoTributaria { get; set; }
        public InfoCompRetencion infoCompRetencion { get; set; }
        public List<DocSustento> docsSustento { get; set; }
        public List<InfoAdicionalRt> infoAdicional { get; set; }

        public string campoAdicional1 { get; set; }
        public string campoAdicional2 { get; set; }

        public string LogoPathOverride { get; set; }
    }

    public class InfoTributariaRt
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

    public class InfoCompRetencion
    {
        public string fechaEmision { get; set; }
        public string dirEstablecimiento { get; set; }
        public string contribuyenteEspecial { get; set; }
        public string obligadoContabilidad { get; set; }
        public string tipoIdentificacionSujetoRetenido { get; set; }
        public string tipoSujetoRetenido { get; set; }
        public string parteRel { get; set; }
        public string razonSocialSujetoRetenido { get; set; }
        public string identificacionSujetoRetenido { get; set; }
        public string periodoFiscal { get; set; }
    }

    public class DocSustento
    {
        public string codSustento { get; set; }
        public string codDocSustento { get; set; }
        public string numDocSustento { get; set; }
        public string factura_relacionada { get; set; }
        public string fechaEmisionDocSustento { get; set; }
        public string fechaRegistroContable { get; set; }
        public string numAutDocSustento { get; set; }
        public string pagoLocExt { get; set; }
        public string tipoRegi { get; set; }
        public string paisEfecPago { get; set; }
        public string aplicConvDobTrib { get; set; }
        public string pagExtSujRetNorLeg { get; set; }
        public string pagRegFis { get; set; }
        public string totalComprobantesReembolso { get; set; }
        public string totalBaseImponibleReembolso { get; set; }
        public string totalSinImpuestos { get; set; }
        public string importeTotal { get; set; }

        public List<ImpuestoDocSustento> impuestosDocSustento { get; set; }
        public List<RetencionDocSustento> retenciones { get; set; }
        public List<PagoDocSustento> pagos { get; set; }
    }

    public class ImpuestoDocSustento
    {
        public string codImpuestoDocSustento { get; set; }
        public string codigoPorcentaje { get; set; }
        public string baseImponible { get; set; }
        public string tarifa { get; set; }
        public string valorImpuesto { get; set; }
    }

    public class RetencionDocSustento
    {
        public string codigo { get; set; }
        public string codigoRetencion { get; set; }
        public string baseImponible { get; set; }
        public string porcentajeRetener { get; set; }
        public string valorRetenido { get; set; }

        public Dividendos dividendos { get; set; }
    }

    public class Dividendos
    {
        public string fechaPagoDiv { get; set; }
        public string imRentaSoc { get; set; }
        public string ejerFisUtDiv { get; set; }
    }

    public class PagoDocSustento
    {
        public string formaPago { get; set; }
        public string total { get; set; }
    }

    public class InfoAdicionalRt
    {
        public string nombre { get; set; }
        public string valor { get; set; }
    }
}