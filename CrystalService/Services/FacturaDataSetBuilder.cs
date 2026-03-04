using CrystalService.Models;
using System;
using System.Data;

namespace CrystalService.Services
{
    /// <summary>
    /// Construye el DataSet que consumirá el .rpt de Crystal para Factura 01, a partir del JSON (FacturaRenderRequest).
    /// Diseña tu .rpt con estas tablas y campos (nombres exactos):
    /// - InfoTributaria (1)
    /// - InfoFactura (1)
    /// - TotalConImpuestos (N)
    /// - Pagos (N)
    /// - Detalles (N)
    /// - DetallesAdicionales (N) -> FK detalleId
    /// - DetallesImpuestos (N)   -> FK detalleId
    /// - Retenciones (N)
    /// - InfoAdicional (N)
    /// - Reembolsos (N) -> PK reembolsoId
    /// - ReembolsoDetalleImpuestos (N) -> FK reembolsoId
    /// - CamposAdicionales (1)
    /// </summary>
    public static class FacturaDataSetBuilder
    {
        public static DataSet Build(FacturaRenderRequest req)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));

            var ds = new DataSet("Factura01DS");

            // -------- Schema
            var tInfoTrib = CreateInfoTributaria();
            var tInfoFact = CreateInfoFactura();
            var tTotImp = CreateTotalConImpuestos();
            var tPagos = CreatePagos();
            var tDetalles = CreateDetalles();
            var tDetAdd = CreateDetallesAdicionales();
            var tDetImp = CreateDetallesImpuestos();
            var tRet = CreateRetenciones();
            var tInfoAd = CreateInfoAdicional();
            var tReemb = CreateReembolsos();
            var tReembImp = CreateReembolsoDetalleImpuestos();
            var tCampos = CreateCamposAdicionales();

            ds.Tables.AddRange(new[]
            {
                tInfoTrib, tInfoFact, tTotImp, tPagos,
                tDetalles, tDetAdd, tDetImp,
                tRet, tInfoAd,
                tReemb, tReembImp,
                tCampos
            });

            // (Opcional) Relaciones
            ds.Relations.Add("Detalles_DetallesAdicionales", tDetalles.Columns["detalleId"], tDetAdd.Columns["detalleId"], false);
            ds.Relations.Add("Detalles_DetallesImpuestos", tDetalles.Columns["detalleId"], tDetImp.Columns["detalleId"], false);
            ds.Relations.Add("Reembolsos_ReembolsoDetalleImpuestos", tReemb.Columns["reembolsoId"], tReembImp.Columns["reembolsoId"], false);

            // -------- Fill

            // InfoTributaria (1)
            if (req.infoTributaria != null)
            {
                var r = tInfoTrib.NewRow();
                Set(r, "ambiente", req.infoTributaria.ambiente);
                Set(r, "tipoEmision", req.infoTributaria.tipoEmision);
                Set(r, "claveAcceso", req.infoTributaria.claveAcceso);
                Set(r, "razonSocial", req.infoTributaria.razonSocial);
                Set(r, "nombreComercial", req.infoTributaria.nombreComercial);
                Set(r, "ruc", req.infoTributaria.ruc);
                Set(r, "codDoc", req.infoTributaria.codDoc);
                Set(r, "estab", req.infoTributaria.estab);
                Set(r, "ptoEmi", req.infoTributaria.ptoEmi);
                Set(r, "secuencial", req.infoTributaria.secuencial);
                Set(r, "dirMatriz", req.infoTributaria.dirMatriz);
                Set(r, "diaEmission", req.infoTributaria.diaEmission);
                Set(r, "mesEmission", req.infoTributaria.mesEmission);
                Set(r, "anioEmission", req.infoTributaria.anioEmission);
                tInfoTrib.Rows.Add(r);
            }

            // InfoFactura (1)
            if (req.infoFactura != null)
            {
                var r = tInfoFact.NewRow();
                Set(r, "fechaEmision", req.infoFactura.fechaEmision);
                Set(r, "dirEstablecimiento", req.infoFactura.dirEstablecimiento);
                Set(r, "contribuyenteEspecial", req.infoFactura.contribuyenteEspecial);
                Set(r, "obligadoContabilidad", req.infoFactura.obligadoContabilidad);
                Set(r, "tipoIdentificacionComprador", req.infoFactura.tipoIdentificacionComprador);
                Set(r, "guiaRemision", req.infoFactura.guiaRemision);
                Set(r, "razonSocialComprador", req.infoFactura.razonSocialComprador);
                Set(r, "identificacionComprador", req.infoFactura.identificacionComprador);
                Set(r, "direccionComprador", req.infoFactura.direccionComprador);
                Set(r, "totalSinImpuestos", req.infoFactura.totalSinImpuestos);
                Set(r, "totalDescuento", req.infoFactura.totalDescuento);
                Set(r, "propina", req.infoFactura.propina);
                Set(r, "importeTotal", req.infoFactura.importeTotal);
                Set(r, "moneda", req.infoFactura.moneda);

                Set(r, "valorRetIva", req.infoFactura.valorRetIva);
                Set(r, "valorRetRenta", req.infoFactura.valorRetRenta);
                Set(r, "comercioExterior", req.infoFactura.comercioExterior);
                Set(r, "IncoTermFactura", req.infoFactura.IncoTermFactura);
                Set(r, "lugarIncoTerm", req.infoFactura.lugarIncoTerm);
                Set(r, "paisOrigen", req.infoFactura.paisOrigen);
                Set(r, "puertoEmbarque", req.infoFactura.puertoEmbarque);
                Set(r, "paisDestino", req.infoFactura.paisDestino);
                Set(r, "paisAdquisicion", req.infoFactura.paisAdquisicion);
                Set(r, "incoTermTotalSinImpuestos", req.infoFactura.incoTermTotalSinImpuestos);
                Set(r, "fleteInternacional", req.infoFactura.fleteInternacional);
                Set(r, "seguroInternacional", req.infoFactura.seguroInternacional);
                Set(r, "gastosAduaneros", req.infoFactura.gastosAduaneros);
                Set(r, "gastosTransporteOtros", req.infoFactura.gastosTransporteOtros);
                Set(r, "codDocReembolso", req.infoFactura.codDocReembolso);
                Set(r, "totalComprobantesReembolso", req.infoFactura.totalComprobantesReembolso);
                Set(r, "totalBaseImponibleReembolso", req.infoFactura.totalBaseImponibleReembolso);
                Set(r, "totalImpuestoReembolso", req.infoFactura.totalImpuestoReembolso);

                tInfoFact.Rows.Add(r);

                // TotalConImpuestos
                if (req.infoFactura.totalConImpuestos != null)
                {
                    foreach (var x in req.infoFactura.totalConImpuestos)
                    {
                        if (x == null) continue;
                        var rr = tTotImp.NewRow();
                        Set(rr, "codigo", x.codigo);
                        Set(rr, "codigoPorcentaje", x.codigoPorcentaje);
                        Set(rr, "baseImponible", x.baseImponible);
                        Set(rr, "valor", x.valor);
                        tTotImp.Rows.Add(rr);
                    }
                }

                // Pagos
                if (req.infoFactura.pagos != null)
                {
                    foreach (var p in req.infoFactura.pagos)
                    {
                        if (p == null) continue;
                        var rr = tPagos.NewRow();
                        Set(rr, "formaPago", p.formaPago);
                        Set(rr, "total", p.total);
                        Set(rr, "plazo", p.plazo);
                        Set(rr, "unidadTiempo", p.unidadTiempo);
                        tPagos.Rows.Add(rr);
                    }
                }

                // Reembolsos + detalle impuestos
                int reembolsoId = 0;
                if (req.infoFactura.reembolsos != null)
                {
                    foreach (var re in req.infoFactura.reembolsos)
                    {
                        if (re == null) continue;
                        reembolsoId++;

                        var rr = tReemb.NewRow();
                        rr["reembolsoId"] = reembolsoId;
                        Set(rr, "tipoIdentificacionProveedorReembolso", re.tipoIdentificacionProveedorReembolso);
                        Set(rr, "identificacionProveedorReembolso", re.identificacionProveedorReembolso);
                        Set(rr, "codPaisPagoProveedorReembolso", re.codPaisPagoProveedorReembolso);
                        Set(rr, "tipoProveedorReembolso", re.tipoProveedorReembolso);
                        Set(rr, "codDocReembolso", re.codDocReembolso);
                        Set(rr, "estabDocReembolso", re.estabDocReembolso);
                        Set(rr, "ptoEmiDocReembolso", re.ptoEmiDocReembolso);
                        Set(rr, "secuencialDocReembolso", re.secuencialDocReembolso);
                        Set(rr, "fechaEmisionDocReembolso", re.fechaEmisionDocReembolso);
                        Set(rr, "numeroautorizacionDocReemb", re.numeroautorizacionDocReemb);
                        tReemb.Rows.Add(rr);

                        if (re.detalleImpuestos != null)
                        {
                            foreach (var imp in re.detalleImpuestos)
                            {
                                if (imp == null) continue;
                                var ri = tReembImp.NewRow();
                                ri["reembolsoId"] = reembolsoId;
                                Set(ri, "codigo", imp.codigo);
                                Set(ri, "codigoPorcentaje", imp.codigoPorcentaje);
                                Set(ri, "baseImponibleReembolso", imp.baseImponibleReembolso);
                                Set(ri, "tarifa", imp.tarifa);
                                Set(ri, "impuestoReembolso", imp.impuestoReembolso);
                                tReembImp.Rows.Add(ri);
                            }
                        }
                    }
                }
            }

            // Detalles + hijos
            int detalleId = 0;
            if (req.detalles != null)
            {
                foreach (var d in req.detalles)
                {
                    if (d == null) continue;
                    detalleId++;

                    var rr = tDetalles.NewRow();
                    rr["detalleId"] = detalleId;
                    Set(rr, "codigoPrincipal", d.codigoPrincipal);
                    Set(rr, "codigoAuxiliar", d.codigoAuxiliar);
                    Set(rr, "descripcion", d.descripcion);
                    rr["cantidad"] = d.cantidad;
                    Set(rr, "precioUnitario", d.precioUnitario);
                    Set(rr, "descuento", d.descuento);
                    Set(rr, "precioTotalSinImpuesto", d.precioTotalSinImpuesto);
                    tDetalles.Rows.Add(rr);

                    if (d.detallesAdicionales != null)
                    {
                        foreach (var a in d.detallesAdicionales)
                        {
                            if (a == null) continue;
                            var ra = tDetAdd.NewRow();
                            ra["detalleId"] = detalleId;
                            Set(ra, "nombre", a.nombre);
                            Set(ra, "valor", a.valor);
                            tDetAdd.Rows.Add(ra);
                        }
                    }

                    if (d.impuestos != null)
                    {
                        foreach (var i in d.impuestos)
                        {
                            if (i == null) continue;
                            var ri = tDetImp.NewRow();
                            ri["detalleId"] = detalleId;
                            Set(ri, "codigo", i.codigo);
                            Set(ri, "codigoPorcentaje", i.codigoPorcentaje);
                            Set(ri, "baseImponible", i.baseImponible);
                            Set(ri, "valor", i.valor);
                            Set(ri, "tarifa", i.tarifa);
                            tDetImp.Rows.Add(ri);
                        }
                    }
                }
            }

            // Retenciones
            if (req.retenciones != null)
            {
                foreach (var x in req.retenciones)
                {
                    if (x == null) continue;
                    var rr = tRet.NewRow();
                    Set(rr, "codigo", x.codigo);
                    Set(rr, "codigoPorcentaje", x.codigoPorcentaje);
                    Set(rr, "tarifa", x.tarifa);
                    Set(rr, "valor", x.valor);
                    tRet.Rows.Add(rr);
                }
            }

            // InfoAdicional
            if (req.infoAdicional != null)
            {
                foreach (var x in req.infoAdicional)
                {
                    if (x == null) continue;
                    var rr = tInfoAd.NewRow();
                    Set(rr, "nombre", x.nombre);
                    Set(rr, "valor", x.valor);
                    tInfoAd.Rows.Add(rr);
                }
            }

            // CamposAdicionales
            {
                var rr = tCampos.NewRow();
                Set(rr, "campoAdicional1", req.campoAdicional1);
                Set(rr, "campoAdicional2", req.campoAdicional2);
                tCampos.Rows.Add(rr);
            }

            ds.AcceptChanges();
            return ds;
        }

        // -------- Schema creators

        private static DataTable CreateInfoTributaria()
        {
            var t = new DataTable("InfoTributaria");
            AddStringCols(t, "ambiente", "tipoEmision", "claveAcceso", "razonSocial", "nombreComercial", "ruc",
                "codDoc", "estab", "ptoEmi", "secuencial", "dirMatriz", "diaEmission", "mesEmission", "anioEmission");
            return t;
        }

        private static DataTable CreateInfoFactura()
        {
            var t = new DataTable("InfoFactura");
            AddStringCols(t,
                "fechaEmision", "dirEstablecimiento", "contribuyenteEspecial", "obligadoContabilidad",
                "tipoIdentificacionComprador", "guiaRemision", "razonSocialComprador", "identificacionComprador", "direccionComprador",
                "totalSinImpuestos", "totalDescuento", "propina", "importeTotal", "moneda",
                "valorRetIva", "valorRetRenta", "comercioExterior",
                "IncoTermFactura", "lugarIncoTerm", "paisOrigen", "puertoEmbarque", "paisDestino", "paisAdquisicion",
                "incoTermTotalSinImpuestos", "fleteInternacional", "seguroInternacional",
                "gastosAduaneros", "gastosTransporteOtros",
                "codDocReembolso", "totalComprobantesReembolso", "totalBaseImponibleReembolso", "totalImpuestoReembolso");
            return t;
        }

        private static DataTable CreateTotalConImpuestos()
        {
            var t = new DataTable("TotalConImpuestos");
            AddStringCols(t, "codigo", "codigoPorcentaje", "baseImponible", "valor");
            return t;
        }

        private static DataTable CreatePagos()
        {
            var t = new DataTable("Pagos");
            AddStringCols(t, "formaPago", "total", "plazo", "unidadTiempo");
            return t;
        }

        private static DataTable CreateDetalles()
        {
            var t = new DataTable("Detalles");
            t.Columns.Add("detalleId", typeof(int));
            AddStringCols(t, "codigoPrincipal", "codigoAuxiliar", "descripcion", "precioUnitario", "descuento", "precioTotalSinImpuesto");
            t.Columns.Add("cantidad", typeof(decimal));
            t.PrimaryKey = new[] { t.Columns["detalleId"] };
            return t;
        }

        private static DataTable CreateDetallesAdicionales()
        {
            var t = new DataTable("DetallesAdicionales");
            t.Columns.Add("detalleId", typeof(int));
            AddStringCols(t, "nombre", "valor");
            return t;
        }

        private static DataTable CreateDetallesImpuestos()
        {
            var t = new DataTable("DetallesImpuestos");
            t.Columns.Add("detalleId", typeof(int));
            AddStringCols(t, "codigo", "codigoPorcentaje", "baseImponible", "valor", "tarifa");
            return t;
        }

        private static DataTable CreateRetenciones()
        {
            var t = new DataTable("Retenciones");
            AddStringCols(t, "codigo", "codigoPorcentaje", "tarifa", "valor");
            return t;
        }

        private static DataTable CreateInfoAdicional()
        {
            var t = new DataTable("InfoAdicional");
            AddStringCols(t, "nombre", "valor");
            return t;
        }

        private static DataTable CreateReembolsos()
        {
            var t = new DataTable("Reembolsos");
            t.Columns.Add("reembolsoId", typeof(int));
            AddStringCols(t,
                "tipoIdentificacionProveedorReembolso", "identificacionProveedorReembolso", "codPaisPagoProveedorReembolso",
                "tipoProveedorReembolso", "codDocReembolso", "estabDocReembolso", "ptoEmiDocReembolso",
                "secuencialDocReembolso", "fechaEmisionDocReembolso", "numeroautorizacionDocReemb");
            t.PrimaryKey = new[] { t.Columns["reembolsoId"] };
            return t;
        }

        private static DataTable CreateReembolsoDetalleImpuestos()
        {
            var t = new DataTable("ReembolsoDetalleImpuestos");
            t.Columns.Add("reembolsoId", typeof(int));
            AddStringCols(t, "codigo", "codigoPorcentaje", "baseImponibleReembolso", "tarifa", "impuestoReembolso");
            return t;
        }

        private static DataTable CreateCamposAdicionales()
        {
            var t = new DataTable("CamposAdicionales");
            AddStringCols(t, "campoAdicional1", "campoAdicional2");
            return t;
        }

        private static void AddStringCols(DataTable t, params string[] cols)
        {
            foreach (var c in cols) t.Columns.Add(c, typeof(string));
        }

        private static void Set(DataRow row, string column, object value)
        {
            row[column] = value ?? DBNull.Value;
        }
    }
}
