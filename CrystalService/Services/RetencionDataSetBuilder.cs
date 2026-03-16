using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public static class RetencionDataSetBuilder
    {
        public static DataSet FillRetencionDataSet(DataSet ds, RetencionRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoCompRetencion");
            ClearIfExists(ds, "docsSustento");
            ClearIfExists(ds, "impuestosDocSustento");
            ClearIfExists(ds, "retenciones");
            ClearIfExists(ds, "dividendos");
            ClearIfExists(ds, "pagos");
            ClearIfExists(ds, "InfoAdicional");
            ClearIfExists(ds, "CamposAdicionales");

            // 1) InfoTributaria
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

            // 2) InfoCompRetencion
            if (req.infoCompRetencion != null)
            {
                var t = RequireTable(ds, "infoCompRetencion");
                var r = t.NewRow();

                Set(r, "fechaEmision", req.infoCompRetencion.fechaEmision);
                Set(r, "dirEstablecimiento", req.infoCompRetencion.dirEstablecimiento);
                Set(r, "contribuyenteEspecial", req.infoCompRetencion.contribuyenteEspecial);
                Set(r, "obligadoContabilidad", req.infoCompRetencion.obligadoContabilidad);
                Set(r, "tipoIdentificacionSujetoRetenido", req.infoCompRetencion.tipoIdentificacionSujetoRetenido);
                Set(r, "tipoSujetoRetenido", req.infoCompRetencion.tipoSujetoRetenido);
                Set(r, "parteRel", req.infoCompRetencion.parteRel);
                Set(r, "razonSocialSujetoRetenido", req.infoCompRetencion.razonSocialSujetoRetenido);
                Set(r, "identificacionSujetoRetenido", req.infoCompRetencion.identificacionSujetoRetenido);
                Set(r, "periodoFiscal", req.infoCompRetencion.periodoFiscal);

                t.Rows.Add(r);
            }

            // 3) DocsSustento + hijos
            int docSustentoId = 0;
            int retencionId = 0;

            if (req.docsSustento != null)
            {
                var tDocs = RequireTable(ds, "docsSustento");
                var tImpDocs = RequireTable(ds, "impuestosDocSustento");
                var tRet = RequireTable(ds, "retenciones");
                var tDiv = RequireTable(ds, "dividendos");
                var tPagos = RequireTable(ds, "pagos");

                foreach (var doc in req.docsSustento)
                {
                    if (doc == null) continue;

                    docSustentoId++;

                    var rDoc = tDocs.NewRow();
                    Set(rDoc, "docSustentoId", docSustentoId);
                    Set(rDoc, "codSustento", doc.codSustento);
                    Set(rDoc, "codDocSustento", doc.codDocSustento);
                    Set(rDoc, "numDocSustento", doc.numDocSustento);
                    Set(rDoc, "factura_relacionada", doc.factura_relacionada);
                    Set(rDoc, "fechaEmisionDocSustento", doc.fechaEmisionDocSustento);
                    Set(rDoc, "fechaRegistroContable", doc.fechaRegistroContable);
                    Set(rDoc, "numAutDocSustento", doc.numAutDocSustento);
                    Set(rDoc, "pagoLocExt", doc.pagoLocExt);
                    Set(rDoc, "tipoRegi", doc.tipoRegi);
                    Set(rDoc, "paisEfecPago", doc.paisEfecPago);
                    Set(rDoc, "aplicConvDobTrib", doc.aplicConvDobTrib);
                    Set(rDoc, "pagExtSujRetNorLeg", doc.pagExtSujRetNorLeg);
                    Set(rDoc, "pagRegFis", doc.pagRegFis);
                    Set(rDoc, "totalComprobantesReembolso", doc.totalComprobantesReembolso);
                    Set(rDoc, "totalBaseImponibleReembolso", doc.totalBaseImponibleReembolso);
                    Set(rDoc, "totalSinImpuestos", doc.totalSinImpuestos);
                    Set(rDoc, "importeTotal", doc.importeTotal);
                    tDocs.Rows.Add(rDoc);

                    // 3.1) ImpuestosDocSustento
                    if (doc.impuestosDocSustento != null)
                    {
                        foreach (var imp in doc.impuestosDocSustento)
                        {
                            if (imp == null) continue;

                            var rImp = tImpDocs.NewRow();
                            Set(rImp, "docSustentoId", docSustentoId);
                            Set(rImp, "codImpuestoDocSustento", imp.codImpuestoDocSustento);
                            Set(rImp, "codigoPorcentaje", imp.codigoPorcentaje);
                            Set(rImp, "baseImponible", imp.baseImponible);
                            Set(rImp, "tarifa", imp.tarifa);
                            Set(rImp, "valorImpuesto", imp.valorImpuesto);
                            tImpDocs.Rows.Add(rImp);
                        }
                    }

                    // 3.2) Retenciones + Dividendos
                    if (doc.retenciones != null)
                    {
                        foreach (var ret in doc.retenciones)
                        {
                            if (ret == null) continue;

                            retencionId++;

                            var rRet = tRet.NewRow();
                            Set(rRet, "retencionId", retencionId);
                            Set(rRet, "docSustentoId", docSustentoId);
                            Set(rRet, "codigo", ret.codigo);
                            Set(rRet, "codigoRetencion", ret.codigoRetencion);
                            Set(rRet, "baseImponible", ret.baseImponible);
                            Set(rRet, "porcentajeRetener", ret.porcentajeRetener);
                            Set(rRet, "valorRetenido", ret.valorRetenido);
                            tRet.Rows.Add(rRet);

                            if (ret.dividendos != null)
                            {
                                var rDiv = tDiv.NewRow();
                                Set(rDiv, "retencionId", retencionId);
                                Set(rDiv, "fechaPagoDiv", ret.dividendos.fechaPagoDiv);
                                Set(rDiv, "imRentaSoc", ret.dividendos.imRentaSoc);
                                Set(rDiv, "ejerFisUtDiv", ret.dividendos.ejerFisUtDiv);
                                tDiv.Rows.Add(rDiv);
                            }
                        }
                    }

                    // 3.3) Pagos
                    if (doc.pagos != null)
                    {
                        foreach (var pago in doc.pagos)
                        {
                            if (pago == null) continue;

                            var rPago = tPagos.NewRow();
                            Set(rPago, "docSustentoId", docSustentoId);
                            Set(rPago, "formaPago", pago.formaPago);
                            Set(rPago, "total", pago.total);
                            tPagos.Rows.Add(rPago);
                        }
                    }
                }
            }

            // 4) InfoAdicional
            if (req.infoAdicional != null)
            {
                var t = RequireTable(ds, "InfoAdicional");

                foreach (var item in req.infoAdicional)
                {
                    if (item == null) continue;

                    var r = t.NewRow();
                    Set(r, "nombre", item.nombre);
                    Set(r, "valor", item.valor);
                    t.Rows.Add(r);
                }
            }

            // 5) CamposAdicionales
            if (ds.Tables.Contains("CamposAdicionales"))
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

        private static void ClearIfExists(DataSet ds, string tableName)
        {
            if (ds.Tables.Contains(tableName))
                ds.Tables[tableName].Rows.Clear();
        }

        private static DataTable RequireTable(DataSet ds, string tableName)
        {
            if (!ds.Tables.Contains(tableName))
                throw new InvalidOperationException(
                    $"El DataSet no contiene la tabla requerida: '{tableName}'.");
            return ds.Tables[tableName];
        }

        private static void Set(DataRow row, string column, object value)
        {
            if (!row.Table.Columns.Contains(column))
                throw new InvalidOperationException(
                    $"La tabla '{row.Table.TableName}' no contiene la columna '{column}'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}