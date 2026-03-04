using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public static class FacturaDataSetBuilder
    {
        /// <summary>
        /// Llena un DataSet (ya creado por ti) con la data del request.
        /// No crea el esquema: solo llena las tablas.
        ///
        /// Tablas esperadas (nombres exactos):
        /// - InfoTributaria
        /// - InfoFactura
        /// - TotalConImpuestos
        /// - Pagos
        /// - Detalles
        /// - DetallesAdicionales
        /// - DetallesImpuestos
        /// - Retenciones
        /// - InfoAdicional
        /// - Reembolsos
        /// - ReembolsoDetalleImpuestos
        /// - CamposAdicionales
        ///
        /// Relaciones recomendadas (si las tienes):
        /// - Detalles(detalleId) -> DetallesAdicionales(detalleId)
        /// - Detalles(detalleId) -> DetallesImpuestos(detalleId)
        /// - Reembolsos(reembolsoId) -> ReembolsoDetalleImpuestos(reembolsoId)
        /// </summary>
        public static DataSet FillFactura01DataSet(DataSet ds, FacturaRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            // Limpia tablas si quieres comportamiento "fresh" por request
            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoFactura");
            ClearIfExists(ds, "TotalConImpuestos");
            ClearIfExists(ds, "Pagos");
            ClearIfExists(ds, "Detalles");
            ClearIfExists(ds, "DetallesAdicionales");
            ClearIfExists(ds, "DetallesImpuestos");
            ClearIfExists(ds, "Retenciones");
            ClearIfExists(ds, "InfoAdicional");
            ClearIfExists(ds, "Reembolsos");
            ClearIfExists(ds, "ReembolsoDetalleImpuestos");
            ClearIfExists(ds, "CamposAdicionales");

            // 1) InfoTributaria (1 fila)
            if (req.infoTributaria != null)
            {
                var t = RequireTable(ds, "InfoTributaria");
                var r = t.NewRow();

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

                t.Rows.Add(r);
            }

            // 2) InfoFactura (1 fila) + TotalConImpuestos + Pagos + Reembolsos
            if (req.infoFactura != null)
            {
                // InfoFactura (1)
                var tInfo = RequireTable(ds, "InfoFactura");
                var rInfo = tInfo.NewRow();

                Set(rInfo, "fechaEmision", req.infoFactura.fechaEmision);
                Set(rInfo, "dirEstablecimiento", req.infoFactura.dirEstablecimiento);
                Set(rInfo, "contribuyenteEspecial", req.infoFactura.contribuyenteEspecial);
                Set(rInfo, "obligadoContabilidad", req.infoFactura.obligadoContabilidad);
                Set(rInfo, "tipoIdentificacionComprador", req.infoFactura.tipoIdentificacionComprador);
                Set(rInfo, "guiaRemision", req.infoFactura.guiaRemision);
                Set(rInfo, "razonSocialComprador", req.infoFactura.razonSocialComprador);
                Set(rInfo, "identificacionComprador", req.infoFactura.identificacionComprador);
                Set(rInfo, "direccionComprador", req.infoFactura.direccionComprador);
                Set(rInfo, "totalSinImpuestos", req.infoFactura.totalSinImpuestos);
                Set(rInfo, "totalDescuento", req.infoFactura.totalDescuento);
                Set(rInfo, "propina", req.infoFactura.propina);
                Set(rInfo, "importeTotal", req.infoFactura.importeTotal);
                Set(rInfo, "moneda", req.infoFactura.moneda);

                Set(rInfo, "valorRetIva", req.infoFactura.valorRetIva);
                Set(rInfo, "valorRetRenta", req.infoFactura.valorRetRenta);
                Set(rInfo, "comercioExterior", req.infoFactura.comercioExterior);
                Set(rInfo, "IncoTermFactura", req.infoFactura.IncoTermFactura);
                Set(rInfo, "lugarIncoTerm", req.infoFactura.lugarIncoTerm);
                Set(rInfo, "paisOrigen", req.infoFactura.paisOrigen);
                Set(rInfo, "puertoEmbarque", req.infoFactura.puertoEmbarque);
                Set(rInfo, "paisDestino", req.infoFactura.paisDestino);
                Set(rInfo, "paisAdquisicion", req.infoFactura.paisAdquisicion);
                Set(rInfo, "incoTermTotalSinImpuestos", req.infoFactura.incoTermTotalSinImpuestos);
                Set(rInfo, "fleteInternacional", req.infoFactura.fleteInternacional);
                Set(rInfo, "seguroInternacional", req.infoFactura.seguroInternacional);
                Set(rInfo, "gastosAduaneros", req.infoFactura.gastosAduaneros);
                Set(rInfo, "gastosTransporteOtros", req.infoFactura.gastosTransporteOtros);
                Set(rInfo, "codDocReembolso", req.infoFactura.codDocReembolso);
                Set(rInfo, "totalComprobantesReembolso", req.infoFactura.totalComprobantesReembolso);
                Set(rInfo, "totalBaseImponibleReembolso", req.infoFactura.totalBaseImponibleReembolso);
                Set(rInfo, "totalImpuestoReembolso", req.infoFactura.totalImpuestoReembolso);

                tInfo.Rows.Add(rInfo);

                // TotalConImpuestos (N)
                if (req.infoFactura.totalConImpuestos != null)
                {
                    var t = RequireTable(ds, "TotalConImpuestos");
                    foreach (var x in req.infoFactura.totalConImpuestos)
                    {
                        if (x == null) continue;
                        var r = t.NewRow();
                        Set(r, "codigo", x.codigo);
                        Set(r, "codigoPorcentaje", x.codigoPorcentaje);
                        Set(r, "baseImponible", x.baseImponible);
                        Set(r, "valor", x.valor);
                        t.Rows.Add(r);
                    }
                }

                // Pagos (N)
                if (req.infoFactura.pagos != null)
                {
                    var t = RequireTable(ds, "Pagos");
                    foreach (var p in req.infoFactura.pagos)
                    {
                        if (p == null) continue;
                        var r = t.NewRow();
                        Set(r, "formaPago", p.formaPago);
                        Set(r, "total", p.total);
                        Set(r, "plazo", p.plazo);
                        Set(r, "unidadTiempo", p.unidadTiempo);
                        t.Rows.Add(r);
                    }
                }

                // Reembolsos (N) + ReembolsoDetalleImpuestos (N hijo)
                var reembolsoId = 0;
                if (req.infoFactura.reembolsos != null)
                {
                    var tReemb = RequireTable(ds, "Reembolsos");
                    var tReembImp = RequireTable(ds, "ReembolsoDetalleImpuestos");

                    foreach (var re in req.infoFactura.reembolsos)
                    {
                        if (re == null) continue;
                        reembolsoId++;

                        var rr = tReemb.NewRow();
                        Set(rr, "reembolsoId", reembolsoId);
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
                                Set(ri, "reembolsoId", reembolsoId);
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

            // 3) Detalles (N) + hijos
            var detalleId = 0;
            if (req.detalles != null)
            {
                var tDet = RequireTable(ds, "Detalles");
                var tDetAdd = RequireTable(ds, "DetallesAdicionales");
                var tDetImp = RequireTable(ds, "DetallesImpuestos");

                foreach (var d in req.detalles)
                {
                    if (d == null) continue;
                    detalleId++;

                    var r = tDet.NewRow();
                    Set(r, "detalleId", detalleId);
                    Set(r, "codigoPrincipal", d.codigoPrincipal);
                    Set(r, "codigoAuxiliar", d.codigoAuxiliar);
                    Set(r, "descripcion", d.descripcion);
                    Set(r, "cantidad", d.cantidad); // decimal
                    Set(r, "precioUnitario", d.precioUnitario);
                    Set(r, "descuento", d.descuento);
                    Set(r, "precioTotalSinImpuesto", d.precioTotalSinImpuesto);
                    tDet.Rows.Add(r);

                    if (d.detallesAdicionales != null)
                    {
                        foreach (var a in d.detallesAdicionales)
                        {
                            if (a == null) continue;
                            var ra = tDetAdd.NewRow();
                            Set(ra, "detalleId", detalleId);
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
                            Set(ri, "detalleId", detalleId);
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

            // 4) Retenciones
            if (req.retenciones != null)
            {
                var t = RequireTable(ds, "Retenciones");
                foreach (var x in req.retenciones)
                {
                    if (x == null) continue;
                    var r = t.NewRow();
                    Set(r, "codigo", x.codigo);
                    Set(r, "codigoPorcentaje", x.codigoPorcentaje);
                    Set(r, "tarifa", x.tarifa);
                    Set(r, "valor", x.valor);
                    t.Rows.Add(r);
                }
            }

            // 5) InfoAdicional
            if (req.infoAdicional != null)
            {
                var t = RequireTable(ds, "InfoAdicional");
                foreach (var x in req.infoAdicional)
                {
                    if (x == null) continue;
                    var r = t.NewRow();
                    Set(r, "nombre", x.nombre);
                    Set(r, "valor", x.valor);
                    t.Rows.Add(r);
                }
            }

            // 6) CamposAdicionales (1 fila)
            {
                var t = RequireTable(ds, "CamposAdicionales");
                var r = t.NewRow();
                Set(r, "campoAdicional1", req.campoAdicional1);
                Set(r, "campoAdicional2", req.campoAdicional2);
                t.Rows.Add(r);
            }

            ds.AcceptChanges();
            return ds;
        }

        // ----------------- Helpers -----------------

        private static void ClearIfExists(DataSet ds, string tableName)
        {
            var t = ds.Tables.Contains(tableName) ? ds.Tables[tableName] : null;
            t?.Rows.Clear();
        }

        private static DataTable RequireTable(DataSet ds, string tableName)
        {
            if (!ds.Tables.Contains(tableName))
                throw new InvalidOperationException($"El DataSet no contiene la tabla requerida: '{tableName}'.");
            return ds.Tables[tableName];
        }

        private static void Set(DataRow row, string column, object value)
        {
            if (!row.Table.Columns.Contains(column))
                throw new InvalidOperationException($"La tabla '{row.Table.TableName}' no contiene la columna: '{column}'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}